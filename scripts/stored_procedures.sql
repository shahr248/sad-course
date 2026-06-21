USE sadcourse3;
GO

-- ============================================================================
-- STORED PROCEDURES - CRUD Operations for PEDIS Tables
-- ============================================================================

-- ============================================================================
-- EmploymentDepartment CRUD
-- ============================================================================

CREATE PROCEDURE sp_EmploymentDepartment_create
    @employment_department_id INT,
    @department_code NVARCHAR(50),
    @department_name NVARCHAR(50),
    @location NVARCHAR(50) = NULL,
    @max_capacity INT = NULL,
    @is_active BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO EmploymentDepartment
        (employment_department_id, department_code, department_name, location, max_capacity, is_active, created_at)
    VALUES
        (@employment_department_id, @department_code, @department_name, @location, @max_capacity, @is_active, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_EmploymentDepartment_update
    @employment_department_id INT,
    @department_code NVARCHAR(50),
    @department_name NVARCHAR(50),
    @location NVARCHAR(50) = NULL,
    @max_capacity INT = NULL,
    @is_active BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE EmploymentDepartment
    SET department_code = @department_code,
        department_name = @department_name,
        location = @location,
        max_capacity = @max_capacity,
        is_active = @is_active,
        modified_at = GETUTCDATE()
    WHERE employment_department_id = @employment_department_id;
END;
GO

CREATE PROCEDURE sp_EmploymentDepartment_delete
    @employment_department_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM EmploymentDepartment WHERE employment_department_id = @employment_department_id;
END;
GO

CREATE PROCEDURE sp_EmploymentDepartment_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM EmploymentDepartment ORDER BY employment_department_id;
END;
GO

CREATE PROCEDURE sp_EmploymentDepartment_get_by_id
    @employment_department_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM EmploymentDepartment WHERE employment_department_id = @employment_department_id;
END;
GO

-- ============================================================================
-- CustomerCompany CRUD
-- ============================================================================

CREATE PROCEDURE sp_CustomerCompany_create
    @customer_company_id INT,
    @company_id NVARCHAR(50),
    @company_name NVARCHAR(50),
    @contact_name NVARCHAR(50) = NULL,
    @phone_number NVARCHAR(20) = NULL,
    @delivery_address NVARCHAR(MAX) = NULL,
    @billing_address NVARCHAR(MAX) = NULL,
    @email NVARCHAR(255) = NULL,
    @activity_status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO CustomerCompany
        (customer_company_id, company_id, company_name, contact_name, phone_number, delivery_address, billing_address, email, activity_status, created_at)
    VALUES
        (@customer_company_id, @company_id, @company_name, @contact_name, @phone_number, @delivery_address, @billing_address, @email, @activity_status, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_CustomerCompany_update
    @customer_company_id INT,
    @company_id NVARCHAR(50),
    @company_name NVARCHAR(50),
    @contact_name NVARCHAR(50) = NULL,
    @phone_number NVARCHAR(20) = NULL,
    @delivery_address NVARCHAR(MAX) = NULL,
    @billing_address NVARCHAR(MAX) = NULL,
    @email NVARCHAR(255) = NULL,
    @activity_status NVARCHAR(50) = NULL
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
        modified_at = GETUTCDATE()
    WHERE customer_company_id = @customer_company_id;
END;
GO

CREATE PROCEDURE sp_CustomerCompany_delete
    @customer_company_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM CustomerCompany WHERE customer_company_id = @customer_company_id;
END;
GO

CREATE PROCEDURE sp_CustomerCompany_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM CustomerCompany ORDER BY customer_company_id;
END;
GO

CREATE PROCEDURE sp_CustomerCompany_get_by_id
    @customer_company_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM CustomerCompany WHERE customer_company_id = @customer_company_id;
END;
GO

-- ============================================================================
-- Product CRUD
-- ============================================================================

CREATE PROCEDURE sp_Product_create
    @product_id INT,
    @sku NVARCHAR(50),
    @product_name NVARCHAR(50),
    @description NVARCHAR(MAX) = NULL,
    @packaging_instructions NVARCHAR(MAX) = NULL,
    @unit_of_measure NVARCHAR(20),
    @activity_status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Product
        (product_id, sku, product_name, description, packaging_instructions, unit_of_measure, activity_status, created_at)
    VALUES
        (@product_id, @sku, @product_name, @description, @packaging_instructions, @unit_of_measure, @activity_status, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_Product_update
    @product_id INT,
    @sku NVARCHAR(50),
    @product_name NVARCHAR(50),
    @description NVARCHAR(MAX) = NULL,
    @packaging_instructions NVARCHAR(MAX) = NULL,
    @unit_of_measure NVARCHAR(20),
    @activity_status NVARCHAR(50) = NULL
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
        modified_at = GETUTCDATE()
    WHERE product_id = @product_id;
END;
GO

CREATE PROCEDURE sp_Product_delete
    @product_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Product WHERE product_id = @product_id;
END;
GO

CREATE PROCEDURE sp_Product_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Product ORDER BY product_id;
END;
GO

CREATE PROCEDURE sp_Product_get_by_id
    @product_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Product WHERE product_id = @product_id;
END;
GO

-- ============================================================================
-- DepartmentManagement CRUD
-- ============================================================================

CREATE PROCEDURE sp_DepartmentManagement_create
    @department_management_id INT,
    @username NVARCHAR(50),
    @password NVARCHAR(50),
    @role NVARCHAR(50),
    @factory NVARCHAR(30) = NULL,
    @employment_department_id INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DepartmentManagement
        (department_management_id, username, password, role, factory, employment_department_id, created_at)
    VALUES
        (@department_management_id, @username, @password, @role, @factory, @employment_department_id, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_DepartmentManagement_update
    @department_management_id INT,
    @username NVARCHAR(50),
    @password NVARCHAR(50),
    @role NVARCHAR(50),
    @factory NVARCHAR(30) = NULL,
    @employment_department_id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE DepartmentManagement
    SET username = @username,
        password = @password,
        role = @role,
        factory = @factory,
        employment_department_id = @employment_department_id,
        modified_at = GETUTCDATE()
    WHERE department_management_id = @department_management_id;
END;
GO

CREATE PROCEDURE sp_DepartmentManagement_delete
    @department_management_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM DepartmentManagement WHERE department_management_id = @department_management_id;
END;
GO

CREATE PROCEDURE sp_DepartmentManagement_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM DepartmentManagement ORDER BY department_management_id;
END;
GO

CREATE PROCEDURE sp_DepartmentManagement_get_by_id
    @department_management_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM DepartmentManagement WHERE department_management_id = @department_management_id;
END;
GO

-- ============================================================================
-- Prisoner CRUD (Updated: removed prisoner_name, changed min_salary to hourly_rate)
-- ============================================================================

CREATE PROCEDURE sp_Prisoner_create
    @prisoner_id INT,
    @prisoner_number NVARCHAR(50),
    @full_name NVARCHAR(50),
    @factory NVARCHAR(30) = NULL,
    @department NVARCHAR(50) = NULL,
    @activity_status NVARCHAR(50),
    @role NVARCHAR(50) = NULL,
    @safety_training_validity DATE = NULL,
    @work_start_date DATE = NULL,
    @release_date DATE = NULL,
    @qualified BIT = 0,
    @shabbat_keeping BIT = 0,
    @hourly_rate DECIMAL(10,2) = 14.70
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Prisoner
        (prisoner_id, prisoner_number, full_name, factory, department, activity_status, role,
         safety_training_validity, work_start_date, release_date, qualified, shabbat_keeping, hourly_rate, created_at)
    VALUES
        (@prisoner_id, @prisoner_number, @full_name, @factory, @department, @activity_status, @role,
         @safety_training_validity, @work_start_date, @release_date, @qualified, @shabbat_keeping, @hourly_rate, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_Prisoner_update
    @prisoner_id INT,
    @prisoner_number NVARCHAR(50),
    @full_name NVARCHAR(50),
    @factory NVARCHAR(30) = NULL,
    @department NVARCHAR(50) = NULL,
    @activity_status NVARCHAR(50),
    @role NVARCHAR(50) = NULL,
    @safety_training_validity DATE = NULL,
    @work_start_date DATE = NULL,
    @release_date DATE = NULL,
    @qualified BIT = 0,
    @shabbat_keeping BIT = 0,
    @hourly_rate DECIMAL(10,2) = 14.70
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Prisoner
    SET prisoner_number = @prisoner_number,
        full_name = @full_name,
        factory = @factory,
        department = @department,
        activity_status = @activity_status,
        role = @role,
        safety_training_validity = @safety_training_validity,
        work_start_date = @work_start_date,
        release_date = @release_date,
        qualified = @qualified,
        shabbat_keeping = @shabbat_keeping,
        hourly_rate = @hourly_rate,
        modified_at = GETUTCDATE()
    WHERE prisoner_id = @prisoner_id;
END;
GO

CREATE PROCEDURE sp_Prisoner_delete
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Prisoner WHERE prisoner_id = @prisoner_id;
END;
GO

CREATE PROCEDURE sp_Prisoner_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Prisoner ORDER BY prisoner_id;
END;
GO

CREATE PROCEDURE sp_Prisoner_get_by_id
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Prisoner WHERE prisoner_id = @prisoner_id;
END;
GO

-- ============================================================================
-- Contract CRUD
-- ============================================================================

CREATE PROCEDURE sp_Contract_create
    @contract_id INT,
    @contract_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @start_date DATE,
    @price_per_unit DECIMAL(10,2) = NULL,
    @payment_terms NVARCHAR(MAX) = NULL,
    @contract_status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Contract
        (contract_id, contract_number, customer_company_id, product_id, start_date, price_per_unit, payment_terms, contract_status, created_at)
    VALUES
        (@contract_id, @contract_number, @customer_company_id, @product_id, @start_date, @price_per_unit, @payment_terms, @contract_status, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_Contract_update
    @contract_id INT,
    @contract_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT = NULL,
    @start_date DATE,
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
        price_per_unit = @price_per_unit,
        payment_terms = @payment_terms,
        contract_status = @contract_status,
        modified_at = GETUTCDATE()
    WHERE contract_id = @contract_id;
END;
GO

CREATE PROCEDURE sp_Contract_delete
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Contract WHERE contract_id = @contract_id;
END;
GO

CREATE PROCEDURE sp_Contract_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Contract ORDER BY contract_id;
END;
GO

CREATE PROCEDURE sp_Contract_get_by_id
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Contract WHERE contract_id = @contract_id;
END;
GO

-- ============================================================================
-- ProductionOrder CRUD (Updated: added completed_quantity, contract_id now NOT NULL)
-- ============================================================================

CREATE PROCEDURE sp_ProductionOrder_create
    @production_order_id INT,
    @order_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT,
    @contract_id INT,
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

CREATE PROCEDURE sp_ProductionOrder_update
    @production_order_id INT,
    @order_number NVARCHAR(50),
    @customer_company_id INT,
    @product_id INT,
    @contract_id INT,
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

CREATE PROCEDURE sp_ProductionOrder_delete
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ProductionOrder WHERE production_order_id = @production_order_id;
END;
GO

CREATE PROCEDURE sp_ProductionOrder_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM ProductionOrder ORDER BY production_order_id;
END;
GO

CREATE PROCEDURE sp_ProductionOrder_get_by_id
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM ProductionOrder WHERE production_order_id = @production_order_id;
END;
GO

-- ============================================================================
-- WorkOrder CRUD (Updated: removed completed_quantity)
-- ============================================================================

CREATE PROCEDURE sp_WorkOrder_create
    @work_order_id INT,
    @work_order_number NVARCHAR(50),
    @production_order_id INT,
    @required_quantity INT,
    @start_date DATE,
    @deadline DATE,
    @factory NVARCHAR(30),
    @status NVARCHAR(50),
    @hold_reason NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO WorkOrder
        (work_order_id, work_order_number, production_order_id, required_quantity, start_date, deadline, factory, status, hold_reason, created_at)
    VALUES
        (@work_order_id, @work_order_number, @production_order_id, @required_quantity, @start_date, @deadline, @factory, @status, @hold_reason, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_WorkOrder_update
    @work_order_id INT,
    @work_order_number NVARCHAR(50),
    @production_order_id INT,
    @required_quantity INT,
    @start_date DATE,
    @deadline DATE,
    @factory NVARCHAR(30),
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
        factory = @factory,
        status = @status,
        hold_reason = @hold_reason,
        modified_at = GETUTCDATE()
    WHERE work_order_id = @work_order_id;
END;
GO

CREATE PROCEDURE sp_WorkOrder_delete
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM WorkOrder WHERE work_order_id = @work_order_id;
END;
GO

CREATE PROCEDURE sp_WorkOrder_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM WorkOrder ORDER BY work_order_id;
END;
GO

CREATE PROCEDURE sp_WorkOrder_get_by_id
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM WorkOrder WHERE work_order_id = @work_order_id;
END;
GO

-- ============================================================================
-- AttendanceRecord CRUD
-- ============================================================================

CREATE PROCEDURE sp_AttendanceRecord_create
    @attendance_record_id INT,
    @prisoner_id INT,
    @attendance_date DATE,
    @factory NVARCHAR(30),
    @entry_time TIME = NULL,
    @exit_time TIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AttendanceRecord
        (attendance_record_id, prisoner_id, attendance_date, factory, entry_time, exit_time, created_at)
    VALUES
        (@attendance_record_id, @prisoner_id, @attendance_date, @factory, @entry_time, @exit_time, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_update
    @attendance_record_id INT,
    @prisoner_id INT,
    @attendance_date DATE,
    @factory NVARCHAR(30),
    @entry_time TIME = NULL,
    @exit_time TIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE AttendanceRecord
    SET prisoner_id = @prisoner_id,
        attendance_date = @attendance_date,
        factory = @factory,
        entry_time = @entry_time,
        exit_time = @exit_time
    WHERE attendance_record_id = @attendance_record_id;
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_delete
    @attendance_record_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM AttendanceRecord WHERE attendance_record_id = @attendance_record_id;
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM AttendanceRecord ORDER BY attendance_record_id;
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_get_by_id
    @attendance_record_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM AttendanceRecord WHERE attendance_record_id = @attendance_record_id;
END;
GO

-- ============================================================================
-- ProductivityRecord CRUD
-- ============================================================================

CREATE PROCEDURE sp_ProductivityRecord_create
    @productivity_record_id INT,
    @prisoner_id INT,
    @work_order_id INT,
    @productivity_date DATE,
    @quantity_produced INT,
    @work_hours DECIMAL(5,2) = NULL,
    @productivity_type NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO ProductivityRecord
        (productivity_record_id, prisoner_id, work_order_id, productivity_date, quantity_produced, work_hours, productivity_type, created_at)
    VALUES
        (@productivity_record_id, @prisoner_id, @work_order_id, @productivity_date, @quantity_produced, @work_hours, @productivity_type, GETUTCDATE());
END;
GO

CREATE PROCEDURE sp_ProductivityRecord_update
    @productivity_record_id INT,
    @prisoner_id INT,
    @work_order_id INT,
    @productivity_date DATE,
    @quantity_produced INT,
    @work_hours DECIMAL(5,2) = NULL,
    @productivity_type NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE ProductivityRecord
    SET prisoner_id = @prisoner_id,
        work_order_id = @work_order_id,
        productivity_date = @productivity_date,
        quantity_produced = @quantity_produced,
        work_hours = @work_hours,
        productivity_type = @productivity_type
    WHERE productivity_record_id = @productivity_record_id;
END;
GO

CREATE PROCEDURE sp_ProductivityRecord_delete
    @productivity_record_id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ProductivityRecord WHERE productivity_record_id = @productivity_record_id;
END;
GO

CREATE PROCEDURE sp_ProductivityRecord_get_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM ProductivityRecord ORDER BY productivity_record_id;
END;
GO

CREATE PROCEDURE sp_ProductivityRecord_get_by_id
    @productivity_record_id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM ProductivityRecord WHERE productivity_record_id = @productivity_record_id;
END;
GO
