using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class EmploymentDepartment
    {
        private int employmentDepartmentId;
        private string departmentCode;
        private string departmentName;
        private string location;
        private int? maxCapacity;
        private bool isActive;
        private List<DepartmentManagement> managementUsers;

        public EmploymentDepartment(int id, string code, string name, string location, int? maxCapacity, bool isActive, bool isNew)
        {
            this.employmentDepartmentId = id;
            this.departmentCode = code;
            this.departmentName = name;
            this.location = location;
            this.maxCapacity = maxCapacity;
            this.isActive = isActive;
            if (isNew)
            {
                this.create();
                Program.EmploymentDepartments.Add(this);
            }
        }

        public int getId() { return this.employmentDepartmentId; }
        public string getCode() { return this.departmentCode; }
        public string getName() { return this.departmentName; }
        public string getLocation() { return this.location; }
        public int? getMaxCapacity() { return this.maxCapacity; }
        public bool getIsActive() { return this.isActive; }

        public void setCode(string code) { this.departmentCode = code; }
        public void setName(string name) { this.departmentName = name; }
        public void setLocation(string location) { this.location = location; }
        public void setMaxCapacity(int? capacity) { this.maxCapacity = capacity; }
        public void setIsActive(bool active) { this.isActive = active; }

        public List<DepartmentManagement> getManagementUsers()
        {
            if (managementUsers == null)
                managementUsers = new List<DepartmentManagement>();
            return managementUsers;
        }

        public void addManagementUser(DepartmentManagement user)
        {
            if (user == null)
                return;
            if (this.managementUsers == null)
                this.managementUsers = new List<DepartmentManagement>();
            if (!this.managementUsers.Contains(user))
                this.managementUsers.Add(user);
        }

        public void removeManagementUser(DepartmentManagement user)
        {
            if (user == null)
                return;
            if (this.managementUsers != null && this.managementUsers.Contains(user))
                this.managementUsers.Remove(user);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_EmploymentDepartment_create @employment_department_id, @department_code, @department_name, @location, @max_capacity, @is_active";
            cmd.Parameters.AddWithValue("@employment_department_id", this.employmentDepartmentId);
            cmd.Parameters.AddWithValue("@department_code", this.departmentCode);
            cmd.Parameters.AddWithValue("@department_name", this.departmentName);
            cmd.Parameters.AddWithValue("@location", this.location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@max_capacity", this.maxCapacity ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@is_active", this.isActive);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_EmploymentDepartment_update @employment_department_id, @department_code, @department_name, @location, @max_capacity, @is_active";
            cmd.Parameters.AddWithValue("@employment_department_id", this.employmentDepartmentId);
            cmd.Parameters.AddWithValue("@department_code", this.departmentCode);
            cmd.Parameters.AddWithValue("@department_name", this.departmentName);
            cmd.Parameters.AddWithValue("@location", this.location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@max_capacity", this.maxCapacity ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@is_active", this.isActive);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.EmploymentDepartments.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_EmploymentDepartment_delete @employment_department_id";
            cmd.Parameters.AddWithValue("@employment_department_id", this.employmentDepartmentId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initEmploymentDepartments()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_EmploymentDepartment_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.EmploymentDepartments = new List<EmploymentDepartment>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string code = reader.GetValue(1).ToString();
                string name = reader.GetValue(2).ToString();
                string loc = reader.IsDBNull(3) ? null : reader.GetValue(3).ToString();
                int? capacity = reader.IsDBNull(4) ? null : Convert.ToInt32(reader.GetValue(4));
                bool active = Convert.ToBoolean(reader.GetValue(5));

                EmploymentDepartment ed = new EmploymentDepartment(id, code, name, loc, capacity, active, false);
                Program.EmploymentDepartments.Add(ed);
            }
            reader.Close();
        }

        public static EmploymentDepartment seekById(int id)
        {
            foreach (EmploymentDepartment ed in Program.EmploymentDepartments)
            {
                if (ed.getId() == id)
                    return ed;
            }
            return null;
        }
    }
}
