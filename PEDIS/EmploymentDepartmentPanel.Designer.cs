namespace PEDIS
{
    partial class EmploymentDepartmentPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lvDepartments;
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
            this.lvDepartments = new System.Windows.Forms.ListView();
            this.btnView = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employment Departments";

            // lvDepartments
            this.lvDepartments.FullRowSelect = true;
            this.lvDepartments.Location = new System.Drawing.Point(20, 50);
            this.lvDepartments.Name = "lvDepartments";
            this.lvDepartments.Size = new System.Drawing.Size(900, 300);
            this.lvDepartments.TabIndex = 1;
            this.lvDepartments.UseCompatibleStateImageBehavior = false;
            this.lvDepartments.View = System.Windows.Forms.View.Details;

            // Add columns
            this.lvDepartments.Columns.Add("ID", 50);
            this.lvDepartments.Columns.Add("Code", 100);
            this.lvDepartments.Columns.Add("Name", 150);
            this.lvDepartments.Columns.Add("Location", 150);
            this.lvDepartments.Columns.Add("Capacity", 100);
            this.lvDepartments.Columns.Add("Active", 70);

            // btnView
            this.btnView.Location = new System.Drawing.Point(20, 360);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(105, 360);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(190, 360);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(275, 360);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(20, 700);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // EmploymentDepartmentPanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lvDepartments);
            this.Controls.Add(this.lblTitle);
            this.Name = "EmploymentDepartmentPanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.EmploymentDepartmentPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
