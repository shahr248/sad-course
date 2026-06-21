using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class CustomerCompanyPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public CustomerCompanyPanel()
        {
            InitializeComponent();
        }

        private void CustomerCompanyPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvCompanies.Items.Clear();
            foreach (CustomerCompany company in Program.CustomerCompanies)
            {
                ListViewItem item = new ListViewItem(company.getId().ToString());
                item.SubItems.Add(company.getCompanyName());
                item.SubItems.Add(company.getContactPerson() ?? "");
                item.SubItems.Add(company.getPhoneNumber() ?? "");
                item.SubItems.Add(company.getEmail() ?? "");
                item.Tag = company;
                lvCompanies.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvCompanies.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a company to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            CustomerCompany company = (CustomerCompany)lvCompanies.SelectedItems[0].Tag;
            string info = "ID: " + company.getId() + "\n" +
                         "Name: " + company.getCompanyName() + "\n" +
                         "Contact: " + (company.getContactPerson() ?? "N/A") + "\n" +
                         "Phone: " + (company.getPhoneNumber() ?? "N/A") + "\n" +
                         "Email: " + (company.getEmail() ?? "N/A");
            MessageBox.Show(info, "Company Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Company Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvCompanies.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a company to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Company Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvCompanies.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a company to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            CustomerCompany company = (CustomerCompany)lvCompanies.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete company: " + company.getCompanyName() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                company.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
