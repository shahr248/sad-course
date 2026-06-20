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
    @department_name = 'מחלקת העסקה',
    @location = 'בנין א',
    @max_capacity = 150,
    @is_active = 1;

EXEC sp_EmploymentDepartment_create
    @employment_department_id = 2,
    @department_code = 'DEPT-MFG',
    @department_name = 'פיקוח ייצור',
    @location = 'בנין ב',
    @max_capacity = 200,
    @is_active = 1;

EXEC sp_EmploymentDepartment_create
    @employment_department_id = 3,
    @department_code = 'DEPT-ADM',
    @department_name = 'שירותים ניהוליים',
    @location = 'בנין א',
    @max_capacity = 100,
    @is_active = 1;

-- ============================================================================
-- 2. CustomerCompany (8 rows)
-- ============================================================================

EXEC sp_CustomerCompany_create
    @customer_company_id = 1,
    @company_id = 'CUST-MAIMON',
    @company_name = 'תבלינים מימון',
    @contact_name = 'דוד כהן',
    @phone_number = '08-6421234',
    @delivery_address = 'ירושלים, רח\' הרצל 10',
    @email = 'david@maimon.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 2,
    @company_id = 'CUST-TECHNO',
    @company_name = 'טכנוסק בע"מ',
    @contact_name = 'רחל לוי',
    @phone_number = '08-9876543',
    @delivery_address = 'תל אביב, רח\' דיזנגוף 50',
    @email = 'rachel@technosak.com',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 3,
    @company_id = 'CUST-SEW',
    @company_name = 'תיקייה וטקסטיל',
    @contact_name = 'משה רביץ',
    @phone_number = '08-1122334',
    @delivery_address = 'באר שבע, רח\' הנסיון 15',
    @email = 'moshe@textile.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 4,
    @company_id = 'CUST-INTERNAL',
    @company_name = 'רשות כלא - מחלקה כלכלית',
    @contact_name = 'יוסי גבר',
    @phone_number = '08-6450000',
    @delivery_address = 'באר שבע',
    @email = 'yosi@prison.gov.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 5,
    @company_id = 'CUST-EXPORT',
    @company_name = 'ייצוא הדרומית',
    @contact_name = 'שרה משפחתי',
    @phone_number = '08-7654321',
    @delivery_address = 'אילת, רח\' ספיר 5',
    @email = 'sarah@export.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 6,
    @company_id = 'CUST-ORGANIC',
    @company_name = 'אורגני טבעי',
    @contact_name = 'דנה גרין',
    @phone_number = '08-5555555',
    @delivery_address = 'מודיעין, רח\' זיו 20',
    @email = 'dana@organic.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 7,
    @company_id = 'CUST-RETAIL',
    @company_name = 'רשת קמעונאית גדולה',
    @contact_name = 'יעקב סלע',
    @phone_number = '08-9999999',
    @delivery_address = 'תל אביב, רח\' דרך ישראל 100',
    @email = 'yaakov@retail.co.il',
    @activity_status = 'Active';

EXEC sp_CustomerCompany_create
    @customer_company_id = 8,
    @company_id = 'CUST-FASHION',
    @company_name = 'אופנה וטקסטיל',
    @contact_name = 'דינה נחשון',
    @phone_number = '08-4444444',
    @delivery_address = 'ירושלים, רח\' יפו 25',
    @email = 'dina@fashion.co.il',
    @activity_status = 'Active';

-- ============================================================================
-- 3. Product (6 rows)
-- ============================================================================

EXEC sp_Product_create
    @product_id = 1,
    @sku = 'SKU-SPICE-001',
    @product_name = 'תערובת תבלינים',
    @description = 'תערובת תבלינים טבעית למזון',
    @packaging_instructions = 'אריזה בשקיות 1 ק"ג או לפי הזמנה',
    @unit_of_measure = 'kg',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 2,
    @sku = 'SKU-TECH-001',
    @product_name = 'מכלולי פלסטיק',
    @description = 'חלקי פלסטיק מחודדים לתעשייה',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 3,
    @sku = 'SKU-SEW-001',
    @product_name = 'תיקיות בד טבעי',
    @description = 'תיקיות עשויות בד טבעי 100%',
    @packaging_instructions = 'אריזה 50 יחידות למגש',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 4,
    @sku = 'SKU-TZITZIT-001',
    @product_name = 'ציצית',
    @description = 'ציצית צבעוני דקים',
    @unit_of_measure = 'gr',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 5,
    @sku = 'SKU-SPICE-002',
    @product_name = 'קמח תבלינים',
    @description = 'קמח תבלינים דק',
    @unit_of_measure = 'kg',
    @activity_status = 'Active';

