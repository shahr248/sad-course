using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class PrisonerPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public PrisonerPanel()
        {
            InitializeComponent();
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
                ListViewItem item = new ListViewItem(prisoner.getPrisonerNumber());
                item.SubItems.Add(prisoner.getFullName());
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
                         "Status: " + prisoner.getActivityStatus() + "\n" +
                         "Role: " + (prisoner.getRole()?.ToString() ?? "N/A") + "\n" +
                         "Hourly Rate: " + prisoner.getHourlyRate().ToString("C") + "\n" +
                         "Qualified: " + (prisoner.getQualified() ? "Yes" : "No");
            MessageBox.Show(info, "Prisoner Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Prisoner Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Edit Prisoner Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
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
            if (onBack != null)
            {
                onBack.Invoke();
            }
            else
            {
                MessageBox.Show("Back handler not wired properly", "Debug", MessageBoxButtons.OK);
            }
        }
    }
}
