using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ProductivityRecordPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public ProductivityRecordPanel()
        {
            InitializeComponent();
        }

        private void ProductivityRecordPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvProductivity.Items.Clear();
            foreach (ProductivityRecord productivity in Program.ProductivityRecords)
            {
                ListViewItem item = new ListViewItem(productivity.getId().ToString());
                item.SubItems.Add(productivity.getPrisoner().getFullName());
                item.SubItems.Add(productivity.getWorkOrder().getWorkOrderNumber());
                item.SubItems.Add(productivity.getProductivityDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(productivity.getQuantityProduced().ToString());
                item.SubItems.Add(productivity.getProductivityType().ToString());
                item.Tag = productivity;
                lvProductivity.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductivityRecord productivity = (ProductivityRecord)lvProductivity.SelectedItems[0].Tag;
            string info = "ID: " + productivity.getId() + "\n" +
                         "Prisoner: " + productivity.getPrisoner().getFullName() + "\n" +
                         "Work Order #: " + productivity.getWorkOrder().getWorkOrderNumber() + "\n" +
                         "Date: " + productivity.getProductivityDate().ToString("yyyy-MM-dd") + "\n" +
                         "Units Produced: " + productivity.getQuantityProduced() + "\n" +
                         "Type: " + productivity.getProductivityType();
            MessageBox.Show(info, "Productivity Record Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Productivity Record Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Productivity Record Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvProductivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a productivity record to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            ProductivityRecord productivity = (ProductivityRecord)lvProductivity.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete productivity record for: " + productivity.getPrisoner().getFullName() + " on " + productivity.getProductivityDate().ToString("yyyy-MM-dd") + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                productivity.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
