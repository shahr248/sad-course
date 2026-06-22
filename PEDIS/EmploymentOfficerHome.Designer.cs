namespace PEDIS
{
    partial class EmploymentOfficerHome
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnPrisoners;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnContracts;
        private System.Windows.Forms.Button btnWorkOrders;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnProductivity;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnPrisoners = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnContracts = new System.Windows.Forms.Button();
            this.btnWorkOrders = new System.Windows.Forms.Button();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnProductivity = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(984, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employment Officer Home";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            // lblUser
            this.lblUser.AutoSize = false;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUser.Location = new System.Drawing.Point(20, 65);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(984, 25);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Logged in as: ...";
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);

            // btnPrisoners
            this.btnPrisoners.Location = new System.Drawing.Point(20, 110);
            this.btnPrisoners.Name = "btnPrisoners";
            this.btnPrisoners.Size = new System.Drawing.Size(150, 60);
            this.btnPrisoners.TabIndex = 2;
            this.btnPrisoners.Text = "👥 Prisoners";
            this.btnPrisoners.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnPrisoners.ForeColor = System.Drawing.Color.White;
            this.btnPrisoners.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrisoners.UseVisualStyleBackColor = false;
            this.btnPrisoners.Click += new System.EventHandler(this.btnPrisoners_Click);

            // btnOrders
            this.btnOrders.Location = new System.Drawing.Point(180, 110);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(150, 60);
            this.btnOrders.TabIndex = 3;
            this.btnOrders.Text = "📋 Orders";
            this.btnOrders.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);

            // btnContracts
            this.btnContracts.Location = new System.Drawing.Point(340, 110);
            this.btnContracts.Name = "btnContracts";
            this.btnContracts.Size = new System.Drawing.Size(150, 60);
            this.btnContracts.TabIndex = 4;
            this.btnContracts.Text = "📄 Contracts";
            this.btnContracts.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnContracts.ForeColor = System.Drawing.Color.White;
            this.btnContracts.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnContracts.UseVisualStyleBackColor = false;
            this.btnContracts.Click += new System.EventHandler(this.btnContracts_Click);

            // btnWorkOrders
            this.btnWorkOrders.Location = new System.Drawing.Point(500, 110);
            this.btnWorkOrders.Name = "btnWorkOrders";
            this.btnWorkOrders.Size = new System.Drawing.Size(150, 60);
            this.btnWorkOrders.TabIndex = 5;
            this.btnWorkOrders.Text = "🔧 Work Orders";
            this.btnWorkOrders.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnWorkOrders.ForeColor = System.Drawing.Color.White;
            this.btnWorkOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnWorkOrders.UseVisualStyleBackColor = false;
            this.btnWorkOrders.Click += new System.EventHandler(this.btnWorkOrders_Click);

            // btnAttendance
            this.btnAttendance.Location = new System.Drawing.Point(660, 110);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(150, 60);
            this.btnAttendance.TabIndex = 6;
            this.btnAttendance.Text = "✓ Attendance";
            this.btnAttendance.BackColor = System.Drawing.Color.FromArgb(26, 188, 156);
            this.btnAttendance.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAttendance.UseVisualStyleBackColor = false;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);

            // btnProductivity
            this.btnProductivity.Location = new System.Drawing.Point(20, 190);
            this.btnProductivity.Name = "btnProductivity";
            this.btnProductivity.Size = new System.Drawing.Size(150, 60);
            this.btnProductivity.TabIndex = 7;
            this.btnProductivity.Text = "📈 Productivity";
            this.btnProductivity.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.btnProductivity.ForeColor = System.Drawing.Color.White;
            this.btnProductivity.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnProductivity.UseVisualStyleBackColor = false;
            this.btnProductivity.Click += new System.EventHandler(this.btnProductivity_Click);

            // btnReports
            this.btnReports.Location = new System.Drawing.Point(820, 110);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(150, 60);
            this.btnReports.TabIndex = 8;
            this.btnReports.Text = "📊 Reports";
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // btnLogout
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLogout.Location = new System.Drawing.Point(844, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(160, 40);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // EmploymentOfficerHome
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnProductivity);
            this.Controls.Add(this.btnAttendance);
            this.Controls.Add(this.btnWorkOrders);
            this.Controls.Add(this.btnContracts);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnPrisoners);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblTitle);
            this.Name = "EmploymentOfficerHome";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.EmploymentOfficerHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
