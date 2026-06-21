USE sadcourse3;

-- ============================================================================
-- SEED DATA - PEDIS Test Database
-- ============================================================================
-- Load Order: Base entities → Core entities → Orders → Transactional records
-- FK references ensure no orphans
-- ============================================================================

-- ============================================================================
-- 1. EmploymentDepartment (3 rows)
-- ============================================================================

EXEC sp_EmploymentDepartment_create
    @employment_department_id = 1,
    @department_code = 'DEPT-EMP',
    @department_name = 'Employment Department',
    @location = 'Building A',
    @max_capacity = 150,
    @is_active = 1;

EXEC sp_EmploymentDepartment_create
    @employment_department_id = 2,
    @department_code = 'DEPT-MFG',
    @department_name = 'Manufacturing Oversight',
    @location = 'Building B',
    @max_capacity = 200,
    @is_active = 1;

EXEC sp_EmploymentDepartment_create
    @employment_department_id = 3,
    @department_code = 'DEPT-ADM',
    @department_name = 'Administrative Services',
    @location = 'Building A',
    @max_capacity = 100,
    @is_active = 1;

-- ============================================================================
-- 2. CustomerCompany (8 rows)
-- ============================================================================

EXEC sp_CustomerCompany_create
    @customer_company_id = 1,
    @company_id = 'CUST-MAIMON',
    @company_name = 'Maimon Spices Ltd',
    @contact_name = 'David Cohen',
    @phone_number = '08-6421234',
    @delivery_address = 'Jerusalem, Herzl Street 10',
    @email = 'david@maimon.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 2,
    @company_id = 'CUST-TECHNO',
    @company_name = 'Technosak Industries',
    @contact_name = 'Rachel Levi',
    @phone_number = '08-9876543',
    @delivery_address = 'Tel Aviv, Dizengoff 50',
    @email = 'rachel@technosak.com',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 3,
    @company_id = 'CUST-SEW',
    @company_name = 'Textile & Sewing Co',
    @contact_name = 'Moses Ravitz',
    @phone_number = '08-1122334',
    @delivery_address = 'Beersheva, HaNisayon 15',
    @email = 'moshe@textile.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 4,
    @company_id = 'CUST-INTERNAL',
    @company_name = 'Prison Authority Economic Dept',
    @contact_name = 'Yosi Gaber',
    @phone_number = '08-6450000',
    @delivery_address = 'Beersheva',
    @email = 'yosi@prison.gov.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 5,
    @company_id = 'CUST-EXPORT',
    @company_name = 'Southern Export Corp',
    @contact_name = 'Sarah Family',
    @phone_number = '08-7654321',
    @delivery_address = 'Eilat, Sapir Street 5',
    @email = 'sarah@export.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 6,
    @company_id = 'CUST-ORGANIC',
    @company_name = 'Organic Natural Products',
    @contact_name = 'Dana Green',
    @phone_number = '08-5555555',
    @delivery_address = 'Modiin, Ziv Street 20',
    @email = 'dana@organic.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 7,
    @company_id = 'CUST-PHARMA',
    @company_name = 'PharmaTech Solutions',
    @contact_name = 'Avi Kaplan',
    @phone_number = '08-4443333',
    @delivery_address = 'Petach Tikva, Bloch 8',
    @email = 'avi@pharmatech.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 8,
    @company_id = 'CUST-RETAIL',
    @company_name = 'Retail Networks Group',
    @contact_name = 'Lisa Stern',
    @phone_number = '08-3332222',
    @delivery_address = 'Ramat Gan, Jabotinsky 50',
    @email = 'lisa@retail.co.il',
    @activity_status = 'Active';

-- ============================================================================
-- 3. Product (6 rows)
-- ============================================================================