EXEC sp_Product_create
    @product_id = 6,
    @sku = 'SKU-SEW-002',
    @product_name = 'חגורה עור',
    @description = 'חגורה עור טבעית',
    @unit_of_measure = 'units',
    @activity_status = 'Active';

-- ============================================================================
-- 4. DepartmentManagement (6 users - 2 per role)
-- ============================================================================

EXEC sp_DepartmentManagement_create
    @department_management_id = 1,
    @username = 'manager1',
    @password = 'password123',
    @role = 'departmentManager',
    @factory = NULL,
    @employment_department_id = 1;

EXEC sp_DepartmentManagement_create
    @department_management_id = 2,
    @username = 'manager2',
    @password = 'password123',
    @role = 'departmentManager',
    @factory = NULL,
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 3,
    @username = 'deputy1',
    @password = 'password123',
    @role = 'deputyOfDepartmentManager',
    @factory = NULL,
    @employment_department_id = 1;

EXEC sp_DepartmentManagement_create
    @department_management_id = 4,
    @username = 'deputy2',
    @password = 'password123',
    @role = 'deputyOfDepartmentManager',
    @factory = NULL,
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 5,
    @username = 'factory_mgr_maimon',
    @password = 'password123',
    @role = 'factoryManager',
    @factory = 'MaimonSpices',
    @employment_department_id = 2;

EXEC sp_DepartmentManagement_create
    @department_management_id = 6,
    @username = 'factory_mgr_techno',
    @password = 'password123',
    @role = 'factoryManager',
    @factory = 'Technosak',
    @employment_department_id = 2;

-- ============================================================================
-- 5. Prisoner (15 rows - various statuses and factories)
-- ============================================================================

EXEC sp_Prisoner_create
    @prisoner_id = 1,
    @prisoner_number = 'P001234',
    @prisoner_name = 'דוד',
    @full_name = 'דוד כהן',
    @factory = 'MaimonSpices',
    @department = 'Production',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2026-12-31',
    @work_start_date = '2025-01-15',
    @release_date = '2028-06-20',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 500.00;

EXEC sp_Prisoner_create
    @prisoner_id = 2,
    @prisoner_number = 'P001235',
    @prisoner_name = 'משה',
    @full_name = 'משה לוי',
    @factory = 'Technosak',
    @department = 'Assembly',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2027-03-15',
    @work_start_date = '2025-03-01',
    @release_date = '2027-12-25',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 550.00;

EXEC sp_Prisoner_create
    @prisoner_id = 3,
    @prisoner_number = 'P001236',
    @prisoner_name = 'יוסי',
    @full_name = 'יוסף רביץ',
    @factory = 'SewingWorkshop',
    @department = 'Sewing',
    @activity_status = 'idle',
    @role = 'Worker',
    @safety_training_validity = '2026-06-30',
    @work_start_date = '2024-06-15',
    @release_date = '2026-12-15',
    @qualified = 0,
    @shabbat_keeping = 1,
    @min_salary = 450.00;

EXEC sp_Prisoner_create
    @prisoner_id = 4,
    @prisoner_number = 'P001237',
    @prisoner_name = 'אלי',
    @full_name = 'אלישע גבר',
    @factory = 'TzitzitWorkshop',
    @department = 'Wrapping',
    @activity_status = 'onShiftWorking',
    @role = 'Supervisor',
    @safety_training_validity = '2028-01-10',
    @work_start_date = '2023-09-01',
    @release_date = '2029-03-20',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 650.00;

EXEC sp_Prisoner_create
    @prisoner_id = 5,
    @prisoner_number = 'P001238',
    @prisoner_name = 'נחום',
    @full_name = 'נחום משפחתי',
    @factory = 'MaimonSpices',
    @department = 'Packing',
    @activity_status = 'waitingForMaterials',
    @role = 'Worker',
    @safety_training_validity = '2026-08-15',
    @work_start_date = '2025-02-10',
    @release_date = '2029-01-30',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 480.00;

