using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class Prisoner
    {
        private int prisonerId;
        private string prisonerNumber;
        private string fullName;
        private Factory? factory;
        private int? department;
        private PrisonerActivityStatus activityStatus;
        private PrisonerRole? role;
        private DateTime? safetyTrainingValidity;
        private DateTime? workStartDate;
        private DateTime? releaseDate;
        private bool qualified;
        private bool sabbatKeeping;
        private decimal hourlyRate;
        private List<AttendanceRecord> attendanceRecords;
        private List<ProductivityRecord> productivityRecords;

        public Prisoner(int id, string prisonerNum, string fullName, Factory? factory, int? dept, PrisonerActivityStatus status, 
                       PrisonerRole? role, DateTime? safetyTrain, DateTime? workStart, DateTime? release, 
                       bool qualified, bool sabbat, decimal hourly, bool isNew)
        {
            this.prisonerId = id;
            this.prisonerNumber = prisonerNum;
            this.fullName = fullName;
            this.factory = factory;
            this.department = dept;
            this.activityStatus = status;
            this.role = role;
            this.safetyTrainingValidity = safetyTrain;
            this.workStartDate = workStart;
            this.releaseDate = release;
            this.qualified = qualified;
            this.sabbatKeeping = sabbat;
            this.hourlyRate = hourly;
            if (isNew)
            {
                this.create();
                Program.Prisoners.Add(this);
            }
        }

        public int getId() { return this.prisonerId; }
        public string getPrisonerNumber() { return this.prisonerNumber; }
        public string getFullName() { return this.fullName; }
        public Factory? getFactory() { return this.factory; }
        public int? getDepartment() { return this.department; }
        public PrisonerActivityStatus getActivityStatus() { return this.activityStatus; }
        public PrisonerRole? getRole() { return this.role; }
        public DateTime? getSafetyTrainingValidity() { return this.safetyTrainingValidity; }
        public DateTime? getWorkStartDate() { return this.workStartDate; }
        public DateTime? getReleaseDate() { return this.releaseDate; }
        public bool getQualified() { return this.qualified; }
        public bool getSabbatKeeping() { return this.sabbatKeeping; }
        public decimal getHourlyRate() { return this.hourlyRate; }

        public void setFullName(string name) { this.fullName = name; }
        public void setFactory(Factory? factory) { this.factory = factory; }
        public void setDepartment(int? dept) { this.department = dept; }
        public void setActivityStatus(PrisonerActivityStatus status) { this.activityStatus = status; }
        public void setRole(PrisonerRole? role) { this.role = role; }
        public void setSafetyTrainingValidity(DateTime? date) { this.safetyTrainingValidity = date; }
        public void setWorkStartDate(DateTime? date) { this.workStartDate = date; }
        public void setReleaseDate(DateTime? date) { this.releaseDate = date; }
        public void setQualified(bool q) { this.qualified = q; }
        public void setSabbatKeeping(bool s) { this.sabbatKeeping = s; }
        public void setHourlyRate(decimal rate) { this.hourlyRate = rate; }

        public List<AttendanceRecord> getAttendanceRecords()
        {
            if (attendanceRecords == null)
                attendanceRecords = new List<AttendanceRecord>();
            return attendanceRecords;
        }

        public void addAttendanceRecord(AttendanceRecord record)
        {
            if (record == null) return;
            if (this.attendanceRecords == null) this.attendanceRecords = new List<AttendanceRecord>();
            if (!this.attendanceRecords.Contains(record)) this.attendanceRecords.Add(record);
        }

        public void removeAttendanceRecord(AttendanceRecord record)
        {
            if (record == null) return;
            if (this.attendanceRecords != null && this.attendanceRecords.Contains(record)) this.attendanceRecords.Remove(record);
        }

        public List<ProductivityRecord> getProductivityRecords()
        {
            if (productivityRecords == null)
                productivityRecords = new List<ProductivityRecord>();
            return productivityRecords;
        }

        public void addProductivityRecord(ProductivityRecord record)
        {
            if (record == null) return;
            if (this.productivityRecords == null) this.productivityRecords = new List<ProductivityRecord>();
            if (!this.productivityRecords.Contains(record)) this.productivityRecords.Add(record);
        }

        public void removeProductivityRecord(ProductivityRecord record)
        {
            if (record == null) return;
            if (this.productivityRecords != null && this.productivityRecords.Contains(record)) this.productivityRecords.Remove(record);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_create @prisoner_id, @prisoner_number, @full_name, @factory, @department, @activity_status, @role, @safety_training_validity, @work_start_date, @release_date, @qualified, @shabbat_keeping, @hourly_rate";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@prisoner_number", this.prisonerNumber);
            cmd.Parameters.AddWithValue("@full_name", this.fullName);
            cmd.Parameters.AddWithValue("@factory", this.factory.HasValue ? EnumHelpers.ToDbString(this.factory.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@department", this.department ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@activity_status", EnumHelpers.ToDbString(this.activityStatus));
            cmd.Parameters.AddWithValue("@role", this.role.HasValue ? EnumHelpers.ToDbString(this.role.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@safety_training_validity", this.safetyTrainingValidity ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@work_start_date", this.workStartDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@release_date", this.releaseDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@qualified", this.qualified);
            cmd.Parameters.AddWithValue("@shabbat_keeping", this.sabbatKeeping);
            cmd.Parameters.AddWithValue("@hourly_rate", this.hourlyRate);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_update @prisoner_id, @prisoner_number, @full_name, @factory, @department, @activity_status, @role, @safety_training_validity, @work_start_date, @release_date, @qualified, @shabbat_keeping, @hourly_rate";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@prisoner_number", this.prisonerNumber);
            cmd.Parameters.AddWithValue("@full_name", this.fullName);
            cmd.Parameters.AddWithValue("@factory", this.factory.HasValue ? EnumHelpers.ToDbString(this.factory.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@department", this.department ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@activity_status", EnumHelpers.ToDbString(this.activityStatus));
            cmd.Parameters.AddWithValue("@role", this.role.HasValue ? EnumHelpers.ToDbString(this.role.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@safety_training_validity", this.safetyTrainingValidity ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@work_start_date", this.workStartDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@release_date", this.releaseDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@qualified", this.qualified);
            cmd.Parameters.AddWithValue("@shabbat_keeping", this.sabbatKeeping);
            cmd.Parameters.AddWithValue("@hourly_rate", this.hourlyRate);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.Prisoners.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_delete @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initPrisoners()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.Prisoners = new List<Prisoner>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string prisonerNum = reader.GetValue(1).ToString();
                string fullName = reader.GetValue(2).ToString();
                Factory? factory = reader.IsDBNull(3) ? null : EnumHelpers.FactoryFromDb(reader.GetValue(3).ToString());
                int? dept = reader.IsDBNull(4) ? null : Convert.ToInt32(reader.GetValue(4));
                PrisonerActivityStatus status = EnumHelpers.ActivityStatusFromDb(reader.GetValue(5).ToString());
                PrisonerRole? role = reader.IsDBNull(6) ? null : EnumHelpers.PrisonerRoleFromDb(reader.GetValue(6).ToString());
                DateTime? safetyTrain = reader.IsDBNull(7) ? null : Convert.ToDateTime(reader.GetValue(7));
                DateTime? workStart = reader.IsDBNull(8) ? null : Convert.ToDateTime(reader.GetValue(8));
                DateTime? release = reader.IsDBNull(9) ? null : Convert.ToDateTime(reader.GetValue(9));
                bool qualified = Convert.ToBoolean(reader.GetValue(10));
                bool sabbat = Convert.ToBoolean(reader.GetValue(11));
                decimal hourly = Convert.ToDecimal(reader.GetValue(12));

                Prisoner p = new Prisoner(id, prisonerNum, fullName, factory, dept, status, role, safetyTrain, workStart, release, qualified, sabbat, hourly, false);
                Program.Prisoners.Add(p);
            }
            reader.Close();
        }

        public static Prisoner seekById(int id)
        {
            foreach (Prisoner p in Program.Prisoners)
            {
                if (p.getId() == id)
                    return p;
            }
            return null;
        }
    }
}
