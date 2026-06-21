namespace PEDIS
{
    partial class AddEditProductivityRecordDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPrisoner;
        private System.Windows.Forms.ComboBox cbPrisoner;
        private System.Windows.Forms.Label lblWorkOrder;
        private System.Windows.Forms.ComboBox cbWorkOrder;
        private System.Windows.Forms.Label lblProductivityDate;
        private System.Windows.Forms.DateTimePicker dtpProductivityDate;
        private System.Windows.Forms.Label lblQuantityProduced;
        private System.Windows.Forms.NumericUpDown nudQuantityProduced;
        private System.Windows.Forms.Label lblWorkHours;
        private System.Windows.Forms.CheckBox chkWorkHours;
        private System.Windows.Forms.NumericUpDown nudWorkHours;
        private System.Windows.Forms.Label lblProductivityType;
        private System.Windows.Forms.ComboBox cbProductivityType;
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
            this.lblWorkOrder = new System.Windows.Forms.Label();
            this.cbWorkOrder = new System.Windows.Forms.ComboBox();
            this.lblProductivityDate = new System.Windows.Forms.Label();
            this.dtpProductivityDate = new System.Windows.Forms.DateTimePicker();
            this.lblQuantityProduced = new System.Windows.Forms.Label();
            this.nudQuantityProduced = new System.Windows.Forms.NumericUpDown();
            this.lblWorkHours = new System.Windows.Forms.Label();
            this.chkWorkHours = new System.Windows.Forms.CheckBox();
            this.nudWorkHours = new System.Windows.Forms.NumericUpDown();
            this.lblProductivityType = new System.Windows.Forms.Label();
            this.cbProductivityType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantityProduced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkHours)).BeginInit();
            this.SuspendLayout();

            this.Text = "Add/Edit Productivity Record";
            this.Size = new System.Drawing.Size(500, 480);
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

            this.lblWorkOrder.AutoSize = true;
            this.lblWorkOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkOrder.Location = new System.Drawing.Point(20, 90);
            this.lblWorkOrder.Text = "Work Order:";

            this.cbWorkOrder.Location = new System.Drawing.Point(150, 90);
            this.cbWorkOrder.Size = new System.Drawing.Size(330, 24);
            this.cbWorkOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblProductivityDate.AutoSize = true;
            this.lblProductivityDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProductivityDate.Location = new System.Drawing.Point(20, 125);
            this.lblProductivityDate.Text = "Productivity Date:";

            this.dtpProductivityDate.Location = new System.Drawing.Point(150, 125);
            this.dtpProductivityDate.Size = new System.Drawing.Size(330, 24);
            this.dtpProductivityDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblQuantityProduced.AutoSize = true;
            this.lblQuantityProduced.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantityProduced.Location = new System.Drawing.Point(20, 160);
            this.lblQuantityProduced.Text = "Quantity Produced:";

            this.nudQuantityProduced.Location = new System.Drawing.Point(150, 160);
            this.nudQuantityProduced.Size = new System.Drawing.Size(150, 24);
            this.nudQuantityProduced.Minimum = 0;
            this.nudQuantityProduced.Maximum = 1000000;

            this.lblWorkHours.AutoSize = true;
            this.lblWorkHours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkHours.Location = new System.Drawing.Point(20, 195);
            this.lblWorkHours.Text = "Work Hours:";

            this.chkWorkHours.AutoSize = true;
            this.chkWorkHours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkWorkHours.Location = new System.Drawing.Point(150, 198);
            this.chkWorkHours.Text = "Record";
            this.chkWorkHours.CheckedChanged += new System.EventHandler(this.chkWorkHours_CheckedChanged);

            this.nudWorkHours.Location = new System.Drawing.Point(220, 195);
            this.nudWorkHours.Size = new System.Drawing.Size(150, 24);
            this.nudWorkHours.DecimalPlaces = 2;
            this.nudWorkHours.Increment = 0.25M;
            this.nudWorkHours.Minimum = 0;
            this.nudWorkHours.Maximum = 24;
            this.nudWorkHours.Enabled = false;

            this.lblProductivityType.AutoSize = true;
            this.lblProductivityType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProductivityType.Location = new System.Drawing.Point(20, 230);
            this.lblProductivityType.Text = "Productivity Type:";

            this.cbProductivityType.Location = new System.Drawing.Point(150, 230);
            this.cbProductivityType.Size = new System.Drawing.Size(330, 24);
            this.cbProductivityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnSave.Location = new System.Drawing.Point(150, 285);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(280, 285);
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
            this.Controls.Add(this.cbProductivityType);
            this.Controls.Add(this.lblProductivityType);
            this.Controls.Add(this.nudWorkHours);
            this.Controls.Add(this.chkWorkHours);
            this.Controls.Add(this.lblWorkHours);
            this.Controls.Add(this.nudQuantityProduced);
            this.Controls.Add(this.lblQuantityProduced);
            this.Controls.Add(this.dtpProductivityDate);
            this.Controls.Add(this.lblProductivityDate);
            this.Controls.Add(this.cbWorkOrder);
            this.Controls.Add(this.lblWorkOrder);
            this.Controls.Add(this.cbPrisoner);
            this.Controls.Add(this.lblPrisoner);
            this.Controls.Add(this.lblTitle);

            this.Name = "AddEditProductivityRecordDialog";
            this.Load += new System.EventHandler(this.AddEditProductivityRecordDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantityProduced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWorkHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
