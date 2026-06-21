using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class AttendanceRecordPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public AttendanceRecordPanel()
        {
            InitializeComponent();
        }

        private void AttendanceRecordPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvAttendance.Items.Clear();
            foreach (AttendanceRecord attendance in Program.AttendanceRecords)
            {
                ListViewItem item = new ListViewItem(attendance.getId().ToString());
                item.SubItems.Add(attendance.getPrisoner().getFullName());
                item.SubItems.Add(attendance.getAttendanceDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(attendance.getEntryTime()?.ToString() ?? "");
                item.SubItems.Add(attendance.getExitTime()?.ToString() ?? "");
                item.Tag = attendance;
                lvAttendance.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            AttendanceRecord attendance = (AttendanceRecord)lvAttendance.SelectedItems[0].Tag;
            string info = "ID: " + attendance.getId() + "\n" +
                         "Prisoner: " + attendance.getPrisoner().getFullName() + "\n" +
                         "Date: " + attendance.getAttendanceDate().ToString("yyyy-MM-dd") + "\n" +
                         "Time In: " + (attendance.getEntryTime()?.ToString() ?? "") + "\n" +
                         "Time Out: " + (attendance.getExitTime()?.ToString() ?? "");
            MessageBox.Show(info, "Attendance Record Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Attendance Record Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvAttendance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an attendance record to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Attendance Record Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
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
                "Are you sure you want to delete attendance record for: " + attendance.getPrisoner().getFullName() + " on " + attendance.getAttendanceDate().ToString("yyyy-MM-dd") + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                attendance.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
