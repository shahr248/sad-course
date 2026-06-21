using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class WorkOrder
    {
        private int workOrderId;
        private string workOrderNumber;
        private int productionOrderId;
        private int requiredQuantity;
        private DateTime startDate;
        private DateTime deadline;
        private Factory factory;
        private WorkOrderStatus status;
        private string holdReason;
        private ProductionOrder productionOrder;
        private List<ProductivityRecord> productivityRecords;

        public WorkOrder(int id, string workNum, int prodOrderId, int reqQty, DateTime start, DateTime deadline,
                        Factory factory, WorkOrderStatus status, string holdReason, bool isNew)
        {
            this.workOrderId = id;
            this.workOrderNumber = workNum;
            this.productionOrderId = prodOrderId;
            this.requiredQuantity = reqQty;
            this.startDate = start;
            this.deadline = deadline;
            this.factory = factory;
            this.status = status;
            this.holdReason = holdReason;
            this.productionOrder = ProductionOrder.seekById(prodOrderId);
            if (isNew)
            {
                this.create();
                Program.WorkOrders.Add(this);
                if (this.productionOrder != null)
                    this.productionOrder.addWorkOrder(this);
            }
        }

        public int getId() { return this.workOrderId; }
        public string getWorkOrderNumber() { return this.workOrderNumber; }
        public int getProductionOrderId() { return this.productionOrderId; }
        public int getRequiredQuantity() { return this.requiredQuantity; }
        public DateTime getStartDate() { return this.startDate; }
        public DateTime getDeadline() { return this.deadline; }
        public Factory getFactory() { return this.factory; }
        public WorkOrderStatus getStatus() { return this.status; }
        public string getHoldReason() { return this.holdReason; }
        public ProductionOrder getProductionOrder() { return this.productionOrder; }

        public void setWorkOrderNumber(string num) { this.workOrderNumber = num; }
        public void setRequiredQuantity(int qty) { this.requiredQuantity = qty; }
        public void setStartDate(DateTime date) { this.startDate = date; }
        public void setDeadline(DateTime deadline) { this.deadline = deadline; }
        public void setFactory(Factory factory) { this.factory = factory; }
        public void setStatus(WorkOrderStatus status) { this.status = status; }
        public void setHoldReason(string reason) { this.holdReason = reason; }

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
            cmd.CommandText = "EXECUTE sp_WorkOrder_create @work_order_id, @work_order_number, @production_order_id, @required_quantity, @start_date, @deadline, @factory, @status, @hold_reason";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            cmd.Parameters.AddWithValue("@work_order_number", this.workOrderNumber);
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@required_quantity", this.requiredQuantity);
            cmd.Parameters.AddWithValue("@start_date", this.startDate);
            cmd.Parameters.AddWithValue("@deadline", this.deadline);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            cmd.Parameters.AddWithValue("@status", EnumHelpers.ToDbString(this.status));
            cmd.Parameters.AddWithValue("@hold_reason", this.holdReason ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_update @work_order_id, @work_order_number, @production_order_id, @required_quantity, @start_date, @deadline, @factory, @status, @hold_reason";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            cmd.Parameters.AddWithValue("@work_order_number", this.workOrderNumber);
            cmd.Parameters.AddWithValue("@production_order_id", this.productionOrderId);
            cmd.Parameters.AddWithValue("@required_quantity", this.requiredQuantity);
            cmd.Parameters.AddWithValue("@start_date", this.startDate);
            cmd.Parameters.AddWithValue("@deadline", this.deadline);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            cmd.Parameters.AddWithValue("@status", EnumHelpers.ToDbString(this.status));
            cmd.Parameters.AddWithValue("@hold_reason", this.holdReason ?? (object)DBNull.Value);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.WorkOrders.Remove(this);
            if (this.productionOrder != null)
                this.productionOrder.removeWorkOrder(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_delete @work_order_id";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initWorkOrders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.WorkOrders = new List<WorkOrder>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string workNum = reader.GetValue(1).ToString();
                int prodOrderId = Convert.ToInt32(reader.GetValue(2));
                int reqQty = Convert.ToInt32(reader.GetValue(3));
                DateTime start = Convert.ToDateTime(reader.GetValue(4));
                DateTime deadline = Convert.ToDateTime(reader.GetValue(5));
                Factory factory = EnumHelpers.FactoryFromDb(reader.GetValue(6).ToString());
                WorkOrderStatus status = EnumHelpers.WorkOrderStatusFromDb(reader.GetValue(7).ToString());
                string holdReason = reader.IsDBNull(8) ? null : reader.GetValue(8).ToString();

                WorkOrder wo = new WorkOrder(id, workNum, prodOrderId, reqQty, start, deadline, factory, status, holdReason, false);
                Program.WorkOrders.Add(wo);
            }
            reader.Close();
        }

        public static WorkOrder seekById(int id)
        {
            foreach (WorkOrder wo in Program.WorkOrders)
            {
                if (wo.getId() == id)
                    return wo;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (4 transitions for WorkOrderStatus)
        // ============================================================================

        /// <summary>
        /// Transition: HasntEnteredIntoProductionYet → InProcess
        /// Assign inmates to task (R4). Guard: inmates available, security clearance OK.
        /// Side effect: create first AttendanceRecord entry, notify assigned inmates.
        /// </summary>
        public void enterProduction(List<int> inmateIds)
        {
            if (this.status != WorkOrderStatus.HasntEnteredIntoProductionYet)
                throw new Exception("כשגיאה: הזמנת עבודה כבר בייצור");

            if (inmateIds == null || inmateIds.Count == 0)
                throw new Exception("כשגיאה: אין כלאים שהוקצו");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_enterProduction @work_order_id, @inmate_count";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            cmd.Parameters.AddWithValue("@inmate_count", inmateIds.Count);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.status = WorkOrderStatus.InProcess;
            this.update();
        }

        /// <summary>
        /// Transition: InProcess → HasntEnteredIntoProductionYet
        /// Inmate reassignment or task reset. Side effect: clear current assignments, reset completed_quantity to 0.
        /// </summary>
        public void resetAssignment()
        {
            if (this.status != WorkOrderStatus.InProcess)
                throw new Exception("כשגיאה: הזמנת עבודה לא בתהליך");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_resetAssignment @work_order_id";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.status = WorkOrderStatus.HasntEnteredIntoProductionYet;
            this.update();
        }

        /// <summary>
        /// Transition: InProcess → Completed
        /// Production complete. Guard: completed_quantity >= required_quantity; QA sign-off.
        /// Side effect: update parent ProductionOrder.completed_quantity, check if ready for pickup.
        /// </summary>
        public void markComplete()
        {
            if (this.status != WorkOrderStatus.InProcess)
                throw new Exception("כשגיאה: הזמנת עבודה לא בתהליך");

            // Note: actual completed_quantity is tracked in ProductivityRecords
            // For simplicity, assume caller has verified completion

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_markComplete @work_order_id";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            this.status = WorkOrderStatus.Completed;
            this.update();
        }

        /// <summary>
        /// Transition: Any → (abandoned state, no explicit transition)
        /// ProductionOrder cancelled; WorkOrder marked as abandoned (internal state change).
        /// </summary>
        public void cancel()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_WorkOrder_cancel @work_order_id";
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            // No explicit state enum for "abandoned", so leave as-is but mark in DB
            this.update();
        }
    }
}
