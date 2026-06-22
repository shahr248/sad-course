using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AttendanceRecordPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DateTime? filterStartDate = null;
        private DateTime? filterEndDate = null;
        private int? filteredPrisonerId = null;
        private DepartmentManagement currentUser;

        public AttendanceRecordPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        private void AttendanceRecordPanel_Load(object sender, EventArgs e)
        {
            initializeFilters();
            refreshList(DateTime.Today, DateTime.Today, null);
        }

        private void initializeFilters()
        {
            dtpFilterStartDate.Value = DateTime.Today;
            dtpFilterEndDate.Value = DateTime.Today;

            bool isFactoryManager = currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager;

            cmbFilterPrisoner.Items.Clear();
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                if (isFactoryManager && prisoner.getFactory() != currentUser.getFactory())
                    continue;

                cmbFilterPrisoner.Items.Add(prisoner.getPrisonerNumber() + " - " + prisoner.getFullName());
            }
            cmbFilterPrisoner.Text = "";
        }

        private void refreshList(DateTime? filterStartDate = null, DateTime? filterEndDate = null, int? filteredPrisonerId = null)
        {
            lvAttendance.Items.Clear();

            foreach (AttendanceRecord attendance in Program.AttendanceRecords)
            {
                // Factory Manager filtering: only show attendance records for their factory
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    if (attendance.getFactory() != currentUser.getFactory())
                        continue;
                }

                bool dateMatch = true;
                if (filterStartDate.HasValue || filterEndDate.HasValue)
                {
                    DateTime recordDate = attendance.getAttendanceDate().Date;
                    if (filterStartDate.HasValue && recordDate < filterStartDate.Value.Date)
                        dateMatch = false;
                    if (filterEndDate.HasValue && recordDate > filterEndDate.Value.Date)
                        dateMatch = false;
                }

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
            if (dtpFilterStartDate.Value.Date > dtpFilterEndDate.Value.Date)
            {
                MessageBox.Show("Start date must be on or before end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? selectedPrisonerId = null;
            string searchText = cmbFilterPrisoner.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                string prisonerNumber = searchText;
                int dashIndex = searchText.IndexOf(" - ");
                if (dashIndex >= 0)
                    prisonerNumber = searchText.Substring(0, dashIndex);

                Prisoner foundPrisoner = Prisoner.seekByNumber(prisonerNumber);
                if (foundPrisoner == null)
                {
                    foreach (Prisoner p in Program.Prisoners)
                    {
                        if (p.getPrisonerNumber().StartsWith(prisonerNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            foundPrisoner = p;
                            break;
                        }
                    }
                }

                if (foundPrisoner == null)
                {
                    MessageBox.Show("No prisoner found matching \"" + searchText + "\".", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                selectedPrisonerId = foundPrisoner.getId();
            }

            filterStartDate = dtpFilterStartDate.Value;
            filterEndDate = dtpFilterEndDate.Value;
            filteredPrisonerId = selectedPrisonerId;
            refreshList(filterStartDate, filterEndDate, filteredPrisonerId);
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            dtpFilterStartDate.Value = DateTime.Today;
            dtpFilterEndDate.Value = DateTime.Today;
            cmbFilterPrisoner.Text = "";
            filterStartDate = null;
            filterEndDate = null;
            filteredPrisonerId = null;
            refreshList(null, null, null);
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
            AddEditAttendanceDialog dialog = new AddEditAttendanceDialog();
            dialog.setCurrentUser(currentUser);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            AttendanceRecord attendance = (AttendanceRecord)lvAttendance.SelectedItems[0].Tag;
            AddEditAttendanceDialog dialog = new AddEditAttendanceDialog();
            dialog.setCurrentUser(currentUser);
            dialog.setAttendanceToEdit(attendance);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId);
            }
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
                refreshList(filterStartDate, filterEndDate, filteredPrisonerId);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
