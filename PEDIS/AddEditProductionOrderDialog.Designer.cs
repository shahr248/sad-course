namespace PEDIS
{
    partial class AddEditProductionOrderDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label lblCustomerCompany;
        private System.Windows.Forms.ComboBox cbCustomerCompany;
        private System.Windows.Forms.Label lblSubmissionDate;
        private System.Windows.Forms.DateTimePicker dtpSubmissionDate;
        private System.Windows.Forms.Label lblDeliveryDeadline;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDeadline;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.ComboBox cbOrderStatus;
        private System.Windows.Forms.Label lblProductLines;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Label lblColProduct;
        private System.Windows.Forms.Label lblColQuantity;
        private System.Windows.Forms.Label lblColPackaging;
        private System.Windows.Forms.FlowLayoutPanel pnlProductLines;
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
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblCustomerCompany = new System.Windows.Forms.Label();
            this.cbCustomerCompany = new System.Windows.Forms.ComboBox();
            this.lblSubmissionDate = new System.Windows.Forms.Label();
            this.dtpSubmissionDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveryDeadline = new System.Windows.Forms.Label();
            this.dtpDeliveryDeadline = new System.Windows.Forms.DateTimePicker();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.cbOrderStatus = new System.Windows.Forms.ComboBox();
            this.lblProductLines = new System.Windows.Forms.Label();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.lblColProduct = new System.Windows.Forms.Label();
            this.lblColQuantity = new System.Windows.Forms.Label();
            this.lblColPackaging = new System.Windows.Forms.Label();
            this.pnlProductLines = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.Text = "Add/Edit Production Order";
            this.Size = new System.Drawing.Size(700, 680);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(660, 30);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderNumber.Location = new System.Drawing.Point(20, 55);
            this.lblOrderNumber.Text = "Order Number:";

            this.txtOrderNumber.Location = new System.Drawing.Point(180, 55);
            this.txtOrderNumber.Size = new System.Drawing.Size(400, 24);
            this.txtOrderNumber.ReadOnly = true;

            this.lblCustomerCompany.AutoSize = true;
            this.lblCustomerCompany.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerCompany.Location = new System.Drawing.Point(20, 90);
            this.lblCustomerCompany.Text = "Customer Company:";

            this.cbCustomerCompany.Location = new System.Drawing.Point(180, 90);
            this.cbCustomerCompany.Size = new System.Drawing.Size(400, 24);
            this.cbCustomerCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblSubmissionDate.AutoSize = true;
            this.lblSubmissionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubmissionDate.Location = new System.Drawing.Point(20, 125);
            this.lblSubmissionDate.Text = "Order Date:";

            this.dtpSubmissionDate.Location = new System.Drawing.Point(180, 125);
            this.dtpSubmissionDate.Size = new System.Drawing.Size(400, 24);

            this.lblDeliveryDeadline.AutoSize = true;
            this.lblDeliveryDeadline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDeliveryDeadline.Location = new System.Drawing.Point(20, 160);
            this.lblDeliveryDeadline.Text = "Due Date / Deadline:";

            this.dtpDeliveryDeadline.Location = new System.Drawing.Point(180, 160);
            this.dtpDeliveryDeadline.Size = new System.Drawing.Size(400, 24);

            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderStatus.Location = new System.Drawing.Point(20, 195);
            this.lblOrderStatus.Text = "Order Status:";

            this.cbOrderStatus.Location = new System.Drawing.Point(180, 195);
            this.cbOrderStatus.Size = new System.Drawing.Size(400, 24);
            this.cbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblProductLines.AutoSize = true;
            this.lblProductLines.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProductLines.Location = new System.Drawing.Point(20, 238);
            this.lblProductLines.Text = "Product Lines:";

            this.btnAddProduct.Location = new System.Drawing.Point(540, 232);
            this.btnAddProduct.Size = new System.Drawing.Size(130, 28);
            this.btnAddProduct.Text = "Add Product +";
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);

            this.lblColProduct.AutoSize = true;
            this.lblColProduct.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColProduct.Location = new System.Drawing.Point(20, 272);
            this.lblColProduct.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblColProduct.Text = "Product";

            this.lblColQuantity.AutoSize = true;
            this.lblColQuantity.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColQuantity.Location = new System.Drawing.Point(230, 272);
            this.lblColQuantity.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblColQuantity.Text = "Quantity";

            this.lblColPackaging.AutoSize = true;
            this.lblColPackaging.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColPackaging.Location = new System.Drawing.Point(310, 272);
            this.lblColPackaging.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblColPackaging.Text = "Packaging Instructions";

            this.pnlProductLines.Location = new System.Drawing.Point(20, 292);
            this.pnlProductLines.Size = new System.Drawing.Size(650, 260);
            this.pnlProductLines.AutoScroll = true;
            this.pnlProductLines.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlProductLines.WrapContents = false;
            this.pnlProductLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProductLines.BackColor = System.Drawing.Color.White;

            this.btnSave.Location = new System.Drawing.Point(180, 575);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(310, 575);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlProductLines);
            this.Controls.Add(this.lblColPackaging);
            this.Controls.Add(this.lblColQuantity);
            this.Controls.Add(this.lblColProduct);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.lblProductLines);
            this.Controls.Add(this.cbOrderStatus);
            this.Controls.Add(this.lblOrderStatus);
            this.Controls.Add(this.dtpDeliveryDeadline);
            this.Controls.Add(this.lblDeliveryDeadline);
            this.Controls.Add(this.dtpSubmissionDate);
            this.Controls.Add(this.lblSubmissionDate);
            this.Controls.Add(this.cbCustomerCompany);
            this.Controls.Add(this.lblCustomerCompany);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.lblOrderNumber);
            this.Controls.Add(this.lblTitle);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
