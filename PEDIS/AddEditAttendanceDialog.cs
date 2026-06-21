using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AddEditAttendanceDialog : Form
    {
        private AttendanceRecord attendanceToEdit;
        private bool isEditMode;

        public AddEditAttendanceDialog()
        {
            InitializeComponent();
            attendanceToEdit = null;
            isEditMode = false;
            this.Text = "Add Attendance Record";
        }

        public void setAttendanceToEdit(AttendanceRecord attendance)
        {
            this.attendanceToEdit = attendance;
            this.isEditMode = true;
            this.Text = "Edit Attendance Record";
            if (attendance != null)
            {
                LoadAttendanceData(attendance);
            }
        }

        private void LoadAttendanceData(AttendanceRecord attendance)
        {
            if (attendance == null) return;

            cbPrisoner.SelectedItem = attendance.getPrisoner()?.getPrisonerNumber() + " - " + attendance.getPrisoner()?.getFullName();
            dtpAttendanceDate.Value = attendance.getAttendanceDate();

            if (attendance.getEntryTime().HasValue)
            {
                chkEntryTime.Checked = true;
                dtpEntryTime.Value = new DateTime(2000, 1, 1) + attendance.getEntryTime().Value;
            }

            if (attendance.getExitTime().HasValue)
            {
                chkExitTime.Checked = true;
                dtpExitTime.Value = new DateTime(2000, 1, 1) + attendance.getExitTime().Value;
            }

            cbFactory.SelectedItem = attendance.getFactory();
        }

        private bool ValidateInput()
        {
            if (cbPrisoner.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a prisoner.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!chkEntryTime.Checked && !chkExitTime.Checked)
            {
                MessageBox.Show("Please specify at least Entry Time or Exit Time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (chkEntryTime.Checked && chkExitTime.Checked)
            {
                TimeSpan entryTime = dtpEntryTime.Value.TimeOfDay;
                TimeSpan exitTime = dtpExitTime.Value.TimeOfDay;
                if (exitTime <= entryTime)
                {
                    MessageBox.Show("Exit Time must be after Entry Time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Multiple records per date are allowed, as long as their hours don't overlap.
            {
                string selectedPrisoner = cbPrisoner.SelectedItem.ToString();
                string[] parts = selectedPrisoner.Split(new string[] { " - " }, StringSplitOptions.None);
                Prisoner prisoner = Prisoner.seekByNumber(parts[0]);

                if (prisoner != null)
                {
                    TimeSpan newEntry = chkEntryTime.Checked ? dtpEntryTime.Value.TimeOfDay : TimeSpan.Zero;
                    TimeSpan newExit = chkExitTime.Checked ? dtpExitTime.Value.TimeOfDay : TimeSpan.FromHours(24);

                    foreach (AttendanceRecord record in Program.AttendanceRecords)
                    {
                        if (isEditMode && record == attendanceToEdit)
                            continue;

                        if (record.getPrisonerId() != prisoner.getId() ||
                            record.getAttendanceDate().Date != dtpAttendanceDate.Value.Date)
                            continue;

                        TimeSpan existingEntry = record.getEntryTime() ?? TimeSpan.Zero;
                        TimeSpan existingExit = record.getExitTime() ?? TimeSpan.FromHours(24);

                        if (newEntry < existingExit && existingEntry < newExit)
                        {
                            MessageBox.Show("This prisoner already has an attendance record on this date with overlapping hours.", "Overlap Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
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
                string selectedPrisoner = cbPrisoner.SelectedItem.ToString();
                string[] parts = selectedPrisoner.Split(new string[] { " - " }, StringSplitOptions.None);
                Prisoner prisoner = Prisoner.seekByNumber(parts[0]);

                if (prisoner == null)
                {
                    MessageBox.Show("Selected prisoner not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TimeSpan? entryTime = chkEntryTime.Checked ? (TimeSpan?)dtpEntryTime.Value.TimeOfDay : null;
                TimeSpan? exitTime = chkExitTime.Checked ? (TimeSpan?)dtpExitTime.Value.TimeOfDay : null;

                if (isEditMode)
                {
                    attendanceToEdit.setEntryTime(entryTime);
                    attendanceToEdit.setExitTime(exitTime);
                    attendanceToEdit.update();
                    MessageBox.Show("Attendance record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Find the maximum ID to avoid duplicates
                    int maxId = 0;
                    foreach (AttendanceRecord record in Program.AttendanceRecords)
                    {
                        if (record.getId() > maxId)
                            maxId = record.getId();
                    }
                    int nextId = maxId + 1;

                    AttendanceRecord newRecord = new AttendanceRecord(
                        nextId,
                        prisoner.getId(),
                        dtpAttendanceDate.Value,
                        (Factory)cbFactory.SelectedItem,
                        entryTime,
                        exitTime,
                        true
                    );
                    MessageBox.Show("Attendance record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving attendance record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddEditAttendanceDialog_Load(object sender, EventArgs e)
        {
            cbPrisoner.Items.Clear();
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                cbPrisoner.Items.Add(prisoner.getPrisonerNumber() + " - " + prisoner.getFullName());
            }
            cbPrisoner.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (var factory in Enum.GetValues(typeof(Factory)))
                cbFactory.Items.Add(factory);
            cbFactory.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cbFactory.Items.Count > 0)
                cbFactory.SelectedIndex = 0;

            dtpAttendanceDate.Value = DateTime.Today;
            dtpEntryTime.Value = new DateTime(2000, 1, 1, 8, 0, 0);
            dtpExitTime.Value = new DateTime(2000, 1, 1, 16, 0, 0);
        }

        private void chkEntryTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpEntryTime.Enabled = chkEntryTime.Checked;
        }

        private void chkExitTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpExitTime.Enabled = chkExitTime.Checked;
        }
    }
}