EXEC sp_Prisoner_create
    @prisoner_id = 6,
    @prisoner_number = 'P001239',
    @prisoner_name = 'דן',
    @full_name = 'דן סלע',
    @factory = 'Technosak',
    @department = 'Quality',
    @activity_status = 'temporarilyUnavailable',
    @role = 'Technician',
    @safety_training_validity = '2027-05-20',
    @work_start_date = '2024-05-15',
    @release_date = '2028-11-10',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 600.00;

EXEC sp_Prisoner_create
    @prisoner_id = 7,
    @prisoner_number = 'P001240',
    @prisoner_name = 'רוני',
    @full_name = 'רוני נחשון',
    @factory = 'SewingWorkshop',
    @department = 'Cutting',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2026-11-30',
    @work_start_date = '2025-04-01',
    @release_date = '2027-09-15',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 520.00;

EXEC sp_Prisoner_create
    @prisoner_id = 8,
    @prisoner_number = 'P001241',
    @prisoner_name = 'אלון',
    @full_name = 'אלון ירוק',
    @factory = 'MaimonSpices',
    @department = 'Production',
    @activity_status = 'idle',
    @role = 'Worker',
    @safety_training_validity = '2026-07-15',
    @work_start_date = '2025-01-01',
    @release_date = '2026-08-20',
    @qualified = 0,
    @shabbat_keeping = 1,
    @min_salary = 400.00;

EXEC sp_Prisoner_create
    @prisoner_id = 9,
    @prisoner_number = 'P001242',
    @prisoner_name = 'גדי',
    @full_name = 'גדי ברקוביץ',
    @factory = 'TzitzitWorkshop',
    @department = 'Packing',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2027-09-10',
    @work_start_date = '2024-10-15',
    @release_date = '2028-05-25',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 510.00;

EXEC sp_Prisoner_create
    @prisoner_id = 10,
    @prisoner_number = 'P001243',
    @prisoner_name = 'שלום',
    @full_name = 'שלום כספי',
    @factory = 'Technosak',
    @department = 'Assembly',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2026-04-20',
    @work_start_date = '2025-02-15',
    @release_date = '2027-07-10',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 540.00;

EXEC sp_Prisoner_create
    @prisoner_id = 11,
    @prisoner_number = 'P001244',
    @prisoner_name = 'יעקב',
    @full_name = 'יעקב אלטמן',
    @factory = 'SewingWorkshop',
    @department = 'Finishing',
    @activity_status = 'inSafetyTraining',
    @role = 'Worker',
    @safety_training_validity = NULL,
    @work_start_date = '2026-06-01',
    @release_date = '2028-12-30',
    @qualified = 0,
    @shabbat_keeping = 1,
    @min_salary = 300.00;

EXEC sp_Prisoner_create
    @prisoner_id = 12,
    @prisoner_number = 'P001245',
    @prisoner_name = 'בנימין',
    @full_name = 'בנימין חיים',
    @factory = 'MaimonSpices',
    @department = 'Grinding',
    @activity_status = 'onShiftWorking',
    @role = 'Technician',
    @safety_training_validity = '2027-12-25',
    @work_start_date = '2024-12-01',
    @release_date = '2029-06-15',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 620.00;

EXEC sp_Prisoner_create
    @prisoner_id = 13,
    @prisoner_number = 'P001246',
    @prisoner_name = 'סימון',
    @full_name = 'סימון כץ',
    @factory = NULL,
    @department = NULL,
    @activity_status = 'pendingPlacement',
    @role = NULL,
    @safety_training_validity = NULL,
    @work_start_date = NULL,
    @release_date = '2030-01-20',
    @qualified = 0,
    @shabbat_keeping = 0,
    @min_salary = NULL;

EXEC sp_Prisoner_create
    @prisoner_id = 14,
    @prisoner_number = 'P001247',
    @prisoner_name = 'צביקא',
    @full_name = 'צביקא דרור',
    @factory = 'Technosak',
    @department = 'Assembly',
    @activity_status = 'onShiftWorking',
    @role = 'Worker',
    @safety_training_validity = '2026-09-30',
    @work_start_date = '2025-05-10',
    @release_date = '2027-11-05',
    @qualified = 1,
    @shabbat_keeping = 1,
    @min_salary = 530.00;

