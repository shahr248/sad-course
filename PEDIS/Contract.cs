using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class Contract
    {
        private int contractId;
        private string contractNumber;
        private int customerCompanyId;
        private int? productId;
        private DateTime startDate;
        private decimal? pricePerUnit;
        private string paymentTerms;
        private ContractStatus? contractStatus;
        private CustomerCompany customerCompany;
        private Product product;
        private List<ProductionOrder> productionOrders;

        public Contract(int id, string contractNum, int custCompId, int? prodId, DateTime start, decimal? price, 
                       string terms, ContractStatus? status, bool isNew)
        {
            this.contractId = id;
            this.contractNumber = contractNum;
            this.customerCompanyId = custCompId;
            this.productId = prodId;
            this.startDate = start;
            this.pricePerUnit = price;
            this.paymentTerms = terms;
            this.contractStatus = status;
            this.customerCompany = CustomerCompany.seekById(custCompId);
            this.product = prodId.HasValue ? Product.seekById(prodId.Value) : null;
            if (isNew)
            {
                this.create();
                Program.Contracts.Add(this);
                if (this.customerCompany != null)
                    this.customerCompany.addContract(this);
                if (this.product != null)
                    this.product.addContract(this);
            }
        }

        public int getId() { return this.contractId; }
        public string getContractNumber() { return this.contractNumber; }
        public int getCustomerCompanyId() { return this.customerCompanyId; }
        public int? getProductId() { return this.productId; }
        public DateTime getStartDate() { return this.startDate; }
        public decimal? getPricePerUnit() { return this.pricePerUnit; }
        public string getPaymentTerms() { return this.paymentTerms; }
        public ContractStatus? getContractStatus() { return this.contractStatus; }
        public CustomerCompany getCustomerCompany() { return this.customerCompany; }
        public Product getProduct() { return this.product; }

        public void setContractNumber(string num) { this.contractNumber = num; }
        public void setPricePerUnit(decimal? price) { this.pricePerUnit = price; }
        public void setPaymentTerms(string terms) { this.paymentTerms = terms; }
        public void setContractStatus(ContractStatus? status) { this.contractStatus = status; }

        public List<ProductionOrder> getProductionOrders()
        {
            if (productionOrders == null)
                productionOrders = new List<ProductionOrder>();
            return productionOrders;
        }

        public void addProductionOrder(ProductionOrder order)
        {
            if (order == null) return;
            if (this.productionOrders == null) this.productionOrders = new List<ProductionOrder>();
            if (!this.productionOrders.Contains(order)) this.productionOrders.Add(order);
        }

        public void removeProductionOrder(ProductionOrder order)
        {
            if (order == null) return;
            if (this.productionOrders != null && this.productionOrders.Contains(order)) this.productionOrders.Remove(order);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_create @contract_id, @contract_number, @customer_company_id, @product_id, @start_date, @price_per_unit, @payment_terms, @contract_status";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            cmd.Parameters.AddWithValue("@contract_number", this.contractNumber);
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@product_id", this.productId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@start_date", this.startDate);
            cmd.Parameters.AddWithValue("@price_per_unit", this.pricePerUnit ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@payment_terms", this.paymentTerms ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_status", this.contractStatus.HasValue ? EnumHelpers.ToDbString(this.contractStatus.Value) : (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_update @contract_id, @contract_number, @customer_company_id, @product_id, @start_date, @price_per_unit, @payment_terms, @contract_status";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            cmd.Parameters.AddWithValue("@contract_number", this.contractNumber);
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@product_id", this.productId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@start_date", this.startDate);
            cmd.Parameters.AddWithValue("@price_per_unit", this.pricePerUnit ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@payment_terms", this.paymentTerms ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@contract_status", this.contractStatus.HasValue ? EnumHelpers.ToDbString(this.contractStatus.Value) : (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.Contracts.Remove(this);
            if (this.customerCompany != null)
                this.customerCompany.removeContract(this);
            if (this.product != null)
                this.product.removeContract(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_delete @contract_id";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initContracts()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.Contracts = new List<Contract>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string contractNum = reader.GetValue(1).ToString();
                int custCompId = Convert.ToInt32(reader.GetValue(2));
                int? prodId = reader.IsDBNull(3) ? null : Convert.ToInt32(reader.GetValue(3));
                DateTime start = Convert.ToDateTime(reader.GetValue(4));
                decimal? price = reader.IsDBNull(5) ? null : Convert.ToDecimal(reader.GetValue(5));
                string terms = reader.IsDBNull(6) ? null : reader.GetValue(6).ToString();
                ContractStatus? status = reader.IsDBNull(7) ? null : EnumHelpers.ContractStatusFromDb(reader.GetValue(7).ToString());

                Contract c = new Contract(id, contractNum, custCompId, prodId, start, price, terms, status, false);
                Program.Contracts.Add(c);
            }
            reader.Close();
        }

        public static Contract seekById(int id)
        {
            foreach (Contract c in Program.Contracts)
            {
                if (c.getId() == id)
                    return c;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (4 transitions for ContractStatus)
        // ============================================================================

        /// <summary>
        /// Transition: Active → Inactive
        /// Manual suspension by Dept Manager. Side effect: prevent new ProductionOrders under this contract (R3).
        /// </summary>
        public void suspend()
        {
            if (this.contractStatus != ContractStatus.Active)
                throw new Exception("כשגיאה: חוזה לא פעיל");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_suspend @contract_id";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.contractStatus = ContractStatus.Inactive;
            this.update();
        }

        /// <summary>
        /// Transition: Inactive → Active
        /// Manual reactivation by Dept Manager. Side effect: re-enable ProductionOrder acceptance (R3).
        /// </summary>
        public void reactivate()
        {
            if (this.contractStatus != ContractStatus.Inactive)
                throw new Exception("כשגיאה: חוזה לא לא פעיל");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_reactivate @contract_id";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.contractStatus = ContractStatus.Active;
            this.update();
        }

        /// <summary>
        /// Transition: Active/Inactive → Expired
        /// End date reached (daily check). Side effect: archive contract, block new orders, notify customer (R12).
        /// Guard: TODAY >= contract.end_date (but this is date-driven, typically called by background job)
        /// </summary>
        public void expire()
        {
            if (this.contractStatus != ContractStatus.Active && this.contractStatus != ContractStatus.Inactive)
                throw new Exception("כשגיאה: חוזה כבר פג");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_expire @contract_id";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.contractStatus = ContractStatus.Expired;
            this.update();
        }

        /// <summary>
        /// Transition: Expired → (terminal)
        /// Archive expired contract. No further transitions allowed.
        /// </summary>
        public void archive()
        {
            if (this.contractStatus != ContractStatus.Expired)
                throw new Exception("כשגיאה: חוזה לא פג");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Contract_archive @contract_id";
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            // No new state; contract remains Expired but marked as archived in DB
            this.update();
        }
    }
}
