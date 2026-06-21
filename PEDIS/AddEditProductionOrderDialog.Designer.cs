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
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cbProduct;
        private System.Windows.Forms.Label lblContract;
        private System.Windows.Forms.ComboBox cbContract;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblSubmissionDate;
        private System.Windows.Forms.DateTimePicker dtpSubmissionDate;
        private System.Windows.Forms.Label lblDeliveryDeadline;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDeadline;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.ComboBox cbOrderStatus;
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
            this.lblProduct = new System.Windows.Forms.Label();
            this.cbProduct = new System.Windows.Forms.ComboBox();
            this.lblContract = new System.Windows.Forms.Label();
            this.cbContract = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblSubmissionDate = new System.Windows.Forms.Label();
            this.dtpSubmissionDate = new System.Windows.Forms.DateTimePicker();
            this.lblDeliveryDeadline = new System.Windows.Forms.Label();
            this.dtpDeliveryDeadline = new System.Windows.Forms.DateTimePicker();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.cbOrderStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.Text = "Add/Edit Production Order";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(560, 30);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderNumber.Location = new System.Drawing.Point(20, 55);
            this.lblOrderNumber.Text = "Order Number:";

            this.txtOrderNumber.Location = new System.Drawing.Point(150, 55);
            this.txtOrderNumber.Size = new System.Drawing.Size(400, 24);
            this.txtOrderNumber.ReadOnly = true;

            this.lblCustomerCompany.AutoSize = true;
            this.lblCustomerCompany.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerCompany.Location = new System.Drawing.Point(20, 90);
            this.lblCustomerCompany.Text = "Customer Company:";

            this.cbCustomerCompany.Location = new System.Drawing.Point(150, 90);
            this.cbCustomerCompany.Size = new System.Drawing.Size(400, 24);
            this.cbCustomerCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProduct.Location = new System.Drawing.Point(20, 125);
            this.lblProduct.Text = "Product:";

            this.cbProduct.Location = new System.Drawing.Point(150, 125);
            this.cbProduct.Size = new System.Drawing.Size(400, 24);
            this.cbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblContract.AutoSize = true;
            this.lblContract.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContract.Location = new System.Drawing.Point(20, 160);
            this.lblContract.Text = "Contract:";

            this.cbContract.Location = new System.Drawing.Point(150, 160);
            this.cbContract.Size = new System.Drawing.Size(400, 24);
            this.cbContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantity.Location = new System.Drawing.Point(20, 195);
            this.lblQuantity.Text = "Quantity:";

            this.txtQuantity.Location = new System.Drawing.Point(150, 195);
            this.txtQuantity.Size = new System.Drawing.Size(400, 24);

            this.lblSubmissionDate.AutoSize = true;
            this.lblSubmissionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubmissionDate.Location = new System.Drawing.Point(20, 230);
            this.lblSubmissionDate.Text = "Submission Date:";

            this.dtpSubmissionDate.Location = new System.Drawing.Point(150, 230);
            this.dtpSubmissionDate.Size = new System.Drawing.Size(400, 24);

            this.lblDeliveryDeadline.AutoSize = true;
            this.lblDeliveryDeadline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDeliveryDeadline.Location = new System.Drawing.Point(20, 265);
            this.lblDeliveryDeadline.Text = "Delivery Deadline:";

            this.dtpDeliveryDeadline.Location = new System.Drawing.Point(150, 265);
            this.dtpDeliveryDeadline.Size = new System.Drawing.Size(400, 24);

            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderStatus.Location = new System.Drawing.Point(20, 300);
            this.lblOrderStatus.Text = "Order Status:";

            this.cbOrderStatus.Location = new System.Drawing.Point(150, 300);
            this.cbOrderStatus.Size = new System.Drawing.Size(400, 24);
            this.cbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnSave.Location = new System.Drawing.Point(150, 345);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(280, 345);
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbOrderStatus);
            this.Controls.Add(this.lblOrderStatus);
            this.Controls.Add(this.dtpDeliveryDeadline);
            this.Controls.Add(this.lblDeliveryDeadline);
            this.Controls.Add(this.dtpSubmissionDate);
            this.Controls.Add(this.lblSubmissionDate);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.cbContract);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.cbProduct);
            this.Controls.Add(this.lblProduct);
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