EXEC sp_Prisoner_create
    @prisoner_id = 15,
    @prisoner_number = 'P001248',
    @prisoner_name = 'חנוך',
    @full_name = 'חנוך סלע',
    @factory = 'SewingWorkshop',
    @department = 'Sewing',
    @activity_status = 'idle',
    @role = 'Worker',
    @safety_training_validity = '2026-10-15',
    @work_start_date = '2024-07-20',
    @release_date = '2026-09-30',
    @qualified = 1,
    @shabbat_keeping = 0,
    @min_salary = 490.00;

-- ============================================================================
-- 6. Contract (5 rows)
-- ============================================================================

EXEC sp_Contract_create
    @contract_id = 1,
    @contract_number = 'CONT-2026-001',
    @customer_company_id = 1,
    @product_id = 1,
    @start_date = '2026-01-01',
    @price_per_unit = 50.00,
    @payment_terms = 'Net 30',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 2,
    @contract_number = 'CONT-2026-002',
    @customer_company_id = 2,
    @product_id = 2,
    @start_date = '2026-02-01',
    @price_per_unit = 150.00,
    @payment_terms = 'Net 45',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 3,
    @contract_number = 'CONT-2026-003',
    @customer_company_id = 3,
    @product_id = 3,
    @start_date = '2026-03-01',
    @price_per_unit = 75.00,
    @payment_terms = 'Net 30',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 4,
    @contract_number = 'CONT-2026-004',
    @customer_company_id = 4,
    @product_id = 5,
    @start_date = '2026-04-01',
    @price_per_unit = 45.00,
    @payment_terms = 'Net 15',
    @contract_status = 'Active';

EXEC sp_Contract_create
    @contract_id = 5,
    @contract_number = 'CONT-2026-005',
    @customer_company_id = 5,
    @product_id = 1,
    @start_date = '2026-05-01',
    @price_per_unit = 55.00,
    @payment_terms = 'Net 60',
    @contract_status = 'Active';

-- ============================================================================
-- 7. ProductionOrder (8 rows - various statuses)
-- ============================================================================

EXEC sp_ProductionOrder_create
    @production_order_id = 1,
    @order_number = 'ORD-2026-0001',
    @customer_company_id = 1,
    @product_id = 1,
    @contract_id = 1,
    @quantity = 500,
    @submission_date = '2026-06-01',
    @delivery_deadline = '2026-07-01',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 2,
    @order_number = 'ORD-2026-0002',
    @customer_company_id = 2,
    @product_id = 2,
    @contract_id = 2,
    @quantity = 300,
    @submission_date = '2026-06-05',
    @delivery_deadline = '2026-07-05',
    @order_status = 'recieved';

EXEC sp_ProductionOrder_create
    @production_order_id = 3,
    @order_number = 'ORD-2026-0003',
    @customer_company_id = 3,
    @product_id = 3,
    @contract_id = 3,
    @quantity = 1000,
    @submission_date = '2026-05-20',
    @delivery_deadline = '2026-06-30',
    @order_status = 'readyForPickup';

EXEC sp_ProductionOrder_create
    @production_order_id = 4,
    @order_number = 'ORD-2026-0004',
    @customer_company_id = 4,
    @product_id = 5,
    @contract_id = 4,
    @quantity = 200,
    @submission_date = '2026-06-10',
    @delivery_deadline = '2026-07-10',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 5,
    @order_number = 'ORD-2026-0005',
    @customer_company_id = 5,
    @product_id = 1,
    @contract_id = 5,
    @quantity = 750,
    @submission_date = '2026-06-15',
    @delivery_deadline = '2026-08-15',
    @order_status = 'inProduction';

EXEC sp_ProductionOrder_create
    @production_order_id = 6,
    @order_number = 'ORD-2026-0006',
    @customer_company_id = 6,
    @product_id = 1,
    @quantity = 400,
    @submission_date = '2026-05-01',
    @delivery_deadline = '2026-06-15',
    @order_status = 'delivered';

EXEC sp_ProductionOrder_create
    @production_order_id = 7,
    @order_number = 'ORD-2026-0007',
    @customer_company_id = 7,
    @product_id = 3,
    @quantity = 600,
    @submission_date = '2026-06-12',
    @delivery_deadline = '2026-07-20',
    @order_status = 'onHold';

