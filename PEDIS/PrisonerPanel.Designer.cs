namespace PEDIS
{
    partial class PrisonerPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnFilterAll;
        private System.Windows.Forms.Button btnFilterActive;
        private System.Windows.Forms.Button btnFilterPresentToday;
        private System.Windows.Forms.Button btnFilterExpiringSafety;
        private System.Windows.Forms.ListView lvPrisoners;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBack;

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
            this.btnFilterAll = new System.Windows.Forms.Button();
            this.btnFilterActive = new System.Windows.Forms.Button();
            this.btnFilterPresentToday = new System.Windows.Forms.Button();
            this.btnFilterExpiringSafety = new System.Windows.Forms.Button();
            this.lvPrisoners = new System.Windows.Forms.ListView();
            this.btnView = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Prisoners";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            // btnFilterAll
            this.btnFilterAll.Location = new System.Drawing.Point(20, 55);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(110, 30);
            this.btnFilterAll.Text = "Show All";
            this.btnFilterAll.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnFilterAll.ForeColor = System.Drawing.Color.White;
            this.btnFilterAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterAll.UseVisualStyleBackColor = false;
            this.btnFilterAll.Click += new System.EventHandler(this.btnFilterAll_Click);

            // btnFilterActive
            this.btnFilterActive.Location = new System.Drawing.Point(140, 55);
            this.btnFilterActive.Name = "btnFilterActive";
            this.btnFilterActive.Size = new System.Drawing.Size(150, 30);
            this.btnFilterActive.Text = "Active Inmates";
            this.btnFilterActive.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnFilterActive.ForeColor = System.Drawing.Color.White;
            this.btnFilterActive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterActive.UseVisualStyleBackColor = false;
            this.btnFilterActive.Click += new System.EventHandler(this.btnFilterActive_Click);

            // btnFilterPresentToday
            this.btnFilterPresentToday.Location = new System.Drawing.Point(300, 55);
            this.btnFilterPresentToday.Name = "btnFilterPresentToday";
            this.btnFilterPresentToday.Size = new System.Drawing.Size(150, 30);
            this.btnFilterPresentToday.Text = "Present Today";
            this.btnFilterPresentToday.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnFilterPresentToday.ForeColor = System.Drawing.Color.White;
            this.btnFilterPresentToday.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterPresentToday.UseVisualStyleBackColor = false;
            this.btnFilterPresentToday.Click += new System.EventHandler(this.btnFilterPresentToday_Click);

            // btnFilterExpiringSafety
            this.btnFilterExpiringSafety.Location = new System.Drawing.Point(460, 55);
            this.btnFilterExpiringSafety.Name = "btnFilterExpiringSafety";
            this.btnFilterExpiringSafety.Size = new System.Drawing.Size(220, 30);
            this.btnFilterExpiringSafety.Text = "Safety Training Expiring Soon";
            this.btnFilterExpiringSafety.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnFilterExpiringSafety.ForeColor = System.Drawing.Color.White;
            this.btnFilterExpiringSafety.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilterExpiringSafety.UseVisualStyleBackColor = false;
            this.btnFilterExpiringSafety.Click += new System.EventHandler(this.btnFilterExpiringSafety_Click);

            // lvPrisoners
            this.lvPrisoners.FullRowSelect = true;
            this.lvPrisoners.GridLines = true;
            this.lvPrisoners.Location = new System.Drawing.Point(20, 95);
            this.lvPrisoners.Name = "lvPrisoners";
            this.lvPrisoners.Size = new System.Drawing.Size(1400, 350);
            this.lvPrisoners.TabIndex = 1;
            this.lvPrisoners.UseCompatibleStateImageBehavior = false;
            this.lvPrisoners.View = System.Windows.Forms.View.Details;
            this.lvPrisoners.BackColor = System.Drawing.Color.White;
            this.lvPrisoners.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            // Add columns
            this.lvPrisoners.Columns.Add("PrisonerID", 70);
            this.lvPrisoners.Columns.Add("Name", 130);
            this.lvPrisoners.Columns.Add("Factory", 90);
            this.lvPrisoners.Columns.Add("Department", 90);
            this.lvPrisoners.Columns.Add("Status", 80);
            this.lvPrisoners.Columns.Add("Role", 70);
            this.lvPrisoners.Columns.Add("Hourly Rate", 70);
            this.lvPrisoners.Columns.Add("Work Start", 100);
            this.lvPrisoners.Columns.Add("Safety Training Exp", 130);
            this.lvPrisoners.Columns.Add("Release Date", 100);
            this.lvPrisoners.Columns.Add("Qualified", 60);

            // btnView
            this.btnView.Location = new System.Drawing.Point(20, 455);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(100, 35);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View";
            this.btnView.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(130, 455);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(240, 455);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(350, 455);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(20, 505);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "← Back";
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // PrisonerPanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lvPrisoners);
            this.Controls.Add(this.btnFilterExpiringSafety);
            this.Controls.Add(this.btnFilterPresentToday);
            this.Controls.Add(this.btnFilterActive);
            this.Controls.Add(this.btnFilterAll);
            this.Controls.Add(this.lblTitle);
            this.Name = "PrisonerPanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.PrisonerPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
