using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class WorkOrderPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DepartmentManagement currentUser;

        public WorkOrderPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        private void WorkOrderPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvWorkOrders.Items.Clear();
            foreach (WorkOrder workOrder in Program.WorkOrders)
            {
                // Factory Manager filtering: only show work orders for their factory
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    if (workOrder.getProductionOrder() == null || workOrder.getProductionOrder().getFactory() != currentUser.getFactory())
                        continue;
                }

                Product product = workOrder.getProductionOrder()?.getProduct();

                ListViewItem item = new ListViewItem(workOrder.getId().ToString());
                item.SubItems.Add(workOrder.getWorkOrderNumber());
                item.SubItems.Add(workOrder.getProductionOrder()?.getOrderNumber() ?? "N/A");
                item.SubItems.Add(product?.getName() ?? "N/A");
                item.SubItems.Add(workOrder.getStatus().ToString());
                item.SubItems.Add(workOrder.getStartDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(workOrder.getDeadline().ToString("yyyy-MM-dd"));
                item.SubItems.Add(product?.getPackagingInstructions() ?? "");
                item.Tag = workOrder;
                lvWorkOrders.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvWorkOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a work order to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            WorkOrder workOrder = (WorkOrder)lvWorkOrders.SelectedItems[0].Tag;
            Product product = workOrder.getProductionOrder()?.getProduct();
            string info = "ID: " + workOrder.getId() + "\n" +
                         "Work Order #: " + workOrder.getWorkOrderNumber() + "\n" +
                         "Parent Order: " + (workOrder.getProductionOrder()?.getOrderNumber() ?? "N/A") + "\n" +
                         "Product: " + (product?.getName() ?? "N/A") + "\n" +
                         "Status: " + workOrder.getStatus() + "\n" +
                         "Start Date: " + workOrder.getStartDate().ToString("yyyy-MM-dd") + "\n" +
                         "End Date: " + workOrder.getDeadline().ToString("yyyy-MM-dd") + "\n" +
                         "Work Instructions: " + (product?.getPackagingInstructions() ?? "N/A");
            MessageBox.Show(info, "Work Order Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Work Order - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvWorkOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a work order to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Work Order - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvWorkOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a work order to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            WorkOrder workOrder = (WorkOrder)lvWorkOrders.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete work order: " + workOrder.getWorkOrderNumber() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                workOrder.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
