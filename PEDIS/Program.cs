using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PEDIS
{
    static class Program
    {
        // =====================================================================
        // Static Lists for In-Memory Data Storage
        // Load order: Base entities first, then entities with FK references
        // =====================================================================
        public static List<EmploymentDepartment> EmploymentDepartments = new List<EmploymentDepartment>();
        public static List<CustomerCompany> CustomerCompanies = new List<CustomerCompany>();
        public static List<Product> Products = new List<Product>();
        public static List<DepartmentManagement> DepartmentManagements = new List<DepartmentManagement>();
        public static List<Prisoner> Prisoners = new List<Prisoner>();
        public static List<Contract> Contracts = new List<Contract>();
        public static List<ProductionOrder> ProductionOrders = new List<ProductionOrder>();
        public static List<WorkOrder> WorkOrders = new List<WorkOrder>();
        public static List<AttendanceRecord> AttendanceRecords = new List<AttendanceRecord>();
        public static List<ProductivityRecord> ProductivityRecords = new List<ProductivityRecord>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Initialize all in-memory lists from database
                initLists();

                // Start main application window
                Application.Run(new mainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error: " + ex.Message + "\n\n" + ex.StackTrace, "Application Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Initialize all in-memory lists from database.
        /// Load order is critical: base entities first, then entities with FK references.
        /// This ensures foreign keys resolve correctly when seeking related entities.
        /// </summary>
        public static void initLists()
        {
            // Load order (respecting FK dependencies):
            // 1. Base entities (no FK references to other entities)
            EmploymentDepartment.initEmploymentDepartments();
            CustomerCompany.initCustomerCompanies();
            Product.initProducts();

            // 2. Entities with FK to base entities
            DepartmentManagement.initDepartmentManagements();
            Prisoner.initPrisoners();
            Contract.initContracts();

            // 3. Entities with FK to tier 2
            ProductionOrder.initProductionOrders();

            // 4. Entities with FK to tier 3
            WorkOrder.initWorkOrders();

            // 5. Transactional records (FK to multiple entities)
            AttendanceRecord.initAttendanceRecords();
            ProductivityRecord.initProductivityRecords();
        }
    }
}
