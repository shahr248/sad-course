using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class CustomerCompany
    {
        private int customerCompanyId;
        private string companyId;
        private string companyName;
        private string contactName;
        private string phoneNumber;
        private string deliveryAddress;
        private string billingAddress;
        private string email;
        private string activityStatus;
        private List<Contract> contracts;
        private List<ProductionOrder> productionOrders;

        public CustomerCompany(int id, string compId, string name, string contact, string phone, string delivery, string billing, string email, string status, bool isNew)
        {
            this.customerCompanyId = id;
            this.companyId = compId;
            this.companyName = name;
            this.contactName = contact;
            this.phoneNumber = phone;
            this.deliveryAddress = delivery;
            this.billingAddress = billing;
            this.email = email;
            this.activityStatus = status;
            if (isNew)
            {
                this.create();
                Program.CustomerCompanies.Add(this);
            }
        }

        public int getId() { return this.customerCompanyId; }
        public string getCompanyId() { return this.companyId; }
        public string getName() { return this.companyName; }
        public string getContactName() { return this.contactName; }
        public string getPhoneNumber() { return this.phoneNumber; }
        public string getDeliveryAddress() { return this.deliveryAddress; }
        public string getBillingAddress() { return this.billingAddress; }
        public string getEmail() { return this.email; }
        public string getActivityStatus() { return this.activityStatus; }

        public void setCompanyId(string id) { this.companyId = id; }
        public void setName(string name) { this.companyName = name; }
        public void setContactName(string contact) { this.contactName = contact; }
        public void setPhoneNumber(string phone) { this.phoneNumber = phone; }
        public void setDeliveryAddress(string address) { this.deliveryAddress = address; }
        public void setBillingAddress(string address) { this.billingAddress = address; }
        public void setEmail(string email) { this.email = email; }
        public void setActivityStatus(string status) { this.activityStatus = status; }

        public List<Contract> getContracts()
        {
            if (contracts == null)
                contracts = new List<Contract>();
            return contracts;
        }

        public void addContract(Contract contract)
        {
            if (contract == null)
                return;
            if (this.contracts == null)
                this.contracts = new List<Contract>();
            if (!this.contracts.Contains(contract))
                this.contracts.Add(contract);
        }

        public void removeContract(Contract contract)
        {
            if (contract == null)
                return;
            if (this.contracts != null && this.contracts.Contains(contract))
                this.contracts.Remove(contract);
        }

        public List<ProductionOrder> getProductionOrders()
        {
            if (productionOrders == null)
                productionOrders = new List<ProductionOrder>();
            return productionOrders;
        }

        public void addProductionOrder(ProductionOrder order)
        {
            if (order == null)
                return;
            if (this.productionOrders == null)
                this.productionOrders = new List<ProductionOrder>();
            if (!this.productionOrders.Contains(order))
                this.productionOrders.Add(order);
        }

        public void removeProductionOrder(ProductionOrder order)
        {
            if (order == null)
                return;
            if (this.productionOrders != null && this.productionOrders.Contains(order))
                this.productionOrders.Remove(order);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_CustomerCompany_create @customer_company_id, @company_id, @company_name, @contact_name, @phone_number, @delivery_address, @billing_address, @email, @activity_status";
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@company_id", this.companyId);
            cmd.Parameters.AddWithValue("@company_name", this.companyName);
            cmd.Parameters.AddWithValue("@contact_name", this.contactName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_number", this.phoneNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address", this.deliveryAddress ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@billing_address", this.billingAddress ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@email", this.email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@activity_status", this.activityStatus ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_CustomerCompany_update @customer_company_id, @company_id, @company_name, @contact_name, @phone_number, @delivery_address, @billing_address, @email, @activity_status";
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@company_id", this.companyId);
            cmd.Parameters.AddWithValue("@company_name", this.companyName);
            cmd.Parameters.AddWithValue("@contact_name", this.contactName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@phone_number", this.phoneNumber ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@delivery_address", this.deliveryAddress ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@billing_address", this.billingAddress ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@email", this.email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@activity_status", this.activityStatus ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.CustomerCompanies.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_CustomerCompany_delete @customer_company_id";
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initCustomerCompanies()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_CustomerCompany_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.CustomerCompanies = new List<CustomerCompany>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string compId = reader.GetValue(1).ToString();
                string name = reader.GetValue(2).ToString();
                string contact = reader.IsDBNull(3) ? null : reader.GetValue(3).ToString();
                string phone = reader.IsDBNull(4) ? null : reader.GetValue(4).ToString();
                string delivery = reader.IsDBNull(5) ? null : reader.GetValue(5).ToString();
                string billing = reader.IsDBNull(6) ? null : reader.GetValue(6).ToString();
                string email = reader.IsDBNull(7) ? null : reader.GetValue(7).ToString();
                string status = reader.IsDBNull(8) ? null : reader.GetValue(8).ToString();

                CustomerCompany cc = new CustomerCompany(id, compId, name, contact, phone, delivery, billing, email, status, false);
                Program.CustomerCompanies.Add(cc);
            }
            reader.Close();
        }

        public static CustomerCompany seekById(int id)
        {
            foreach (CustomerCompany cc in Program.CustomerCompanies)
            {
                if (cc.getId() == id)
                    return cc;
            }
            return null;
        }
    }
}
