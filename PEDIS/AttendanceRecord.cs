using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class AttendanceRecord
    {
        private int attendanceRecordId;
        private int prisonerId;
        private DateTime attendanceDate;
        private Factory factory;
        private TimeSpan? entryTime;
        private TimeSpan? exitTime;
        private Prisoner prisoner;

        public AttendanceRecord(int id, int prisonId, DateTime date, Factory factory, TimeSpan? entry, TimeSpan? exit, bool isNew)
        {
            this.attendanceRecordId = id;
            this.prisonerId = prisonId;
            this.attendanceDate = date;
            this.factory = factory;
            this.entryTime = entry;
            this.exitTime = exit;
            this.prisoner = Prisoner.seekById(prisonId);
            if (isNew)
            {
                this.create();
                Program.AttendanceRecords.Add(this);
                if (this.prisoner != null)
                    this.prisoner.addAttendanceRecord(this);
            }
        }

        public int getId() { return this.attendanceRecordId; }
        public int getPrisonerId() { return this.prisonerId; }
        public DateTime getAttendanceDate() { return this.attendanceDate; }
        public Factory getFactory() { return this.factory; }
        public TimeSpan? getEntryTime() { return this.entryTime; }
        public TimeSpan? getExitTime() { return this.exitTime; }
        public Prisoner getPrisoner() { return this.prisoner; }

        public void setEntryTime(TimeSpan? time) { this.entryTime = time; }
        public void setExitTime(TimeSpan? time) { this.exitTime = time; }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_create @attendance_record_id, @prisoner_id, @attendance_date, @factory, @entry_time, @exit_time";
            cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@attendance_date", this.attendanceDate);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            cmd.Parameters.AddWithValue("@entry_time", this.entryTime ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@exit_time", this.exitTime ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
            syncPrisonerActivityStatus();
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_update @attendance_record_id, @prisoner_id, @attendance_date, @factory, @entry_time, @exit_time";
            cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@attendance_date", this.attendanceDate);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            cmd.Parameters.AddWithValue("@entry_time", this.entryTime ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@exit_time", this.exitTime ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
            syncPrisonerActivityStatus();
        }

        public void delete()
        {
            Program.AttendanceRecords.Remove(this);
            Prisoner affectedPrisoner = this.prisoner;
            if (this.prisoner != null)
                this.prisoner.removeAttendanceRecord(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_delete @attendance_record_id";
            cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            // Deleting a prisoner's only active record can leave them stuck as
            // OnShiftWorking with nothing backing it -- reconcile right away.
            if (affectedPrisoner != null)
                Prisoner.reconcileOnShiftStatuses();
        }

        // Multiple UI flows (PrisonerPanel Clock In/Out, AddEditAttendanceDialog Add/Edit)
        // write entry_time/exit_time directly rather than through a single shared transition
        // method, so the Prisoner.activityStatus sync lives here where every save converges.
        private void syncPrisonerActivityStatus()
        {
            if (this.prisoner == null)
                return;

            bool isActiveToday = this.attendanceDate.Date == DateTime.Today &&
                                  this.entryTime.HasValue && !this.exitTime.HasValue;

            if (isActiveToday && this.prisoner.getActivityStatus() != PrisonerActivityStatus.OnShiftWorking)
            {
                this.prisoner.setActivityStatus(PrisonerActivityStatus.OnShiftWorking);
                this.prisoner.update();
            }
            else if (!isActiveToday && this.prisoner.getActivityStatus() == PrisonerActivityStatus.OnShiftWorking
                      && !AttendanceRecord.hasActiveToday(this.prisoner.getId()))
            {
                this.prisoner.setActivityStatus(PrisonerActivityStatus.Idle);
                this.prisoner.update();
            }
        }

        // Active = same prisoner, attendance_date = today, entry_time set, exit_time not set.
        public static bool hasActiveToday(int prisonerId)
        {
            foreach (AttendanceRecord ar in Program.AttendanceRecords)
            {
                if (ar.getPrisonerId() == prisonerId &&
                    ar.getAttendanceDate().Date == DateTime.Today &&
                    ar.getEntryTime().HasValue &&
                    !ar.getExitTime().HasValue)
                    return true;
            }
            return false;
        }

        public static void initAttendanceRecords()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.AttendanceRecords = new List<AttendanceRecord>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                int prisonId = Convert.ToInt32(reader.GetValue(1));
                DateTime date = Convert.ToDateTime(reader.GetValue(2));
                Factory factory = EnumHelpers.FactoryFromDb(reader.GetValue(3).ToString());
                TimeSpan? entry = reader.IsDBNull(4) ? null : (TimeSpan?)reader.GetValue(4);
                TimeSpan? exit = reader.IsDBNull(5) ? null : (TimeSpan?)reader.GetValue(5);

                AttendanceRecord ar = new AttendanceRecord(id, prisonId, date, factory, entry, exit, false);
                Program.AttendanceRecords.Add(ar);
            }
            reader.Close();
        }

        public static AttendanceRecord seekById(int id)
        {
            foreach (AttendanceRecord ar in Program.AttendanceRecords)
            {
                if (ar.getId() == id)
                    return ar;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (3 implicit states: Pending, In Progress, Complete)
        // Implicit states inferred from entry_time/exit_time values
        // ============================================================================

        /// <summary>
        /// Transition: (Create) → Pending
        /// Prisoner clocks in (R5). Create new AttendanceRecord with entry_time = NOW.
        /// Side effect: update Prisoner status to OnShiftWorking.
        /// </summary>
        public void recordEntry(DateTime entryTime)
        {
            if (this.entryTime.HasValue)
                throw new Exception("כשגיאה: כלא כבר נרשם כנוכח היום");

            this.entryTime = new TimeSpan(entryTime.Hour, entryTime.Minute, entryTime.Second);
            this.update();

            // Side effect: update prisoner status
            if (this.prisoner != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EXECUTE sp_AttendanceRecord_recordEntry @attendance_record_id, @prisoner_id";
                cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
                cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
                SQL_CON sc = new SQL_CON();
                sc.execute_non_query(cmd);
            }
        }

        /// <summary>
        /// Transition: Pending → Pending (with temp exit recorded)
        /// Prisoner exits temporarily (e.g., lunch break). Record temporary exit time.
        /// </summary>
        public void recordTemporaryExit(DateTime tempExitTime)
        {
            if (!this.entryTime.HasValue)
                throw new Exception("כשגיאה: כלא לא נרשם כנוכח");

            if (this.exitTime.HasValue)
                throw new Exception("כשגיאה: כלא כבר רשום כעזב");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_recordTemporaryExit @attendance_record_id, @temp_exit_time";
            cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
            cmd.Parameters.AddWithValue("@temp_exit_time", new TimeSpan(tempExitTime.Hour, tempExitTime.Minute, tempExitTime.Second));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        /// <summary>
        /// Transition: Pending → Complete
        /// Prisoner clocks out (R5). Record exit_time and calculate total hours worked.
        /// Side effect: update Prisoner status to Idle; calc hours_worked contribution to PayrollRecord (R10).
        /// </summary>
        public void recordExit(DateTime exitTime)
        {
            if (!this.entryTime.HasValue)
                throw new Exception("כשגיאה: כלא לא נרשם כנוכח");

            if (this.exitTime.HasValue)
                throw new Exception("כשגיאה: כלא כבר רשום כעזב");

            this.exitTime = new TimeSpan(exitTime.Hour, exitTime.Minute, exitTime.Second);
            this.update();

            // Side effect: calculate hours worked and update prisoner
            if (this.prisoner != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EXECUTE sp_AttendanceRecord_recordExit @attendance_record_id, @prisoner_id, @hours_worked";

                decimal hoursWorked = 0;
                if (this.entryTime.HasValue && this.exitTime.HasValue)
                {
                    hoursWorked = (decimal)(this.exitTime.Value.TotalSeconds - this.entryTime.Value.TotalSeconds) / 3600;
                }

                cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
                cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
                cmd.Parameters.AddWithValue("@hours_worked", hoursWorked);
                SQL_CON sc = new SQL_CON();
                sc.execute_non_query(cmd);
            }
        }
    }
}
