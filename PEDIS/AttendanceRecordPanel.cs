using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AttendanceRecordPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DateTime? filterDate = null;
        private int? filteredPrisonerId = null;

        public AttendanceRecordPanel()
        {
            InitializeComponent();
        }

        private void AttendanceRecordPanel_Load(object sender, EventArgs e)
        {
            initializeFilters();
            refreshList(DateTime.Today, null);
        }

        private void initializeFilters()
        {
            dtpFilterDate.Value = DateTime.Today;

            cmbFilterPrisoner.Items.Clear();
            cmbFilterPrisoner.Items.Add("All Prisoners");
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                cmbFilterPrisoner.Items.Add(prisoner.getPrisonerNumber() + " - " + prisoner.getFullName());
            }
            cmbFilterPrisoner.SelectedIndex = 0;
        }

        private void refreshList(DateTime? filterDate = null, int? filteredPrisonerId = null)
        {
            lvAttendance.Items.Clear();

            foreach (AttendanceRecord attendance in Program.AttendanceRecords)
            {
                bool dateMatch = filterDate == null || attendance.getAttendanceDate().Date == filterDate.Value.Date;
                bool prisonerMatch = filteredPrisonerId == null || attendance.getPrisonerId() == filteredPrisonerId;

                if (!dateMatch || !prisonerMatch)
                    continue;

                ListViewItem item = new ListViewItem(attendance.getId().ToString());
                item.SubItems.Add(attendance.getPrisoner()?.getPrisonerNumber() ?? "N/A");
                item.SubItems.Add(attendance.getPrisoner()?.getFullName() ?? "N/A");
                item.SubItems.Add(attendance.getAttendanceDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(attendance.getEntryTime()?.ToString(@"hh\:mm") ?? "");
                item.SubItems.Add(attendance.getExitTime()?.ToString(@"hh\:mm") ?? "");

                decimal hoursWorked = 0;
                if (attendance.getEntryTime().HasValue && attendance.getExitTime().HasValue)
                {
                    hoursWorked = (decimal)(attendance.getExitTime().Value.TotalSeconds - attendance.getEntryTime().Value.TotalSeconds) / 3600;
                }
                string hoursDisplay = hoursWorked > 0 ? hoursWorked.ToString("F2") : "In Progress";
                item.SubItems.Add(hoursDisplay);

                item.SubItems.Add(attendance.getFactory().ToString());
                item.Tag = attendance;
                lvAttendance.Items.Add(item);
            }
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpFilterDate.Value;
            int? selectedPrisonerId = null;

            if (cmbFilterPrisoner.SelectedIndex > 0)
            {
                string selectedText = cmbFilterPrisoner.SelectedItem.ToString();
                string[] parts = selectedText.Split(new string[] { " - " }, StringSplitOptions.None);
                if (parts.Length > 0 && int.TryParse(parts[0], out int prisonerId))
                {
                    selectedPrisonerId = prisonerId;
                }
            }

            filterDate = selectedDate;
            filteredPrisonerId = selectedPrisonerId;
            refreshList(filterDate, filteredPrisonerId);
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            dtpFilterDate.Value = DateTime.Today;
            cmbFilterPrisoner.SelectedIndex = 0;
            filterDate = null;
            filteredPrisonerId = null;
            refreshList(DateTime.Today, null);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            AttendanceRecord attendance = (AttendanceRecord)lvAttendance.SelectedItems[0].Tag;
            Prisoner prisoner = attendance.getPrisoner();
            string prisonerInfo = prisoner != null ? prisoner.getFullName() + " (" + prisoner.getPrisonerNumber() + ")" : "N/A";

            decimal hoursWorked = 0;
            if (attendance.getEntryTime().HasValue && attendance.getExitTime().HasValue)
            {
                hoursWorked = (decimal)(attendance.getExitTime().Value.TotalSeconds - attendance.getEntryTime().Value.TotalSeconds) / 3600;
            }

            string info = "ID: " + attendance.getId() + "\n" +
                         "Prisoner: " + prisonerInfo + "\n" +
                         "Date: " + attendance.getAttendanceDate().ToString("yyyy-MM-dd") + "\n" +
                         "Entry Time: " + (attendance.getEntryTime()?.ToString(@"hh\:mm\:ss") ?? "Not recorded") + "\n" +
                         "Exit Time: " + (attendance.getExitTime()?.ToString(@"hh\:mm\:ss") ?? "Not recorded") + "\n" +
                         "Hours Worked: " + (hoursWorked > 0 ? hoursWorked.ToString("F2") : "In Progress") + "\n" +
                         "Factory: " + attendance.getFactory();
            MessageBox.Show(info, "Attendance Record Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Attendance Record - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Attendance Record - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            AttendanceRecord attendance = (AttendanceRecord)lvAttendance.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete attendance record for: " + (attendance.getPrisoner()?.getFullName() ?? "Unknown") + " on " + attendance.getAttendanceDate().ToString("yyyy-MM-dd") + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                attendance.delete();
                refreshList(filterDate, filteredPrisonerId);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
