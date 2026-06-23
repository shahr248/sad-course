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

CREATE PROCEDURE sp_CustomerCompany_update
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

CREATE PROCEDURE sp_Product_update
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
    @department INT,
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
    @department INT,
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

CREATE PROCEDURE sp_Contract_update
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

CREATE PROCEDURE sp_ProductionOrder_update
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

CREATE PROCEDURE sp_WorkOrder_update
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

-- ============================================================================
-- PRISONER STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_Prisoner_approvePrison
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'pendingDepartmentManagerApproval'
        WHERE prisoner_id = @prisoner_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_rejectPrison
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'archived'
        WHERE prisoner_id = @prisoner_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_approveDeptManager
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'pendingPlacement'
        WHERE prisoner_id = @prisoner_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_rejectDeptManager
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'archived'
        WHERE prisoner_id = @prisoner_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_assignToFactory
    @prisoner_id INT,
    @factory NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle',
            factory = @factory
        WHERE prisoner_id = @prisoner_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_clockIn
    @prisoner_id INT,
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'onShiftWorking'
        WHERE prisoner_id = @prisoner_id;

        -- Create DailyAttendance entry if not exists
        DECLARE @new_attendance_id INT;
        SELECT @new_attendance_id = ISNULL(MAX(attendance_record_id), 0) + 1 FROM AttendanceRecord;

        INSERT INTO AttendanceRecord (attendance_record_id, prisoner_id, attendance_date, factory, entry_time)
        SELECT @new_attendance_id, @prisoner_id, CAST(GETUTCDATE() AS DATE), factory, CAST(GETUTCDATE() AS TIME)
        FROM Prisoner WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_pauseForMaterials
    @prisoner_id INT,
    @reason NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'waitingForMaterials'
        WHERE prisoner_id = @prisoner_id;

        -- Create Alert (R16) - simplified: no Alert table implementation yet
        -- FUTURE: INSERT INTO Alert (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_clockOut
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle'
        WHERE prisoner_id = @prisoner_id;

        -- Update DailyAttendance exit_time (update latest record for today)
        UPDATE AttendanceRecord
        SET exit_time = CAST(GETUTCDATE() AS TIME)
        WHERE prisoner_id = @prisoner_id
          AND attendance_date = CAST(GETUTCDATE() AS DATE)
          AND exit_time IS NULL;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_resumeFromMaterials
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'onShiftWorking'
        WHERE prisoner_id = @prisoner_id;

        -- Clear Alert (R16) - simplified
        -- FUTURE: DELETE FROM Alert WHERE prisoner_id = @prisoner_id AND ...

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_abortTask
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle'
        WHERE prisoner_id = @prisoner_id;

        -- Update DailyAttendance if in progress
        UPDATE AttendanceRecord
        SET exit_time = CAST(GETUTCDATE() AS TIME)
        WHERE prisoner_id = @prisoner_id
          AND attendance_date = CAST(GETUTCDATE() AS DATE)
          AND exit_time IS NULL;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_enrollInProfessionalTraining
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'inProfessionalTraining'
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_completeProfessionalTraining
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle'
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_enrollInSafetyTraining
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'inSafetyTraining'
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_completeSafetyTraining
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle',
            safety_training_validity = DATEADD(YEAR, 1, GETUTCDATE())
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_placeOnHold
    @prisoner_id INT,
    @reason NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'temporarilyUnavailable'
        WHERE prisoner_id = @prisoner_id;

        -- Create Alert (R16)
        -- FUTURE: INSERT INTO Alert (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_releaseFromHold
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'idle'
        WHERE prisoner_id = @prisoner_id;

        -- Clear Alert (R16)
        -- FUTURE: DELETE FROM Alert WHERE prisoner_id = @prisoner_id AND ...

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_archiveUnavailable
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'archived'
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Prisoner_archive
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Prisoner
        SET activity_status = 'archived'
        WHERE prisoner_id = @prisoner_id;

        -- Cease PayrollRecord generation (R10) - mark as inactive in PayrollRecord
        -- FUTURE: UPDATE PayrollRecord SET is_active = 0 WHERE prisoner_id = @prisoner_id

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- PRODUCTION ORDER STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_ProductionOrder_acceptOrder
    @production_order_id INT,
    @factory_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'inProduction'
        WHERE production_order_id = @production_order_id;

        -- Create WorkOrders (R2, R3) - simplified: assume caller creates separately
        -- FUTURE: INSERT INTO WorkOrder (...) SELECT ... FROM ProductionOrder

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_cancelOrder
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'cancelled'
        WHERE production_order_id = @production_order_id;

        -- Notify customer (R12) - simplified
        -- FUTURE: INSERT INTO Notification (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_holdOrder
    @production_order_id INT,
    @reason NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'onHold'
        WHERE production_order_id = @production_order_id;

        -- Create Alert (R16)
        -- FUTURE: INSERT INTO Alert (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_resumeOrder
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'inProduction'
        WHERE production_order_id = @production_order_id;

        -- Clear Alert (R16)
        -- FUTURE: DELETE FROM Alert WHERE production_order_id = @production_order_id AND ...

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_markReadyForPickup
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'readyForPickup'
        WHERE production_order_id = @production_order_id;

        -- Notify customer (R12)
        -- FUTURE: INSERT INTO Notification (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_markDelivered
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'delivered'
        WHERE production_order_id = @production_order_id;

        -- Generate PayrollRecords (R10) for all inmates who worked on this order
        -- FUTURE: Complex query to insert PayrollRecords based on ProductivityRecords

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductionOrder_cancelOnHold
    @production_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE ProductionOrder
        SET order_status = 'cancelled'
        WHERE production_order_id = @production_order_id;

        -- Notify customer (R12)
        -- FUTURE: INSERT INTO Notification (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- WORK ORDER STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_WorkOrder_enterProduction
    @work_order_id INT,
    @inmate_count INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE WorkOrder
        SET status = 'inProcess'
        WHERE work_order_id = @work_order_id;

        -- Create AttendanceRecord entries for assigned inmates
        -- FUTURE: INSERT INTO AttendanceRecord (...) FOR EACH inmate

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_WorkOrder_resetAssignment
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE WorkOrder
        SET status = 'hasntEnteredIntoProductionYet'
        WHERE work_order_id = @work_order_id;

        -- Clear AttendanceRecords for this work order
        -- FUTURE: DELETE FROM AttendanceRecord WHERE work_order_id = @work_order_id

        -- Reset completed_quantity in parent ProductionOrder
        UPDATE ProductionOrder
        SET completed_quantity = 0
        WHERE production_order_id = (SELECT production_order_id FROM WorkOrder WHERE work_order_id = @work_order_id);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_WorkOrder_markComplete
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE WorkOrder
        SET status = 'completed'
        WHERE work_order_id = @work_order_id;

        -- Check if parent ProductionOrder is ready for pickup
        -- FUTURE: UPDATE ProductionOrder IF all WorkOrders completed

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_WorkOrder_cancel
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Mark as abandoned (no explicit state, but update internal flags)
        UPDATE WorkOrder
        SET status = 'hasntEnteredIntoProductionYet'
        WHERE work_order_id = @work_order_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- CONTRACT STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_Contract_suspend
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Contract
        SET contract_status = 'Inactive'
        WHERE contract_id = @contract_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Contract_reactivate
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Contract
        SET contract_status = 'Active'
        WHERE contract_id = @contract_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Contract_expire
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        UPDATE Contract
        SET contract_status = 'Expired'
        WHERE contract_id = @contract_id;

        -- Notify customer (R12)
        -- FUTURE: INSERT INTO Notification (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Contract_archive
    @contract_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- No new state, but mark as archived in DB (could add archived_at timestamp)
        UPDATE Contract
        SET modified_at = GETUTCDATE()
        WHERE contract_id = @contract_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- ATTENDANCE RECORD STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_AttendanceRecord_recordEntry
    @attendance_record_id INT,
    @prisoner_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Update Prisoner to OnShiftWorking
        UPDATE Prisoner
        SET activity_status = 'onShiftWorking'
        WHERE prisoner_id = @prisoner_id;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_recordTemporaryExit
    @attendance_record_id INT,
    @temp_exit_time TIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Record temporary exit (simplified: no separate table, just log in SP)
        -- FUTURE: INSERT INTO AttendanceBreak (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_AttendanceRecord_recordExit
    @attendance_record_id INT,
    @prisoner_id INT,
    @hours_worked DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Update Prisoner to Idle
        UPDATE Prisoner
        SET activity_status = 'idle'
        WHERE prisoner_id = @prisoner_id;

        -- Log hours worked to contribute to PayrollRecord (R10)
        -- FUTURE: UPDATE PayrollRecord SET total_hours = total_hours + @hours_worked

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- PRODUCTIVITY RECORD STATE TRANSITION PROCEDURES
-- ============================================================================

CREATE PROCEDURE sp_ProductivityRecord_submitProduction
    @productivity_record_id INT,
    @units_produced INT,
    @defective_units INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Calculate quality rate
        DECLARE @quality_rate DECIMAL(5,2) = 0;
        IF @units_produced > 0
            SET @quality_rate = CAST((@units_produced - @defective_units) AS DECIMAL) / @units_produced * 100;

        -- Record quality metrics (simplified)
        -- FUTURE: INSERT INTO QualityMetric (...) VALUES (...)

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_ProductivityRecord_approveProduction
    @productivity_record_id INT,
    @work_order_id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;
    BEGIN TRY
        -- Update WorkOrder.completed_quantity
        DECLARE @units_produced INT;
        SELECT @units_produced = quantity_produced FROM ProductivityRecord
        WHERE productivity_record_id = @productivity_record_id;

        -- Increment WorkOrder completed_quantity
        -- FUTURE: More complex logic to track actual units

        -- Check if WorkOrder is complete (all units produced)
        -- FUTURE: IF WorkOrder.completed_quantity >= WorkOrder.required_quantity THEN mark complete

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO
