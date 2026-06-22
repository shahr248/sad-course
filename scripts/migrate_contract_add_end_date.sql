-- ============================================================================
-- Migration: Add end_date to Contract
--
-- Run this once against the existing sadcourse3 database. It is safe to
-- re-run (steps that would fail on a second run are guarded).
--
-- Steps:
--   1. Add a nullable end_date column to Contract.
--   2. Add CHK_Contract_EndDate (end_date IS NULL OR end_date >= start_date).
--   3. Update sp_Contract_create / sp_Contract_update to accept @end_date.
-- ============================================================================

USE sadcourse3;
GO

-- ----------------------------------------------------------------------------
-- 1. Add nullable end_date column to Contract
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('Contract') AND name = 'end_date'
)
BEGIN
    ALTER TABLE Contract ADD end_date DATE NULL;
END;
GO

-- ----------------------------------------------------------------------------
-- 2. Constrain end_date >= start_date when present
-- ----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints WHERE name = 'CHK_Contract_EndDate'
)
BEGIN
    ALTER TABLE Contract
        ADD CONSTRAINT CHK_Contract_EndDate
        CHECK (end_date IS NULL OR end_date >= start_date);
END;
GO

-- ----------------------------------------------------------------------------
-- 3. sp_Contract_create / sp_Contract_update: accept @end_date
-- ----------------------------------------------------------------------------
ALTER PROCEDURE sp_Contract_create
    @contract_id INT,
    @contract_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @start_date DATE,
    @end_date DATE = NULL,
    @price_per_unit DECIMAL(10,2) = NULL,
    @payment_terms NVARCHAR(MAX) = NULL,
    @contract_status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Contract
        (contract_id, contract_number, customer_company_id, product_id, start_date, end_date, price_per_unit, payment_terms, contract_status, created_at)
    VALUES
        (@contract_id, @contract_number, @customer_company_id, @product_id, @start_date, @end_date, @price_per_unit, @payment_terms, @contract_status, GETUTCDATE());
END;
GO

ALTER PROCEDURE sp_Contract_update
    @contract_id INT,
    @contract_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @start_date DATE,
    @end_date DATE = NULL,
    @price_per_unit DECIMAL(10,2) = NULL,
    @payment_terms NVARCHAR(MAX) = NULL,
    @contract_status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Contract
    SET contract_number = @contract_number,
        customer_company_id = @customer_company_id,
        product_id = @product_id,
        start_date = @start_date,
        end_date = @end_date,
        price_per_unit = @price_per_unit,
        payment_terms = @payment_terms,
        contract_status = @contract_status,
        modified_at = GETUTCDATE()
    WHERE contract_id = @contract_id;
END;
GO
