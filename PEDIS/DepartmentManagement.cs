using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class DepartmentManagement
    {
        private int departmentManagementId;
        private string username;
        private string password;
        private DepartmentManagementRole role;
        private Factory? factory;
        private int employmentDepartmentId;
        private EmploymentDepartment employmentDepartment;

        public DepartmentManagement(int id, string user, string pass, DepartmentManagementRole role, Factory? factory, int deptId, bool isNew)
        {
            this.departmentManagementId = id;
            this.username = user;
            this.password = pass;
            this.role = role;
            this.factory = factory;
            this.employmentDepartmentId = deptId;
            this.employmentDepartment = EmploymentDepartment.seekById(deptId);
            if (isNew)
            {
                this.create();
                Program.DepartmentManagements.Add(this);
                if (this.employmentDepartment != null)
                    this.employmentDepartment.addManagementUser(this);
            }
        }

        public int getId() { return this.departmentManagementId; }
        public string getUsername() { return this.username; }
        public string getPassword() { return this.password; }
        public DepartmentManagementRole getRole() { return this.role; }
        public Factory? getFactory() { return this.factory; }
        public int getEmploymentDepartmentId() { return this.employmentDepartmentId; }
        public EmploymentDepartment getEmploymentDepartment() { return this.employmentDepartment; }

        public void setUsername(string user) { this.username = user; }
        public void setPassword(string pass) { this.password = pass; }
        public void setRole(DepartmentManagementRole role) { this.role = role; }
        public void setFactory(Factory? factory) { this.factory = factory; }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_DepartmentManagement_create @department_management_id, @username, @password, @role, @factory, @employment_department_id";
            cmd.Parameters.AddWithValue("@department_management_id", this.departmentManagementId);
            cmd.Parameters.AddWithValue("@username", this.username);
            cmd.Parameters.AddWithValue("@password", this.password);
            cmd.Parameters.AddWithValue("@role", EnumHelpers.ToDbString(this.role));
            cmd.Parameters.AddWithValue("@factory", this.factory.HasValue ? EnumHelpers.ToDbString(this.factory.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@employment_department_id", this.employmentDepartmentId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_DepartmentManagement_update @department_management_id, @username, @password, @role, @factory, @employment_department_id";
            cmd.Parameters.AddWithValue("@department_management_id", this.departmentManagementId);
            cmd.Parameters.AddWithValue("@username", this.username);
            cmd.Parameters.AddWithValue("@password", this.password);
            cmd.Parameters.AddWithValue("@role", EnumHelpers.ToDbString(this.role));
            cmd.Parameters.AddWithValue("@factory", this.factory.HasValue ? EnumHelpers.ToDbString(this.factory.Value) : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@employment_department_id", this.employmentDepartmentId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.DepartmentManagements.Remove(this);
            if (this.employmentDepartment != null)
                this.employmentDepartment.removeManagementUser(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_DepartmentManagement_delete @department_management_id";
            cmd.Parameters.AddWithValue("@department_management_id", this.departmentManagementId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initDepartmentManagements()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_DepartmentManagement_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.DepartmentManagements = new List<DepartmentManagement>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string user = reader.GetValue(1).ToString();
                string pass = reader.GetValue(2).ToString();
                DepartmentManagementRole role = EnumHelpers.DeptMgmtRoleFromDb(reader.GetValue(3).ToString());
                Factory? factory = reader.IsDBNull(4) ? null : EnumHelpers.FactoryFromDb(reader.GetValue(4).ToString());
                int deptId = Convert.ToInt32(reader.GetValue(5));

                DepartmentManagement dm = new DepartmentManagement(id, user, pass, role, factory, deptId, false);
                Program.DepartmentManagements.Add(dm);
            }
            reader.Close();
        }

        public static DepartmentManagement seekById(int id)
        {
            foreach (DepartmentManagement dm in Program.DepartmentManagements)
            {
                if (dm.getId() == id)
                    return dm;
            }
            return null;
        }
    }
}