EXEC sp_Product_create
    @product_id = 1,
    @sku = 'SKU-SPICE-001',
    @product_name = 'Black Pepper Powder',
    @description = 'Premium black pepper, ground',
    @packaging_instructions = 'Pack in 500g bags, seal with tamper-proof label',
    @unit_of_measure = 'gr',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 2,
    @sku = 'SKU-SPICE-002',
    @product_name = 'Paprika Seasoning Mix',
    @description = 'Spiced paprika blend for cooking',
    @packaging_instructions = 'Pack in 250g containers',
    @unit_of_measure = 'gr',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 3,
    @sku = 'SKU-PLASTIC-001',
    @product_name = 'Clear Plastic Bottles',
    @description = '500ml clear plastic bottles with caps',
    @packaging_instructions = 'Pack in cartons of 100 units',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 4,
    @sku = 'SKU-TEXTILE-001',
    @product_name = 'Woven Fabric Rolls',
    @description = '100% cotton woven fabric, 1.5m wide',
    @packaging_instructions = 'Roll and wrap in protective plastic',
    @unit_of_measure = 'kg',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 5,
    @sku = 'SKU-TZITZIT-001',
    @product_name = 'Tzitzit Thread Bundles',
    @description = 'Hand-spun tzitzit thread, traditional quality',
    @packaging_instructions = 'Bundle in sets of 4, wrap per order',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 6,
    @sku = 'SKU-CRAFT-001',
    @product_name = 'Handmade Soap Bars',
    @description = 'Natural handmade soap, various scents',
    @packaging_instructions = 'Pack individually in cardboard boxes',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

-- ============================================================================
-- 4. DepartmentManagement (6 rows) - System Users
-- ============================================================================

EXEC sp_DepartmentManagement_create
    @department_management_id = 1,
    @username = 'admin_emp',
    @password = 'AdminPass123!',
    @role = 'departmentManager',
    @factory = NULL,
    @employment_department_id = 1;

EXEC sp_DepartmentManagement_create
    @department_management_id = 2,
    @username = 'deputy_emp',
    @password = 'DeputyPass123!',
    @role = 'deputyOfDepartmentManager',
    @factory = NULL,
    @employment_department_id = 1;

EXEC sp_DepartmentManagement_create
    @department_management_id = 3,
    @username = 'mgr_maimon',
    @password = 'MgrPass123!',
    @role = 'factoryManager',
    @factory = 'MaimonSpices',
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 4,
    @username = 'mgr_techno',
    @password = 'MgrPass123!',
    @role = 'factoryManager',
    @factory = 'Technosak',
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 5,
    @username = 'mgr_sewing',
    @password = 'MgrPass123!',
    @role = 'factoryManager',
    @factory = 'SewingWorkshop',
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 6,
    @username = 'mgr_tzitzit',
    @password = 'MgrPass123!',
    @role = 'factoryManager',
    @factory = 'TzitzitWorkshop',
    @employment_department_id = 2;

-- ============================================================================
-- 5. Prisoner (15 rows) - Inmates in Employment Program
-- ============================================================================

EXEC sp_Prisoner_create
    @prisoner_id = 1,
    @prisoner_number = 'P123456',
    @prisoner_name = 'Inmate A',
    @full_name = 'David Moshe Cohen',
    @factory = 'MaimonSpices',
    @department = 'Grinding',
    @activity_status = 'onShiftWorking',
    @role = 'Spice Grinder',
    @safety_training_validity = '2027-06-20',
    @work_start_date = '2025-06-20',
    @release_date = '2028-12-31',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2500.00;

EXEC sp_Prisoner_create
    @prisoner_id = 2,
    @prisoner_number = 'P123457',
    @prisoner_name = 'Inmate B',
    @full_name = 'Rachel Leah Levi',
    @factory = 'Technosak',
    @department = 'Molding',
    @activity_status = 'onShiftWorking',
    @role = 'Plastic Molder',
    @safety_training_validity = '2027-03-15',
    @work_start_date = '2025-03-15',
    @release_date = '2027-06-30',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 2300.00;

EXEC sp_Prisoner_create
    @prisoner_id = 3,
    @prisoner_number = 'P123458',
    @prisoner_name = 'Inmate C',
    @full_name = 'Yosi Mordecai Gaber',
    @factory = 'SewingWorkshop',
    @department = 'Sewing',
    @activity_status = 'inSafetyTraining',
    @role = 'Textile Operator',
    @safety_training_validity = NULL,
    @work_start_date = '2026-06-01',
    @release_date = '2029-01-15',
    @qualified = 0,
    @shabbat_keeping = 0,
    @min_salary = 2000.00;

