namespace PEDIS
{
    partial class AttendanceRecordPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFilterDate;
        private System.Windows.Forms.Label lblFilterPrisoner;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.ComboBox cmbFilterPrisoner;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.ListView lvAttendance;
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
            this.lblFilterDate = new System.Windows.Forms.Label();
            this.lblFilterPrisoner = new System.Windows.Forms.Label();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.cmbFilterPrisoner = new System.Windows.Forms.ComboBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.lvAttendance = new System.Windows.Forms.ListView();
            this.btnView = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Text = "Attendance Records";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            // Filter Controls Section
            this.lblFilterDate.AutoSize = false;
            this.lblFilterDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFilterDate.Location = new System.Drawing.Point(20, 60);
            this.lblFilterDate.Size = new System.Drawing.Size(100, 20);
            this.lblFilterDate.Text = "Filter by Date:";
            this.lblFilterDate.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);

            this.dtpFilterDate.Location = new System.Drawing.Point(130, 60);
            this.dtpFilterDate.Size = new System.Drawing.Size(150, 25);
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblFilterPrisoner.AutoSize = false;
            this.lblFilterPrisoner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFilterPrisoner.Location = new System.Drawing.Point(300, 60);
            this.lblFilterPrisoner.Size = new System.Drawing.Size(120, 20);
            this.lblFilterPrisoner.Text = "Filter by Prisoner:";
            this.lblFilterPrisoner.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);

            this.cmbFilterPrisoner.Location = new System.Drawing.Point(430, 60);
            this.cmbFilterPrisoner.Size = new System.Drawing.Size(250, 25);
            this.cmbFilterPrisoner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnApplyFilters.Location = new System.Drawing.Point(700, 60);
            this.btnApplyFilters.Size = new System.Drawing.Size(100, 25);
            this.btnApplyFilters.Text = "Apply Filters";
            this.btnApplyFilters.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnApplyFilters.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilters.UseVisualStyleBackColor = false;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);

            this.btnClearFilters.Location = new System.Drawing.Point(810, 60);
            this.btnClearFilters.Size = new System.Drawing.Size(100, 25);
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnClearFilters.ForeColor = System.Drawing.Color.White;
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearFilters.UseVisualStyleBackColor = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);

            this.lvAttendance.FullRowSelect = true;
            this.lvAttendance.GridLines = true;
            this.lvAttendance.Location = new System.Drawing.Point(20, 100);
            this.lvAttendance.Size = new System.Drawing.Size(984, 300);
            this.lvAttendance.UseCompatibleStateImageBehavior = false;
            this.lvAttendance.View = System.Windows.Forms.View.Details;
            this.lvAttendance.BackColor = System.Drawing.Color.White;
            this.lvAttendance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            this.lvAttendance.Columns.Add("ID", 50);
            this.lvAttendance.Columns.Add("Prisoner ID", 80);
            this.lvAttendance.Columns.Add("Prisoner Name", 150);
            this.lvAttendance.Columns.Add("Date", 100);
            this.lvAttendance.Columns.Add("Entry Time", 80);
            this.lvAttendance.Columns.Add("Exit Time", 80);
            this.lvAttendance.Columns.Add("Hours Worked", 80);
            this.lvAttendance.Columns.Add("Factory", 80);

            this.btnView.Location = new System.Drawing.Point(20, 420);
            this.btnView.Size = new System.Drawing.Size(100, 35);
            this.btnView.Text = "View";
            this.btnView.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);

            this.btnAdd.Location = new System.Drawing.Point(130, 420);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Location = new System.Drawing.Point(240, 420);
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.Text = "Edit";
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Location = new System.Drawing.Point(350, 420);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnBack.Location = new System.Drawing.Point(20, 470);
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.Text = "← Back";
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lvAttendance);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.btnApplyFilters);
            this.Controls.Add(this.cmbFilterPrisoner);
            this.Controls.Add(this.lblFilterPrisoner);
            this.Controls.Add(this.dtpFilterDate);
            this.Controls.Add(this.lblFilterDate);
            this.Controls.Add(this.lblTitle);
            this.Name = "AttendanceRecordPanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.AttendanceRecordPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
