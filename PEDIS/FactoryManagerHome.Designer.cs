namespace PEDIS
{
    partial class FactoryManagerHome
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnPrisoners;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnProductivity;
        private System.Windows.Forms.Button btnWorkOrders;
        private System.Windows.Forms.Button btnInventory;
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
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnProductivity = new System.Windows.Forms.Button();
            this.btnWorkOrders = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Factory Manager Home";

            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(20, 50);
            this.lblUser.Name = "lblUser";
            this.lblUser.Text = "Logged in as: ...";

            this.btnPrisoners.Location = new System.Drawing.Point(20, 80);
            this.btnPrisoners.Size = new System.Drawing.Size(120, 40);
            this.btnPrisoners.Text = "Prisoners";
            this.btnPrisoners.Click += new System.EventHandler(this.btnPrisoners_Click);

            this.btnAttendance.Location = new System.Drawing.Point(150, 80);
            this.btnAttendance.Size = new System.Drawing.Size(120, 40);
            this.btnAttendance.Text = "Attendance";
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);

            this.btnProductivity.Location = new System.Drawing.Point(280, 80);
            this.btnProductivity.Size = new System.Drawing.Size(120, 40);
            this.btnProductivity.Text = "Productivity";
            this.btnProductivity.Click += new System.EventHandler(this.btnProductivity_Click);

            this.btnWorkOrders.Location = new System.Drawing.Point(20, 130);
            this.btnWorkOrders.Size = new System.Drawing.Size(120, 40);
            this.btnWorkOrders.Text = "Work Orders";
            this.btnWorkOrders.Click += new System.EventHandler(this.btnWorkOrders_Click);

            this.btnInventory.Location = new System.Drawing.Point(150, 130);
            this.btnInventory.Size = new System.Drawing.Size(120, 40);
            this.btnInventory.Text = "Inventory";
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);

            this.btnReports.Location = new System.Drawing.Point(280, 130);
            this.btnReports.Size = new System.Drawing.Size(120, 40);
            this.btnReports.Text = "Reports";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            this.btnLogout.Location = new System.Drawing.Point(20, 350);
            this.btnLogout.Size = new System.Drawing.Size(100, 30);
            this.btnLogout.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnWorkOrders);
            this.Controls.Add(this.btnProductivity);
            this.Controls.Add(this.btnAttendance);
            this.Controls.Add(this.btnPrisoners);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblTitle);
            this.Name = "FactoryManagerHome";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.FactoryManagerHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
