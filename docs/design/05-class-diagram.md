# System Design - Class Diagram

## Overview
This document presents the class diagram for the Prison Employment Department Information System (PEDIS). The diagram illustrates the core domain entities, their relationships, and key attributes.

---

## Class Diagram

![Class Diagram][class-diagram-1.png]

---

## Domain Entities Description

### Core Entity Classes

#### Inmate
Represents an inmate in the prison system with employment status.

**Attributes:**
- inmateId: String (primary key)
- firstName: String
- lastName: String
- hebrewName: String
- dateOfBirth: Date
- releaseDate: Date
- securityClearanceLevel: Enum (Low, Medium, High)
- employmentStatus: Enum (Active, Inactive, OnHold)
- assignedFactory: Factory (foreign key)
- skillCertifications: List<SafetyCertification>

**Methods:**
- getAvailabilityStatus(): AvailabilityStatus
- addCertification(cert: SafetyCertification)
- removeCertification(certId: String)
- isQualifiedFor(task: Task): Boolean
- getProductivityMetrics(startDate: Date, endDate: Date): ProductivityMetrics

---

#### Factory
Represents a manufacturing facility within the prison.

**Attributes:**
- factoryId: String (primary key)
- factoryName: String
- location: String
- capacity: Integer (maximum inmates)
- currentWorkload: Integer (inmates currently assigned)
- materialInventory: List<InventoryItem>
- activeOrders: List<Order>

**Methods:**
- getUtilizationRate(): Double
- canAccommodateOrder(order: Order): Boolean
- getAvailableInmates(): List<Inmate>
- getInventoryStatus(): Map<String, InventoryStatus>

---

#### Order
Represents a production order from a customer.

**Attributes:**
- orderId: String (primary key)
- orderNumber: String (unique, auto-generated)
- customerId: String (foreign key)
- customerName: String
- productDescription: String
- quantity: Integer
- submissionDate: Date
- deliveryDeadline: Date
- currentStatus: Enum (Submitted, Approved, InProgress, Completed, Cancelled)
- assignedFactory: Factory (foreign key)
- technicalSpecifications: String (may include file attachments)

**Methods:**
- isOnTrack(): Boolean
- getRemainingTime(): Integer (days)
- getEstimatedCompletionDate(): Date
- markAsInProgress()
- markAsCompleted(completionDate: Date)
- updateDeadline(newDeadline: Date)

---

#### Task
Represents a specific job assignment within an order.

**Attributes:**
- taskId: String (primary key)
- orderId: String (foreign key to Order)
- taskDescription: String
- requiredQuantity: Integer
- completedQuantity: Integer
- assignedInmates: List<Inmate>
- startDate: Date
- deadline: Date
- assignedFactory: Factory (foreign key)
- status: Enum (Assigned, InProgress, Completed, Blocked)

**Methods:**
- assignInmate(inmate: Inmate)
- removeInmate(inmate: Inmate)
- recordOutput(inmateId: String, quantity: Integer, defectCount: Integer)
- getProgressPercentage(): Double
- getProductivityRate(): Double (units per hour)

---

#### DailyAttendance
Records daily attendance for an inmate.

**Attributes:**
- attendanceId: String (primary key)
- inmateId: String (foreign key)
- date: Date
- entryTime: Time
- exitTime: Time
- totalHoursWorked: Double
- assignedTaskId: String (foreign key to Task)
- attendanceStatus: Enum (Present, Absent, Excused, Medical)
- recordedBy: String (user who recorded attendance)

**Methods:**
- calculateTotalHours(): Double
- getProductivityForDay(): Double
- validateTimes(): Boolean

---

#### ProductionOutput
Records production metrics for a specific task/shift.

**Attributes:**
- outputId: String (primary key)
- taskId: String (foreign key)
- inmateId: String (foreign key)
- date: Date
- unitsProduced: Integer
- defectCount: Integer
- wasteQuantity: Double
- recordedAt: DateTime
- recordedBy: String (user)

**Methods:**
- getQualityRate(): Double (percentage of good units)
- getProductivityRate(): Double (units per hour)
- getWastePercentage(): Double

---

#### Inventory
Tracks raw materials and work-in-progress items per factory.

**Attributes:**
- inventoryId: String (primary key)
- factoryId: String (foreign key)
- materialName: String
- sku: String (stock keeping unit)
- currentQuantity: Integer
- minimumThreshold: Integer
- maximumCapacity: Integer
- lastReceivedDate: Date
- lastUsedDate: Date
- reorderPoint: Integer

**Methods:**
- addStock(quantity: Integer, supplierId: String, receiptDate: Date)
- consumeStock(quantity: Integer, taskId: String)
- getCurrentStatus(): InventoryStatus (OK, LowStock, CriticalLow, Surplus)
- isNeedingReorder(): Boolean
- getEstimatedDaysToDepletion(): Integer

---

#### SafetyCertification
Represents a safety training certification for an inmate.

**Attributes:**
- certificationId: String (primary key)
- inmateId: String (foreign key)
- trainingType: String (e.g., "Fire Safety", "Equipment Operation")
- certificationDate: Date
- expirationDate: Date
- trainerName: String
- certificateNumber: String
- status: Enum (Active, Expired, Expiring)

**Methods:**
- isValid(): Boolean
- daysToExpiration(): Integer
- hasExpired(): Boolean

---

#### PayrollRecord
Represents monthly wage calculation for an inmate.