EXEC sp_Prisoner_create
    @prisoner_id = 4,
    @prisoner_number = 'P123459',
    @prisoner_name = 'Inmate D',
    @full_name = 'Ariel Benjamin Rotman',
    @factory = 'TzitzitWorkshop',
    @department = 'Tzitzit Assembly',
    @activity_status = 'onShiftWorking',
    @role = 'Tzitzit Assembler',
    @safety_training_validity = '2026-12-01',
    @work_start_date = '2025-12-01',
    @release_date = '2026-09-30',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 2100.00;

EXEC sp_Prisoner_create
    @prisoner_id = 5,
    @prisoner_number = 'P123460',
    @prisoner_name = 'Inmate E',
    @full_name = 'Shlomo Kalman Ravitz',
    @factory = 'MaimonSpices',
    @department = 'Packaging',
    @activity_status = 'waitingForMaterials',
    @role = 'Packager',
    @safety_training_validity = '2027-09-10',
    @work_start_date = '2025-09-10',
    @release_date = '2030-03-20',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2200.00;

EXEC sp_Prisoner_create
    @prisoner_id = 6,
    @prisoner_number = 'P123461',
    @prisoner_name = 'Inmate F',
    @full_name = 'Avi Shaul Kaplan',
    @factory = 'Technosak',
    @department = 'Quality Control',
    @activity_status = 'onShiftWorking',
    @role = 'QC Inspector',
    @safety_training_validity = '2027-05-15',
    @work_start_date = '2025-05-15',
    @release_date = '2028-08-10',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 2600.00;

EXEC sp_Prisoner_create
    @prisoner_id = 7,
    @prisoner_number = 'P123462',
    @prisoner_name = 'Inmate G',
    @full_name = 'Sarah Miriam Stern',
    @factory = 'SewingWorkshop',
    @department = 'Cutting',
    @activity_status = 'inProfessionalTraining',
    @role = 'Cutter',
    @safety_training_validity = '2027-02-28',
    @work_start_date = '2025-02-28',
    @release_date = '2027-11-30',
    @qualified = 0,
    @shabbat_keeping = 0,
    @min_salary = 2150.00;

EXEC sp_Prisoner_create
    @prisoner_id = 8,
    @prisoner_number = 'P123463',
    @prisoner_name = 'Inmate H',
    @full_name = 'Moshe Netanel Rosen',
    @factory = NULL,
    @department = NULL,
    @activity_status = 'pendingPlacement',
    @role = NULL,
    @safety_training_validity = NULL,
    @work_start_date = NULL,
    @release_date = '2025-12-15',
    @qualified = 0,
    @shabbat_keeping = 0,
    @min_salary = NULL;

EXEC sp_Prisoner_create
    @prisoner_id = 9,
    @prisoner_number = 'P123464',
    @prisoner_name = 'Inmate I',
    @full_name = 'Eliyahu Yosef Wasserman',
    @factory = 'MaimonSpices',
    @department = 'Grinding',
    @activity_status = 'onShiftWorking',
    @role = 'Spice Grinder',
    @safety_training_validity = '2027-11-01',
    @work_start_date = '2025-11-01',
    @release_date = '2031-04-05',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 2400.00;

EXEC sp_Prisoner_create
    @prisoner_id = 10,
    @prisoner_number = 'P123465',
    @prisoner_name = 'Inmate J',
    @full_name = 'Lisa Chana Goldberg',
    @factory = NULL,
    @department = NULL,
    @activity_status = 'temporarilyUnavailable',
    @role = NULL,
    @safety_training_validity = NULL,
    @work_start_date = '2025-01-10',
    @release_date = '2029-06-20',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2350.00;

