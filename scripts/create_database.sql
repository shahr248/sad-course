USE sadcourse3;

-- ============================================================================
-- TIER 1: BASE ENTITIES (No FK dependencies)
-- ============================================================================

CREATE TABLE EmploymentDepartment (
    employment_department_id INT NOT NULL PRIMARY KEY,
    department_code NVARCHAR(50) NOT NULL UNIQUE,
    department_name NVARCHAR(50) NOT NULL,
    location NVARCHAR(50),
    max_capacity INT,
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL
);

CREATE TABLE CustomerCompany (
    customer_company_id INT NOT NULL PRIMARY KEY,
    company_id NVARCHAR(50) NOT NULL UNIQUE,
    company_name NVARCHAR(50) NOT NULL,
    contact_name NVARCHAR(50),
    phone_number NVARCHAR(20),
    delivery_address NVARCHAR(MAX),
    billing_address NVARCHAR(MAX),
    email NVARCHAR(255),
    activity_status NVARCHAR(50),
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    factory NVARCHAR(30) NOT NULL,
    CONSTRAINT CHK_CustomerCompany_Factory CHECK (factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop'))
);

CREATE TABLE Product (
    product_id INT NOT NULL PRIMARY KEY,
    sku NVARCHAR(50) NOT NULL UNIQUE,
    product_name NVARCHAR(50) NOT NULL,
    description NVARCHAR(MAX),
    packaging_instructions NVARCHAR(MAX),
    unit_of_measure NVARCHAR(20) NOT NULL,
    activity_status NVARCHAR(50),
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    factory NVARCHAR(30) NOT NULL,
    CONSTRAINT CHK_Product_UnitOfMeasure CHECK (unit_of_measure IN ('kg', 'gr', 'units')),
    CONSTRAINT CHK_Product_Factory CHECK (factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop'))
);

-- ============================================================================
-- TIER 2: CORE ENTITIES
-- ============================================================================

CREATE TABLE DepartmentManagement (
    department_management_id INT NOT NULL PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(50) NOT NULL,
    role NVARCHAR(50) NOT NULL,
    factory NVARCHAR(30) NULL,
    employment_department_id INT NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    CONSTRAINT CHK_DepartmentManagement_Role CHECK (role IN ('departmentManager', 'deputyOfDepartmentManager', 'factoryManager')),
    CONSTRAINT CHK_DepartmentManagement_Factory CHECK (factory IS NULL OR factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop')),
    CONSTRAINT FK_DepartmentManagement_EmploymentDepartment FOREIGN KEY (employment_department_id)
        REFERENCES EmploymentDepartment(employment_department_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Prisoner (
    prisoner_id INT NOT NULL PRIMARY KEY,
    prisoner_number NVARCHAR(50) NOT NULL UNIQUE,
    full_name NVARCHAR(50) NOT NULL,
    factory NVARCHAR(30) NULL,
    department INT NOT NULL,
    activity_status NVARCHAR(50) NOT NULL,
    role NVARCHAR(50),
    safety_training_validity DATE,
    work_start_date DATE,
    release_date DATE,
    qualified BIT DEFAULT 0,
    shabbat_keeping BIT DEFAULT 0,
    hourly_rate DECIMAL(10,2) NOT NULL DEFAULT 14.70,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    CONSTRAINT CHK_Prisoner_Factory CHECK (factory IS NULL OR factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop')),
    CONSTRAINT CHK_Prisoner_ActivityStatus CHECK (activity_status IN ('pendingPrisonAdministrationApproval', 'pendingDepartmentManagerApproval', 'pendingPlacement', 'idle', 'onShiftWorking', 'waitingForMaterials', 'inProfessionalTraining', 'inSafetyTraining', 'temporarilyUnavailable', 'archived')),
    CONSTRAINT CHK_Prisoner_Role CHECK (role IS NULL OR role IN ('supervisor', 'general'))
);

CREATE TABLE Contract (
    contract_id INT NOT NULL PRIMARY KEY,
    contract_number NVARCHAR(50) NOT NULL UNIQUE,
    customer_company_id INT NOT NULL,
    product_id INT NULL,
    start_date DATE NOT NULL,
    price_per_unit DECIMAL(10,2),
    payment_terms NVARCHAR(MAX),
    contract_status NVARCHAR(50),
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    end_date DATE NULL,
    CONSTRAINT CHK_Contract_Status CHECK (contract_status IN ('Active', 'Inactive', 'Expired')),
    CONSTRAINT CHK_Contract_EndDate CHECK (end_date IS NULL OR end_date >= start_date),
    CONSTRAINT FK_Contract_CustomerCompany FOREIGN KEY (customer_company_id)
        REFERENCES CustomerCompany(customer_company_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_Contract_Product FOREIGN KEY (product_id)
        REFERENCES Product(product_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================================================
-- TIER 3: ORDER MANAGEMENT
-- ============================================================================

CREATE TABLE ProductionOrder (
    production_order_id INT NOT NULL PRIMARY KEY,
    order_number NVARCHAR(50) NOT NULL UNIQUE,
    customer_company_id INT NOT NULL,
    product_id INT NULL,
    contract_id INT NULL,
    quantity INT NOT NULL,
    completed_quantity INT NOT NULL DEFAULT 0,
    submission_date DATE NOT NULL,
    delivery_deadline DATE NOT NULL,
    order_status NVARCHAR(50) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    CONSTRAINT CHK_ProductionOrder_Status CHECK (order_status IN ('recieved', 'inProduction', 'readyForPickup', 'delivered', 'cancelled', 'onHold')),
    CONSTRAINT FK_ProductionOrder_CustomerCompany FOREIGN KEY (customer_company_id)
        REFERENCES CustomerCompany(customer_company_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_ProductionOrder_Product FOREIGN KEY (product_id)
        REFERENCES Product(product_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_ProductionOrder_Contract FOREIGN KEY (contract_id)
        REFERENCES Contract(contract_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
-- product_id / contract_id are nullable: a ProductionOrder is created against a
-- Customer Company only; its products live on its WorkOrder line items instead
-- (see WorkOrder.product_id below). quantity is the sum of those line items'
-- required_quantity, computed in C# at save time, not entered directly.

CREATE TABLE WorkOrder (
    work_order_id INT NOT NULL PRIMARY KEY,
    work_order_number NVARCHAR(50) NOT NULL UNIQUE,
    production_order_id INT NOT NULL,
    required_quantity INT NOT NULL,
    start_date DATE NOT NULL,
    deadline DATE NOT NULL,
    status NVARCHAR(50) NOT NULL,
    hold_reason NVARCHAR(MAX) NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    modified_at DATETIME2 NULL,
    -- product_id / packaging_instructions are appended last (not grouped next to
    -- required_quantity) so a fresh install's column order matches a migrated DB's
    -- (ALTER TABLE ADD appends at the end) -- WorkOrder.initWorkOrders() reads
    -- SELECT * positionally, so the two layouts must agree.
    product_id INT NOT NULL,
    packaging_instructions NVARCHAR(MAX) NULL,
    CONSTRAINT CHK_WorkOrder_Status CHECK (status IN ('inProcess', 'completed', 'hasntEnteredIntoProductionYet')),
    CONSTRAINT FK_WorkOrder_ProductionOrder FOREIGN KEY (production_order_id)
        REFERENCES ProductionOrder(production_order_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_WorkOrder_Product FOREIGN KEY (product_id)
        REFERENCES Product(product_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- ============================================================================
-- TIER 4: TRANSACTIONAL RECORDS
-- ============================================================================

CREATE TABLE AttendanceRecord (
    attendance_record_id INT NOT NULL PRIMARY KEY,
    prisoner_id INT NOT NULL,
    attendance_date DATE NOT NULL,
    factory NVARCHAR(30) NOT NULL,
    entry_time TIME NULL,
    exit_time TIME NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT CHK_AttendanceRecord_Factory CHECK (factory IN ('MaimonSpices', 'Technosak', 'SewingWorkshop', 'TzitzitWorkshop')),
    CONSTRAINT FK_AttendanceRecord_Prisoner FOREIGN KEY (prisoner_id)
        REFERENCES Prisoner(prisoner_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE ProductivityRecord (
    productivity_record_id INT NOT NULL PRIMARY KEY,
    prisoner_id INT NOT NULL,
    work_order_id INT NOT NULL,
    productivity_date DATE NOT NULL,
    quantity_produced INT NOT NULL,
    work_hours DECIMAL(5,2),
    productivity_type NVARCHAR(50) NOT NULL,
    created_at DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT CHK_ProductivityRecord_Type CHECK (productivity_type IN ('Individual', 'Group')),
    CONSTRAINT UQ_ProductivityRecord_PrisonerWorkOrderDate UNIQUE (prisoner_id, work_order_id, productivity_date),
    CONSTRAINT FK_ProductivityRecord_Prisoner FOREIGN KEY (prisoner_id)
        REFERENCES Prisoner(prisoner_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_ProductivityRecord_WorkOrder FOREIGN KEY (work_order_id)
        REFERENCES WorkOrder(work_order_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
