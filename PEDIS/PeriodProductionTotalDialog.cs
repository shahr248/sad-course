using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PEDIS
{
    public class PeriodProductionTotalDialog : Form
    {
        private DateTime periodStartDate;
        private DateTime periodEndDate;
        private int? filteredProductId;
        private DepartmentManagement currentUser;
        private Factory? selectedFactoryFilter;
        private ListView lvPeriodTotals;
        private Button btnApplyToOrder;
        private Button btnClose;
        private Label lblTitle;
        private Label lblFactory;
        private ComboBox cmbFactory;

        public PeriodProductionTotalDialog(DateTime periodStartDate, DateTime periodEndDate, DepartmentManagement currentUser, int? filteredProductId = null)
        {
            this.periodStartDate = periodStartDate;
            this.periodEndDate = periodEndDate;
            this.currentUser = currentUser;
            this.filteredProductId = filteredProductId;
            InitializeComponent();
            InitializeFactoryFilter();
            LoadPeriodTotals();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblFactory = new Label();
            this.cmbFactory = new ComboBox();
            this.lvPeriodTotals = new ListView();
            this.btnApplyToOrder = new Button();
            this.btnClose = new Button();
            this.SuspendLayout();

            this.Text = "Period Production Totals";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(550, 30);
            this.lblTitle.Text = "Period Production Totals - " + periodStartDate.ToString("yyyy-MM-dd") + " to " + periodEndDate.ToString("yyyy-MM-dd");
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lblFactory.AutoSize = false;
            this.lblFactory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFactory.Location = new System.Drawing.Point(20, 50);
            this.lblFactory.Size = new System.Drawing.Size(100, 25);
            this.lblFactory.Text = "Filter by Factory:";
            this.lblFactory.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);

            this.cmbFactory.Location = new System.Drawing.Point(130, 50);
            this.cmbFactory.Size = new System.Drawing.Size(200, 25);
            this.cmbFactory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFactory.SelectedIndexChanged += new EventHandler(this.cmbFactory_SelectedIndexChanged);

            this.lvPeriodTotals.FullRowSelect = true;
            this.lvPeriodTotals.GridLines = true;
            this.lvPeriodTotals.Location = new System.Drawing.Point(20, 85);
            this.lvPeriodTotals.Size = new System.Drawing.Size(550, 235);
            this.lvPeriodTotals.UseCompatibleStateImageBehavior = false;
            this.lvPeriodTotals.View = System.Windows.Forms.View.Details;
            this.lvPeriodTotals.BackColor = System.Drawing.Color.White;
            this.lvPeriodTotals.BorderStyle = BorderStyle.Fixed3D;

            this.lvPeriodTotals.Columns.Add("Product Name", 250);
            this.lvPeriodTotals.Columns.Add("Total Units", 150);
            this.lvPeriodTotals.Columns.Add("Work Orders", 150);

            this.btnApplyToOrder.Location = new System.Drawing.Point(20, 330);
            this.btnApplyToOrder.Size = new System.Drawing.Size(150, 35);
            this.btnApplyToOrder.Text = "Apply to Order";
            this.btnApplyToOrder.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnApplyToOrder.ForeColor = System.Drawing.Color.White;
            this.btnApplyToOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApplyToOrder.UseVisualStyleBackColor = false;
            this.btnApplyToOrder.Click += new System.EventHandler(this.btnApplyToOrder_Click);

            this.btnClose.Location = new System.Drawing.Point(420, 330);
            this.btnClose.Size = new System.Drawing.Size(150, 35);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.DialogResult = DialogResult.OK;

            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApplyToOrder);
            this.Controls.Add(this.lvPeriodTotals);
            this.Controls.Add(this.cmbFactory);
            this.Controls.Add(this.lblFactory);
            this.Controls.Add(this.lblTitle);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeFactoryFilter()
        {
            bool isFactoryManager = currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager;

            if (isFactoryManager)
            {
                // Factory Manager: always scoped to their own factory, no choice to make
                selectedFactoryFilter = currentUser.getFactory();
                lblFactory.Text = "Factory: " + currentUser.getFactory();
                cmbFactory.Visible = false;
            }
            else
            {
                // Wing Manager / Deputy Wing Manager: see all factories by default, with the option to narrow down
                cmbFactory.Items.Add("All Factories");
                foreach (Factory factory in Enum.GetValues(typeof(Factory)))
                {
                    cmbFactory.Items.Add(factory.ToString());
                }
                cmbFactory.SelectedIndex = 0;
            }
        }

        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactory.SelectedIndex <= 0)
                selectedFactoryFilter = null;
            else
                selectedFactoryFilter = (Factory)Enum.Parse(typeof(Factory), cmbFactory.SelectedItem.ToString());

            LoadPeriodTotals();
        }

        private void LoadPeriodTotals()
        {
            lvPeriodTotals.Items.Clear();
            Dictionary<int, (string productName, int totalUnits, List<int> workOrderIds)> productTotals =
                new Dictionary<int, (string, int, List<int>)>();

            foreach (ProductivityRecord record in Program.ProductivityRecords)
            {
                if (record.getProductivityDate().Date < periodStartDate.Date || record.getProductivityDate().Date > periodEndDate.Date)
                    continue;

                if (selectedFactoryFilter.HasValue && record.getFactory() != selectedFactoryFilter.Value)
                    continue;

                WorkOrder workOrder = record.getWorkOrder();
                if (workOrder == null)
                    continue;

                ProductionOrder prodOrder = workOrder.getProductionOrder();
                if (prodOrder == null)
                    continue;

                Product product = prodOrder.getProduct();
                if (product == null)
                    continue;

                int productId = product.getId();

                if (!productTotals.ContainsKey(productId))
                {
                    productTotals[productId] = (product.getName(), 0, new List<int>());
                }

                var (name, total, woIds) = productTotals[productId];
                total += record.getQuantityProduced();
                if (!woIds.Contains(workOrder.getId()))
                    woIds.Add(workOrder.getId());
                productTotals[productId] = (name, total, woIds);
            }

            foreach (var kvp in productTotals)
            {
                int productId = kvp.Key;
                var (productName, totalUnits, workOrderIds) = kvp.Value;

                ListViewItem item = new ListViewItem(productName);
                item.SubItems.Add(totalUnits.ToString());
                item.SubItems.Add(workOrderIds.Count.ToString());
                item.Tag = new { productId, totalUnits, workOrderIds };
                lvPeriodTotals.Items.Add(item);
            }
        }

        private void btnApplyToOrder_Click(object sender, EventArgs e)
        {
            if (lvPeriodTotals.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to apply totals", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ListViewItem selectedItem = lvPeriodTotals.SelectedItems[0];
            dynamic tagData = selectedItem.Tag;
            int productId = tagData.productId;
            int totalUnits = tagData.totalUnits;
            List<int> workOrderIds = tagData.workOrderIds;

            int updatedCount = 0;
            foreach (int workOrderId in workOrderIds)
            {
                WorkOrder workOrder = WorkOrder.seekById(workOrderId);
                if (workOrder == null)
                    continue;

                ProductionOrder prodOrder = workOrder.getProductionOrder();
                if (prodOrder == null)
                    continue;

                int currentCompleted = prodOrder.getCompletedQuantity();
                prodOrder.setCompletedQuantity(currentCompleted + totalUnits);
                prodOrder.update();
                updatedCount++;
            }

            MessageBox.Show(
                "Updated " + updatedCount + " order(s) with total " + totalUnits + " units produced",
                "Success",
                MessageBoxButtons.OK);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