EXEC sp_Prisoner_create
    @prisoner_id = 11,
    @prisoner_number = 'P123466',
    @prisoner_name = 'Inmate K',
    @full_name = 'Tzion Meir Friedman',
    @factory = 'TzitzitWorkshop',
    @department = 'Tzitzit Assembly',
    @activity_status = 'onShiftWorking',
    @role = 'Tzitzit Assembler',
    @safety_training_validity = '2026-08-22',
    @work_start_date = '2025-08-22',
    @release_date = '2027-05-10',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 2050.00;

EXEC sp_Prisoner_create
    @prisoner_id = 12,
    @prisoner_number = 'P123467',
    @prisoner_name = 'Inmate L',
    @full_name = 'Daphna Noa Katz',
    @factory = 'SewingWorkshop',
    @department = 'Sewing',
    @activity_status = 'onShiftWorking',
    @role = 'Textile Operator',
    @safety_training_validity = '2027-04-10',
    @work_start_date = '2025-04-10',
    @release_date = '2028-10-30',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2250.00;

EXEC sp_Prisoner_create
    @prisoner_id = 13,
    @prisoner_number = 'P123468',
    @prisoner_name = 'Inmate M',
    @full_name = 'Amnon David Rabin',
    @factory = 'Technosak',
    @department = 'Molding',
    @activity_status = 'idle',
    @role = 'Plastic Molder',
    @safety_training_validity = '2026-10-05',
    @work_start_date = '2025-10-05',
    @release_date = '2026-07-20',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2300.00;

EXEC sp_Prisoner_create
    @prisoner_id = 14,
    @prisoner_number = 'P123469',
    @prisoner_name = 'Inmate N',
    @full_name = 'Chaim Yaakov Landau',
    @factory = NULL,
    @department = NULL,
    @activity_status = 'pendingDepartmentManagerApproval',
    @role = NULL,
    @safety_training_validity = NULL,
    @work_start_date = NULL,
    @release_date = '2028-02-14',
    @qualified = 0,
    @shabbat_keeping = 1,
    @min_salary = NULL;

EXEC sp_Prisoner_create
    @prisoner_id = 15,
    @prisoner_number = 'P123470',
    @prisoner_name = 'Inmate O',
    @full_name = 'Ronit Esther Bloch',
    @factory = 'MaimonSpices',
    @department = 'Packaging',
    @activity_status = 'onShiftWorking',
    @role = 'Packager',
    @safety_training_validity = '2027-07-18',
    @work_start_date = '2025-07-18',
    @release_date = '2030-09-25',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 2200.00;

-- ============================================================================
-- 6. Contract (5 rows)
-- ============================================================================

EXEC sp_Contract_create
    @contract_id = 1,
    @contract_number = 'CON-2026-001',
    @customer_company_id = 1,
    @product_id = 1,
    @start_date = '2026-01-01',
    @price_per_unit = 45.50,
    @payment_terms = 'Net 30, monthly invoicing',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 2,
    @contract_number = 'CON-2026-002',
    @customer_company_id = 2,
    @product_id = 3,
    @start_date = '2026-02-01',
    @price_per_unit = 12.75,
    @payment_terms = 'Net 45, quarterly review',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 3,
    @contract_number = 'CON-2026-003',
    @customer_company_id = 3,
    @product_id = 4,
    @start_date = '2026-03-15',
    @price_per_unit = 125.00,
    @payment_terms = 'Net 30, prepayment required',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 4,
    @contract_number = 'CON-2026-004',
    @customer_company_id = 4,
    @product_id = 2,
    @start_date = '2026-01-15',
    @price_per_unit = 38.00,
    @payment_terms = 'Net 60, government terms',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 5,
    @contract_number = 'CON-2026-005',
    @customer_company_id = 5,
    @product_id = 5,
    @start_date = '2026-04-01',
    @price_per_unit = 55.00,
    @payment_terms = 'Net 30, FOB destination',
    @contract_status = 'Inactive';

-- ============================================================================
-- 7. ProductionOrder (8 rows)
-- ============================================================================

