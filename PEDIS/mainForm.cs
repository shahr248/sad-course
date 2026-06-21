using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class mainForm : Form
    {
        private LoginPanel loginPanel;
        private EmploymentOfficerHome employmentOfficerHome;
        private FactoryManagerHome factoryManagerHome;
        private DepartmentManagement currentUser;

        public mainForm()
        {
            InitializeComponent();
            this.Text = "PEDIS - Prison Employment Department Information System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1024, 768);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Initialize in-memory data from database
            Program.initLists();

            // Show login panel on startup
            showLoginPanel();
        }

        public void showLoginPanel()
        {
            if (loginPanel == null)
            {
                loginPanel = new LoginPanel();
                loginPanel.onLoginSuccess += handleLoginSuccess;
            }

            clearPanel();
            this.Controls.Add(loginPanel);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Focus();
        }

        private void handleLoginSuccess(DepartmentManagement user)
        {
            this.currentUser = user;

            // Route to appropriate home panel based on role
            switch (user.getRole())
            {
                case DepartmentManagementRole.DepartmentManager:
                case DepartmentManagementRole.DeputyOfDepartmentManager:
                    showEmploymentOfficerHome(user);
                    break;

                case DepartmentManagementRole.FactoryManager:
                    showFactoryManagerHome(user);
                    break;

                default:
                    MessageBox.Show("Unknown role: " + user.getRole(), "Error", MessageBoxButtons.OK);
                    showLoginPanel();
                    break;
            }
        }

        private void showEmploymentOfficerHome(DepartmentManagement user)
        {
            if (employmentOfficerHome == null)
            {
                employmentOfficerHome = new EmploymentOfficerHome();
                employmentOfficerHome.onLogout += handleLogout;
                employmentOfficerHome.onShowPanel += showPanel;
            }

            employmentOfficerHome.setCurrentUser(user);
            showPanel(employmentOfficerHome);
        }

        private void showFactoryManagerHome(DepartmentManagement user)
        {
            if (factoryManagerHome == null)
            {
                factoryManagerHome = new FactoryManagerHome();
                factoryManagerHome.onLogout += handleLogout;
                factoryManagerHome.onShowPanel += showPanel;
            }

            factoryManagerHome.setCurrentUser(user);
            showPanel(factoryManagerHome);
        }

        private void handleLogout()
        {
            this.currentUser = null;
            showLoginPanel();
        }

        public void showPanel(UserControl panel)
        {
            clearPanel();
            this.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
        }

        private void clearPanel()
        {
            foreach (Control control in this.Controls)
            {
                control.Dispose();
            }
            this.Controls.Clear();
        }
    }
}
