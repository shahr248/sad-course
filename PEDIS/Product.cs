using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PEDIS
{
    public class Product
    {
        private int productId;
        private string sku;
        private string productName;
        private string description;
        private string packagingInstructions;
        private UnitOfMeasure unitOfMeasure;
        private string activityStatus;
        private Factory factory;
        private List<Contract> contracts;
        private List<ProductionOrder> productionOrders;

        public Product(int id, string sku, string name, string desc, string packing, UnitOfMeasure unit, string status, Factory factory, bool isNew)
        {
            this.productId = id;
            this.sku = sku;
            this.productName = name;
            this.description = desc;
            this.packagingInstructions = packing;
            this.unitOfMeasure = unit;
            this.activityStatus = status;
            this.factory = factory;
            if (isNew)
            {
                this.create();
                Program.Products.Add(this);
            }
        }

        public int getId() { return this.productId; }
        public string getSku() { return this.sku; }
        public string getName() { return this.productName; }
        public string getDescription() { return this.description; }
        public string getPackagingInstructions() { return this.packagingInstructions; }
        public UnitOfMeasure getUnitOfMeasure() { return this.unitOfMeasure; }
        public string getActivityStatus() { return this.activityStatus; }
        public Factory getFactory() { return this.factory; }

        public void setSku(string sku) { this.sku = sku; }
        public void setName(string name) { this.productName = name; }
        public void setDescription(string desc) { this.description = desc; }
        public void setPackagingInstructions(string packing) { this.packagingInstructions = packing; }
        public void setUnitOfMeasure(UnitOfMeasure unit) { this.unitOfMeasure = unit; }
        public void setActivityStatus(string status) { this.activityStatus = status; }
        public void setFactory(Factory factory) { this.factory = factory; }

        public List<Contract> getContracts()
        {
            if (contracts == null)
                contracts = new List<Contract>();
            return contracts;
        }

        public void addContract(Contract contract)
        {
            if (contract == null) return;
            if (this.contracts == null) this.contracts = new List<Contract>();
            if (!this.contracts.Contains(contract)) this.contracts.Add(contract);
        }

        public void removeContract(Contract contract)
        {
            if (contract == null) return;
            if (this.contracts != null && this.contracts.Contains(contract)) this.contracts.Remove(contract);
        }

        public List<ProductionOrder> getProductionOrders()
        {
            if (productionOrders == null)
                productionOrders = new List<ProductionOrder>();
            return productionOrders;
        }

        public void addProductionOrder(ProductionOrder order)
        {
            if (order == null) return;
            if (this.productionOrders == null) this.productionOrders = new List<ProductionOrder>();
            if (!this.productionOrders.Contains(order)) this.productionOrders.Add(order);
        }

        public void removeProductionOrder(ProductionOrder order)
        {
            if (order == null) return;
            if (this.productionOrders != null && this.productionOrders.Contains(order)) this.productionOrders.Remove(order);
        }

        public void create()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Product_create @product_id, @sku, @product_name, @description, @packaging_instructions, @unit_of_measure, @activity_status, @factory";
            cmd.Parameters.AddWithValue("@product_id", this.productId);
            cmd.Parameters.AddWithValue("@sku", this.sku);
            cmd.Parameters.AddWithValue("@product_name", this.productName);
            cmd.Parameters.AddWithValue("@description", this.description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@packaging_instructions", this.packagingInstructions ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@unit_of_measure", EnumHelpers.ToDbString(this.unitOfMeasure));
            cmd.Parameters.AddWithValue("@activity_status", this.activityStatus ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void update()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Product_update @product_id, @sku, @product_name, @description, @packaging_instructions, @unit_of_measure, @activity_status, @factory";
            cmd.Parameters.AddWithValue("@product_id", this.productId);
            cmd.Parameters.AddWithValue("@sku", this.sku);
            cmd.Parameters.AddWithValue("@product_name", this.productName);
            cmd.Parameters.AddWithValue("@description", this.description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@packaging_instructions", this.packagingInstructions ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@unit_of_measure", EnumHelpers.ToDbString(this.unitOfMeasure));
            cmd.Parameters.AddWithValue("@activity_status", this.activityStatus ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@factory", EnumHelpers.ToDbString(this.factory));
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public void delete()
        {
            Program.Products.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Product_delete @product_id";
            cmd.Parameters.AddWithValue("@product_id", this.productId);
            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);
        }

        public static void initProducts()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_Product_get_all";
            SQL_CON sc = new SQL_CON();
            SqlDataReader reader = sc.execute_query(cmd);

            Program.Products = new List<Product>();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string sku = reader.GetValue(1).ToString();
                string name = reader.GetValue(2).ToString();
                string desc = reader.IsDBNull(3) ? null : reader.GetValue(3).ToString();
                string packing = reader.IsDBNull(4) ? null : reader.GetValue(4).ToString();
                UnitOfMeasure unit = EnumHelpers.UnitOfMeasureFromDb(reader.GetValue(5).ToString());
                string status = reader.IsDBNull(6) ? null : reader.GetValue(6).ToString();
                // index 7/8 are created_at/modified_at (not modeled on this entity);
                // factory is appended after them (see create_database.sql)
                Factory factory = EnumHelpers.FactoryFromDb(reader.GetValue(9).ToString());

                Product p = new Product(id, sku, name, desc, packing, unit, status, factory, false);
                Program.Products.Add(p);
            }
            reader.Close();
        }

        public static Product seekById(int id)
        {
            foreach (Product p in Program.Products)
            {
                if (p.getId() == id)
                    return p;
            }
            return null;
        }
    }
}
