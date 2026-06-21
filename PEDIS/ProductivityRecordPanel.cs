using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ProductivityRecordPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DateTime? filterStartDate = null;
        private DateTime? filterEndDate = null;
        private int? filteredPrisonerId = null;
        private int? filteredWorkOrderId = null;
        private DepartmentManagement currentUser;

        public ProductivityRecordPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        private void ProductivityRecordPanel_Load(object sender, EventArgs e)
        {
            initializeFilters();
            refreshList(DateTime.Today, DateTime.Today, null, null);
        }

        private void initializeFilters()
        {
            dtpFilterStartDate.Value = DateTime.Today;
            dtpFilterEndDate.Value = DateTime.Today;

            cmbFilterPrisoner.Items.Clear();
            cmbFilterPrisoner.Items.Add("All Prisoners");
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                cmbFilterPrisoner.Items.Add(prisoner.getPrisonerNumber() + " - " + prisoner.getFullName());
            }
            cmbFilterPrisoner.SelectedIndex = 0;

            cmbFilterWorkOrder.Items.Clear();
            foreach (WorkOrder workOrder in Program.WorkOrders)
            {
                cmbFilterWorkOrder.Items.Add(workOrder.getWorkOrderNumber());
            }
            cmbFilterWorkOrder.Text = "";
        }

        private void refreshList(DateTime? filterStartDate = null, DateTime? filterEndDate = null, int? filteredPrisonerId = null, int? filteredWorkOrderId = null)
        {
            lvProductivity.Items.Clear();

            foreach (ProductivityRecord productivity in Program.ProductivityRecords)
            {
                // Factory Manager filtering: only show productivity records for the factory where the
                // work was actually produced (a prisoner can work across multiple factories, so their
                // own "home" factory is not a reliable filter key here)
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    if (productivity.getFactory() != currentUser.getFactory())
                        continue;
                }

                bool dateMatch = true;
                if (filterStartDate.HasValue || filterEndDate.HasValue)
                {
                    DateTime recordDate = productivity.getProductivityDate().Date;
                    if (filterStartDate.HasValue && recordDate < filterStartDate.Value.Date)
                        dateMatch = false;
                    if (filterEndDate.HasValue && recordDate > filterEndDate.Value.Date)
                        dateMatch = false;
                }

                bool prisonerMatch = filteredPrisonerId == null || productivity.getPrisonerId() == filteredPrisonerId;
                bool workOrderMatch = filteredWorkOrderId == null || productivity.getWorkOrderId() == filteredWorkOrderId;

                if (!dateMatch || !prisonerMatch || !workOrderMatch)
                    continue;

                ListViewItem item = new ListViewItem(productivity.getId().ToString());
                item.SubItems.Add(productivity.getPrisoner()?.getPrisonerNumber() ?? "N/A");
                item.SubItems.Add(productivity.getPrisoner()?.getFullName() ?? "N/A");
                item.SubItems.Add(productivity.getWorkOrder()?.getWorkOrderNumber() ?? "N/A");
                item.SubItems.Add(productivity.getWorkOrder()?.getProductionOrder()?.getProduct()?.getName() ?? "N/A");
                item.SubItems.Add(productivity.getProductivityDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(productivity.getQuantityProduced().ToString());
                item.SubItems.Add(productivity.getProductivityType().ToString());
                item.Tag = productivity;
                lvProductivity.Items.Add(item);
            }
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            if (dtpFilterStartDate.Value.Date > dtpFilterEndDate.Value.Date)
            {
                MessageBox.Show("Start date must be on or before end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? selectedPrisonerId = null;
            if (cmbFilterPrisoner.SelectedIndex > 0)
            {
                string selectedText = cmbFilterPrisoner.SelectedItem.ToString();
                string[] parts = selectedText.Split(new string[] { " - " }, StringSplitOptions.None);
                if (parts.Length > 0)
                {
                    string prisonerNumber = parts[0];
                    Prisoner foundPrisoner = Prisoner.seekByNumber(prisonerNumber);
                    if (foundPrisoner != null)
                    {
                        selectedPrisonerId = foundPrisoner.getId();
                    }
                }
            }

            int? selectedWorkOrderId = null;
            string workOrderSearch = cmbFilterWorkOrder.Text.Trim();
            if (!string.IsNullOrEmpty(workOrderSearch))
            {
                WorkOrder foundWorkOrder = WorkOrder.seekByWorkOrderNumber(workOrderSearch);
                if (foundWorkOrder == null)
                {
                    foreach (WorkOrder wo in Program.WorkOrders)
                    {
                        if (wo.getWorkOrderNumber().IndexOf(workOrderSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            foundWorkOrder = wo;
                            break;
                        }
                    }
                }

                if (foundWorkOrder == null)
                {
                    MessageBox.Show("No work order found matching \"" + workOrderSearch + "\".", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                selectedWorkOrderId = foundWorkOrder.getId();
            }

            filterStartDate = dtpFilterStartDate.Value;
            filterEndDate = dtpFilterEndDate.Value;
            filteredPrisonerId = selectedPrisonerId;
            filteredWorkOrderId = selectedWorkOrderId;
            refreshList(filterStartDate, filterEndDate, filteredPrisonerId, filteredWorkOrderId);
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            dtpFilterStartDate.Value = DateTime.Today;
            dtpFilterEndDate.Value = DateTime.Today;
            cmbFilterPrisoner.SelectedIndex = 0;
            cmbFilterWorkOrder.Text = "";
            filterStartDate = null;
            filterEndDate = null;
            filteredPrisonerId = null;
            filteredWorkOrderId = null;
            refreshList(null, null, null, null);
        }

        private void btnDailyTotals_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = filterStartDate ?? DateTime.Today;
            DailyProductionTotalDialog dialog = new DailyProductionTotalDialog(selectedDate, currentUser);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId, filteredWorkOrderId);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductivityRecord productivity = (ProductivityRecord)lvProductivity.SelectedItems[0].Tag;
            Prisoner prisoner = productivity.getPrisoner();
            string prisonerInfo = prisoner != null ? prisoner.getFullName() + " (" + prisoner.getPrisonerNumber() + ")" : "N/A";

            WorkOrder workOrder = productivity.getWorkOrder();
            string workOrderInfo = workOrder != null ? workOrder.getWorkOrderNumber() : "N/A";
            string productInfo = workOrder?.getProductionOrder()?.getProduct()?.getName() ?? "N/A";

            string info = "ID: " + productivity.getId() + "\n" +
                         "Prisoner: " + prisonerInfo + "\n" +
                         "Work Order #: " + workOrderInfo + "\n" +
                         "Product: " + productInfo + "\n" +
                         "Date: " + productivity.getProductivityDate().ToString("yyyy-MM-dd") + "\n" +
                         "Units Produced: " + productivity.getQuantityProduced() + "\n" +
                         "Work Hours: " + (productivity.getWorkHours()?.ToString("F2") ?? "N/A") + "\n" +
                         "Productivity Type: " + productivity.getProductivityType();
            MessageBox.Show(info, "Productivity Record Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditProductivityRecordDialog dialog = new AddEditProductivityRecordDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId, filteredWorkOrderId);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductivityRecord productivity = (ProductivityRecord)lvProductivity.SelectedItems[0].Tag;
            AddEditProductivityRecordDialog dialog = new AddEditProductivityRecordDialog();
            dialog.setRecordToEdit(productivity);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId, filteredWorkOrderId);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductivityRecord productivity = (ProductivityRecord)lvProductivity.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete productivity record for: " + (productivity.getPrisoner()?.getFullName() ?? "Unknown") + " on " + productivity.getProductivityDate().ToString("yyyy-MM-dd") + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                productivity.delete();
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId, filteredWorkOrderId);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
