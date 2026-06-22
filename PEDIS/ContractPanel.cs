using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ContractPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public ContractPanel()
        {
            InitializeComponent();
        }

        private void ContractPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvContracts.Items.Clear();
            foreach (Contract contract in Program.Contracts)
            {
                ListViewItem item = new ListViewItem(contract.getId().ToString());
                item.SubItems.Add(contract.getCustomerCompany()?.getName() ?? "N/A");
                item.SubItems.Add(contract.getFactory()?.ToString() ?? "N/A");
                item.SubItems.Add(contract.getContractNumber());
                item.SubItems.Add(contract.getContractStatus().ToString());
                item.SubItems.Add(contract.getStartDate().ToString("yyyy-MM-dd"));
                item.SubItems.Add(contract.getEndDate()?.ToString("yyyy-MM-dd") ?? "N/A");
                item.Tag = contract;
                lvContracts.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvContracts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contract to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Contract contract = (Contract)lvContracts.SelectedItems[0].Tag;
            string info = "ID: " + contract.getId() + "\n" +
                         "Company: " + (contract.getCustomerCompany()?.getName() ?? "N/A") + "\n" +
                         "Factory: " + (contract.getFactory()?.ToString() ?? "N/A") + "\n" +
                         "Number: " + contract.getContractNumber() + "\n" +
                         "Status: " + contract.getContractStatus() + "\n" +
                         "Start Date: " + contract.getStartDate().ToString("yyyy-MM-dd") + "\n" +
                         "End Date: " + (contract.getEndDate()?.ToString("yyyy-MM-dd") ?? "N/A");
            MessageBox.Show(info, "Contract Details", MessageBoxButtons.OK);
        }

        private void lvContracts_DoubleClick(object sender, EventArgs e)
        {
            if (lvContracts.SelectedItems.Count == 0)
                return;

            Contract contract = (Contract)lvContracts.SelectedItems[0].Tag;
            CustomerCompany company = contract.getCustomerCompany();
            if (company == null)
            {
                MessageBox.Show("This contract has no associated customer company.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerCompanyDetailsDialog dialog = new CustomerCompanyDetailsDialog(company);
            dialog.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Contract - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvContracts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contract to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Contract - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvContracts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a contract to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Contract contract = (Contract)lvContracts.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete contract: " + contract.getContractNumber() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                contract.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
