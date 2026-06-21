using System;
using System.Windows.Forms;

namespace PEDIS
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public ComboBoxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class AddEditProductionOrderDialog : Form
    {
        private ProductionOrder orderToEdit;
        private bool isEditMode;

        private Label lblTitle;
        private Label lblOrderNumber;
        private TextBox txtOrderNumber;
        private Label lblCustomerCompany;
        private ComboBox cbCustomerCompany;
        private Label lblProduct;
        private ComboBox cbProduct;
        private Label lblContract;
        private ComboBox cbContract;
        private Label lblQuantity;
        private TextBox txtQuantity;
        private Label lblSubmissionDate;
        private DateTimePicker dtpSubmissionDate;
        private Label lblDeliveryDeadline;
        private DateTimePicker dtpDeliveryDeadline;
        private Label lblOrderStatus;
        private ComboBox cbOrderStatus;
        private Button btnSave;
        private Button btnCancel;

        public AddEditProductionOrderDialog(ProductionOrder orderToEdit = null)
        {
            this.orderToEdit = orderToEdit;
            this.isEditMode = (orderToEdit != null);
            InitializeComponent();
            if (isEditMode)
            {
                setPrisonOrderToEdit(orderToEdit);
            }
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblOrderNumber = new Label();
            this.txtOrderNumber = new TextBox();
            this.lblCustomerCompany = new Label();
            this.cbCustomerCompany = new ComboBox();
            this.lblProduct = new Label();
            this.cbProduct = new ComboBox();
            this.lblContract = new Label();
            this.cbContract = new ComboBox();
            this.lblQuantity = new Label();
            this.txtQuantity = new TextBox();
            this.lblSubmissionDate = new Label();
            this.dtpSubmissionDate = new DateTimePicker();
            this.lblDeliveryDeadline = new Label();
            this.dtpDeliveryDeadline = new DateTimePicker();
            this.lblOrderStatus = new Label();
            this.cbOrderStatus = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // Form
            this.Text = isEditMode ? "Edit Production Order" : "Add Production Order";
            this.Size = new System.Drawing.Size(600, 620);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(560, 30);
            this.lblTitle.Text = isEditMode ? "Edit Production Order" : "Add Production Order";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102);

            // Order Number
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderNumber.Location = new System.Drawing.Point(20, 55);
            this.lblOrderNumber.Size = new System.Drawing.Size(100, 19);
            this.lblOrderNumber.Text = "Order Number:";

            this.txtOrderNumber.Location = new System.Drawing.Point(150, 55);
            this.txtOrderNumber.Size = new System.Drawing.Size(400, 24);
            this.txtOrderNumber.BackColor = System.Drawing.Color.White;
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Customer Company
            this.lblCustomerCompany.AutoSize = true;
            this.lblCustomerCompany.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerCompany.Location = new System.Drawing.Point(20, 90);
            this.lblCustomerCompany.Size = new System.Drawing.Size(130, 19);
            this.lblCustomerCompany.Text = "Customer Company:";

            this.cbCustomerCompany.Location = new System.Drawing.Point(150, 90);
            this.cbCustomerCompany.Size = new System.Drawing.Size(400, 24);
            this.cbCustomerCompany.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbCustomerCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateCustomerCompanies();

            // Product
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProduct.Location = new System.Drawing.Point(20, 125);
            this.lblProduct.Size = new System.Drawing.Size(70, 19);
            this.lblProduct.Text = "Product:";

            this.cbProduct.Location = new System.Drawing.Point(150, 125);
            this.cbProduct.Size = new System.Drawing.Size(400, 24);
            this.cbProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateProducts();

            // Contract
            this.lblContract.AutoSize = true;
            this.lblContract.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContract.Location = new System.Drawing.Point(20, 160);
            this.lblContract.Size = new System.Drawing.Size(70, 19);
            this.lblContract.Text = "Contract:";

            this.cbContract.Location = new System.Drawing.Point(150, 160);
            this.cbContract.Size = new System.Drawing.Size(400, 24);
            this.cbContract.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbContract.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateContracts();

            // Quantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.Location = new System.Drawing.Point(20, 195);
            this.lblQuantity.Size = new System.Drawing.Size(70, 19);
            this.lblQuantity.Text = "Quantity:";

            this.txtQuantity.Location = new System.Drawing.Point(150, 195);
            this.txtQuantity.Size = new System.Drawing.Size(400, 24);
            this.txtQuantity.BackColor = System.Drawing.Color.White;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Submission Date
            this.lblSubmissionDate.AutoSize = true;
            this.lblSubmissionDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubmissionDate.Location = new System.Drawing.Point(20, 230);
            this.lblSubmissionDate.Size = new System.Drawing.Size(120, 19);
            this.lblSubmissionDate.Text = "Submission Date:";

            this.dtpSubmissionDate.Location = new System.Drawing.Point(150, 230);
            this.dtpSubmissionDate.Size = new System.Drawing.Size(400, 24);
            this.dtpSubmissionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpSubmissionDate.Format = DateTimePickerFormat.Short;
            this.dtpSubmissionDate.Value = DateTime.Now;

            // Delivery Deadline
            this.lblDeliveryDeadline.AutoSize = true;
            this.lblDeliveryDeadline.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDeliveryDeadline.Location = new System.Drawing.Point(20, 265);
            this.lblDeliveryDeadline.Size = new System.Drawing.Size(130, 19);
            this.lblDeliveryDeadline.Text = "Delivery Deadline:";

            this.dtpDeliveryDeadline.Location = new System.Drawing.Point(150, 265);
            this.dtpDeliveryDeadline.Size = new System.Drawing.Size(400, 24);
            this.dtpDeliveryDeadline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDeliveryDeadline.Format = DateTimePickerFormat.Short;
            this.dtpDeliveryDeadline.Value = DateTime.Now.AddDays(7);

            // Order Status
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderStatus.Location = new System.Drawing.Point(20, 300);
            this.lblOrderStatus.Size = new System.Drawing.Size(100, 19);
            this.lblOrderStatus.Text = "Order Status:";

            this.cbOrderStatus.Location = new System.Drawing.Point(150, 300);
            this.cbOrderStatus.Size = new System.Drawing.Size(400, 24);
            this.cbOrderStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            PopulateOrderStatuses();

            // Save Button
            this.btnSave.Location = new System.Drawing.Point(150, 540);
            this.btnSave.Size = new System.Drawing.Size(150, 35);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Cancel Button
            this.btnCancel.Location = new System.Drawing.Point(310, 540);
            this.btnCancel.Size = new System.Drawing.Size(150, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;

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

        private void PopulateCustomerCompanies()
        {
            cbCustomerCompany.Items.Clear();
            foreach (CustomerCompany cc in Program.CustomerCompanies)
            {
                string displayText = cc.getName() ?? "Unknown";
                cbCustomerCompany.Items.Add(new ComboBoxItem(displayText, cc));
            }
        }

        private void PopulateProducts()
        {
            cbProduct.Items.Clear();
            foreach (Product p in Program.Products)
            {
                string displayText = p.getName() ?? "Unknown";
                cbProduct.Items.Add(new ComboBoxItem(displayText, p));
            }
        }

        private void PopulateContracts()
        {
            cbContract.Items.Clear();
            foreach (Contract c in Program.Contracts)
            {
                string displayText = c.getContractNumber() ?? "Unknown";
                cbContract.Items.Add(new ComboBoxItem(displayText, c));
            }
        }

        private void PopulateOrderStatuses()
        {
            cbOrderStatus.Items.Clear();
            foreach (ProductionOrderStatus status in System.Enum.GetValues(typeof(ProductionOrderStatus)))
            {
                cbOrderStatus.Items.Add(status);
            }
            cbOrderStatus.SelectedIndex = 0;
        }

        public void setPrisonOrderToEdit(ProductionOrder po)
        {
            if (po == null)
                return;

            txtOrderNumber.Text = po.getOrderNumber();

            // Set Customer Company
            CustomerCompany cc = po.getCustomerCompany();
            if (cc != null)
            {
                for (int i = 0; i < cbCustomerCompany.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)cbCustomerCompany.Items[i];
                    if ((CustomerCompany)item.Value == cc)
                    {
                        cbCustomerCompany.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Set Product
            Product p = po.getProduct();
            if (p != null)
            {
                for (int i = 0; i < cbProduct.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)cbProduct.Items[i];
                    if ((Product)item.Value == p)
                    {
                        cbProduct.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Set Contract
            Contract c = po.getContract();
            if (c != null)
            {
                for (int i = 0; i < cbContract.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)cbContract.Items[i];
                    if ((Contract)item.Value == c)
                    {
                        cbContract.SelectedIndex = i;
                        break;
                    }
                }
            }

            txtQuantity.Text = po.getQuantity().ToString();
            dtpSubmissionDate.Value = po.getSubmissionDate();
            dtpDeliveryDeadline.Value = po.getDeliveryDeadline();
            cbOrderStatus.SelectedItem = po.getOrderStatus();
        }

        private bool validateInput()
        {
            // Check Order Number is not empty
            if (string.IsNullOrWhiteSpace(txtOrderNumber.Text))
            {
                MessageBox.Show("Order Number cannot be empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check for uniqueness (if in Add mode)
            if (!isEditMode)
            {
                foreach (ProductionOrder po in Program.ProductionOrders)
                {
                    if (po.getOrderNumber() == txtOrderNumber.Text)
                    {
                        MessageBox.Show("Order Number already exists", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            // Check Customer Company is selected
            if (cbCustomerCompany.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Customer Company", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check Product is selected
            if (cbProduct.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Product", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check Contract is selected
            if (cbContract.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Contract", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Quantity
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Dates
            if (dtpDeliveryDeadline.Value <= dtpSubmissionDate.Value)
            {
                MessageBox.Show("Delivery Deadline must be after Submission Date", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private ProductionOrder getProductionOrderData()
        {
            CustomerCompany cc = ((ComboBoxItem)cbCustomerCompany.SelectedItem).Value as CustomerCompany;
            Product p = ((ComboBoxItem)cbProduct.SelectedItem).Value as Product;
            Contract c = ((ComboBoxItem)cbContract.SelectedItem).Value as Contract;
            int quantity = int.Parse(txtQuantity.Text);
            ProductionOrderStatus status = (ProductionOrderStatus)cbOrderStatus.SelectedItem;

            if (isEditMode)
            {
                // Update existing
                orderToEdit.setQuantity(quantity);
                orderToEdit.setDeliveryDeadline(dtpDeliveryDeadline.Value);
                orderToEdit.setOrderStatus(status);
                return orderToEdit;
            }
            else
            {
                // Create new
                int nextId = Program.ProductionOrders.Count + 1;
                ProductionOrder po = new ProductionOrder(
                    nextId,
                    txtOrderNumber.Text,
                    cc.getId(),
                    p.getId(),
                    c.getId(),
                    quantity,
                    0,
                    dtpSubmissionDate.Value,
                    dtpDeliveryDeadline.Value,
                    status,
                    true
                );
                return po;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!validateInput())
                return;

            try
            {
                ProductionOrder po = getProductionOrderData();
                if (isEditMode)
                {
                    po.update();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving Production Order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
