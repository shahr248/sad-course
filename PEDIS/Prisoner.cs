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
        private int department;
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

        public Prisoner(int id, string prisonerNum, string fullName, Factory? factory, int dept, PrisonerActivityStatus status,
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
        public int getDepartment() { return this.department; }
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
        public void setDepartment(int dept) { this.department = dept; }
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
            cmd.Parameters.AddWithValue("@department", this.department);
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
            cmd.Parameters.AddWithValue("@department", this.department);
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
                int dept = Convert.ToInt32(reader.GetValue(4));
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

        public static Prisoner seekByNumber(string prisonerNumber)
        {
            foreach (Prisoner p in Program.Prisoners)
            {
                if (p.getPrisonerNumber() == prisonerNumber)
                    return p;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (18 transitions for PrisonerActivityStatus)
        // ============================================================================

        /// <summary>
        /// Transition: PendingPrisonAdministrationApproval → PendingDepartmentManagerApproval
        /// Prison admin has approved prisoner enrollment; move to dept manager review.
        /// </summary>
        public void approvePrison()
        {
            if (this.activityStatus != PrisonerActivityStatus.PendingPrisonAdministrationApproval)
                throw new Exception("Error: prisoner is not in pending prison administration approval status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_approvePrison @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.PendingDepartmentManagerApproval;
            this.update();
        }

        /// <summary>
        /// Transition: PendingPrisonAdministrationApproval → Archived
        /// Prison admin rejects prisoner; archive record immediately.
        /// </summary>
        public void rejectPrison()
        {
            if (this.activityStatus != PrisonerActivityStatus.PendingPrisonAdministrationApproval)
                throw new Exception("Error: prisoner is not in pending prison administration approval status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_rejectPrison @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Archived;
            this.update();
        }

        /// <summary>
        /// Transition: PendingDepartmentManagerApproval → PendingPlacement
        /// Department manager approves; prisoner ready for factory assignment (R4).
        /// Guard: no security holds
        /// </summary>
        public void approveDeptManager()
        {
            if (this.activityStatus != PrisonerActivityStatus.PendingDepartmentManagerApproval)
                throw new Exception("Error: prisoner is not in pending department manager approval status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_approveDeptManager @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.PendingPlacement;
            this.update();
        }

        /// <summary>
        /// Transition: PendingDepartmentManagerApproval → Archived
        /// Department manager rejects prisoner; archive record.
        /// </summary>
        public void rejectDeptManager()
        {
            if (this.activityStatus != PrisonerActivityStatus.PendingDepartmentManagerApproval)
                throw new Exception("Error: prisoner is not in pending department manager approval status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_rejectDeptManager @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Archived;
            this.update();
        }

        /// <summary>
        /// Transition: PendingPlacement → Idle
        /// Assign prisoner to factory (R4). Guard: factory has capacity, prisoner available.
        /// </summary>
        public void assignToFactory(Factory factory)
        {
            if (this.activityStatus != PrisonerActivityStatus.PendingPlacement)
                throw new Exception("Error: prisoner is not in pending placement status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_assignToFactory @prisoner_id, @factory";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(factory));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.factory = factory;
            this.activityStatus = PrisonerActivityStatus.Idle;
            // Employment start date is system-controlled (R5): set once, the first time a
            // prisoner is actually placed with a factory; never overwritten afterward.
            if (!this.workStartDate.HasValue)
                this.workStartDate = DateTime.Today;
            this.update();
        }

        /// <summary>
        /// Transition: Idle → OnShiftWorking
        /// Prisoner clocks in to assigned task (R5). Guard: certs current (R9), task exists.
        /// Side effect: create DailyAttendance entry_time
        /// </summary>
        public void clockIn(int workOrderId)
        {
            if (this.activityStatus != PrisonerActivityStatus.Idle)
                throw new Exception("Error: prisoner is not in idle status");

            // Guard: check safety cert validity (R9)
            if (this.safetyTrainingValidity.HasValue && this.safetyTrainingValidity.Value < DateTime.Now)
                throw new Exception("Error: prisoner's safety certification has expired");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_clockIn @prisoner_id, @work_order_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@work_order_id", workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.OnShiftWorking;
            this.update();
        }

        /// <summary>
        /// Transition: OnShiftWorking → WaitingForMaterials
        /// Prisoner pauses due to unavailable materials (R7). Side effect: create Alert (R16).
        /// </summary>
        public void pauseForMaterials(string reason)
        {
            if (this.activityStatus != PrisonerActivityStatus.OnShiftWorking)
                throw new Exception("Error: prisoner is not currently working a shift");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_pauseForMaterials @prisoner_id, @reason";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@reason", reason ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.WaitingForMaterials;
            this.update();
        }

        /// <summary>
        /// Transition: OnShiftWorking → Idle
        /// Prisoner clocks out (R5). Side effect: update DailyAttendance exit_time, calc hours.
        /// </summary>
        public void clockOut()
        {
            if (this.activityStatus != PrisonerActivityStatus.OnShiftWorking)
                throw new Exception("Error: prisoner is not currently working a shift");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_clockOut @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Idle;
            this.update();
        }

        /// <summary>
        /// Transition: WaitingForMaterials → OnShiftWorking
        /// Materials restocked (R7). Side effect: clear Alert, resume task.
        /// </summary>
        public void resumeFromMaterials()
        {
            if (this.activityStatus != PrisonerActivityStatus.WaitingForMaterials)
                throw new Exception("Error: prisoner is not waiting for materials");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_resumeFromMaterials @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.OnShiftWorking;
            this.update();
        }

        /// <summary>
        /// Transition: WaitingForMaterials → Idle
        /// Task cancelled or shift ends while waiting for materials.
        /// </summary>
        public void abortTask()
        {
            if (this.activityStatus != PrisonerActivityStatus.WaitingForMaterials)
                throw new Exception("Error: prisoner is not waiting for materials");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_abortTask @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Idle;
            this.update();
        }

        /// <summary>
        /// Transition: Idle → InProfessionalTraining
        /// Enroll prisoner in job skills training.
        /// </summary>
        public void enrollInProfessionalTraining()
        {
            if (this.activityStatus != PrisonerActivityStatus.Idle)
                throw new Exception("Error: prisoner is not in idle status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_enrollInProfessionalTraining @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.InProfessionalTraining;
            this.update();
        }

        /// <summary>
        /// Transition: InProfessionalTraining → Idle
        /// Professional training completed; prisoner available for work again.
        /// </summary>
        public void completeProfessionalTraining()
        {
            if (this.activityStatus != PrisonerActivityStatus.InProfessionalTraining)
                throw new Exception("Error: prisoner is not in professional training");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_completeProfessionalTraining @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Idle;
            this.qualified = true;
            this.update();
        }

        /// <summary>
        /// Transition: Idle → InSafetyTraining
        /// Enroll prisoner in safety/compliance training.
        /// </summary>
        public void enrollInSafetyTraining()
        {
            if (this.activityStatus != PrisonerActivityStatus.Idle)
                throw new Exception("Error: prisoner is not in idle status");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_enrollInSafetyTraining @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.InSafetyTraining;
            this.update();
        }

        /// <summary>
        /// Transition: InSafetyTraining → Idle
        /// Safety training completed and cert acquired (R9).
        /// Side effect: create SafetyCertification record.
        /// </summary>
        public void completeSafetyTraining()
        {
            if (this.activityStatus != PrisonerActivityStatus.InSafetyTraining)
                throw new Exception("Error: prisoner is not in safety training");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_completeSafetyTraining @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            // Update safety training validity (set to 1 year from now)
            this.safetyTrainingValidity = DateTime.Now.AddYears(1);
            this.activityStatus = PrisonerActivityStatus.Idle;
            this.update();
        }

        /// <summary>
        /// Transition: Any → TemporarilyUnavailable
        /// Place prisoner on hold (security, medical, court). Side effect: create Alert (R16).
        /// </summary>
        public void placeOnHold(string reason)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_placeOnHold @prisoner_id, @reason";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@reason", reason ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.TemporarilyUnavailable;
            this.update();
        }

        /// <summary>
        /// Transition: TemporarilyUnavailable → Idle
        /// Hold released; prisoner available for work again. Side effect: clear Alert.
        /// </summary>
        public void releaseFromHold()
        {
            if (this.activityStatus != PrisonerActivityStatus.TemporarilyUnavailable)
                throw new Exception("Error: prisoner is not temporarily unavailable");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_releaseFromHold @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Idle;
            this.update();
        }

        /// <summary>
        /// Transition: TemporarilyUnavailable → Archived
        /// Permanent unavailability; archive prisoner.
        /// </summary>
        public void archiveUnavailable()
        {
            if (this.activityStatus != PrisonerActivityStatus.TemporarilyUnavailable)
                throw new Exception("Error: prisoner is not temporarily unavailable");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_archiveUnavailable @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Archived;
            this.update();
        }

        /// <summary>
        /// Transition: Any → Archived
        /// Release date reached or program termination. Side effect: cease PayrollRecord generation (R10).
        /// </summary>
        public void archive()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Prisoner_archive @prisoner_id";
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.activityStatus = PrisonerActivityStatus.Archived;
            this.update();
        }
    }
}
