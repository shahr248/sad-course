namespace PEDIS
{
    partial class AddEditPrisonerDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPrisonerNumber;
        private System.Windows.Forms.TextBox txtPrisonerNumber;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFactory;
        private System.Windows.Forms.ComboBox cbFactory;
        private System.Windows.Forms.Label lblActivityStatus;
        private System.Windows.Forms.ComboBox cbActivityStatus;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label lblHourlyRate;
        private System.Windows.Forms.TextBox txtHourlyRate;
        private System.Windows.Forms.CheckBox chkQualified;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPrisonerNumber = new System.Windows.Forms.Label();
            this.txtPrisonerNumber = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFactory = new System.Windows.Forms.Label();
            this.cbFactory = new System.Windows.Forms.ComboBox();
            this.lblActivityStatus = new System.Windows.Forms.Label();
            this.cbActivityStatus = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.lblHourlyRate = new System.Windows.Forms.Label();
            this.txtHourlyRate = new System.Windows.Forms.TextBox();
            this.chkQualified = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblPrisonerNumber
            this.lblPrisonerNumber.AutoSize = true;
            this.lblPrisonerNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrisonerNumber.Location = new System.Drawing.Point(20, 20);
            this.lblPrisonerNumber.Text = "Prisoner Number:";

            // txtPrisonerNumber
            this.txtPrisonerNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrisonerNumber.Location = new System.Drawing.Point(150, 20);
            this.txtPrisonerNumber.Size = new System.Drawing.Size(300, 25);

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFullName.Location = new System.Drawing.Point(20, 60);
            this.lblFullName.Text = "Full Name:";

            // txtFullName
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Location = new System.Drawing.Point(150, 60);
            this.txtFullName.Size = new System.Drawing.Size(300, 25);

            // lblFactory
            this.lblFactory.AutoSize = true;
            this.lblFactory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFactory.Location = new System.Drawing.Point(20, 100);
            this.lblFactory.Text = "Factory:";

            // cbFactory
            this.cbFactory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbFactory.Location = new System.Drawing.Point(150, 100);
            this.cbFactory.Size = new System.Drawing.Size(300, 25);

            // lblActivityStatus
            this.lblActivityStatus.AutoSize = true;
            this.lblActivityStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblActivityStatus.Location = new System.Drawing.Point(20, 140);
            this.lblActivityStatus.Text = "Activity Status:";

            // cbActivityStatus
            this.cbActivityStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbActivityStatus.Location = new System.Drawing.Point(150, 140);
            this.cbActivityStatus.Size = new System.Drawing.Size(300, 25);

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRole.Location = new System.Drawing.Point(20, 180);
            this.lblRole.Text = "Role:";

            // cbRole
            this.cbRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbRole.Location = new System.Drawing.Point(150, 180);
            this.cbRole.Size = new System.Drawing.Size(300, 25);

            // lblHourlyRate
            this.lblHourlyRate.AutoSize = true;
            this.lblHourlyRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHourlyRate.Location = new System.Drawing.Point(20, 220);
            this.lblHourlyRate.Text = "Hourly Rate:";

            // txtHourlyRate
            this.txtHourlyRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHourlyRate.Location = new System.Drawing.Point(150, 220);
            this.txtHourlyRate.Size = new System.Drawing.Size(300, 25);
            this.txtHourlyRate.Text = "0.00";

            // chkQualified
            this.chkQualified.AutoSize = true;
            this.chkQualified.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkQualified.Location = new System.Drawing.Point(150, 260);
            this.chkQualified.Text = "Qualified";
            this.chkQualified.Size = new System.Drawing.Size(100, 20);

            // btnSave
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(150, 310);
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(260, 310);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // AddEditPrisonerDialog
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkQualified);
            this.Controls.Add(this.txtHourlyRate);
            this.Controls.Add(this.lblHourlyRate);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cbActivityStatus);
            this.Controls.Add(this.lblActivityStatus);
            this.Controls.Add(this.cbFactory);
            this.Controls.Add(this.lblFactory);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtPrisonerNumber);
            this.Controls.Add(this.lblPrisonerNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditPrisonerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Prisoner";
            this.Load += new System.EventHandler(this.AddEditPrisonerDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
