using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class PrisonerPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private DepartmentManagement currentUser;

        public PrisonerPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        private void PrisonerPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvPrisoners.Items.Clear();
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                // Factory Manager filtering: only show prisoners from their assigned factory
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    if (prisoner.getFactory() != currentUser.getFactory())
                        continue;
                }

                ListViewItem item = new ListViewItem(prisoner.getPrisonerNumber());
                item.SubItems.Add(prisoner.getFullName());
                item.SubItems.Add(prisoner.getFactory()?.ToString() ?? "");
                item.SubItems.Add(prisoner.getActivityStatus().ToString());
                item.SubItems.Add(prisoner.getRole()?.ToString() ?? "");
                item.SubItems.Add(prisoner.getHourlyRate().ToString("C"));
                item.SubItems.Add(prisoner.getQualified() ? "Yes" : "No");
                item.Tag = prisoner;
                lvPrisoners.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            string info = "Number: " + prisoner.getPrisonerNumber() + "\n" +
                         "Name: " + prisoner.getFullName() + "\n" +
                         "Factory: " + (prisoner.getFactory()?.ToString() ?? "") + "\n" +
                         "Status: " + prisoner.getActivityStatus() + "\n" +
                         "Role: " + (prisoner.getRole()?.ToString() ?? "") + "\n" +
                         "Hourly Rate: " + prisoner.getHourlyRate().ToString("C") + "\n" +
                         "Qualified: " + (prisoner.getQualified() ? "Yes" : "No");
            MessageBox.Show(info, "Prisoner Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditPrisonerDialog dialog = new AddEditPrisonerDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            AddEditPrisonerDialog dialog = new AddEditPrisonerDialog();
            dialog.setPrisonerToEdit(prisoner);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete prisoner: " + prisoner.getFullName() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                prisoner.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
