using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AddEditPrisonerDialog : Form
    {
        private Prisoner prisonerToEdit;
        private bool isEditMode;

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

        public void setPrisonerToEdit(Prisoner prisoner)
        {
            this.prisonerToEdit = prisoner;
            this.isEditMode = true;
            this.Text = "Edit Prisoner";
            if (prisoner != null)
            {
                LoadPrisonerData(prisoner);
            }
        }

        private void LoadPrisonerData(Prisoner prisoner)
        {
            if (prisoner == null) return;

            txtPrisonerNumber.Text = prisoner.getPrisonerNumber();
            txtFullName.Text = prisoner.getFullName();

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
                    prisonerToEdit.setFactory(selectedFactory);
                    prisonerToEdit.setActivityStatus((PrisonerActivityStatus)cbActivityStatus.SelectedItem);
                    prisonerToEdit.setRole(selectedRole);
                    prisonerToEdit.setHourlyRate(decimal.Parse(txtHourlyRate.Text));
                    prisonerToEdit.setQualified(chkQualified.Checked);
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

                    Prisoner newPrisoner = new Prisoner(
                        nextId,
                        txtPrisonerNumber.Text,
                        txtFullName.Text,
                        selectedFactory,
                        null,
                        (PrisonerActivityStatus)cbActivityStatus.SelectedItem,
                        selectedRole,
                        null,
                        null,
                        null,
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

        private void AddEditPrisonerDialog_Load(object sender, EventArgs e)
        {
            // Populate Factory ComboBox with "Unassigned" option
            cbFactory.Items.Add("(Unassigned)");
            foreach (var factory in Enum.GetValues(typeof(Factory)))
                cbFactory.Items.Add(factory);
            cbFactory.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!isEditMode)
                cbFactory.SelectedIndex = 0; // Default to Unassigned for new prisoners

            // Populate ActivityStatus ComboBox
            foreach (var status in Enum.GetValues(typeof(PrisonerActivityStatus)))
                cbActivityStatus.Items.Add(status);
            cbActivityStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // Populate Role ComboBox with empty option
            cbRole.Items.Add("(None)");
            foreach (var role in Enum.GetValues(typeof(PrisonerRole)))
                cbRole.Items.Add(role);
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            if (!isEditMode)
                cbRole.SelectedIndex = 0; // Default to None for new prisoners
        }
    }
}
