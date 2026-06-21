using System;
using System.Linq;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ProductionOrderPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DepartmentManagement currentUser;

        public ProductionOrderPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
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
                // Factory Manager filtering: only show orders assigned to their factory
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    var workOrders = order.getWorkOrders();
                    if (workOrders == null || workOrders.Count == 0)
                        continue;

                    // Check if any work order belongs to this factory manager's factory
                    var hasFactoryMatch = workOrders.Any(wo => wo.getFactory() == currentUser.getFactory());
                    if (!hasFactoryMatch)
                        continue;
                }

                ListViewItem item = new ListViewItem(order.getId().ToString());
                item.SubItems.Add(order.getOrderNumber());

                // Add Factory from first work order
                var firstWorkOrder = order.getWorkOrders()?.FirstOrDefault();
                string factory = firstWorkOrder != null ? firstWorkOrder.getFactory().ToString() : "";
                item.SubItems.Add(factory);

                // Add Customer Company
                string company = order.getCustomerCompany() != null ? order.getCustomerCompany().getName() : "N/A";
                item.SubItems.Add(company);

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
            var firstWorkOrder = order.getWorkOrders()?.FirstOrDefault();
            string factory = firstWorkOrder != null ? firstWorkOrder.getFactory().ToString() : "";
            string company = order.getCustomerCompany() != null ? order.getCustomerCompany().getName() : "N/A";

            string info = "ID: " + order.getId() + "\n" +
                         "Order #: " + order.getOrderNumber() + "\n" +
                         "Factory: " + factory + "\n" +
                         "Customer Company: " + company + "\n" +
                         "Status: " + order.getOrderStatus() + "\n" +
                         "Order Date: " + order.getSubmissionDate().ToString("yyyy-MM-dd") + "\n" +
                         "Due Date: " + order.getDeliveryDeadline().ToString("yyyy-MM-dd");
            MessageBox.Show(info, "Production Order Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Production Order - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Production Order - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
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
