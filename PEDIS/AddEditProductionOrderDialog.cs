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

    public partial class AddEditProductionOrderDialog : Form
    {
        private ProductionOrder orderToEdit;
        private bool isEditMode;

        public AddEditProductionOrderDialog(ProductionOrder orderToEdit = null)
        {
            this.orderToEdit = orderToEdit;
            this.isEditMode = (orderToEdit != null);
            InitializeComponent();
            PopulateCustomerCompanies();
            PopulateProducts();
            PopulateContracts();
            PopulateOrderStatuses();
            if (isEditMode)
            {
                setPrisonOrderToEdit(orderToEdit);
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
            foreach (var statusValue in System.Enum.GetValues(typeof(ProductionOrderStatus)))
            {
                ProductionOrderStatus status = (ProductionOrderStatus)statusValue;
                cbOrderStatus.Items.Add(status);
            }
            if (cbOrderStatus.Items.Count > 0)
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
                // Create new - find max ID to avoid duplicates
                int maxId = 0;
                foreach (ProductionOrder po in Program.ProductionOrders)
                {
                    if (po.getId() > maxId)
                        maxId = po.getId();
                }
                int nextId = maxId + 1;

                ProductionOrder newPO = new ProductionOrder(
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
