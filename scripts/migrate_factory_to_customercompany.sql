-- ============================================================================
-- Migration: Move `factory` from WorkOrder to CustomerCompany
--
-- Run this once against the existing sadcourse3 database. It is safe to
-- re-run (steps that would fail on a second run are guarded).
--
-- Steps:
--   1. Add a nullable `factory` column to CustomerCompany.
--   2. Backfill it from existing WorkOrder data (most frequent factory per
--      customer, tie-broken by earliest work_order_id), defaulting to
--      'MaimonSpices' for any customer with no WorkOrder history.
--   3. Constrain the column to NOT NULL + CHECK, matching the old WorkOrder rule.
--   4. Update sp_CustomerCompany_create / sp_CustomerCompany_update to accept @factory.
--   5. Update sp_WorkOrder_create / sp_WorkOrder_update to drop @factory.
--   6. Drop the factory column (and its CHECK constraint) from WorkOrder.
-- ============================================================================

USE sadcourse3;
GO

-- ----------------------------------------------------------------------------
-- 1. Add nullable factory column to CustomerCompany
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('CustomerCompany') AND name = 'factory'
)
BEGIN
    ALTER TABLE CustomerCompany ADD factory NVARCHAR(30) NULL;
END;
GO

-- ----------------------------------------------------------------------------
-- 2. Backfill from existing WorkOrder data (data-driven, not hardcoded)
--    Picks each customer's most common WorkOrder factory; ties broken by the
--    earliest work_order_id. Customers with no production/work orders fall
--    back to 'MaimonSpices'.
-- ----------------------------------------------------------------------------
;WITH FactoryCounts AS (
    SELECT
        po.customer_company_id,
        wo.factory,
        COUNT(*) AS factory_count,
        MIN(wo.work_order_id) AS earliest_work_order_id
    FROM WorkOrder wo
    INNER JOIN ProductionOrder po ON po.production_order_id = wo.production_order_id
    GROUP BY po.customer_company_id, wo.factory
),
RankedFactories AS (
    SELECT
        customer_company_id,
        factory,
        ROW_NUMBER() OVER (
            PARTITION BY customer_company_id
            ORDER BY factory_count DESC, earliest_work_order_id ASC
        ) AS rn
    FROM FactoryCounts
)
UPDATE cc
SET cc.factory = COALESCE(rf.factory, 'MaimonSpices')
FROM CustomerCompany cc
LEFT JOIN RankedFactories rf
    ON rf.customer_company_id = cc.customer_company_id AND rf.rn = 1
WHERE cc.factory IS NULL;
GO

-- ----------------------------------------------------------------------------
-- 3. Constrain factory to NOT NULL + CHECK
-- ----------------------------------------------------------------------------
ALTER TABLE CustomerCompany ALTER COLUMN factory NVARCHAR(30) NOT NULL;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints WHERE name = 'CHK_CustomerCompany_Factory'
)
BEGIN
    ALTER TABLE CustomerCompany
        ADD CONSTRAINT CHK_CustomerCompany_Factory
        CHECK (factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop'));
END;
GO

-- ----------------------------------------------------------------------------
-- 4. sp_CustomerCompany_create / sp_CustomerCompany_update: accept @factory
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_CustomerCompany_create
    @customer_company_id INT,
    @company_id NVARCHAR(50),
    @company_name NVARCHAR(50),
    @contact_name NVARCHAR(50) = NULL,
    @phone_number NVARCHAR(20) = NULL,
    @delivery_address NVARCHAR(MAX) = NULL,
    @billing_address NVARCHAR(MAX) = NULL,
    @email NVARCHAR(255) = NULL,
    @activity_status NVARCHAR(50) = NULL,
    @factory NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO CustomerCompany
        (customer_company_id, company_id, company_name, contact_name, phone_number, delivery_address, billing_address, email, activity_status, created_at, factory)
    VALUES
        (@customer_company_id, @company_id, @company_name, @contact_name, @phone_number, @delivery_address, @billing_address, @email, @activity_status, GETUTCDATE(), @factory);
END;
GO

ALTER PROCEDURE sp_CustomerCompany_update
    @customer_company_id INT,
    @company_id NVARCHAR(50),
    @company_name NVARCHAR(50),
    @contact_name NVARCHAR(50) = NULL,
    @phone_number NVARCHAR(20) = NULL,
    @delivery_address NVARCHAR(MAX) = NULL,
    @billing_address NVARCHAR(MAX) = NULL,
    @email NVARCHAR(255) = NULL,
    @activity_status NVARCHAR(50) = NULL,
    @factory NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE CustomerCompany
    SET company_id = @company_id,
        company_name = @company_name,
        contact_name = @contact_name,
        phone_number = @phone_number,
        delivery_address = @delivery_address,
        billing_address = @billing_address,
        email = @email,
        activity_status = @activity_status,
        factory = @factory,
        modified_at = GETUTCDATE()
    WHERE customer_company_id = @customer_company_id;
END;
GO

-- ----------------------------------------------------------------------------
-- 5. sp_WorkOrder_create / sp_WorkOrder_update: drop @factory
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_WorkOrder_create
    @work_order_id INT,
    @work_order_number NVARCHAR(50),
    @production_order_id INT,
    @required_quantity INT,
    @start_date DATE,
    @deadline DATE,
    @status NVARCHAR(50),
    @hold_reason NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO WorkOrder
        (work_order_id, work_order_number, production_order_id, required_quantity, start_date, deadline, status, hold_reason, created_at)
    VALUES
        (@work_order_id, @work_order_number, @production_order_id, @required_quantity, @start_date, @deadline, @status, @hold_reason, GETUTCDATE());
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
    @hold_reason NVARCHAR(MAX) = NULL
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
        modified_at = GETUTCDATE()
    WHERE work_order_id = @work_order_id;
END;
GO

-- ----------------------------------------------------------------------------
-- 6. Drop factory column (and its CHECK constraint) from WorkOrder
-- ----------------------------------------------------------------------------
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CHK_WorkOrder_Factory')
BEGIN
    ALTER TABLE WorkOrder DROP CONSTRAINT CHK_WorkOrder_Factory;
END;
GO

IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('WorkOrder') AND name = 'factory'
)
BEGIN
    ALTER TABLE WorkOrder DROP COLUMN factory;
END;
GO