EXEC sp_ProductionOrder_create
    @production_order_id = 1,
    @order_number = 'ORD-2026-0001',
    @customer_company_id = 1,
    @product_id = 1,
    @contract_id = 1,
    @quantity = 5000,
    @submission_date = '2026-06-01',
    @delivery_deadline = '2026-07-15',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 2,
    @order_number = 'ORD-2026-0002',
    @customer_company_id = 2,
    @product_id = 3,
    @contract_id = 2,
    @quantity = 10000,
    @submission_date = '2026-06-05',
    @delivery_deadline = '2026-08-01',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 3,
    @order_number = 'ORD-2026-0003',
    @customer_company_id = 3,
    @product_id = 4,
    @contract_id = 3,
    @quantity = 500,
    @submission_date = '2026-05-20',
    @delivery_deadline = '2026-07-01',
    @order_status = 'readyForPickup';

EXEC sp_ProductionOrder_create
    @production_order_id = 4,
    @order_number = 'ORD-2026-0004',
    @customer_company_id = 4,
    @product_id = 2,
    @contract_id = 4,
    @quantity = 3000,
    @submission_date = '2026-04-10',
    @delivery_deadline = '2026-06-15',
    @order_status = 'delivered';

EXEC sp_ProductionOrder_create
    @production_order_id = 5,
    @order_number = 'ORD-2026-0005',
    @customer_company_id = 6,
    @product_id = 6,
    @contract_id = NULL,
    @quantity = 2000,
    @submission_date = '2026-06-10',
    @delivery_deadline = '2026-07-20',
    @order_status = 'recieved';

EXEC sp_ProductionOrder_create
    @production_order_id = 6,
    @order_number = 'ORD-2026-0006',
    @customer_company_id = 1,
    @product_id = 1,
    @contract_id = 1,
    @quantity = 3000,
    @submission_date = '2026-05-15',
    @delivery_deadline = '2026-08-10',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 7,
    @order_number = 'ORD-2026-0007',
    @customer_company_id = 7,
    @product_id = 2,
    @contract_id = NULL,
    @quantity = 1500,
    @submission_date = '2026-06-12',
    @delivery_deadline = '2026-07-25',
    @order_status = 'onHold';

EXEC sp_ProductionOrder_create
    @production_order_id = 8,
    @order_number = 'ORD-2026-0008',
    @customer_company_id = 8,
    @product_id = 3,
    @contract_id = NULL,
    @quantity = 5000,
    @submission_date = '2026-06-08',
    @delivery_deadline = '2026-08-20',
    @order_status = 'inProduction';

-- ============================================================================
-- 8. WorkOrder (12 rows)
-- ============================================================================

EXEC sp_WorkOrder_create
    @work_order_id = 1,
    @work_order_number = 'WO-2026-0001',
    @production_order_id = 1,
    @required_quantity = 5000,
    @completed_quantity = 2500,
    @start_date = '2026-06-15',
    @deadline = '2026-07-15',
    @factory = 'MaimonSpices',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 2,
    @work_order_number = 'WO-2026-0002',
    @production_order_id = 2,
    @required_quantity = 10000,
    @completed_quantity = 3000,
    @start_date = '2026-06-20',
    @deadline = '2026-08-01',
    @factory = 'Technosak',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 3,
    @work_order_number = 'WO-2026-0003',
    @production_order_id = 3,
    @required_quantity = 500,
    @completed_quantity = 500,
    @start_date = '2026-05-20',
    @deadline = '2026-07-01',
    @factory = 'SewingWorkshop',
    @status = 'completed',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 4,
    @work_order_number = 'WO-2026-0004',
    @production_order_id = 4,
    @required_quantity = 3000,
    @completed_quantity = 3000,
    @start_date = '2026-04-10',
    @deadline = '2026-06-15',
    @factory = 'MaimonSpices',
    @status = 'completed',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 5,
    @work_order_number = 'WO-2026-0005',
    @production_order_id = 5,
    @required_quantity = 2000,
    @completed_quantity = 0,
    @start_date = '2026-06-20',
    @deadline = '2026-07-20',
    @factory = 'TzitzitWorkshop',
    @status = 'hasntEnteredIntoProductionYet',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 6,
    @work_order_number = 'WO-2026-0006',
    @production_order_id = 6,
    @required_quantity = 3000,
    @completed_quantity = 1500,
    @start_date = '2026-06-01',
    @deadline = '2026-08-10',
    @factory = 'MaimonSpices',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 7,
    @work_order_number = 'WO-2026-0007',
    @production_order_id = 7,
    @required_quantity = 1500,
    @completed_quantity = 0,
    @start_date = '2026-06-25',
    @deadline = '2026-07-25',
    @factory = 'MaimonSpices',
    @status = 'hasntEnteredIntoProductionYet',
    @hold_reason = 'Waiting for ingredient shipment';

EXEC sp_WorkOrder_create
    @work_order_id = 8,
    @work_order_number = 'WO-2026-0008',
    @production_order_id = 8,
    @required_quantity = 5000,
    @completed_quantity = 2000,
    @start_date = '2026-06-18',
    @deadline = '2026-08-20',
    @factory = 'Technosak',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 9,
    @work_order_number = 'WO-2026-0009',
    @production_order_id = 1,
    @required_quantity = 5000,
    @completed_quantity = 2500,
    @start_date = '2026-07-01',
    @deadline = '2026-07-20',
    @factory = 'MaimonSpices',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 10,
    @work_order_number = 'WO-2026-0010',
    @production_order_id = 2,
    @required_quantity = 10000,
    @completed_quantity = 5000,
    @start_date = '2026-06-15',
    @deadline = '2026-07-30',
    @factory = 'Technosak',
    @status = 'inProcess',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 11,
    @work_order_number = 'WO-2026-0011',
    @production_order_id = 3,
    @required_quantity = 500,
    @completed_quantity = 500,
    @start_date = '2026-06-10',
    @deadline = '2026-07-01',
    @factory = 'SewingWorkshop',
    @status = 'completed',
    @hold_reason = NULL;

EXEC sp_WorkOrder_create
    @work_order_id = 12,
    @work_order_number = 'WO-2026-0012',
    @production_order_id = 6,
    @required_quantity = 3000,
    @completed_quantity = 1000,
    @start_date = '2026-05-25',
    @deadline = '2026-08-05',
    @factory = 'MaimonSpices',
    @status = 'inProcess',
    @hold_reason = NULL;

-- ============================================================================
-- 9. AttendanceRecord (20 rows)
-- ============================================================================

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 1,
    @prisoner_id = 1,
    @attendance_date = '2026-06-20',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 2,
    @prisoner_id = 2,
    @attendance_date = '2026-06-20',
    @factory = 'Technosak',
    @entry_time = '06:30:00',
    @exit_time = '14:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 3,
    @prisoner_id = 4,
    @attendance_date = '2026-06-20',
    @factory = 'TzitzitWorkshop',
    @entry_time = '07:30:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 4,
    @prisoner_id = 5,
    @attendance_date = '2026-06-20',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 5,
    @prisoner_id = 6,
    @attendance_date = '2026-06-20',
    @factory = 'Technosak',
    @entry_time = '06:45:00',
    @exit_time = '15:15:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 6,
    @prisoner_id = 9,
    @attendance_date = '2026-06-20',
    @factory = 'MaimonSpices',
    @entry_time = '07:05:00',
    @exit_time = '15:35:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 7,
    @prisoner_id = 11,
    @attendance_date = '2026-06-20',
    @factory = 'TzitzitWorkshop',
    @entry_time = '07:30:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 8,
    @prisoner_id = 12,
    @attendance_date = '2026-06-20',
    @factory = 'SewingWorkshop',
    @entry_time = '07:15:00',
    @exit_time = '15:45:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 9,
    @prisoner_id = 13,
    @attendance_date = '2026-06-20',
    @factory = 'Technosak',
    @entry_time = '06:30:00',
    @exit_time = '14:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 10,
    @prisoner_id = 15,
    @attendance_date = '2026-06-20',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 11,
    @prisoner_id = 1,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 12,
    @prisoner_id = 2,
    @attendance_date = '2026-06-19',
    @factory = 'Technosak',
    @entry_time = '06:30:00',
    @exit_time = '14:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 13,
    @prisoner_id = 4,
    @attendance_date = '2026-06-19',
    @factory = 'TzitzitWorkshop',
    @entry_time = '07:30:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 14,
    @prisoner_id = 5,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 15,
    @prisoner_id = 6,
    @attendance_date = '2026-06-19',
    @factory = 'Technosak',
    @entry_time = '06:45:00',
    @exit_time = '15:15:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 16,
    @prisoner_id = 9,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '07:05:00',
    @exit_time = '15:35:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 17,
    @prisoner_id = 12,
    @attendance_date = '2026-06-19',
    @factory = 'SewingWorkshop',
    @entry_time = '07:15:00',
    @exit_time = '15:45:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 18,
    @prisoner_id = 15,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 19,
    @prisoner_id = 1,
    @attendance_date = '2026-06-18',
    @factory = 'MaimonSpices',
    @entry_time = '07:00:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 20,
    @prisoner_id = 2,
    @attendance_date = '2026-06-18',
    @factory = 'Technosak',
    @entry_time = '06:30:00',
    @exit_time = '14:30:00';

