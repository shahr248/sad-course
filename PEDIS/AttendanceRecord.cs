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
        }

        public void delete()
        {
            Program.AttendanceRecords.Remove(this);
            if (this.prisoner != null)
                this.prisoner.removeAttendanceRecord(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_AttendanceRecord_delete @attendance_record_id";
            cmd.Parameters.AddWithValue("@attendance_record_id", this.attendanceRecordId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
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
    }
}
