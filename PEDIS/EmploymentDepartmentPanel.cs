using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class EmploymentDepartmentPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public EmploymentDepartmentPanel()
        {
            InitializeComponent();
        }

        private void EmploymentDepartmentPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvDepartments.Items.Clear();
            foreach (EmploymentDepartment dept in Program.EmploymentDepartments)
            {
                ListViewItem item = new ListViewItem(dept.getId().ToString());
                item.SubItems.Add(dept.getCode());
                item.SubItems.Add(dept.getName());
                item.SubItems.Add(dept.getLocation() ?? "");
                item.SubItems.Add(dept.getMaxCapacity()?.ToString() ?? "");
                item.SubItems.Add(dept.getIsActive() ? "Yes" : "No");
                item.Tag = dept;
                lvDepartments.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvDepartments.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a department to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            EmploymentDepartment dept = (EmploymentDepartment)lvDepartments.SelectedItems[0].Tag;
            string info = "ID: " + dept.getId() + "\n" +
                         "Code: " + dept.getCode() + "\n" +
                         "Name: " + dept.getName() + "\n" +
                         "Location: " + (dept.getLocation() ?? "N/A") + "\n" +
                         "Max Capacity: " + (dept.getMaxCapacity()?.ToString() ?? "N/A") + "\n" +
                         "Active: " + (dept.getIsActive() ? "Yes" : "No");
            MessageBox.Show(info, "Department Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Placeholder for add dialog
            MessageBox.Show("Add Department Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
            // When implemented, refresh list after successful add:
            // refreshList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvDepartments.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a department to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            // Placeholder for edit dialog
            MessageBox.Show("Edit Department Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
            // When implemented, refresh list after successful edit:
            // refreshList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvDepartments.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a department to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            EmploymentDepartment dept = (EmploymentDepartment)lvDepartments.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete department: " + dept.getName() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                dept.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
