using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AddEditPrisonerDialog : Form
    {
        private Prisoner prisonerToEdit;
        private bool isEditMode;
        private DepartmentManagement currentUser;

        public AddEditPrisonerDialog()
        {
            InitializeComponent();
            prisonerToEdit = null;
            isEditMode = false;
            this.Text = "Add Prisoner";
            // Set default hourly rate when adding a new prisoner
            if (!isEditMode)
            {
                txtHourlyRate.Text = "14.7";
            }
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        // Statuses in which a prisoner has not yet been placed with a factory --
        // employment start date only applies once a prisoner moves past this stage.
        private static bool isPendingPlacementStatus(PrisonerActivityStatus status)
        {
            return status == PrisonerActivityStatus.PendingPrisonAdministrationApproval ||
                   status == PrisonerActivityStatus.PendingDepartmentManagerApproval ||
                   status == PrisonerActivityStatus.PendingPlacement;
        }

        public void setPrisonerToEdit(Prisoner prisoner)
        {
            this.prisonerToEdit = prisoner;
            this.isEditMode = true;
            this.Text = "Edit Prisoner";
            // Combo box items are populated in the Load handler, which hasn't fired yet at this
            // point (ShowDialog() hasn't been called) -- defer applying record values until then,
            // otherwise SelectedItem assignments below would have nothing to match against.
        }

        private void LoadPrisonerData(Prisoner prisoner)
        {
            if (prisoner == null) return;

            txtPrisonerNumber.Text = prisoner.getPrisonerNumber();
            txtFullName.Text = prisoner.getFullName();
            cbDepartment.Text = prisoner.getDepartment().ToString();

            if (prisoner.getReleaseDate().HasValue)
                dtpReleaseDate.Value = prisoner.getReleaseDate().Value;

            if (prisoner.getSafetyTrainingValidity().HasValue)
            {
                chkSafetyTrainingSet.Checked = true;
                dtpSafetyTrainingExpiration.Value = prisoner.getSafetyTrainingValidity().Value;
            }
            else
            {
                chkSafetyTrainingSet.Checked = false;
            }

            // Set factory (or "Unassigned" if null)
            if (prisoner.getFactory().HasValue)
            {
                foreach (object item in cbFactory.Items)
                {
                    if (item.ToString() == prisoner.getFactory().ToString())
                    {
                        cbFactory.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                cbFactory.SelectedIndex = 0; // Select "Unassigned"
            }

            cbActivityStatus.SelectedItem = prisoner.getActivityStatus();

            if (prisoner.getRole().HasValue)
                cbRole.SelectedItem = prisoner.getRole().Value;
            else
                cbRole.SelectedIndex = 0;

            txtHourlyRate.Text = prisoner.getHourlyRate().ToString();
            chkQualified.Checked = prisoner.getQualified();

            // Disable prisoner number editing in edit mode
            txtPrisonerNumber.ReadOnly = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPrisonerNumber.Text))
            {
                MessageBox.Show("Please enter a prisoner number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter a full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtFullName.Text, @"^[A-Za-z\s]+$"))
            {
                MessageBox.Show("שם האסיר יכול להכיל אותיות באנגלית בלבד (ניתן להשתמש ברווחים). אין להשתמש בספרות או בסימנים.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(cbDepartment.Text, out int department) || department <= 0)
            {
                MessageBox.Show("Please enter a valid department (positive number). This is the inmate's home wing/department, independent of the employment department.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtHourlyRate.Text, out decimal hourlyRate) || hourlyRate < 0)
            {
                MessageBox.Show("Please enter a valid hourly rate (non-negative number).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbActivityStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select an activity status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check for duplicate prisoner number (only in Add mode)
            if (!isEditMode)
            {
                foreach (Prisoner p in Program.Prisoners)
                {
                    if (p.getPrisonerNumber() == txtPrisonerNumber.Text)
                    {
                        MessageBox.Show("Prisoner number already exists. Please enter a unique number.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
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
                if (isEditMode)
                {
                    // Edit existing prisoner
                    // Handle factory selection (Unassigned = null)
                    Factory? selectedFactory = null;
                    if (cbFactory.SelectedItem != null && cbFactory.SelectedItem.ToString() != "(Unassigned)")
                    {
                        selectedFactory = (Factory)cbFactory.SelectedItem;
                    }

                    // Handle role selection (None = null)
                    PrisonerRole? selectedRole = null;
                    if (cbRole.SelectedItem != null && cbRole.SelectedItem.ToString() != "(None)")
                    {
                        selectedRole = (PrisonerRole)cbRole.SelectedItem;
                    }

                    prisonerToEdit.setFullName(txtFullName.Text);
                    prisonerToEdit.setDepartment(int.Parse(cbDepartment.Text));
                    prisonerToEdit.setFactory(selectedFactory);
                    prisonerToEdit.setActivityStatus((PrisonerActivityStatus)cbActivityStatus.SelectedItem);
                    prisonerToEdit.setRole(selectedRole);
                    prisonerToEdit.setHourlyRate(decimal.Parse(txtHourlyRate.Text));
                    prisonerToEdit.setQualified(chkQualified.Checked);
                    prisonerToEdit.setReleaseDate(dtpReleaseDate.Value.Date);
                    prisonerToEdit.setSafetyTrainingValidity(chkSafetyTrainingSet.Checked ? dtpSafetyTrainingExpiration.Value.Date : (DateTime?)null);

                    // Employment start date is system-controlled (R5): backfill it the moment a
                    // factory-assigned, non-pending prisoner is found without one, but never
                    // overwrite an existing value once set.
                    if (selectedFactory.HasValue && !isPendingPlacementStatus(prisonerToEdit.getActivityStatus()) && !prisonerToEdit.getWorkStartDate().HasValue)
                        prisonerToEdit.setWorkStartDate(DateTime.Today);

                    prisonerToEdit.update();

                    MessageBox.Show("Prisoner updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Add new prisoner - find max ID to avoid duplicates
                    int maxId = 0;
                    foreach (Prisoner p in Program.Prisoners)
                    {
                        if (p.getId() > maxId)
                            maxId = p.getId();
                    }
                    int nextId = maxId + 1;

                    // Handle factory selection (Unassigned = null)
                    Factory? selectedFactory = null;
                    if (cbFactory.SelectedItem != null && cbFactory.SelectedItem.ToString() != "(Unassigned)")
                    {
                        selectedFactory = (Factory)cbFactory.SelectedItem;
                    }

                    // Handle role selection (None = null)
                    PrisonerRole? selectedRole = null;
                    if (cbRole.SelectedItem != null && cbRole.SelectedItem.ToString() != "(None)")
                    {
                        selectedRole = (PrisonerRole)cbRole.SelectedItem;
                    }

                    PrisonerActivityStatus selectedStatus = (PrisonerActivityStatus)cbActivityStatus.SelectedItem;
                    DateTime releaseDateValue = dtpReleaseDate.Value.Date;
                    DateTime? workStart;

                    if (releaseDateValue < DateTime.Today)
                    {
                        // Historical entry: this prisoner was already released before being
                        // added to the system. Refuse to backfill records that are too stale
                        // to be meaningfully tracked.
                        DateTime sevenYearsAgo = DateTime.Today.AddYears(-7);
                        if (releaseDateValue < sevenYearsAgo)
                        {
                            MessageBox.Show("Release date is too old: a prisoner released more than 7 years ago cannot be added.", "Date Too Old", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DateTime manualWorkStart = dtpWorkStartDate.Value.Date;
                        if (manualWorkStart >= releaseDateValue)
                            throw new Exception("Work start date must be earlier than the release date for a prisoner who has already been released.");

                        workStart = manualWorkStart;
                        selectedStatus = PrisonerActivityStatus.Archived;
                    }
                    else
                    {
                        // Employment start date is system-controlled (R5): auto-set to the date the
                        // prisoner is added to the system, but only once they're actually placed with
                        // a factory (not while still pending placement/approval).
                        workStart = (selectedFactory.HasValue && !isPendingPlacementStatus(selectedStatus))
                            ? DateTime.Today
                            : (DateTime?)null;

                        if (workStart.HasValue && releaseDateValue <= workStart.Value)
                            throw new Exception("Release date must be later than the work start date.");
                    }

                    DateTime? safetyTrain = chkSafetyTrainingSet.Checked ? dtpSafetyTrainingExpiration.Value.Date : (DateTime?)null;

                    Prisoner newPrisoner = new Prisoner(
                        nextId,
                        txtPrisonerNumber.Text,
                        txtFullName.Text,
                        selectedFactory,
                        int.Parse(cbDepartment.Text),
                        selectedStatus,
                        selectedRole,
                        safetyTrain,
                        workStart,
                        releaseDateValue,
                        chkQualified.Checked,
                        false,
                        decimal.Parse(txtHourlyRate.Text),
                        true
                    );

                    MessageBox.Show("Prisoner added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving prisoner: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkSafetyTrainingSet_CheckedChanged(object sender, EventArgs e)
        {
            dtpSafetyTrainingExpiration.Enabled = chkSafetyTrainingSet.Checked;
        }

        // Work start date is normally system-controlled (R5) and stays hidden. It's only
        // revealed -- in Add mode -- when Release Date is pushed into the past, since that's
        // the one case where the auto-computed "today" start date would land after release.
        private void dtpReleaseDate_ValueChanged(object sender, EventArgs e)
        {
            if (isEditMode || cbActivityStatus.Items.Count == 0)
                return;

            bool releasedInPast = dtpReleaseDate.Value.Date < DateTime.Today;
            lblWorkStartDate.Visible = releasedInPast;
            dtpWorkStartDate.Visible = releasedInPast;
            dtpWorkStartDate.Enabled = releasedInPast;

            if (releasedInPast)
            {
                DateTime maxStart = dtpReleaseDate.Value.Date.AddDays(-1);
                dtpWorkStartDate.MaxDate = maxStart;
                if (dtpWorkStartDate.Value.Date > maxStart)
                    dtpWorkStartDate.Value = maxStart;

                cbActivityStatus.SelectedItem = PrisonerActivityStatus.Archived;
                cbActivityStatus.Enabled = false;
            }
            else
            {
                dtpWorkStartDate.MaxDate = DateTimePicker.MaximumDateTime;
                cbActivityStatus.Enabled = true;
            }
        }

        private void AddEditPrisonerDialog_Load(object sender, EventArgs e)
        {
            // Factory Managers manage exactly one factory -- every prisoner they add or edit
            // must stay in that factory, so there's nothing else to offer or choose.
            bool isFactoryManager = currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager;

            // Populate Factory ComboBox with "Unassigned" option
            cbFactory.Items.Clear();
            if (!isFactoryManager)
                cbFactory.Items.Add("(Unassigned)");
            foreach (var factory in Enum.GetValues(typeof(Factory)))
            {
                if (isFactoryManager && (Factory)factory != currentUser.getFactory())
                    continue;

                cbFactory.Items.Add(factory);
            }
            cbFactory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFactory.Enabled = !isFactoryManager;

            // Populate ActivityStatus ComboBox
            cbActivityStatus.Items.Clear();
            foreach (var status in Enum.GetValues(typeof(PrisonerActivityStatus)))
                cbActivityStatus.Items.Add(status);
            cbActivityStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // Populate Role ComboBox with empty option
            cbRole.Items.Clear();
            cbRole.Items.Add("(None)");
            foreach (var role in Enum.GetValues(typeof(PrisonerRole)))
                cbRole.Items.Add(role);
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            if (isEditMode && prisonerToEdit != null)
            {
                LoadPrisonerData(prisonerToEdit);
            }
            else
            {
                cbFactory.SelectedIndex = 0; // Unassigned for most roles; Factory Managers only have their own factory in the list
                cbRole.SelectedIndex = 0; // Default to None for new prisoners
                dtpReleaseDate_ValueChanged(this, EventArgs.Empty); // sync Work Start Date visibility to the default Release Date
            }
        }
    }
}
