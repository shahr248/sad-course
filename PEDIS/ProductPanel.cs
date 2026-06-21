using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class ProductPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        public ProductPanel()
        {
            InitializeComponent();
        }

        private void ProductPanel_Load(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            lvProducts.Items.Clear();
            foreach (Product product in Program.Products)
            {
                ListViewItem item = new ListViewItem(product.getId().ToString());
                item.SubItems.Add(product.getName());
                item.SubItems.Add(product.getUnitOfMeasure().ToString());
                item.SubItems.Add(product.getDescription() ?? "");
                item.Tag = product;
                lvProducts.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvProducts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Product product = (Product)lvProducts.SelectedItems[0].Tag;
            string info = "ID: " + product.getId() + "\n" +
                         "Name: " + product.getName() + "\n" +
                         "Unit of Measure: " + product.getUnitOfMeasure() + "\n" +
                         "Description: " + (product.getDescription() ?? "N/A");
            MessageBox.Show(info, "Product Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Product - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvProducts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Edit Product - Feature coming soon", "Not Implemented", MessageBoxButtons.OK);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvProducts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Product product = (Product)lvProducts.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete product: " + product.getName() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                product.delete();
                refreshList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }
    }
}
