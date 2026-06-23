using System;
using System.Windows.Forms;

namespace PEDIS
{
    public class PasswordConfirmationDialog : Form
    {
        private Label lblPrompt;
        private TextBox txtPassword;
        private Button btnOk;
        private Button btnCancel;

        public string EnteredPassword { get; private set; }

        public PasswordConfirmationDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblPrompt = new Label();
            this.txtPassword = new TextBox();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            this.Text = "Confirm Password";
            this.Size = new System.Drawing.Size(380, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblPrompt.AutoSize = false;
            this.lblPrompt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrompt.Location = new System.Drawing.Point(20, 20);
            this.lblPrompt.Size = new System.Drawing.Size(320, 25);
            this.lblPrompt.Text = "Enter your password to confirm:";
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);

            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(20, 50);
            this.txtPassword.Size = new System.Drawing.Size(320, 25);
            this.txtPassword.PasswordChar = '*';

            this.btnOk.Location = new System.Drawing.Point(130, 95);
            this.btnOk.Size = new System.Drawing.Size(100, 35);
            this.btnOk.Text = "OK";
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);

            this.btnCancel.Location = new System.Drawing.Point(240, 95);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPrompt);
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.EnteredPassword = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
