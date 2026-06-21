using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class FactoryManagerHome : UserControl
    {
        public delegate void LogoutHandler();
        public event LogoutHandler onLogout;

        public delegate void ShowPanelHandler(UserControl panel);
        public event ShowPanelHandler onShowPanel;

        private DepartmentManagement currentUser;

        public FactoryManagerHome()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
            string factoryName = user.getFactory().HasValue ? user.getFactory().ToString() : "N/A";
            lblUser.Text = "Logged in as: " + user.getUsername() + " (Factory Manager - " + factoryName + ")";
        }

        private void FactoryManagerHome_Load(object sender, EventArgs e)
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

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            AttendanceRecordPanel panel = new AttendanceRecordPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnProductivity_Click(object sender, EventArgs e)
        {
            ProductivityRecordPanel panel = new ProductivityRecordPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnWorkOrders_Click(object sender, EventArgs e)
        {
            WorkOrderPanel panel = new WorkOrderPanel();
            panel.onBack += () => onShowPanel?.Invoke(this);
            onShowPanel?.Invoke(panel);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inventory Management Panel\n(To be implemented - no Inventory table in schema)", "Feature", MessageBoxButtons.OK);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Factory Reports Panel\n(To be implemented)", "Feature", MessageBoxButtons.OK);
        }
    }
}