EXEC sp_ProductionOrder_create
    @production_order_id = 8,
    @order_number = 'ORD-2026-0008',
    @customer_company_id = 8,
    @product_id = 6,
    @quantity = 250,
    @submission_date = '2026-06-18',
    @delivery_deadline = '2026-08-01',
    @order_status = 'recieved';

-- ============================================================================
-- 8. WorkOrder (12 rows - 1-2 per ProductionOrder)
-- ============================================================================

EXEC sp_WorkOrder_create
    @work_order_id = 1,
    @work_order_number = 'WO-2026-0001',
    @production_order_id = 1,
    @required_quantity = 500,
    @completed_quantity = 450,
    @start_date = '2026-06-05',
    @deadline = '2026-06-25',
    @factory = 'MaimonSpices',
    @status = 'inProcess';

EXEC sp_WorkOrder_create
    @work_order_id = 2,
    @work_order_number = 'WO-2026-0002',
    @production_order_id = 2,
    @required_quantity = 300,
    @completed_quantity = 0,
    @start_date = '2026-06-08',
    @deadline = '2026-07-05',
    @factory = 'Technosak',
    @status = 'hasntEnteredIntoProductionYet';

EXEC sp_WorkOrder_create
    @work_order_id = 3,
    @work_order_number = 'WO-2026-0003',
    @production_order_id = 3,
    @required_quantity = 1000,
    @completed_quantity = 1000,
    @start_date = '2026-05-25',
    @deadline = '2026-06-20',
    @factory = 'SewingWorkshop',
    @status = 'completed';

EXEC sp_WorkOrder_create
    @work_order_id = 4,
    @work_order_number = 'WO-2026-0004',
    @production_order_id = 4,
    @required_quantity = 200,
    @completed_quantity = 50,
    @start_date = '2026-06-12',
    @deadline = '2026-07-10',
    @factory = 'TzitzitWorkshop',
    @status = 'inProcess',
    @hold_reason = 'חיתוך חוט חסר';

EXEC sp_WorkOrder_create
    @work_order_id = 5,
    @work_order_number = 'WO-2026-0005',
    @production_order_id = 5,
    @required_quantity = 750,
    @completed_quantity = 200,
    @start_date = '2026-06-17',
    @deadline = '2026-08-15',
    @factory = 'MaimonSpices',
    @status = 'inProcess';

EXEC sp_WorkOrder_create
    @work_order_id = 6,
    @work_order_number = 'WO-2026-0006',
    @production_order_id = 6,
    @required_quantity = 400,
    @completed_quantity = 400,
    @start_date = '2026-05-05',
    @deadline = '2026-06-10',
    @factory = 'MaimonSpices',
    @status = 'completed';

EXEC sp_WorkOrder_create
    @work_order_id = 7,
    @work_order_number = 'WO-2026-0007',
    @production_order_id = 7,
    @required_quantity = 600,
    @completed_quantity = 100,
    @start_date = '2026-06-15',
    @deadline = '2026-07-20',
    @factory = 'SewingWorkshop',
    @status = 'inProcess',
    @hold_reason = 'בעיה בתיאום עם לקוח';

EXEC sp_WorkOrder_create
    @work_order_id = 8,
    @work_order_number = 'WO-2026-0008',
    @production_order_id = 8,
    @required_quantity = 250,
    @completed_quantity = 0,
    @start_date = '2026-06-20',
    @deadline = '2026-08-01',
    @factory = 'SewingWorkshop',
    @status = 'hasntEnteredIntoProductionYet';

EXEC sp_WorkOrder_create
    @work_order_id = 9,
    @work_order_number = 'WO-2026-0009',
    @production_order_id = 1,
    @required_quantity = 500,
    @completed_quantity = 450,
    @start_date = '2026-06-05',
    @deadline = '2026-06-25',
    @factory = 'MaimonSpices',
    @status = 'completed';

EXEC sp_WorkOrder_create
    @work_order_id = 10,
    @work_order_number = 'WO-2026-0010',
    @production_order_id = 4,
    @required_quantity = 200,
    @completed_quantity = 150,
    @start_date = '2026-06-15',
    @deadline = '2026-07-15',
    @factory = 'TzitzitWorkshop',
    @status = 'inProcess';

