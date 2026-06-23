using System;
using System.Collections.Generic;
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

    public partial class AddEditProductionOrderDialog : Form
    {
        private class ProductLineRow
        {
            public Panel Container;
            public ComboBox cbProduct;
            public TextBox txtQuantity;
            public TextBox txtPackagingInstructions;
            public Button btnRemove;
        }

        private ProductionOrder orderToEdit;
        private bool isEditMode;
        private List<ProductLineRow> productLines = new List<ProductLineRow>();

        public AddEditProductionOrderDialog(ProductionOrder orderToEdit = null)
        {
            this.orderToEdit = orderToEdit;
            this.isEditMode = (orderToEdit != null);
            InitializeComponent();
            PopulateCustomerCompanies();
            PopulateOrderStatuses();
            if (isEditMode)
            {
                setPrisonOrderToEdit(orderToEdit);
            }
            else
            {
                txtOrderNumber.Text = generateNextOrderNumber();
                dtpSubmissionDate.Value = DateTime.Now;
                dtpDeliveryDeadline.Value = DateTime.Now;
                AddProductLineRow(true);
            }
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

        private void PopulateOrderStatuses()
        {
            cbOrderStatus.Items.Clear();
            foreach (var statusValue in System.Enum.GetValues(typeof(ProductionOrderStatus)))
            {
                ProductionOrderStatus status = (ProductionOrderStatus)statusValue;
                cbOrderStatus.Items.Add(status);
            }
            if (cbOrderStatus.Items.Count > 0)
                cbOrderStatus.SelectedIndex = 0;
        }

        private string generateNextOrderNumber()
        {
            int year = DateTime.Now.Year;
            int maxSeq = 0;
            string prefix = $"ORD-{year}-";
            foreach (ProductionOrder po in Program.ProductionOrders)
            {
                string num = po.getOrderNumber();
                if (num != null && num.StartsWith(prefix) &&
                    int.TryParse(num.Substring(prefix.Length), out int seq) && seq > maxSeq)
                    maxSeq = seq;
            }
            return prefix + (maxSeq + 1).ToString("D4");
        }

        private string generateNextWorkOrderNumber()
        {
            int year = DateTime.Now.Year;
            int maxSeq = 0;
            string prefix = $"WO-{year}-";
            foreach (WorkOrder wo in Program.WorkOrders)
            {
                string num = wo.getWorkOrderNumber();
                if (num != null && num.StartsWith(prefix) &&
                    int.TryParse(num.Substring(prefix.Length), out int seq) && seq > maxSeq)
                    maxSeq = seq;
            }
            return prefix + (maxSeq + 1).ToString("D4");
        }

        private int getNextWorkOrderId()
        {
            int maxId = 0;
            foreach (WorkOrder wo in Program.WorkOrders)
                if (wo.getId() > maxId) maxId = wo.getId();
            return maxId + 1;
        }

        private ProductLineRow AddProductLineRow(bool editable, Product preselectedProduct = null, int? quantity = null, string packagingInstructions = null)
        {
            ProductLineRow row = new ProductLineRow();

            row.Container = new Panel();
            row.Container.Size = new System.Drawing.Size(630, 32);
            row.Container.Margin = new Padding(0, 0, 0, 4);

            row.cbProduct = new ComboBox();
            row.cbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            row.cbProduct.Location = new System.Drawing.Point(0, 4);
            row.cbProduct.Size = new System.Drawing.Size(200, 24);
            foreach (Product p in Program.Products)
            {
                string displayText = p.getName() ?? "Unknown";
                row.cbProduct.Items.Add(new ComboBoxItem(displayText, p));
            }
            if (preselectedProduct != null)
            {
                for (int i = 0; i < row.cbProduct.Items.Count; i++)
                {
                    if (((ComboBoxItem)row.cbProduct.Items[i]).Value == (object)preselectedProduct)
                    {
                        row.cbProduct.SelectedIndex = i;
                        break;
                    }
                }
            }
            row.cbProduct.Enabled = editable;

            row.txtQuantity = new TextBox();
            row.txtQuantity.Location = new System.Drawing.Point(210, 4);
            row.txtQuantity.Size = new System.Drawing.Size(70, 24);
            row.txtQuantity.Text = quantity.HasValue ? quantity.Value.ToString() : "";
            row.txtQuantity.Enabled = editable;

            row.txtPackagingInstructions = new TextBox();
            row.txtPackagingInstructions.Location = new System.Drawing.Point(290, 4);
            row.txtPackagingInstructions.Size = new System.Drawing.Size(300, 24);
            row.txtPackagingInstructions.Text = packagingInstructions ?? "";
            row.txtPackagingInstructions.Enabled = editable;

            row.btnRemove = new Button();
            row.btnRemove.Location = new System.Drawing.Point(600, 2);
            row.btnRemove.Size = new System.Drawing.Size(28, 28);
            row.btnRemove.Text = "X";
            row.btnRemove.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            row.btnRemove.ForeColor = System.Drawing.Color.White;
            row.btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            row.btnRemove.UseVisualStyleBackColor = false;
            row.btnRemove.Visible = editable;
            row.btnRemove.Click += (s, e) => RemoveProductLineRow(row);

            row.Container.Controls.Add(row.cbProduct);
            row.Container.Controls.Add(row.txtQuantity);
            row.Container.Controls.Add(row.txtPackagingInstructions);
            row.Container.Controls.Add(row.btnRemove);

            pnlProductLines.Controls.Add(row.Container);
            productLines.Add(row);
            return row;
        }

        private void RemoveProductLineRow(ProductLineRow row)
        {
            pnlProductLines.Controls.Remove(row.Container);
            productLines.Remove(row);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductLineRow(true);
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

            dtpSubmissionDate.Value = po.getSubmissionDate();
            dtpDeliveryDeadline.Value = po.getDeliveryDeadline();
            cbOrderStatus.SelectedItem = po.getOrderStatus();

            // Product lines are read-only in Edit mode for now (see plan §7)
            btnAddProduct.Visible = false;
            foreach (WorkOrder wo in po.getWorkOrders())
            {
                AddProductLineRow(false, wo.getProduct(), wo.getRequiredQuantity(), wo.getPackagingInstructions());
            }
        }

        private bool validateInput()
        {
            if (cbCustomerCompany.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Customer Company", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpDeliveryDeadline.Value < dtpSubmissionDate.Value)
            {
                MessageBox.Show("Due Date cannot be before Order Date", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!isEditMode)
            {
                if (productLines.Count == 0)
                {
                    MessageBox.Show("Please add at least one product line", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                foreach (ProductLineRow row in productLines)
                {
                    if (row.cbProduct.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a Product for every product line", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (!int.TryParse(row.txtQuantity.Text, out int qty) || qty <= 0)
                    {
                        MessageBox.Show("Quantity must be a positive number for every product line", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // Order Number is auto-generated, but double-check uniqueness defensively
                foreach (ProductionOrder po in Program.ProductionOrders)
                {
                    if (po.getOrderNumber() == txtOrderNumber.Text)
                    {
                        MessageBox.Show("Order Number already exists, please reopen the dialog and try again", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private ProductionOrder getProductionOrderData()
        {
            CustomerCompany cc = ((ComboBoxItem)cbCustomerCompany.SelectedItem).Value as CustomerCompany;
            ProductionOrderStatus status = (ProductionOrderStatus)cbOrderStatus.SelectedItem;

            if (isEditMode)
            {
                orderToEdit.setCustomerCompanyId(cc.getId());
                orderToEdit.setSubmissionDate(dtpSubmissionDate.Value);
                orderToEdit.setDeliveryDeadline(dtpDeliveryDeadline.Value);
                orderToEdit.setOrderStatus(status);
                return orderToEdit;
            }
            else
            {
                int quantity = 0;
                foreach (ProductLineRow row in productLines)
                    quantity += int.Parse(row.txtQuantity.Text);

                int maxId = 0;
                foreach (ProductionOrder po in Program.ProductionOrders)
                    if (po.getId() > maxId) maxId = po.getId();
                int nextId = maxId + 1;

                ProductionOrder newPO = new ProductionOrder(
                    nextId,
                    txtOrderNumber.Text,
                    cc.getId(),
                    null,
                    null,
                    quantity,
                    0,
                    dtpSubmissionDate.Value,
                    dtpDeliveryDeadline.Value,
                    status,
                    true
                );

                foreach (ProductLineRow row in productLines)
                {
                    Product p = ((ComboBoxItem)row.cbProduct.SelectedItem).Value as Product;
                    int lineQty = int.Parse(row.txtQuantity.Text);
                    string packaging = string.IsNullOrWhiteSpace(row.txtPackagingInstructions.Text) ? null : row.txtPackagingInstructions.Text;

                    new WorkOrder(
                        getNextWorkOrderId(),
                        generateNextWorkOrderNumber(),
                        nextId,
                        lineQty,
                        dtpSubmissionDate.Value,
                        dtpDeliveryDeadline.Value,
                        WorkOrderStatus.HasntEnteredIntoProductionYet,
                        null,
                        p.getId(),
                        packaging,
                        true
                    );
                }

                return newPO;
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
