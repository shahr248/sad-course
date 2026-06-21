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
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnCustomerCompanies;
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
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnCustomerCompanies = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(984, 40);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.lblTitle.Text = "Factory Manager Home";

            // lblUser
            this.lblUser.AutoSize = false;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUser.Location = new System.Drawing.Point(20, 65);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(984, 25);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblUser.Text = "Logged in as: ...";

            // btnPrisoners
            this.btnPrisoners.Location = new System.Drawing.Point(20, 110);
            this.btnPrisoners.Size = new System.Drawing.Size(150, 60);
            this.btnPrisoners.Text = "👥 Prisoners";
            this.btnPrisoners.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnPrisoners.ForeColor = System.Drawing.Color.White;
            this.btnPrisoners.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrisoners.UseVisualStyleBackColor = false;
            this.btnPrisoners.Click += new System.EventHandler(this.btnPrisoners_Click);

            // btnAttendance
            this.btnAttendance.Location = new System.Drawing.Point(180, 110);
            this.btnAttendance.Size = new System.Drawing.Size(150, 60);
            this.btnAttendance.Text = "✓ Attendance";
            this.btnAttendance.BackColor = System.Drawing.Color.FromArgb(26, 188, 156);
            this.btnAttendance.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAttendance.UseVisualStyleBackColor = false;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);

            // btnProductivity
            this.btnProductivity.Location = new System.Drawing.Point(340, 110);
            this.btnProductivity.Size = new System.Drawing.Size(150, 60);
            this.btnProductivity.Text = "📈 Productivity";
            this.btnProductivity.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnProductivity.ForeColor = System.Drawing.Color.White;
            this.btnProductivity.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnProductivity.UseVisualStyleBackColor = false;
            this.btnProductivity.Click += new System.EventHandler(this.btnProductivity_Click);

            // btnWorkOrders
            this.btnWorkOrders.Location = new System.Drawing.Point(500, 110);
            this.btnWorkOrders.Size = new System.Drawing.Size(150, 60);
            this.btnWorkOrders.Text = "🔧 Work Orders";
            this.btnWorkOrders.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnWorkOrders.ForeColor = System.Drawing.Color.White;
            this.btnWorkOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnWorkOrders.UseVisualStyleBackColor = false;
            this.btnWorkOrders.Click += new System.EventHandler(this.btnWorkOrders_Click);

            // btnInventory
            this.btnInventory.Location = new System.Drawing.Point(660, 110);
            this.btnInventory.Size = new System.Drawing.Size(150, 60);
            this.btnInventory.Text = "📦 Inventory";
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);

            // btnReports
            this.btnReports.Location = new System.Drawing.Point(820, 110);
            this.btnReports.Size = new System.Drawing.Size(150, 60);
            this.btnReports.Text = "📊 Reports";
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // btnOrders
            this.btnOrders.Location = new System.Drawing.Point(20, 190);
            this.btnOrders.Size = new System.Drawing.Size(150, 60);
            this.btnOrders.Text = "📋 Orders";
            this.btnOrders.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);

            // btnCustomerCompanies
            this.btnCustomerCompanies.Location = new System.Drawing.Point(180, 190);
            this.btnCustomerCompanies.Size = new System.Drawing.Size(150, 60);
            this.btnCustomerCompanies.Text = "🏢 Customer Companies";
            this.btnCustomerCompanies.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnCustomerCompanies.ForeColor = System.Drawing.Color.White;
            this.btnCustomerCompanies.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCustomerCompanies.UseVisualStyleBackColor = false;
            this.btnCustomerCompanies.Click += new System.EventHandler(this.btnCustomerCompanies_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(20, 700);
            this.btnLogout.Size = new System.Drawing.Size(120, 40);
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // FactoryManagerHome
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnCustomerCompanies);
            this.Controls.Add(this.btnOrders);
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
