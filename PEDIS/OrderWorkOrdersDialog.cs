using System;
using System.Windows.Forms;

namespace PEDIS
{
    public class OrderWorkOrdersDialog : Form
    {
        private ProductionOrder order;
        private Label lblTitle;
        private ListView lvWorkOrders;
        private Button btnClose;

        public OrderWorkOrdersDialog(ProductionOrder order)
        {
            this.order = order;
            InitializeComponent();
            LoadWorkOrders();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lvWorkOrders = new ListView();
            this.btnClose = new Button();
            this.SuspendLayout();

            this.Text = "Work Orders";
            this.Size = new System.Drawing.Size(950, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(900, 30);
            this.lblTitle.Text = "Work Orders for Order " + order.getOrderNumber();
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lvWorkOrders.FullRowSelect = true;
            this.lvWorkOrders.GridLines = true;
            this.lvWorkOrders.Location = new System.Drawing.Point(20, 50);
            this.lvWorkOrders.Size = new System.Drawing.Size(900, 320);
            this.lvWorkOrders.UseCompatibleStateImageBehavior = false;
            this.lvWorkOrders.View = View.Details;
            this.lvWorkOrders.BackColor = System.Drawing.Color.White;
            this.lvWorkOrders.BorderStyle = BorderStyle.Fixed3D;

            this.lvWorkOrders.Columns.Add("Work Order #", 120);
            this.lvWorkOrders.Columns.Add("Product", 150);
            this.lvWorkOrders.Columns.Add("Requested Qty", 110);
            this.lvWorkOrders.Columns.Add("Produced So Far", 120);
            this.lvWorkOrders.Columns.Add("Status", 100);
            this.lvWorkOrders.Columns.Add("Work Instructions", 300);

            this.btnClose.Location = new System.Drawing.Point(795, 380);
            this.btnClose.Size = new System.Drawing.Size(125, 35);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.DialogResult = DialogResult.OK;

            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvWorkOrders);
            this.Controls.Add(this.lblTitle);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadWorkOrders()
        {
            lvWorkOrders.Items.Clear();
            string productName = order.getProduct() != null ? order.getProduct().getName() : "N/A";
            string workInstructions = order.getProduct() != null ? (order.getProduct().getPackagingInstructions() ?? "N/A") : "N/A";

            foreach (WorkOrder workOrder in Program.WorkOrders)
            {
                if (workOrder.getProductionOrderId() != order.getId())
                    continue;

                int producedSoFar = 0;
                foreach (ProductivityRecord record in Program.ProductivityRecords)
                {
                    if (record.getWorkOrderId() == workOrder.getId())
                        producedSoFar += record.getQuantityProduced();
                }

                ListViewItem item = new ListViewItem(workOrder.getWorkOrderNumber());
                item.SubItems.Add(productName);
                item.SubItems.Add(workOrder.getRequiredQuantity().ToString());
                item.SubItems.Add(producedSoFar.ToString());
                item.SubItems.Add(workOrder.getStatus().ToString());
                item.SubItems.Add(workInstructions);
                item.Tag = workOrder;
                lvWorkOrders.Items.Add(item);
            }

            if (lvWorkOrders.Items.Count == 0)
            {
                lvWorkOrders.Items.Add(new ListViewItem("No work orders found for this order"));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
