namespace PEDIS
{
    partial class ProductionOrderPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTitleAccent;
        private System.Windows.Forms.Panel pnlTitleSeparator;
        private System.Windows.Forms.ListView lvOrders;
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
            this.pnlTitleAccent = new System.Windows.Forms.Panel();
            this.pnlTitleSeparator = new System.Windows.Forms.Panel();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.btnView = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(500, 35);
            this.lblTitle.Text = "Production Orders";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);

            this.pnlTitleAccent.Location = new System.Drawing.Point(16, 15);
            this.pnlTitleAccent.Size = new System.Drawing.Size(4, 35);
            this.pnlTitleAccent.BackColor = System.Drawing.Color.FromArgb(16, 42, 67);

            this.pnlTitleSeparator.Location = new System.Drawing.Point(20, 52);
            this.pnlTitleSeparator.Size = new System.Drawing.Size(984, 1);
            this.pnlTitleSeparator.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);

            this.lvOrders.FullRowSelect = true;
            this.lvOrders.GridLines = true;
            this.lvOrders.Location = new System.Drawing.Point(20, 60);
            this.lvOrders.Size = new System.Drawing.Size(984, 350);
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            this.lvOrders.BackColor = System.Drawing.Color.White;
            this.lvOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvOrders.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lvOrders.DoubleClick += new System.EventHandler(this.lvOrders_DoubleClick);

            this.lvOrders.Columns.Add("ID", 60);
            this.lvOrders.Columns.Add("Order #", 130);
            this.lvOrders.Columns.Add("Factory", 110);
            this.lvOrders.Columns.Add("Customer Company", 220);
            this.lvOrders.Columns.Add("Status", 120);
            this.lvOrders.Columns.Add("Order Date", 130);
            this.lvOrders.Columns.Add("Due Date", 130);

            this.btnView.Location = new System.Drawing.Point(20, 430);
            this.btnView.Size = new System.Drawing.Size(100, 35);
            this.btnView.Text = "View";
            this.btnView.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);

            this.btnAdd.Location = new System.Drawing.Point(130, 430);
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.Text = "Add";
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Location = new System.Drawing.Point(240, 430);
            this.btnEdit.Size = new System.Drawing.Size(100, 35);
            this.btnEdit.Text = "Edit";
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Location = new System.Drawing.Point(350, 430);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.Text = "Delete";
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnBack.Location = new System.Drawing.Point(20, 485);
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.Text = "← Back";
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lvOrders);
            this.Controls.Add(this.pnlTitleSeparator);
            this.Controls.Add(this.pnlTitleAccent);
            this.Controls.Add(this.lblTitle);
            this.Name = "ProductionOrderPanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.ProductionOrderPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