EXEC sp_WorkOrder_create
    @work_order_id = 11,
    @work_order_number = 'WO-2026-0011',
    @production_order_id = 5,
    @required_quantity = 750,
    @completed_quantity = 350,
    @start_date = '2026-06-17',
    @deadline = '2026-08-15',
    @factory = 'MaimonSpices',
    @status = 'inProcess';

EXEC sp_WorkOrder_create
    @work_order_id = 12,
    @work_order_number = 'WO-2026-0012',
    @production_order_id = 2,
    @required_quantity = 300,
    @completed_quantity = 100,
    @start_date = '2026-06-10',
    @deadline = '2026-07-08',
    @factory = 'Technosak',
    @status = 'inProcess';

-- ============================================================================
-- 9. AttendanceRecord (20 rows - various dates and factories)
-- ============================================================================

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 1,
    @prisoner_id = 1,
    @attendance_date = '2026-06-18',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 2,
    @prisoner_id = 1,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '16:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 3,
    @prisoner_id = 2,
    @attendance_date = '2026-06-18',
    @factory = 'Technosak',
    @entry_time = '07:30:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 4,
    @prisoner_id = 3,
    @attendance_date = '2026-06-18',
    @factory = 'SewingWorkshop',
    @entry_time = NULL,
    @exit_time = NULL;

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 5,
    @prisoner_id = 4,
    @attendance_date = '2026-06-18',
    @factory = 'TzitzitWorkshop',
    @entry_time = '07:00:00',
    @exit_time = '17:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 6,
    @prisoner_id = 5,
    @attendance_date = '2026-06-18',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '12:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 7,
    @prisoner_id = 6,
    @attendance_date = '2026-06-18',
    @factory = 'Technosak',
    @entry_time = NULL,
    @exit_time = NULL;

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 8,
    @prisoner_id = 7,
    @attendance_date = '2026-06-18',
    @factory = 'SewingWorkshop',
    @entry_time = '08:15:00',
    @exit_time = '16:15:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 9,
    @prisoner_id = 8,
    @attendance_date = '2026-06-18',
    @factory = 'MaimonSpices',
    @entry_time = NULL,
    @exit_time = NULL;

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 10,
    @prisoner_id = 9,
    @attendance_date = '2026-06-18',
    @factory = 'TzitzitWorkshop',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 11,
    @prisoner_id = 1,
    @attendance_date = '2026-06-17',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 12,
    @prisoner_id = 2,
    @attendance_date = '2026-06-17',
    @factory = 'Technosak',
    @entry_time = '07:30:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 13,
    @prisoner_id = 4,
    @attendance_date = '2026-06-17',
    @factory = 'TzitzitWorkshop',
    @entry_time = '07:00:00',
    @exit_time = '17:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 14,
    @prisoner_id = 7,
    @attendance_date = '2026-06-17',
    @factory = 'SewingWorkshop',
    @entry_time = '08:15:00',
    @exit_time = '16:15:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 15,
    @prisoner_id = 10,
    @attendance_date = '2026-06-18',
    @factory = 'Technosak',
    @entry_time = '07:45:00',
    @exit_time = '15:45:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 16,
    @prisoner_id = 12,
    @attendance_date = '2026-06-18',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 17,
    @prisoner_id = 14,
    @attendance_date = '2026-06-18',
    @factory = 'Technosak',
    @entry_time = '07:30:00',
    @exit_time = '15:30:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 18,
    @prisoner_id = 15,
    @attendance_date = '2026-06-18',
    @factory = 'SewingWorkshop',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 19,
    @prisoner_id = 5,
    @attendance_date = '2026-06-19',
    @factory = 'MaimonSpices',
    @entry_time = '08:00:00',
    @exit_time = '14:00:00';

EXEC sp_AttendanceRecord_create
    @attendance_record_id = 20,
    @prisoner_id = 9,
    @attendance_date = '2026-06-19',
    @factory = 'TzitzitWorkshop',
    @entry_time = '08:00:00',
    @exit_time = '16:00:00';

