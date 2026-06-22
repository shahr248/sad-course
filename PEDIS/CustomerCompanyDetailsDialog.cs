using System;
using System.Windows.Forms;

namespace PEDIS
{
    public class CustomerCompanyDetailsDialog : Form
    {
        private CustomerCompany customerCompany;
        private Label lblTitle;
        private ListView lvContacts;
        private Button btnClose;

        public CustomerCompanyDetailsDialog(CustomerCompany customerCompany)
        {
            this.customerCompany = customerCompany;
            InitializeComponent();
            LoadDetails();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lvContacts = new ListView();
            this.btnClose = new Button();
            this.SuspendLayout();

            this.Text = "Customer Company Details";
            this.Size = new System.Drawing.Size(560, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(510, 30);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lvContacts.FullRowSelect = true;
            this.lvContacts.GridLines = true;
            this.lvContacts.Location = new System.Drawing.Point(20, 55);
            this.lvContacts.Size = new System.Drawing.Size(510, 165);
            this.lvContacts.UseCompatibleStateImageBehavior = false;
            this.lvContacts.View = System.Windows.Forms.View.Details;
            this.lvContacts.BackColor = System.Drawing.Color.White;
            this.lvContacts.BorderStyle = BorderStyle.Fixed3D;

            this.lvContacts.Columns.Add("Contact Person", 170);
            this.lvContacts.Columns.Add("Phone Number", 170);
            this.lvContacts.Columns.Add("Email", 170);

            this.btnClose.Location = new System.Drawing.Point(20, 235);
            this.btnClose.Size = new System.Drawing.Size(150, 35);
            this.btnClose.Text = "Close";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.DialogResult = DialogResult.OK;

            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvContacts);
            this.Controls.Add(this.lblTitle);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadDetails()
        {
            lblTitle.Text = customerCompany.getName();

            lvContacts.Items.Clear();
            ListViewItem item = new ListViewItem(customerCompany.getContactName() ?? "N/A");
            item.SubItems.Add(customerCompany.getPhoneNumber() ?? "N/A");
            item.SubItems.Add(customerCompany.getEmail() ?? "N/A");
            lvContacts.Items.Add(item);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
