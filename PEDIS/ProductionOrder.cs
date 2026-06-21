using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class ProductionOrder
    {
        private int productionOrderId;
        private string orderNumber;
        private int customerCompanyId;
        private int productId;
        private int contractId;
        private int quantity;
        private int completedQuantity;
        private DateTime submissionDate;
        private DateTime deliveryDeadline;
        private ProductionOrderStatus orderStatus;
        private CustomerCompany customerCompany;
        private Product product;
        private Contract contract;
        private List<WorkOrder> workOrders;

        public ProductionOrder(int id, string orderNum, int custCompId, int prodId, int contId, int qty, int completed,
                              DateTime submission, DateTime deadline, ProductionOrderStatus status, bool isNew)
        {
            this.productionOrderId = id;
            this.orderNumber = orderNum;
            this.customerCompanyId = custCompId;
            this.productId = prodId;
            this.contractId = contId;
            this.quantity = qty;
            this.completedQuantity = completed;
            this.submissionDate = submission;
            this.deliveryDeadline = deadline;
            this.orderStatus = status;
            this.customerCompany = CustomerCompany.seekById(custCompId);
            this.product = Product.seekById(prodId);
            this.contract = Contract.seekById(contId);
            if (isNew)
            {
                this.create();
                Program.ProductionOrders.Add(this);
                if (this.customerCompany != null)
                    this.customerCompany.addProductionOrder(this);
                if (this.product != null)
                    this.product.addProductionOrder(this);
                if (this.contract != null)
                    this.contract.addProductionOrder(this);
            }
        }

        public int getId() { return this.productionOrderId; }
        public string getOrderNumber() { return this.orderNumber; }
        public int getCustomerCompanyId() { return this.customerCompanyId; }
        public int getProductId() { return this.productId; }
        public int getContractId() { return this.contractId; }
        public int getQuantity() { return this.quantity; }
        public int getCompletedQuantity() { return this.completedQuantity; }
        public DateTime getSubmissionDate() { return this.submissionDate; }
        public DateTime getDeliveryDeadline() { return this.deliveryDeadline; }
        public ProductionOrderStatus getOrderStatus() { return this.orderStatus; }
        public CustomerCompany getCustomerCompany() { return this.customerCompany; }
        public Product getProduct() { return this.product; }
        public Contract getContract() { return this.contract; }

        public void setOrderNumber(string num) { this.orderNumber = num; }
        public void setQuantity(int qty) { this.quantity = qty; }
        public void setCompletedQuantity(int completed) { this.completedQuantity = completed; }
        public void setDeliveryDeadline(DateTime deadline) { this.deliveryDeadline = deadline; }
        public void setOrderStatus(ProductionOrderStatus status) { this.orderStatus = status; }

        public List<WorkOrder> getWorkOrders()
        {
            if (workOrders == null)
                workOrders = new List<WorkOrder>();
            return workOrders;
        }

        public void addWorkOrder(WorkOrder order)
        {
            if (order == null) return;
            if (this.workOrders == null) this.workOrders = new List<WorkOrder>();
            if (!this.workOrders.Contains(order)) this.workOrders.Add(order);
        }

        public void removeWorkOrder(WorkOrder order)
        {
            if (order == null) return;
            if (this.workOrders != null && this.workOrders.Contains(order)) this.workOrders.Remove(order);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_create @production_order_id, @order_number, @customer_company_id, @product_id, @contract_id, @quantity, @completed_quantity, @submission_date, @delivery_deadline, @order_status";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@order_number", this.orderNumber);
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@product_id", this.productId);
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            cmd.Parameters.AddWithValue("@quantity", this.quantity);
            cmd.Parameters.AddWithValue("@completed_quantity", this.completedQuantity);
            cmd.Parameters.AddWithValue("@submission_date", this.submissionDate);
            cmd.Parameters.AddWithValue("@delivery_deadline", this.deliveryDeadline);
            cmd.Parameters.AddWithValue("@order_status", EnumHelpers.ToDbString(this.orderStatus));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_update @production_order_id, @order_number, @customer_company_id, @product_id, @contract_id, @quantity, @completed_quantity, @submission_date, @delivery_deadline, @order_status";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@order_number", this.orderNumber);
            cmd.Parameters.AddWithValue("@customer_company_id", this.customerCompanyId);
            cmd.Parameters.AddWithValue("@product_id", this.productId);
            cmd.Parameters.AddWithValue("@contract_id", this.contractId);
            cmd.Parameters.AddWithValue("@quantity", this.quantity);
            cmd.Parameters.AddWithValue("@completed_quantity", this.completedQuantity);
            cmd.Parameters.AddWithValue("@submission_date", this.submissionDate);
            cmd.Parameters.AddWithValue("@delivery_deadline", this.deliveryDeadline);
            cmd.Parameters.AddWithValue("@order_status", EnumHelpers.ToDbString(this.orderStatus));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.ProductionOrders.Remove(this);
            if (this.customerCompany != null)
                this.customerCompany.removeProductionOrder(this);
            if (this.product != null)
                this.product.removeProductionOrder(this);
            if (this.contract != null)
                this.contract.removeProductionOrder(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_delete @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initProductionOrders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.ProductionOrders = new List<ProductionOrder>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string orderNum = reader.GetValue(1).ToString();
                int custCompId = Convert.ToInt32(reader.GetValue(2));
                int prodId = Convert.ToInt32(reader.GetValue(3));
                int contId = Convert.ToInt32(reader.GetValue(4));
                int qty = Convert.ToInt32(reader.GetValue(5));
                int completed = Convert.ToInt32(reader.GetValue(6));
                DateTime submission = Convert.ToDateTime(reader.GetValue(7));
                DateTime deadline = Convert.ToDateTime(reader.GetValue(8));
                ProductionOrderStatus status = EnumHelpers.OrderStatusFromDb(reader.GetValue(9).ToString());

                ProductionOrder po = new ProductionOrder(id, orderNum, custCompId, prodId, contId, qty, completed, submission, deadline, status, false);
                Program.ProductionOrders.Add(po);
            }
            reader.Close();
        }

        public static ProductionOrder seekById(int id)
        {
            foreach (ProductionOrder po in Program.ProductionOrders)
            {
                if (po.getId() == id)
                    return po;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (7 transitions for ProductionOrderStatus)
        // ============================================================================

        /// <summary>
        /// Transition: Received → InProduction
        /// Accept order and assign factory (R2, R3). Guard: factory has capacity; deadline achievable.
        /// Side effect: create WorkOrders, assign inmates (R4), notify Factory Manager.
        /// </summary>
        public void acceptOrder(int factoryId)
        {
            if (this.orderStatus != ProductionOrderStatus.Received)
                throw new Exception("כשגיאה: הזמנה לא בסטטוס קבלה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_acceptOrder @production_order_id, @factory_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@factory_id", factoryId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.InProduction;
            this.update();
        }

        /// <summary>
        /// Transition: Received → Cancelled
        /// Reject order or insufficient capacity. Side effect: notify customer (R12).
        /// </summary>
        public void cancelOrder()
        {
            if (this.orderStatus != ProductionOrderStatus.Received)
                throw new Exception("כשגיאה: הזמנה לא בסטטוס קבלה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_cancelOrder @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.Cancelled;
            this.update();
        }

        /// <summary>
        /// Transition: InProduction → OnHold
        /// Materials unavailable (R7), quality issue, or security incident.
        /// Side effect: create Alert (R16), notify Factory Manager.
        /// </summary>
        public void holdOrder(string reason)
        {
            if (this.orderStatus != ProductionOrderStatus.InProduction)
                throw new Exception("כשגיאה: הזמנה לא בייצור");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_holdOrder @production_order_id, @reason";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@reason", reason ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.OnHold;
            this.update();
        }

        /// <summary>
        /// Transition: OnHold → InProduction
        /// Issue resolved (materials restocked, quality rework complete).
        /// Side effect: clear Alert, resume WorkOrders, notify Factory Manager.
        /// </summary>
        public void resumeOrder()
        {
            if (this.orderStatus != ProductionOrderStatus.OnHold)
                throw new Exception("כשגיאה: הזמנה לא בהחזקה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_resumeOrder @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.InProduction;
            this.update();
        }

        /// <summary>
        /// Transition: InProduction → ReadyForPickup
        /// All units completed (R6) and quality passed (R6). Guard: completed_quantity >= quantity; quality pass.
        /// Side effect: notify customer (R12) "Order ready".
        /// </summary>
        public void markReadyForPickup()
        {
            if (this.orderStatus != ProductionOrderStatus.InProduction)
                throw new Exception("כשגיאה: הזמנה לא בייצור");

            // Guard: check quantity and quality (simplified - assume quality tracked elsewhere)
            if (this.completedQuantity < this.quantity)
                throw new Exception("כשגיאה: לא הושלמה כמות יחידות מספקת");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_markReadyForPickup @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.ReadyForPickup;
            this.update();
        }

        /// <summary>
        /// Transition: ReadyForPickup → Delivered
        /// Customer pickup confirmed (R8). Side effect: generate PayrollRecords (R10) for all inmates worked on order.
        /// </summary>
        public void markDelivered()
        {
            if (this.orderStatus != ProductionOrderStatus.ReadyForPickup)
                throw new Exception("כשגיאה: הזמנה לא מוכנה לאיסוף");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_markDelivered @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.Delivered;
            this.update();
        }

        /// <summary>
        /// Transition: OnHold → Cancelled
        /// Hold cannot be resolved; cancel order. Side effect: notify customer, archive.
        /// </summary>
        public void cancelOnHold()
        {
            if (this.orderStatus != ProductionOrderStatus.OnHold)
                throw new Exception("כשגיאה: הזמנה לא בהחזקה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductionOrder_cancelOnHold @production_order_id";
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.orderStatus = ProductionOrderStatus.Cancelled;
            this.update();
        }
    }
}