-- ============================================================================
-- 10. ProductivityRecord (20 rows - Individual and Group types)
-- ============================================================================

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 1,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-18',
    @quantity_produced = 50,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 2,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-19',
    @quantity_produced = 45,
    @work_hours = 8.5,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 3,
    @prisoner_id = 2,
    @work_order_id = 12,
    @productivity_date = '2026-06-18',
    @quantity_produced = 30,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 4,
    @prisoner_id = 4,
    @work_order_id = 4,
    @productivity_date = '2026-06-18',
    @quantity_produced = 25,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 5,
    @prisoner_id = 5,
    @work_order_id = 1,
    @productivity_date = '2026-06-18',
    @quantity_produced = 40,
    @work_hours = 4.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 6,
    @prisoner_id = 7,
    @work_order_id = 3,
    @productivity_date = '2026-06-18',
    @quantity_produced = 120,
    @work_hours = 8.0,
    @productivity_type = 'Group';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 7,
    @prisoner_id = 9,
    @work_order_id = 10,
    @productivity_date = '2026-06-18',
    @quantity_produced = 35,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 8,
    @prisoner_id = 10,
    @work_order_id = 12,
    @productivity_date = '2026-06-18',
    @quantity_produced = 28,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 9,
    @prisoner_id = 12,
    @work_order_id = 5,
    @productivity_date = '2026-06-18',
    @quantity_produced = 55,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 10,
    @prisoner_id = 14,
    @work_order_id = 12,
    @productivity_date = '2026-06-18',
    @quantity_produced = 32,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 11,
    @prisoner_id = 15,
    @work_order_id = 3,
    @productivity_date = '2026-06-18',
    @quantity_produced = 125,
    @work_hours = 8.0,
    @productivity_type = 'Group';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 12,
    @prisoner_id = 1,
    @work_order_id = 9,
    @productivity_date = '2026-06-17',
    @quantity_produced = 48,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 13,
    @prisoner_id = 2,
    @work_order_id = 12,
    @productivity_date = '2026-06-17',
    @quantity_produced = 28,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 14,
    @prisoner_id = 4,
    @work_order_id = 10,
    @productivity_date = '2026-06-17',
    @quantity_produced = 40,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 15,
    @prisoner_id = 7,
    @work_order_id = 7,
    @productivity_date = '2026-06-17',
    @quantity_produced = 50,
    @work_hours = 8.0,
    @productivity_type = 'Group';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 16,
    @prisoner_id = 9,
    @work_order_id = 10,
    @productivity_date = '2026-06-17',
    @quantity_produced = 38,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 17,
    @prisoner_id = 5,
    @work_order_id = 5,
    @productivity_date = '2026-06-19',
    @quantity_produced = 35,
    @work_hours = 6.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 18,
    @prisoner_id = 12,
    @work_order_id = 11,
    @productivity_date = '2026-06-18',
    @quantity_produced = 60,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 19,
    @prisoner_id = 1,
    @work_order_id = 1,
    @productivity_date = '2026-06-17',
    @quantity_produced = 52,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

EXEC sp_ProductivityRecord_create
    @productivity_record_id = 20,
    @prisoner_id = 9,
    @work_order_id = 4,
    @productivity_date = '2026-06-19',
    @quantity_produced = 30,
    @work_hours = 8.0,
    @productivity_type = 'Individual';

-- ============================================================================
-- SUMMARY - Row counts per table
-- ============================================================================

PRINT '=== DATA SEED SUMMARY ===';
PRINT '';

SELECT 'EmploymentDepartment' AS TableName, COUNT(*) AS RowCount FROM EmploymentDepartment
UNION ALL
SELECT 'CustomerCompany', COUNT(*) FROM CustomerCompany
UNION ALL
SELECT 'Product', COUNT(*) FROM Product
UNION ALL
SELECT 'DepartmentManagement', COUNT(*) FROM DepartmentManagement
UNION ALL
SELECT 'Prisoner', COUNT(*) FROM Prisoner
UNION ALL
SELECT 'Contract', COUNT(*) FROM Contract
UNION ALL
SELECT 'ProductionOrder', COUNT(*) FROM ProductionOrder
UNION ALL
SELECT 'WorkOrder', COUNT(*) FROM WorkOrder
UNION ALL
SELECT 'AttendanceRecord', COUNT(*) FROM AttendanceRecord
UNION ALL
SELECT 'ProductivityRecord', COUNT(*) FROM ProductivityRecord
ORDER BY TableName;
