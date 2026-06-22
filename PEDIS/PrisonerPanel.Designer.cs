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

        // Fixed state-machine Actions area — buttons are created here at design time
        // and shown/hidden at runtime by updateAvailableActions(), never created dynamically.
        private System.Windows.Forms.Label lblActions;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAssignToFactory;
        private System.Windows.Forms.ComboBox cmbAssignFactory;
        private System.Windows.Forms.Button btnStartProfessionalTraining;
        private System.Windows.Forms.Button btnCompleteProfessionalTraining;
        private System.Windows.Forms.Button btnStartSafetyTraining;
        private System.Windows.Forms.Button btnCompleteSafetyTraining;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.TextBox txtSuspendReason;
        private System.Windows.Forms.Button btnReactivate;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnClockIn;
        private System.Windows.Forms.Button btnClockOut;

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
            this.lblActions = new System.Windows.Forms.Label();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAssignToFactory = new System.Windows.Forms.Button();
            this.cmbAssignFactory = new System.Windows.Forms.ComboBox();
            this.btnStartProfessionalTraining = new System.Windows.Forms.Button();
            this.btnCompleteProfessionalTraining = new System.Windows.Forms.Button();
            this.btnStartSafetyTraining = new System.Windows.Forms.Button();
            this.btnCompleteSafetyTraining = new System.Windows.Forms.Button();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.txtSuspendReason = new System.Windows.Forms.TextBox();
            this.btnReactivate = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnClockIn = new System.Windows.Forms.Button();
            this.btnClockOut = new System.Windows.Forms.Button();
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
            this.lvPrisoners.Size = new System.Drawing.Size(984, 350);
            this.lvPrisoners.TabIndex = 1;
            this.lvPrisoners.UseCompatibleStateImageBehavior = false;
            this.lvPrisoners.View = System.Windows.Forms.View.Details;
            this.lvPrisoners.BackColor = System.Drawing.Color.White;
            this.lvPrisoners.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lvPrisoners.SelectedIndexChanged += new System.EventHandler(this.lvPrisoners_SelectedIndexChanged);

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
            this.btnBack.Location = new System.Drawing.Point(20, 500);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "← Back";
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // lblActions
            this.lblActions.AutoSize = false;
            this.lblActions.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblActions.Location = new System.Drawing.Point(20, 545);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(400, 25);
            this.lblActions.Text = "Actions for Selected Prisoner";
            this.lblActions.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            // btnApprove
            this.btnApprove.Location = new System.Drawing.Point(20, 580);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(110, 35);
            this.btnApprove.Text = "Approve";
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Visible = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);

            // btnReject
            this.btnReject.Location = new System.Drawing.Point(140, 580);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(110, 35);
            this.btnReject.Text = "Reject";
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Visible = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);

            // btnAssignToFactory
            this.btnAssignToFactory.Location = new System.Drawing.Point(260, 580);
            this.btnAssignToFactory.Name = "btnAssignToFactory";
            this.btnAssignToFactory.Size = new System.Drawing.Size(150, 35);
            this.btnAssignToFactory.Text = "Assign to Factory";
            this.btnAssignToFactory.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnAssignToFactory.ForeColor = System.Drawing.Color.White;
            this.btnAssignToFactory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAssignToFactory.UseVisualStyleBackColor = false;
            this.btnAssignToFactory.Visible = false;
            this.btnAssignToFactory.Click += new System.EventHandler(this.btnAssignToFactory_Click);

            // cmbAssignFactory
            this.cmbAssignFactory.Location = new System.Drawing.Point(420, 587);
            this.cmbAssignFactory.Name = "cmbAssignFactory";
            this.cmbAssignFactory.Size = new System.Drawing.Size(130, 25);
            this.cmbAssignFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignFactory.Visible = false;

            // btnStartProfessionalTraining
            this.btnStartProfessionalTraining.Location = new System.Drawing.Point(560, 580);
            this.btnStartProfessionalTraining.Name = "btnStartProfessionalTraining";
            this.btnStartProfessionalTraining.Size = new System.Drawing.Size(190, 35);
            this.btnStartProfessionalTraining.Text = "Start Professional Training";
            this.btnStartProfessionalTraining.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnStartProfessionalTraining.ForeColor = System.Drawing.Color.White;
            this.btnStartProfessionalTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartProfessionalTraining.UseVisualStyleBackColor = false;
            this.btnStartProfessionalTraining.Visible = false;
            this.btnStartProfessionalTraining.Click += new System.EventHandler(this.btnStartProfessionalTraining_Click);

            // btnCompleteProfessionalTraining
            this.btnCompleteProfessionalTraining.Location = new System.Drawing.Point(20, 620);
            this.btnCompleteProfessionalTraining.Name = "btnCompleteProfessionalTraining";
            this.btnCompleteProfessionalTraining.Size = new System.Drawing.Size(210, 35);
            this.btnCompleteProfessionalTraining.Text = "Complete Professional Training";
            this.btnCompleteProfessionalTraining.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnCompleteProfessionalTraining.ForeColor = System.Drawing.Color.White;
            this.btnCompleteProfessionalTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCompleteProfessionalTraining.UseVisualStyleBackColor = false;
            this.btnCompleteProfessionalTraining.Visible = false;
            this.btnCompleteProfessionalTraining.Click += new System.EventHandler(this.btnCompleteProfessionalTraining_Click);

            // btnStartSafetyTraining
            this.btnStartSafetyTraining.Location = new System.Drawing.Point(240, 620);
            this.btnStartSafetyTraining.Name = "btnStartSafetyTraining";
            this.btnStartSafetyTraining.Size = new System.Drawing.Size(160, 35);
            this.btnStartSafetyTraining.Text = "Start Safety Training";
            this.btnStartSafetyTraining.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnStartSafetyTraining.ForeColor = System.Drawing.Color.White;
            this.btnStartSafetyTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartSafetyTraining.UseVisualStyleBackColor = false;
            this.btnStartSafetyTraining.Visible = false;
            this.btnStartSafetyTraining.Click += new System.EventHandler(this.btnStartSafetyTraining_Click);

            // btnCompleteSafetyTraining
            this.btnCompleteSafetyTraining.Location = new System.Drawing.Point(410, 620);
            this.btnCompleteSafetyTraining.Name = "btnCompleteSafetyTraining";
            this.btnCompleteSafetyTraining.Size = new System.Drawing.Size(170, 35);
            this.btnCompleteSafetyTraining.Text = "Complete Safety Training";
            this.btnCompleteSafetyTraining.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnCompleteSafetyTraining.ForeColor = System.Drawing.Color.White;
            this.btnCompleteSafetyTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCompleteSafetyTraining.UseVisualStyleBackColor = false;
            this.btnCompleteSafetyTraining.Visible = false;
            this.btnCompleteSafetyTraining.Click += new System.EventHandler(this.btnCompleteSafetyTraining_Click);

            // btnSuspend
            this.btnSuspend.Location = new System.Drawing.Point(20, 660);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(100, 35);
            this.btnSuspend.Text = "Suspend";
            this.btnSuspend.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnSuspend.ForeColor = System.Drawing.Color.White;
            this.btnSuspend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuspend.UseVisualStyleBackColor = false;
            this.btnSuspend.Visible = false;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);

            // txtSuspendReason
            this.txtSuspendReason.Location = new System.Drawing.Point(130, 668);
            this.txtSuspendReason.Name = "txtSuspendReason";
            this.txtSuspendReason.Size = new System.Drawing.Size(200, 25);
            this.txtSuspendReason.PlaceholderText = "Reason (optional)";
            this.txtSuspendReason.Visible = false;

            // btnReactivate
            this.btnReactivate.Location = new System.Drawing.Point(340, 660);
            this.btnReactivate.Name = "btnReactivate";
            this.btnReactivate.Size = new System.Drawing.Size(110, 35);
            this.btnReactivate.Text = "Reactivate";
            this.btnReactivate.BackColor = System.Drawing.Color.FromArgb(26, 188, 156);
            this.btnReactivate.ForeColor = System.Drawing.Color.White;
            this.btnReactivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReactivate.UseVisualStyleBackColor = false;
            this.btnReactivate.Visible = false;
            this.btnReactivate.Click += new System.EventHandler(this.btnReactivate_Click);

            // btnRelease
            this.btnRelease.Location = new System.Drawing.Point(460, 660);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(100, 35);
            this.btnRelease.Text = "Release";
            this.btnRelease.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnRelease.ForeColor = System.Drawing.Color.White;
            this.btnRelease.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Visible = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);

            // btnClockIn
            this.btnClockIn.Location = new System.Drawing.Point(20, 705);
            this.btnClockIn.Name = "btnClockIn";
            this.btnClockIn.Size = new System.Drawing.Size(120, 35);
            this.btnClockIn.Text = "Clock In";
            this.btnClockIn.BackColor = System.Drawing.Color.FromArgb(26, 188, 156);
            this.btnClockIn.ForeColor = System.Drawing.Color.White;
            this.btnClockIn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClockIn.UseVisualStyleBackColor = false;
            this.btnClockIn.Visible = false;
            this.btnClockIn.Click += new System.EventHandler(this.btnClockIn_Click);

            // btnClockOut
            this.btnClockOut.Location = new System.Drawing.Point(150, 705);
            this.btnClockOut.Name = "btnClockOut";
            this.btnClockOut.Size = new System.Drawing.Size(120, 35);
            this.btnClockOut.Text = "Clock Out";
            this.btnClockOut.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.btnClockOut.ForeColor = System.Drawing.Color.White;
            this.btnClockOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClockOut.UseVisualStyleBackColor = false;
            this.btnClockOut.Visible = false;
            this.btnClockOut.Click += new System.EventHandler(this.btnClockOut_Click);

            // PrisonerPanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.btnClockOut);
            this.Controls.Add(this.btnClockIn);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnReactivate);
            this.Controls.Add(this.txtSuspendReason);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.btnCompleteSafetyTraining);
            this.Controls.Add(this.btnStartSafetyTraining);
            this.Controls.Add(this.btnCompleteProfessionalTraining);
            this.Controls.Add(this.btnStartProfessionalTraining);
            this.Controls.Add(this.cmbAssignFactory);
            this.Controls.Add(this.btnAssignToFactory);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.lblActions);
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
