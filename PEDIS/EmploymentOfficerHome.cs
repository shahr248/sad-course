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
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            ProductionOrderPanel panel = new ProductionOrderPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            ContractPanel panel = new ContractPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnWorkOrders_Click(object sender, EventArgs e)
        {
            WorkOrderPanel panel = new WorkOrderPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            AttendanceRecordPanel panel = new AttendanceRecordPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }
    }
}
