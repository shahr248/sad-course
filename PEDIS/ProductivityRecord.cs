using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class ProductivityRecord
    {
        private int productivityRecordId;
        private int prisonerId;
        private int workOrderId;
        private DateTime productivityDate;
        private int quantityProduced;
        private decimal? workHours;
        private ProductivityType productivityType;
        private Prisoner prisoner;
        private WorkOrder workOrder;

        public ProductivityRecord(int id, int prisonId, int workOrderId, DateTime date, int qtyProduced,
                                 decimal? hours, ProductivityType type, bool isNew)
        {
            this.productivityRecordId = id;
            this.prisonerId = prisonId;
            this.workOrderId = workOrderId;
            this.productivityDate = date;
            this.quantityProduced = qtyProduced;
            this.workHours = hours;
            this.productivityType = type;
            this.prisoner = Prisoner.seekById(prisonId);
            this.workOrder = WorkOrder.seekById(workOrderId);
            if (isNew)
            {
                this.create();
                Program.ProductivityRecords.Add(this);
            }
            // Link back to parents regardless of isNew -- same fix as WorkOrder's link to
            // ProductionOrder, so records loaded from the DB at startup are reachable too.
            if (this.prisoner != null)
                this.prisoner.addProductivityRecord(this);
            if (this.workOrder != null)
                this.workOrder.addProductivityRecord(this);
        }

        public int getId() { return this.productivityRecordId; }
        public int getPrisonerId() { return this.prisonerId; }
        public int getWorkOrderId() { return this.workOrderId; }
        public DateTime getProductivityDate() { return this.productivityDate; }
        public int getQuantityProduced() { return this.quantityProduced; }
        public decimal? getWorkHours() { return this.workHours; }
        public ProductivityType getProductivityType() { return this.productivityType; }
        public Prisoner getPrisoner() { return this.prisoner; }
        public WorkOrder getWorkOrder() { return this.workOrder; }

        // Factory where this production actually happened (via the WorkOrder's ProductionOrder),
        // not the prisoner's home factory — a prisoner can work across multiple factories.
        public Factory? getFactory() { return this.workOrder?.getFactory(); }

        public void setQuantityProduced(int qty) { this.quantityProduced = qty; }
        public void setWorkHours(decimal? hours) { this.workHours = hours; }
        public void setProductivityType(ProductivityType type) { this.productivityType = type; }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_create @productivity_record_id, @prisoner_id, @work_order_id, @productivity_date, @quantity_produced, @work_hours, @productivity_type";
            cmd.Parameters.AddWithValue("@productivity_record_id", this.productivityRecordId);
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            cmd.Parameters.AddWithValue("@productivity_date", this.productivityDate);
            cmd.Parameters.AddWithValue("@quantity_produced", this.quantityProduced);
            cmd.Parameters.AddWithValue("@work_hours", this.workHours ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@productivity_type", EnumHelpers.ToDbString(this.productivityType));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_update @productivity_record_id, @prisoner_id, @work_order_id, @productivity_date, @quantity_produced, @work_hours, @productivity_type";
            cmd.Parameters.AddWithValue("@productivity_record_id", this.productivityRecordId);
            cmd.Parameters.AddWithValue("@prisoner_id", this.prisonerId);
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            cmd.Parameters.AddWithValue("@productivity_date", this.productivityDate);
            cmd.Parameters.AddWithValue("@quantity_produced", this.quantityProduced);
            cmd.Parameters.AddWithValue("@work_hours", this.workHours ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@productivity_type", EnumHelpers.ToDbString(this.productivityType));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.ProductivityRecords.Remove(this);
            if (this.prisoner != null)
                this.prisoner.removeProductivityRecord(this);
            if (this.workOrder != null)
                this.workOrder.removeProductivityRecord(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_delete @productivity_record_id";
            cmd.Parameters.AddWithValue("@productivity_record_id", this.productivityRecordId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initProductivityRecords()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.ProductivityRecords = new List<ProductivityRecord>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                int prisonId = Convert.ToInt32(reader.GetValue(1));
                int workOrderId = Convert.ToInt32(reader.GetValue(2));
                DateTime date = Convert.ToDateTime(reader.GetValue(3));
                int qtyProduced = Convert.ToInt32(reader.GetValue(4));
                decimal? hours = reader.IsDBNull(5) ? null : Convert.ToDecimal(reader.GetValue(5));
                ProductivityType type = EnumHelpers.ProductivityTypeFromDb(reader.GetValue(6).ToString());

                ProductivityRecord pr = new ProductivityRecord(id, prisonId, workOrderId, date, qtyProduced, hours, type, false);
                Program.ProductivityRecords.Add(pr);
            }
            reader.Close();
        }

        public static ProductivityRecord seekById(int id)
        {
            foreach (ProductivityRecord pr in Program.ProductivityRecords)
            {
                if (pr.getId() == id)
                    return pr;
            }
            return null;
        }

        // ============================================================================
        // STATE TRANSITION METHODS (2 implicit states: Pending, Verified)
        // Implicit states inferred from verification status
        // ============================================================================

        /// <summary>
        /// Transition: (Create) → Pending
        /// Worker inputs units produced during shift (R6). Create ProductivityRecord with units_produced.
        /// Side effect: calculate productivity rate.
        /// </summary>
        public void submitProduction(int unitsProduced, int defectiveUnits)
        {
            if (unitsProduced < 0)
                throw new Exception("כשגיאה: כמות יחידות לא תקנית");

            this.quantityProduced = unitsProduced;
            this.create();
            this.update();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_submitProduction @productivity_record_id, @units_produced, @defective_units";
            cmd.Parameters.AddWithValue("@productivity_record_id", this.productivityRecordId);
            cmd.Parameters.AddWithValue("@units_produced", unitsProduced);
            cmd.Parameters.AddWithValue("@defective_units", defectiveUnits);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        /// <summary>
        /// Transition: Pending → Verified
        /// Supervisor QA review (manual or auto-check quality).
        /// Side effect: update WorkOrder.completed_quantity, check if WorkOrder complete (R6).
        /// Also feed into PerformanceEvaluation (R11) and PayrollRecord (R10).
        /// </summary>
        public void approveProduction()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_ProductivityRecord_approveProduction @productivity_record_id, @work_order_id";
            cmd.Parameters.AddWithValue("@productivity_record_id", this.productivityRecordId);
            cmd.Parameters.AddWithValue("@work_order_id", this.workOrderId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            // Side effect: update parent WorkOrder completed quantity
            if (this.workOrder != null)
            {
                int currentCompleted = this.workOrder.getProductionOrder() != null ?
                    this.workOrder.getProductionOrder().getCompletedQuantity() : 0;
                this.workOrder.getProductionOrder()?.setCompletedQuantity(currentCompleted + this.quantityProduced);
                this.workOrder.getProductionOrder()?.update();
            }

            this.update();
        }
    }
}