**Attributes:**
- payrollId: String (primary key)
- inmateId: String (foreign key)
- payrollPeriod: String (e.g., "2026-06")
- basePay: Double (base hourly rate × hours worked)
- productivityBonus: Double (based on output metrics)
- deductions: Double (approved deductions)
- familySupport: Double (support contribution to family)
- totalWage: Double (basePay + bonus - deductions + familySupport)
- calculatedDate: Date
- status: Enum (Draft, Approved, Submitted, Paid)
- submittedToTamar: Boolean

**Methods:**
- calculateTotalWage(): Double
- validateWage(): ValidationResult
- submitToTamar(tamarInterface: TamarInterface): Boolean
- generateWageStatement(): String (formatted wage slip)

---

#### PerformanceEvaluation
Records periodic performance assessments for parole board consideration.

**Attributes:**
- evaluationId: String (primary key)
- inmateId: String (foreign key)
- evaluationDate: Date
- evaluationPeriodStart: Date
- evaluationPeriodEnd: Date
- managerId: String (factory manager who performed evaluation)
- quantitativeScore: Double (based on productivity, attendance)
- qualitativeNarrative: String (work quality, behavior, attitude)
- recommendedAction: Enum (Advance, Maintain, Demote, ReferToParole)
- submittedToParoleBoard: Boolean
- paroleReviewDate: Date (optional)

**Methods:**
- generateParoleBoardReport(): String (formatted report)
- calculateQuantitativeScore(): Double
- submitToParoleBoard()

---

#### User
System user account with role-based access.

**Attributes:**
- userId: String (primary key)
- username: String (unique)
- password: String (hashed)
- role: Enum (EmploymentOfficer, FactoryManager, DataEntry, Management, Accountant)
- factoryId: String (optional, for factory managers)
- lastLoginDate: Date
- accountStatus: Enum (Active, Inactive, Locked)

**Methods:**
- authenticate(password: String): Boolean
- hasPermission(action: String): Boolean
- updateLastLogin()
- changePassword(newPassword: String)

---

#### Alert
System alerts for exceptions and escalations.

**Attributes:**
- alertId: String (primary key)
- alertType: Enum (Safety, Operational, Financial, Material, Quality)
- severity: Enum (Info, Warning, Critical)
- description: String
- relatedEntityId: String (Task, Inmate, Order, etc.)
- createdAt: DateTime
- acknowledgedAt: DateTime (optional)
- resolvedAt: DateTime (optional)
- assignedToUserId: String (optional)
- resolutionNotes: String (optional)

**Methods:**
- acknowledge(userId: String)
- resolve(resolutionNotes: String)
- escalateAfterTimeout(): Boolean

---

## Relationships

### Association Relationships

1. **Inmate -- Factory** (Many-to-One)
   - An inmate is assigned to one factory at a time
   - A factory can employ multiple inmates

2. **Order -- Factory** (Many-to-One)
   - An order is assigned to one factory
   - A factory processes multiple orders

3. **Order -- Task** (One-to-Many)
   - An order contains multiple tasks
   - A task belongs to one order

4. **Task -- Inmate** (Many-to-Many)
   - A task can be assigned to multiple inmates
   - An inmate can be assigned to multiple tasks (over time)

5. **Inmate -- SafetyCertification** (One-to-Many)
   - An inmate can hold multiple certifications
   - A certification belongs to one inmate

6. **Factory -- Inventory** (One-to-Many)
   - A factory manages multiple inventory items
   - An inventory item belongs to one factory

7. **Inmate -- DailyAttendance** (One-to-Many)
   - An inmate has multiple attendance records (one per day)
   - An attendance record belongs to one inmate

8. **Task -- ProductionOutput** (One-to-Many)
   - A task has multiple output records (multiple shifts/days)
   - An output record belongs to one task

9. **Inmate -- ProductionOutput** (One-to-Many)
   - An inmate produces multiple output records
   - An output record is for one inmate

10. **Inmate -- PayrollRecord** (One-to-Many)
    - An inmate has multiple payroll records (one per month)
    - A payroll record belongs to one inmate

11. **Inmate -- PerformanceEvaluation** (One-to-Many)
    - An inmate has multiple evaluations (quarterly or on-demand)
    - An evaluation belongs to one inmate

12. **Alert -- Entity** (Many-to-One)
    - Multiple alerts can relate to the same entity
    - An alert relates to one entity (Task, Inmate, Order, etc.)

---

## Key Design Patterns

### Temporal Patterns
- **Effective Dating**: SafetyCertification, PerformanceEvaluation, and PayrollRecord are time-bound entities
- **Audit Trail**: All modifications logged with timestamp and user ID (not shown in diagram for simplicity)

### Status Tracking
- **State Machines**: Order, Task, and Alert have defined status states with valid transitions
- **Enumeration**: AlertType, AlertSeverity, and AttendanceStatus use enumerations for type-safe operations

### Relationships
- **Aggregation**: Factory aggregates Inmate, Task, and Inventory entities
- **Composition**: Order contains Task entities that cannot exist independently

---

## Summary

The class diagram represents a comprehensive data model for managing prison employment operations. Key design considerations include:
- Clear separation between operational entities (Inmate, Factory, Order, Task) and administrative entities (PayrollRecord, PerformanceEvaluation)
- Time-bounded entities for compliance and audit purposes
- Flexible status tracking through enumeration types
- Support for role-based access through User entity
- Alert management for exception handling and escalation

This model supports all functional requirements defined in the requirements specification document.
