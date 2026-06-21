using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ProductionOrderPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public ProductionOrderPanel()
        {
            InitializeComponent();
        }

        private void ProductionOrderPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvOrders.Items.Clear();
            foreach (ProductionOrder order in Program.ProductionOrders)
            {
                ListViewItem item = new ListViewItem(order.getId().ToString());
                item.SubItems.Add(order.getOrderNumber());
                item.SubItems.Add(order.getOrderStatus().ToString());
                item.SubItems.Add(order.getSubmissionDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(order.getDeliveryDeadline().ToString("yyyy-MM-dd"));
                item.Tag = order;
                lvOrders.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductionOrder order = (ProductionOrder)lvOrders.SelectedItems[0].Tag;
            string info = "ID: " + order.getId() + "\n" +
                         "Order #: " + order.getOrderNumber() + "\n" +
                         "Status: " + order.getOrderStatus() + "\n" +
                         "Order Date: " + order.getSubmissionDate().ToString("yyyy-MM-dd") + "\n" +
                         "Due Date: " + order.getDeliveryDeadline().ToString("yyyy-MM-dd");
            MessageBox.Show(info, "Production Order Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Order Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Order Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductionOrder order = (ProductionOrder)lvOrders.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete order: " + order.getOrderNumber() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                order.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
