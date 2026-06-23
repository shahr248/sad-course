-- ============================================================================
-- Migration: Add Factory ownership to Product
--
-- Each product is manufactured by exactly one of the four workshops, the same
-- way CustomerCompany already records its own factory. This lets the Add
-- Production Order dialog restrict a Factory Manager to the customers and
-- products that belong to their own factory.
--
-- Steps:
--   1. Add Product.factory as nullable.
--   2. Backfill the 6 existing seeded products by name/SKU.
--   3. Constrain Product.factory to NOT NULL + the same CHECK used by
--      CustomerCompany.factory.
--   4. Update sp_Product_create / sp_Product_update to accept @factory.
--
-- IMPORTANT: Product.initProducts() in C# reads columns positionally
-- (SELECT *), so factory is appended here (after modified_at) via ALTER
-- TABLE, matching where CustomerCompany.factory already sits as the last
-- column. create_database.sql mirrors this same column order for fresh
-- installs.
--
-- Safe to re-run.
-- ============================================================================

USE sadcourse3;
GO

-- ----------------------------------------------------------------------------
-- 1. Add Product.factory (nullable for now)
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('Product') AND name = 'factory'
)
BEGIN
    ALTER TABLE Product ADD factory NVARCHAR(30) NULL;
END;
GO

-- ----------------------------------------------------------------------------
-- 2. Backfill existing products by name. Product 6 ("Handmade Soap Bars")
-- doesn't map cleanly to one workshop by name -- assigned to TzitzitWorkshop
-- as a judgment call; reassign later via UPDATE if a better fit is known.
-- ----------------------------------------------------------------------------
UPDATE Product SET factory = 'MaimonSpices'    WHERE product_id = 1 AND factory IS NULL; -- Black Pepper Powder
UPDATE Product SET factory = 'MaimonSpices'    WHERE product_id = 2 AND factory IS NULL; -- Paprika Seasoning Mix
UPDATE Product SET factory = 'Technosak'       WHERE product_id = 3 AND factory IS NULL; -- Clear Plastic Bottles
UPDATE Product SET factory = 'SewingWorkshop'  WHERE product_id = 4 AND factory IS NULL; -- Woven Fabric Rolls
UPDATE Product SET factory = 'TzitzitWorkshop' WHERE product_id = 5 AND factory IS NULL; -- Tzitzit Thread Bundles
UPDATE Product SET factory = 'TzitzitWorkshop' WHERE product_id = 6 AND factory IS NULL; -- Handmade Soap Bars (judgment call)
GO

-- Any other pre-existing product rows not covered above default to MaimonSpices
-- so the NOT NULL constraint below never fails on unanticipated data.
UPDATE Product SET factory = 'MaimonSpices' WHERE factory IS NULL;
GO

-- ----------------------------------------------------------------------------
-- 3. Constrain Product.factory to NOT NULL + CHECK
-- ----------------------------------------------------------------------------
ALTER TABLE Product ALTER COLUMN factory NVARCHAR(30) NOT NULL;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints WHERE name = 'CHK_Product_Factory'
)
BEGIN
    ALTER TABLE Product
        ADD CONSTRAINT CHK_Product_Factory CHECK (factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop'));
END;
GO

-- ----------------------------------------------------------------------------
-- 4. sp_Product_create / sp_Product_update: accept @factory
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_Product_create
    @product_id INT,
    @sku NVARCHAR(50),
    @product_name NVARCHAR(50),
    @description NVARCHAR(MAX) = NULL,
    @packaging_instructions NVARCHAR(MAX) = NULL,
    @unit_of_measure NVARCHAR(20),
    @activity_status NVARCHAR(50) = NULL,
    @factory NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Product
        (product_id, sku, product_name, description, packaging_instructions, unit_of_measure, activity_status, created_at, factory)
    VALUES
        (@product_id, @sku, @product_name, @description, @packaging_instructions, @unit_of_measure, @activity_status, GETUTCDATE(), @factory);
END;
GO

ALTER PROCEDURE sp_Product_update
    @product_id INT,
    @sku NVARCHAR(50),
    @product_name NVARCHAR(50),
    @description NVARCHAR(MAX) = NULL,
    @packaging_instructions NVARCHAR(MAX) = NULL,
    @unit_of_measure NVARCHAR(20),
    @activity_status NVARCHAR(50) = NULL,
    @factory NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Product
    SET sku = @sku,
        product_name = @product_name,
        description = @description,
        packaging_instructions = @packaging_instructions,
        unit_of_measure = @unit_of_measure,
        activity_status = @activity_status,
        factory = @factory,
        modified_at = GETUTCDATE()
    WHERE product_id = @product_id;
END;
GO
