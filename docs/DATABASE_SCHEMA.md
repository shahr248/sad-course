# PEDIS Database Schema

Complete documentation of the Prison Employment Department Information System (PEDIS) database schema.

---

## Table of Contents
1. [Tables Overview](#tables-overview)
2. [Detailed Table Structures](#detailed-table-structures)
3. [Relationships & Foreign Keys](#relationships--foreign-keys)
4. [Enum Values](#enum-values)

---

## Tables Overview

| # | Table Name | Purpose | Rows | Type |
|---|---|---|---|---|
| 1 | EmploymentDepartment | Prison departments | ~3 | Base |
| 2 | CustomerCompany | External customers | ~8 | Base |
| 3 | Product | Product catalog | ~6 | Base |
| 4 | DepartmentManagement | System users & login | ~6 | Core |
| 5 | Prisoner | Prison inmates/employees | ~15 | Core |
| 6 | Contract | Customer agreements | ~5 | Core |
| 7 | ProductionOrder | Customer orders | ~8 | Order |
| 8 | WorkOrder | Internal work assignments | ~12 | Order |
| 9 | AttendanceRecord | Daily prisoner attendance | ~20 | Transactional |
| 10 | ProductivityRecord | Prisoner output metrics | ~20 | Transactional |

---

## Detailed Table Structures

### 1. EmploymentDepartment
Prison organizational departments.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| employment_department_id | INT | PK | Primary key |
| department_code | NVARCHAR(50) | UNIQUE, NOT NULL | Department code (e.g., DEPT-EMP) |
| department_name | NVARCHAR(50) | NOT NULL | Department name |
| location | NVARCHAR(50) | NULL | Physical location |
| max_capacity | INT | NULL | Maximum staff capacity |
| is_active | BIT | DEFAULT 1 | Active/Inactive flag |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation timestamp |
| modified_at | DATETIME2 | NULL | Last modification timestamp |

---

### 2. CustomerCompany
External and internal customers who place orders.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| customer_company_id | INT | PK | Primary key |
| company_id | NVARCHAR(50) | UNIQUE, NOT NULL | Business ID |
| company_name | NVARCHAR(50) | NOT NULL | Company name |
| contact_name | NVARCHAR(50) | NULL | Contact person |
| phone_number | NVARCHAR(20) | NULL | Phone number |
| delivery_address | NVARCHAR(MAX) | NULL | Delivery address |
| billing_address | NVARCHAR(MAX) | NULL | Billing address |
| email | NVARCHAR(255) | NULL | Email address |
| activity_status | NVARCHAR(50) | NULL | Active/Inactive |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

---

### 3. Product
Products manufactured by the prison workshops.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| product_id | INT | PK | Primary key |
| sku | NVARCHAR(50) | UNIQUE, NOT NULL | Stock keeping unit |
| product_name | NVARCHAR(50) | NOT NULL | Product name |
| description | NVARCHAR(MAX) | NULL | Product description |
| packaging_instructions | NVARCHAR(MAX) | NULL | Packaging guidelines |
| unit_of_measure | NVARCHAR(20) | NOT NULL, CHECK | kg, gr, or units |
| activity_status | NVARCHAR(50) | NULL | Active/Inactive |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**unit_of_measure values:** `kg` | `gr` | `units`

---

### 4. DepartmentManagement
System users with role-based access control.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| department_management_id | INT | PK | Primary key |
| username | NVARCHAR(50) | UNIQUE, NOT NULL | Login username |
| password | NVARCHAR(50) | NOT NULL | Password (plain text for course) |
| role | NVARCHAR(50) | NOT NULL, CHECK | User role |
| factory | NVARCHAR(30) | NULL, CHECK | Assigned factory (if factory manager) |
| employment_department_id | INT | FK | Links to EmploymentDepartment |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**role values:** `departmentManager` | `deputyOfDepartmentManager` | `factoryManager`

**factory values:** `MaimonSpices` | `Technosak` | `SewingWorkshop` | `TzitzitWorkshop` | NULL

---

### 5. Prisoner
Prison inmates working in employment program.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| prisoner_id | INT | PK | Primary key |
| prisoner_number | NVARCHAR(50) | UNIQUE, NOT NULL | Prison ID number |
| prisoner_name | NVARCHAR(50) | NULL | Prisoner name |
| full_name | NVARCHAR(50) | NOT NULL | Full name |
| factory | NVARCHAR(30) | NULL, CHECK | Assigned factory |
| department | NVARCHAR(50) | NULL | Department/section |
| activity_status | NVARCHAR(50) | NOT NULL, CHECK | Employment status |
| role | NVARCHAR(50) | NULL | Job role |
| safety_training_validity | DATE | NULL | Safety cert expiration |
| work_start_date | DATE | NULL | Employment start date |
| release_date | DATE | NULL | Prison release date |
| qualified | BIT | DEFAULT 0 | Qualified for work |
| shabbat_keeping | BIT | DEFAULT 0 | Observes Shabbat |
| min_salary | DECIMAL(10,2) | NULL | Minimum wage |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**activity_status values:** 
- `pendingPrisonAdministrationApproval`
- `pendingDepartmentManagerApproval`
- `pendingPlacement`
- `idle`
- `onShiftWorking`
- `waitingForMaterials`
- `inProfessionalTraining`
- `inSafetyTraining`
- `temporarilyUnavailable`
- `archived`

---

### 6. Contract
Business contracts with customers.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| contract_id | INT | PK | Primary key |
| contract_number | NVARCHAR(50) | UNIQUE, NOT NULL | Contract ID |
| customer_company_id | INT | FK, NOT NULL | Links to CustomerCompany |
| product_id | INT | FK, NULL | Links to Product |
| start_date | DATE | NOT NULL | Contract start date |
| price_per_unit | DECIMAL(10,2) | NULL | Unit price |
| payment_terms | NVARCHAR(MAX) | NULL | Payment terms |
| contract_status | NVARCHAR(50) | NULL, CHECK | Contract status |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**contract_status values:** `Active` | `Inactive` | `Expired`

---

### 7. ProductionOrder
Customer orders to be manufactured.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| production_order_id | INT | PK | Primary key |
| order_number | NVARCHAR(50) | UNIQUE, NOT NULL | Order number |
| customer_company_id | INT | FK, NOT NULL | Links to CustomerCompany |
| product_id | INT | FK, NOT NULL | Links to Product |
| contract_id | INT | FK, NULL | Links to Contract |
| quantity | INT | NOT NULL | Units to produce |
| submission_date | DATE | NOT NULL | Order submission date |
| delivery_deadline | DATE | NOT NULL | Delivery deadline |
| order_status | NVARCHAR(50) | NOT NULL, CHECK | Order status |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**order_status values:** `recieved` | `inProduction` | `readyForPickup` | `delivered` | `cancelled` | `onHold`

---

### 8. WorkOrder
Internal work assignments derived from ProductionOrders.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| work_order_id | INT | PK | Primary key |
| work_order_number | NVARCHAR(50) | UNIQUE, NOT NULL | Work order ID |
| production_order_id | INT | FK, NOT NULL | Links to ProductionOrder |
| required_quantity | INT | NOT NULL | Total units required |
| completed_quantity | INT | DEFAULT 0 | Units completed so far |
| start_date | DATE | NOT NULL | Work start date |
| deadline | DATE | NOT NULL | Work deadline |
| factory | NVARCHAR(30) | NOT NULL, CHECK | Assigned factory |
| status | NVARCHAR(50) | NOT NULL, CHECK | Work status |
| hold_reason | NVARCHAR(MAX) | NULL | Reason if on hold |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |
| modified_at | DATETIME2 | NULL | Last modification |

**factory values:** `MaimonSpices` | `Technosak` | `SewingWorkshop` | `TzitzitWorkshop`

**status values:** `inProcess` | `completed` | `hasntEnteredIntoProductionYet`

---

### 9. AttendanceRecord
Daily prisoner attendance tracking.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| attendance_record_id | INT | PK | Primary key |
| prisoner_id | INT | FK, NOT NULL | Links to Prisoner |
| attendance_date | DATE | NOT NULL | Date of attendance |
| factory | NVARCHAR(30) | NOT NULL, CHECK | Factory location |
| entry_time | TIME | NULL | Clock-in time |
| exit_time | TIME | NULL | Clock-out time |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |

**Unique constraint:** (prisoner_id, attendance_date, factory)

---

### 10. ProductivityRecord
Daily prisoner output and productivity metrics.

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| productivity_record_id | INT | PK | Primary key |
| prisoner_id | INT | FK, NOT NULL | Links to Prisoner |
| work_order_id | INT | FK, NOT NULL | Links to WorkOrder |
| productivity_date | DATE | NOT NULL | Date of work |
| quantity_produced | INT | NOT NULL | Units produced |
| work_hours | DECIMAL(5,2) | NULL | Hours worked |
| productivity_type | NVARCHAR(50) | NOT NULL, CHECK | Individual or Group |
| created_at | DATETIME2 | DEFAULT GETUTCDATE() | Record creation |

**productivity_type values:** `Individual` | `Group`

**Unique constraint:** (prisoner_id, work_order_id, productivity_date)

---

## Relationships & Foreign Keys

```
EmploymentDepartment (1) ──→ (N) DepartmentManagement
                                  ↓
CustomerCompany (1) ──→ (N) Contract
                        ↓
                  (1) ──→ (N) ProductionOrder
                               ↓
Product (1) ──────────→ (N) ProductionOrder
                        ↓
                  (1) ──→ (N) WorkOrder
                               ↓
Prisoner (1) ──→ (N) AttendanceRecord
         ↓
       (1) ──→ (N) ProductivityRecord (N) ←── WorkOrder (1)
```

### Foreign Key Details

| FK Name | From Table | Column | References | Constraint |
|---------|-----------|--------|-----------|-----------|
| FK_DepartmentManagement_EmploymentDepartment | DepartmentManagement | employment_department_id | EmploymentDepartment.employment_department_id | NO ACTION |
| FK_Contract_CustomerCompany | Contract | customer_company_id | CustomerCompany.customer_company_id | NO ACTION |
| FK_Contract_Product | Contract | product_id | Product.product_id | NO ACTION |
| FK_ProductionOrder_CustomerCompany | ProductionOrder | customer_company_id | CustomerCompany.customer_company_id | NO ACTION |
| FK_ProductionOrder_Product | ProductionOrder | product_id | Product.product_id | NO ACTION |
| FK_ProductionOrder_Contract | ProductionOrder | contract_id | Contract.contract_id | NO ACTION |
| FK_WorkOrder_ProductionOrder | WorkOrder | production_order_id | ProductionOrder.production_order_id | NO ACTION |
| FK_AttendanceRecord_Prisoner | AttendanceRecord | prisoner_id | Prisoner.prisoner_id | NO ACTION |
| FK_ProductivityRecord_Prisoner | ProductivityRecord | prisoner_id | Prisoner.prisoner_id | NO ACTION |
| FK_ProductivityRecord_WorkOrder | ProductivityRecord | work_order_id | WorkOrder.work_order_id | NO ACTION |

---

## Enum Values

### Factory (4 values)
- `MaimonSpices` - Spice manufacturing workshop
- `Technosak` - Plastic manufacturing workshop
- `SewingWorkshop` - Textile/sewing workshop
- `TzitzitWorkshop` - Tzitzit (religious thread) workshop

### Order Status (6 values)
- `recieved` - Order received from customer
- `inProduction` - Currently being manufactured
- `readyForPickup` - Completed and ready
- `delivered` - Shipped to customer
- `cancelled` - Order cancelled
- `onHold` - Temporarily halted

### Production Status (3 values)
- `inProcess` - Work in progress
- `completed` - Work finished
- `hasntEnteredIntoProductionYet` - Not yet started

### Prisoner Activity Status (10 values)
- `pendingPrisonAdministrationApproval` - Awaiting prison approval
- `pendingDepartmentManagerApproval` - Awaiting department approval
- `pendingPlacement` - Awaiting job placement
- `idle` - Assigned but not working
- `onShiftWorking` - Currently working
- `waitingForMaterials` - Work halted, waiting for materials
- `inProfessionalTraining` - Professional development course
- `inSafetyTraining` - Safety certification training
- `temporarilyUnavailable` - Medical/court leave
- `archived` - Released/discharged

### Unit of Measure (3 values)
- `kg` - Kilograms
- `gr` - Grams
- `units` - Individual units

### DepartmentManagement Roles (3 values)
- `departmentManager` - Full system access
- `deputyOfDepartmentManager` - Full system access
- `factoryManager` - Access only to assigned factory records

---

## Load Order (for initialization)

1. EmploymentDepartment
2. CustomerCompany
3. Product
4. DepartmentManagement
5. Prisoner
6. Contract
7. ProductionOrder
8. WorkOrder
9. AttendanceRecord
10. ProductivityRecord

---

## Notes

- All timestamps use UTC timezone (GETUTCDATE())
- Primary keys are INT, assigned by application layer (no IDENTITY)
- Foreign keys use ON DELETE NO ACTION, ON UPDATE NO ACTION
- Enum values stored as NVARCHAR with CHECK constraints
- No soft deletes; records deleted permanently
- No audit trail fields (created_by, modified_by)
