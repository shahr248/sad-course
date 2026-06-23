-- ============================================================================
-- Migration: Move Product from ProductionOrder (one per order) to WorkOrder
-- (one per line item), and drop the mandatory Contract link from
-- ProductionOrder.
--
-- ProductionOrder used to model "one product, one contract, one quantity"
-- per order. The new Add Order flow lets a user add multiple product lines
-- (Product + Quantity + Packaging Instructions) under a single order, each
-- becoming its own WorkOrder. Contract is no longer collected when creating
-- an order.
--
-- Steps:
--   1. Make ProductionOrder.product_id and contract_id nullable.
--   2. Add nullable product_id / packaging_instructions to WorkOrder.
--   3. Backfill existing WorkOrder rows' product_id from their parent
--      ProductionOrder (today's data is strictly 1 order : 1 product :
--      1 WorkOrder, so this is unambiguous).
--   4. Constrain WorkOrder.product_id to NOT NULL + FK, now that every row
--      has a value.
--   5. Update sp_ProductionOrder_create / _update: @product_id and
--      @contract_id become optional.
--   6. Update sp_WorkOrder_create / _update: accept @product_id and
--      @packaging_instructions.
--
-- IMPORTANT: WorkOrder.initWorkOrders() in C# reads columns positionally
-- (SELECT *), so product_id/packaging_instructions are appended via ALTER
-- TABLE here (after created_at/modified_at) rather than inserted next to
-- required_quantity. create_database.sql mirrors this same column order for
-- fresh installs so a migrated DB and a fresh DB read identically by index.
--
-- Safe to re-run.
-- ============================================================================

USE sadcourse3;
GO

-- ----------------------------------------------------------------------------
-- 1. ProductionOrder.product_id / contract_id: NOT NULL -> NULL
-- ----------------------------------------------------------------------------
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('ProductionOrder') AND name = 'product_id' AND is_nullable = 0
)
BEGIN
    ALTER TABLE ProductionOrder ALTER COLUMN product_id INT NULL;
END;
GO

IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('ProductionOrder') AND name = 'contract_id' AND is_nullable = 0
)
BEGIN
    ALTER TABLE ProductionOrder ALTER COLUMN contract_id INT NULL;
END;
GO

-- ----------------------------------------------------------------------------
-- 2. Add nullable product_id / packaging_instructions to WorkOrder
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('WorkOrder') AND name = 'product_id'
)
BEGIN
    ALTER TABLE WorkOrder ADD product_id INT NULL;
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('WorkOrder') AND name = 'packaging_instructions'
)
BEGIN
    ALTER TABLE WorkOrder ADD packaging_instructions NVARCHAR(MAX) NULL;
END;
GO

-- ----------------------------------------------------------------------------
-- 3. Backfill WorkOrder.product_id from the parent ProductionOrder
-- ----------------------------------------------------------------------------
UPDATE wo
SET wo.product_id = po.product_id
FROM WorkOrder wo
INNER JOIN ProductionOrder po ON po.production_order_id = wo.production_order_id
WHERE wo.product_id IS NULL;
GO

-- ----------------------------------------------------------------------------
-- 4. Constrain WorkOrder.product_id to NOT NULL + FK
-- ----------------------------------------------------------------------------
ALTER TABLE WorkOrder ALTER COLUMN product_id INT NOT NULL;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_WorkOrder_Product'
)
BEGIN
    ALTER TABLE WorkOrder
        ADD CONSTRAINT FK_WorkOrder_Product FOREIGN KEY (product_id)
        REFERENCES Product(product_id) ON DELETE NO ACTION ON UPDATE NO ACTION;
END;
GO

-- ----------------------------------------------------------------------------
-- 5. sp_ProductionOrder_create / sp_ProductionOrder_update: optional product/contract
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_ProductionOrder_create
    @production_order_id INT,
    @order_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @contract_id INT = NULL,
    @quantity INT,
    @completed_quantity INT = 0,
    @submission_date DATE,
    @delivery_deadline DATE,
    @order_status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ProductionOrder
        (production_order_id, order_number, customer_company_id, product_id, contract_id, quantity, completed_quantity, submission_date, delivery_deadline, order_status, created_at)
    VALUES
        (@production_order_id, @order_number, @customer_company_id, @product_id, @contract_id, @quantity, @completed_quantity, @submission_date, @delivery_deadline, @order_status, GETUTCDATE());
END;
GO

ALTER PROCEDURE sp_ProductionOrder_update
    @production_order_id INT,
    @order_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @contract_id INT = NULL,
    @quantity INT,
    @completed_quantity INT = 0,
    @submission_date DATE,
    @delivery_deadline DATE,
    @order_status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE ProductionOrder
    SET order_number = @order_number,
        customer_company_id = @customer_company_id,
        product_id = @product_id,
        contract_id = @contract_id,
        quantity = @quantity,
        completed_quantity = @completed_quantity,
        submission_date = @submission_date,
        delivery_deadline = @delivery_deadline,
        order_status = @order_status,
        modified_at = GETUTCDATE()
    WHERE production_order_id = @production_order_id;
END;
GO

-- ----------------------------------------------------------------------------
-- 6. sp_WorkOrder_create / sp_WorkOrder_update: accept product_id / packaging_instructions
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_WorkOrder_create
    @work_order_id INT,
    @work_order_number NVARCHAR(50),
    @production_order_id INT,
    @required_quantity INT,
    @start_date DATE,
    @deadline DATE,
    @status NVARCHAR(50),
    @hold_reason NVARCHAR(MAX) = NULL,
    @product_id INT,
    @packaging_instructions NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO WorkOrder
        (work_order_id, work_order_number, production_order_id, required_quantity, start_date, deadline, status, hold_reason, created_at, product_id, packaging_instructions)
    VALUES
        (@work_order_id, @work_order_number, @production_order_id, @required_quantity, @start_date, @deadline, @status, @hold_reason, GETUTCDATE(), @product_id, @packaging_instructions);
END;
GO

ALTER PROCEDURE sp_WorkOrder_update
    @work_order_id INT,
    @work_order_number NVARCHAR(50),
    @production_order_id INT,
    @required_quantity INT,
    @start_date DATE,
    @deadline DATE,
    @status NVARCHAR(50),
    @hold_reason NVARCHAR(MAX) = NULL,
    @product_id INT,
    @packaging_instructions NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE WorkOrder
    SET work_order_number = @work_order_number,
        production_order_id = @production_order_id,
        required_quantity = @required_quantity,
        start_date = @start_date,
        deadline = @deadline,
        status = @status,
        hold_reason = @hold_reason,
        product_id = @product_id,
        packaging_instructions = @packaging_instructions,
        modified_at = GETUTCDATE()
    WHERE work_order_id = @work_order_id;
END;
GO
