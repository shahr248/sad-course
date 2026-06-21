namespace PEDIS
{
    partial class AddEditAttendanceDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPrisoner;
        private System.Windows.Forms.ComboBox cbPrisoner;
        private System.Windows.Forms.Label lblAttendanceDate;
        private System.Windows.Forms.DateTimePicker dtpAttendanceDate;
        private System.Windows.Forms.Label lblEntryTime;
        private System.Windows.Forms.CheckBox chkEntryTime;
        private System.Windows.Forms.DateTimePicker dtpEntryTime;
        private System.Windows.Forms.Label lblExitTime;
        private System.Windows.Forms.CheckBox chkExitTime;
        private System.Windows.Forms.DateTimePicker dtpExitTime;
        private System.Windows.Forms.Label lblFactory;
        private System.Windows.Forms.ComboBox cbFactory;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPrisoner = new System.Windows.Forms.Label();
            this.cbPrisoner = new System.Windows.Forms.ComboBox();
            this.lblAttendanceDate = new System.Windows.Forms.Label();
            this.dtpAttendanceDate = new System.Windows.Forms.DateTimePicker();
            this.lblEntryTime = new System.Windows.Forms.Label();
            this.chkEntryTime = new System.Windows.Forms.CheckBox();
            this.dtpEntryTime = new System.Windows.Forms.DateTimePicker();
            this.lblExitTime = new System.Windows.Forms.Label();
            this.chkExitTime = new System.Windows.Forms.CheckBox();
            this.dtpExitTime = new System.Windows.Forms.DateTimePicker();
            this.lblFactory = new System.Windows.Forms.Label();
            this.cbFactory = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.Text = "Add/Edit Attendance Record";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(460, 30);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lblPrisoner.AutoSize = true;
            this.lblPrisoner.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrisoner.Location = new System.Drawing.Point(20, 55);
            this.lblPrisoner.Text = "Prisoner:";

            this.cbPrisoner.Location = new System.Drawing.Point(150, 55);
            this.cbPrisoner.Size = new System.Drawing.Size(330, 24);
            this.cbPrisoner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblAttendanceDate.AutoSize = true;
            this.lblAttendanceDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAttendanceDate.Location = new System.Drawing.Point(20, 90);
            this.lblAttendanceDate.Text = "Attendance Date:";

            this.dtpAttendanceDate.Location = new System.Drawing.Point(150, 90);
            this.dtpAttendanceDate.Size = new System.Drawing.Size(330, 24);
            this.dtpAttendanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblEntryTime.AutoSize = true;
            this.lblEntryTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEntryTime.Location = new System.Drawing.Point(20, 130);
            this.lblEntryTime.Text = "Entry Time:";

            this.chkEntryTime.AutoSize = true;
            this.chkEntryTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEntryTime.Location = new System.Drawing.Point(150, 130);
            this.chkEntryTime.Text = "Record";
            this.chkEntryTime.CheckedChanged += new System.EventHandler(this.chkEntryTime_CheckedChanged);

            this.dtpEntryTime.Location = new System.Drawing.Point(220, 130);
            this.dtpEntryTime.Size = new System.Drawing.Size(260, 24);
            this.dtpEntryTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEntryTime.ShowUpDown = true;
            this.dtpEntryTime.Enabled = false;

            this.lblExitTime.AutoSize = true;
            this.lblExitTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblExitTime.Location = new System.Drawing.Point(20, 165);
            this.lblExitTime.Text = "Exit Time:";

            this.chkExitTime.AutoSize = true;
            this.chkExitTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkExitTime.Location = new System.Drawing.Point(150, 165);
            this.chkExitTime.Text = "Record";
            this.chkExitTime.CheckedChanged += new System.EventHandler(this.chkExitTime_CheckedChanged);

            this.dtpExitTime.Location = new System.Drawing.Point(220, 165);
            this.dtpExitTime.Size = new System.Drawing.Size(260, 24);
            this.dtpExitTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpExitTime.ShowUpDown = true;
            this.dtpExitTime.Enabled = false;

            this.lblFactory.AutoSize = true;
            this.lblFactory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFactory.Location = new System.Drawing.Point(20, 205);
            this.lblFactory.Text = "Factory:";

            this.cbFactory.Location = new System.Drawing.Point(150, 205);
            this.cbFactory.Size = new System.Drawing.Size(330, 24);
            this.cbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnSave.Location = new System.Drawing.Point(150, 260);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(280, 260);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbFactory);
            this.Controls.Add(this.lblFactory);
            this.Controls.Add(this.dtpExitTime);
            this.Controls.Add(this.chkExitTime);
            this.Controls.Add(this.lblExitTime);
            this.Controls.Add(this.dtpEntryTime);
            this.Controls.Add(this.chkEntryTime);
            this.Controls.Add(this.lblEntryTime);
            this.Controls.Add(this.dtpAttendanceDate);
            this.Controls.Add(this.lblAttendanceDate);
            this.Controls.Add(this.cbPrisoner);
            this.Controls.Add(this.lblPrisoner);
            this.Controls.Add(this.lblTitle);

            this.Name = "AddEditAttendanceDialog";
            this.Load += new System.EventHandler(this.AddEditAttendanceDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