-- ============================================================================
-- 10. ProductivityRecord (20 rows)
-- ============================================================================

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 1,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-20',
    @quantity_produced = 450,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 2,
    @prisoner_id = 2,
    @work_order_id = 2,
    @productivity_date = '2026-06-20',
    @quantity_produced = 800,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 3,
    @prisoner_id = 4,
    @work_order_id = 5,
    @productivity_date = '2026-06-20',
    @quantity_produced = 120,
    @work_hours = 8.5,
    @productivity_type = 'Group';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 4,
    @prisoner_id = 5,
    @work_order_id = 1,
    @productivity_date = '2026-06-20',
    @quantity_produced = 380,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 5,
    @prisoner_id = 6,
    @work_order_id = 2,
    @productivity_date = '2026-06-20',
    @quantity_produced = 650,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 6,
    @prisoner_id = 9,
    @work_order_id = 6,
    @productivity_date = '2026-06-20',
    @quantity_produced = 500,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 7,
    @prisoner_id = 11,
    @work_order_id = 5,
    @productivity_date = '2026-06-20',
    @quantity_produced = 100,
    @work_hours = 8.5,
    @productivity_type = 'Group';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 8,
    @prisoner_id = 12,
    @work_order_id = 3,
    @productivity_date = '2026-06-20',
    @quantity_produced = 200,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 9,
    @prisoner_id = 13,
    @work_order_id = 8,
    @productivity_date = '2026-06-20',
    @quantity_produced = 700,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 10,
    @prisoner_id = 15,
    @work_order_id = 12,
    @productivity_date = '2026-06-20',
    @quantity_produced = 320,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 11,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-19',
    @quantity_produced = 420,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 12,
    @prisoner_id = 2,
    @work_order_id = 2,
    @productivity_date = '2026-06-19',
    @quantity_produced = 750,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 13,
    @prisoner_id = 5,
    @work_order_id = 1,
    @productivity_date = '2026-06-19',
    @quantity_produced = 400,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 14,
    @prisoner_id = 6,
    @work_order_id = 2,
    @productivity_date = '2026-06-19',
    @quantity_produced = 680,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 15,
    @prisoner_id = 9,
    @work_order_id = 6,
    @productivity_date = '2026-06-19',
    @quantity_produced = 480,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 16,
    @prisoner_id = 12,
    @work_order_id = 3,
    @productivity_date = '2026-06-19',
    @quantity_produced = 180,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 17,
    @prisoner_id = 15,
    @work_order_id = 12,
    @productivity_date = '2026-06-19',
    @quantity_produced = 350,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 18,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-18',
    @quantity_produced = 400,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 19,
    @prisoner_id = 2,
    @work_order_id = 2,
    @productivity_date = '2026-06-18',
    @quantity_produced = 770,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 20,
    @prisoner_id = 5,
    @work_order_id = 1,
    @productivity_date = '2026-06-18',
    @quantity_produced = 390,
    @work_hours = 8.5,
    @productivity_type = 'Individual';
