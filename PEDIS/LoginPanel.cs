using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class LoginPanel : UserControl
    {
        public delegate void LoginSuccessHandler(DepartmentManagement user);
        public event LoginSuccessHandler onLoginSuccess;

        public LoginPanel()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required", "Validation Error", MessageBoxButtons.OK);
                return;
            }

            // Authenticate against DepartmentManagement records
            DepartmentManagement user = authenticateUser(username, password);

            if (user != null)
            {
                // Clear password field for security
                txtPassword.Clear();
                txtUsername.Clear();

                // Fire login success event
                onLoginSuccess?.Invoke(user);
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Authentication Failed", MessageBoxButtons.OK);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private DepartmentManagement authenticateUser(string username, string password)
        {
            // Search DepartmentManagement list for matching credentials
            foreach (DepartmentManagement user in Program.DepartmentManagements)
            {
                if (user.getUsername() == username && user.getPassword() == password)
                {
                    return user;
                }
            }
            return null;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                btnLogin_Click(null, null);
            }
        }

        private void LoginPanel_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
