using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class EmploymentOfficerHome : UserControl
    {
        public delegate void LogoutHandler();
        public event LogoutHandler onLogout;

        public delegate void ShowPanelHandler(UserControl panel);
        public event ShowPanelHandler onShowPanel;

        private DepartmentManagement currentUser;

        public EmploymentOfficerHome()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
            lblUser.Text = "Logged in as: " + user.getUsername() + " (" + user.getRole().ToString() + ")";
        }

        private void EmploymentOfficerHome_Load(object sender, EventArgs e)
        {
            // Initialize home panel
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            onLogout?.Invoke();
        }

        private void btnPrisoners_Click(object sender, EventArgs e)
        {
            PrisonerPanel panel = new PrisonerPanel();
            MessageBox.Show("PrisonerPanel created. onShowPanel is: " + (onShowPanel == null ? "NULL" : "WIRED"), "Debug", MessageBoxButtons.OK);

            panel.onBack += () =>
            {
                MessageBox.Show("onBack event received in EmploymentOfficerHome!", "Debug", MessageBoxButtons.OK);
                onShowPanel?.Invoke(this);
            };

            MessageBox.Show("Back handler wired to PrisonerPanel. About to show panel...", "Debug", MessageBoxButtons.OK);
            onShowPanel?.Invoke(panel);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order Management Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contract Management Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnWorkOrders_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Work Order Management Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Attendance Tracking Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }
    }
}
