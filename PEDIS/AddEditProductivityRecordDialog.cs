using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AddEditProductivityRecordDialog : Form
    {
        private ProductivityRecord recordToEdit;
        private bool isEditMode;

        public AddEditProductivityRecordDialog()
        {
            InitializeComponent();
            recordToEdit = null;
            isEditMode = false;
            this.Text = "Add Productivity Record";
        }

        public void setRecordToEdit(ProductivityRecord record)
        {
            this.recordToEdit = record;
            this.isEditMode = true;
            this.Text = "Edit Productivity Record";
            // Combo boxes are populated in the Load handler, which fires after this call
            // (ShowDialog hasn't run yet) -- defer applying record values until then.
        }

        private void LoadRecordData(ProductivityRecord record)
        {
            if (record == null) return;

            Prisoner prisoner = record.getPrisoner();
            if (prisoner != null)
                cbPrisoner.SelectedItem = prisoner.getPrisonerNumber() + " - " + prisoner.getFullName();

            WorkOrder workOrder = record.getWorkOrder();
            if (workOrder != null)
            {
                string productName = workOrder.getProductionOrder()?.getProduct()?.getName() ?? "N/A";
                cbWorkOrder.SelectedItem = workOrder.getWorkOrderNumber() + " - " + productName;
            }

            dtpProductivityDate.Value = record.getProductivityDate();
            nudQuantityProduced.Value = record.getQuantityProduced();

            if (record.getWorkHours().HasValue)
            {
                chkWorkHours.Checked = true;
                nudWorkHours.Value = record.getWorkHours().Value;
            }
            else
            {
                chkWorkHours.Checked = false;
            }

            cbProductivityType.SelectedItem = record.getProductivityType();
        }

        private bool ValidateInput()
        {
            if (cbPrisoner.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a prisoner.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbWorkOrder.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a work order.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbProductivityType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a productivity type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudQuantityProduced.Value < 0)
            {
                MessageBox.Show("Quantity produced cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string selectedPrisoner = cbPrisoner.SelectedItem.ToString();
            string[] prisonerParts = selectedPrisoner.Split(new string[] { " - " }, StringSplitOptions.None);
            Prisoner prisoner = Prisoner.seekByNumber(prisonerParts[0]);

            string selectedWorkOrder = cbWorkOrder.SelectedItem.ToString();
            string[] workOrderParts = selectedWorkOrder.Split(new string[] { " - " }, StringSplitOptions.None);
            WorkOrder workOrder = WorkOrder.seekByWorkOrderNumber(workOrderParts[0]);

            if (prisoner == null)
            {
                MessageBox.Show("Selected prisoner not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (workOrder == null)
            {
                MessageBox.Show("Selected work order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // DB enforces UNIQUE(prisoner_id, work_order_id, productivity_date) -- check here
            // first so the user gets a clear message instead of a raw SQL constraint error.
            foreach (ProductivityRecord record in Program.ProductivityRecords)
            {
                if (isEditMode && record == recordToEdit)
                    continue;

                if (record.getPrisonerId() == prisoner.getId() &&
                    record.getWorkOrderId() == workOrder.getId() &&
                    record.getProductivityDate().Date == dtpProductivityDate.Value.Date)
                {
                    MessageBox.Show("This prisoner already has a productivity record for this work order on this date.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int quantityProduced = (int)nudQuantityProduced.Value;
                decimal? workHours = chkWorkHours.Checked ? (decimal?)nudWorkHours.Value : null;
                ProductivityType type = (ProductivityType)cbProductivityType.SelectedItem;

                if (isEditMode)
                {
                    recordToEdit.setQuantityProduced(quantityProduced);
                    recordToEdit.setWorkHours(workHours);
                    recordToEdit.setProductivityType(type);
                    recordToEdit.update();
                    MessageBox.Show("Productivity record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string selectedPrisoner = cbPrisoner.SelectedItem.ToString();
                    string[] prisonerParts = selectedPrisoner.Split(new string[] { " - " }, StringSplitOptions.None);
                    Prisoner prisoner = Prisoner.seekByNumber(prisonerParts[0]);

                    string selectedWorkOrder = cbWorkOrder.SelectedItem.ToString();
                    string[] workOrderParts = selectedWorkOrder.Split(new string[] { " - " }, StringSplitOptions.None);
                    WorkOrder workOrder = WorkOrder.seekByWorkOrderNumber(workOrderParts[0]);

                    int maxId = 0;
                    foreach (ProductivityRecord record in Program.ProductivityRecords)
                    {
                        if (record.getId() > maxId)
                            maxId = record.getId();
                    }
                    int nextId = maxId + 1;

                    ProductivityRecord newRecord = new ProductivityRecord(
                        nextId,
                        prisoner.getId(),
                        workOrder.getId(),
                        dtpProductivityDate.Value,
                        quantityProduced,
                        workHours,
                        type,
                        true
                    );
                    MessageBox.Show("Productivity record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving productivity record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddEditProductivityRecordDialog_Load(object sender, EventArgs e)
        {
            cbPrisoner.Items.Clear();
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                cbPrisoner.Items.Add(prisoner.getPrisonerNumber() + " - " + prisoner.getFullName());
            }
            cbPrisoner.DropDownStyle = ComboBoxStyle.DropDownList;

            cbWorkOrder.Items.Clear();
            foreach (WorkOrder workOrder in Program.WorkOrders)
            {
                string productName = workOrder.getProductionOrder()?.getProduct()?.getName() ?? "N/A";
                cbWorkOrder.Items.Add(workOrder.getWorkOrderNumber() + " - " + productName);
            }
            cbWorkOrder.DropDownStyle = ComboBoxStyle.DropDownList;

            cbProductivityType.Items.Clear();
            foreach (var type in Enum.GetValues(typeof(ProductivityType)))
                cbProductivityType.Items.Add(type);
            cbProductivityType.DropDownStyle = ComboBoxStyle.DropDownList;

            if (isEditMode && recordToEdit != null)
            {
                LoadRecordData(recordToEdit);

                // ProductivityRecord has no setters for prisoner/work order/date, and the DB's
                // unique key is keyed on this triple -- lock them instead of silently dropping edits.
                cbPrisoner.Enabled = false;
                cbWorkOrder.Enabled = false;
                dtpProductivityDate.Enabled = false;
            }
            else
            {
                dtpProductivityDate.Value = DateTime.Today;
                nudQuantityProduced.Value = 0;
                chkWorkHours.Checked = false;
                nudWorkHours.Value = 0;
                if (cbProductivityType.Items.Count > 0)
                    cbProductivityType.SelectedIndex = 0;
            }
        }

        private void chkWorkHours_CheckedChanged(object sender, EventArgs e)
        {
            nudWorkHours.Enabled = chkWorkHours.Checked;
        }
    }
}
