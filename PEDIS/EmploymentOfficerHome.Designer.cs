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
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employment Officer Home";

            // lblUser
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(20, 50);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(100, 13);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Logged in as: ...";

            // btnPrisoners
            this.btnPrisoners.Location = new System.Drawing.Point(20, 80);
            this.btnPrisoners.Name = "btnPrisoners";
            this.btnPrisoners.Size = new System.Drawing.Size(120, 40);
            this.btnPrisoners.TabIndex = 2;
            this.btnPrisoners.Text = "Prisoners";
            this.btnPrisoners.UseVisualStyleBackColor = true;
            this.btnPrisoners.Click += new System.EventHandler(this.btnPrisoners_Click);

            // btnOrders
            this.btnOrders.Location = new System.Drawing.Point(150, 80);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(120, 40);
            this.btnOrders.TabIndex = 3;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);

            // btnContracts
            this.btnContracts.Location = new System.Drawing.Point(280, 80);
            this.btnContracts.Name = "btnContracts";
            this.btnContracts.Size = new System.Drawing.Size(120, 40);
            this.btnContracts.TabIndex = 4;
            this.btnContracts.Text = "Contracts";
            this.btnContracts.UseVisualStyleBackColor = true;
            this.btnContracts.Click += new System.EventHandler(this.btnContracts_Click);

            // btnWorkOrders
            this.btnWorkOrders.Location = new System.Drawing.Point(20, 130);
            this.btnWorkOrders.Name = "btnWorkOrders";
            this.btnWorkOrders.Size = new System.Drawing.Size(120, 40);
            this.btnWorkOrders.TabIndex = 5;
            this.btnWorkOrders.Text = "Work Orders";
            this.btnWorkOrders.UseVisualStyleBackColor = true;
            this.btnWorkOrders.Click += new System.EventHandler(this.btnWorkOrders_Click);

            // btnAttendance
            this.btnAttendance.Location = new System.Drawing.Point(150, 130);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(120, 40);
            this.btnAttendance.TabIndex = 6;
            this.btnAttendance.Text = "Attendance";
            this.btnAttendance.UseVisualStyleBackColor = true;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);

            // btnReports
            this.btnReports.Location = new System.Drawing.Point(280, 130);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(120, 40);
            this.btnReports.TabIndex = 7;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(20, 350);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 30);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // EmploymentOfficerHome
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReports);
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
