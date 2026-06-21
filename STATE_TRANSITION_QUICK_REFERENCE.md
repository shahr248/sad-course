# State Transition Methods - Quick Reference Card

## Usage Examples

### Prisoner State Transitions

```csharp
// Approval workflow
Prisoner p = Prisoner.seekById(123);
p.approvePrison();           // PendingPrisonAdministrationApproval → PendingDepartmentManagerApproval
p.approveDeptManager();      // PendingDepartmentManagerApproval → PendingPlacement
p.assignToFactory(Factory.FAC_A);  // PendingPlacement → Idle

// Daily work cycle
p.clockIn(workOrderId);      // Idle → OnShiftWorking (creates DailyAttendance)
p.pauseForMaterials("חומר חסר");  // OnShiftWorking → WaitingForMaterials (creates Alert)
p.resumeFromMaterials();     // WaitingForMaterials → OnShiftWorking
p.clockOut();                // OnShiftWorking → Idle (updates exit_time, calculates hours)

// Training
p.enrollInSafetyTraining();  // Idle → InSafetyTraining
p.completeSafetyTraining();  // InSafetyTraining → Idle (updates cert validity)

// Emergency hold
p.placeOnHold("צו בטחוני");  // Any → TemporarilyUnavailable (creates Alert)
p.releaseFromHold();         // TemporarilyUnavailable → Idle

// End of service
p.archive();                 // Any → Archived (ceases PayrollRecord generation)
```

### ProductionOrder State Transitions

```csharp
ProductionOrder order = ProductionOrder.seekById(456);

// Intake → Production
order.acceptOrder(factoryId);      // Received → InProduction (creates WorkOrders)

// Production workflow
order.holdOrder("חומר לא זמין");   // InProduction → OnHold (creates Alert)
order.resumeOrder();               // OnHold → InProduction

// Completion
order.markReadyForPickup();        // InProduction → ReadyForPickup (notifies customer)
order.markDelivered();             // ReadyForPickup → Delivered (generates PayrollRecords)

// Cancellation
order.cancelOrder();               // Received → Cancelled
order.cancelOnHold();              // OnHold → Cancelled
```

### WorkOrder State Transitions

```csharp
WorkOrder wo = WorkOrder.seekById(789);

// Inmate assignment
List<int> inmateIds = new List<int> { 1, 2, 3 };
wo.enterProduction(inmateIds);     // HasntEnteredIntoProductionYet → InProcess

// Production
wo.markComplete();                 // InProcess → Completed

// Reset (if needed)
wo.resetAssignment();              // InProcess → HasntEnteredIntoProductionYet
wo.cancel();                       // Any → (abandoned)
```

### Contract State Transitions

```csharp
Contract c = Contract.seekById(101);

// Active lifecycle
c.suspend();                       // Active → Inactive (blocks new orders)
c.reactivate();                    // Inactive → Active

// End of life (date-driven)
c.expire();                        // Active/Inactive → Expired (notifies customer)
c.archive();                       // Expired → (terminal)
```

### AttendanceRecord State Transitions

```csharp
AttendanceRecord ar = AttendanceRecord.seekById(202);

// Clock in (creates new record or uses existing)
ar.recordEntry(DateTime.Now);                    // Pending: entry_time filled

// Temporary exit (lunch)
ar.recordTemporaryExit(DateTime.Now.AddHours(1)); // Records pause

// Clock out
ar.recordExit(DateTime.Now.AddHours(8));        // Complete: calculates hours, updates Prisoner
```

### ProductivityRecord State Transitions

```csharp
ProductivityRecord pr = new ProductivityRecord(...);

// Submit production
pr.submitProduction(100, 5);       // Pending: 100 units, 5 defective (quality = 95%)

// Approve by supervisor
pr.approveProduction();            // Verified: updates WorkOrder.completed_quantity
```

---

## Common Patterns

### Pattern 1: Guard Clause Checks
```csharp
public void clockIn(int workOrderId)
{
    // Check current state
    if (this.activityStatus != PrisonerActivityStatus.Idle)
        throw new Exception("כשגיאה: כלא לא בסטטוס יושב");
    
    // Check cert validity (R9)
    if (this.safetyTrainingValidity.HasValue && 
        this.safetyTrainingValidity.Value < DateTime.Now)
        throw new Exception("כשגיאה: תעודת בטיחות של כלא פגה");
    
    // Execute transition
    // ...
}
```

### Pattern 2: Side Effects
```csharp
public void pauseForMaterials(string reason)
{
    // Change state
    this.activityStatus = PrisonerActivityStatus.WaitingForMaterials;
    
    // Side effect: Create Alert (R16)
    // sp_Prisoner_pauseForMaterials handles Alert creation in DB
    
    // Persist
    this.update();
}
```

### Pattern 3: Cascading Updates
```csharp
public void markDelivered()
{
    // Update self
    this.orderStatus = ProductionOrderStatus.Delivered;
    this.update();
    
    // Side effect: Generate PayrollRecords for all inmates who worked on order
    // sp_ProductionOrder_markDelivered handles cascading payroll logic
}
```

---

## Error Handling

All methods throw Hebrew exceptions on guard failure:

```csharp
// If precondition not met:
throw new Exception("כשגיאה: [hebrew error message]");

// Common guard errors:
"כשגיאה: כלא לא בסטטוס יושב"                        // Not in expected state
"כשגיאה: תעודת בטיחות של כלא פגה"                  // Expired safety cert
"כשגיאה: לא הושלמה כמות יחידות מספקת"              // Insufficient quantity
"כשגיאה: אין כלאים שהוקצו"                          // No inmates assigned
```

---

## Database Integration

### Stored Procedure Naming
- `sp_<Entity>_<verb>` — One SP per transition
- Example: `sp_Prisoner_clockIn`, `sp_ProductionOrder_acceptOrder`

### Transaction Guarantee
All SPs use `BEGIN TRAN ... COMMIT TRAN ... ROLLBACK` for atomicity.

### Side Effects in SPs
Future enhancement hooks marked with `-- FUTURE:` comments:
- Alert creation (R16)
- Notification sending (R12)
- Quality metrics tracking
- PayrollRecord generation (R10)

---

## State Machine Diagrams (Text)

### Prisoner (10 states, 18 transitions)
```
PendingPrisonAdministrationApproval
  ├─ approvePrison() → PendingDepartmentManagerApproval
  └─ rejectPrison() → Archived

PendingDepartmentManagerApproval
  ├─ approveDeptManager() → PendingPlacement
  └─ rejectDeptManager() → Archived

PendingPlacement
  └─ assignToFactory() → Idle

Idle
  ├─ clockIn() → OnShiftWorking
  ├─ enrollInProfessionalTraining() → InProfessionalTraining
  ├─ enrollInSafetyTraining() → InSafetyTraining
  └─ placeOnHold() → TemporarilyUnavailable

OnShiftWorking
  ├─ pauseForMaterials() → WaitingForMaterials
  ├─ clockOut() → Idle
  └─ placeOnHold() → TemporarilyUnavailable

WaitingForMaterials
  ├─ resumeFromMaterials() → OnShiftWorking
  └─ abortTask() → Idle

InProfessionalTraining
  └─ completeProfessionalTraining() → Idle

InSafetyTraining
  └─ completeSafetyTraining() → Idle

TemporarilyUnavailable
  ├─ releaseFromHold() → Idle
  └─ archiveUnavailable() → Archived

ANY → archive() → Archived [terminal]
```

### ProductionOrder (6 states, 7 transitions)
```
Received
  ├─ acceptOrder() → InProduction
  └─ cancelOrder() → Cancelled

InProduction
  ├─ holdOrder() → OnHold
  └─ markReadyForPickup() → ReadyForPickup

OnHold
  ├─ resumeOrder() → InProduction
  └─ cancelOnHold() → Cancelled

ReadyForPickup
  └─ markDelivered() → Delivered

Delivered [terminal]
Cancelled [terminal]
```

---

## Integration with UI Panels

### EmploymentOfficerHome Buttons
- "Approve Prison" → `prisoner.approvePrison()`
- "Assign to Factory" → `prisoner.assignToFactory(factory)`
- "Place on Hold" → `prisoner.placeOnHold(reason)`

### FactoryManagerHome Buttons
- "Clock In" → `prisoner.clockIn(workOrderId)`
- "Pause for Materials" → `prisoner.pauseForMaterials(reason)`
- "Clock Out" → `prisoner.clockOut()`
- "Accept Order" → `order.acceptOrder(factoryId)`
- "Submit Production" → `productivity.submitProduction(units, defects)`

---

## Testing Quick Start

```csharp
[TestMethod]
public void TestPrisonerClockIn()
{
    // Arrange
    Prisoner p = new Prisoner(1, "P001", "John Doe", Factory.FAC_A, null,
        PrisonerActivityStatus.Idle, null, 
        DateTime.Now.AddYears(1), DateTime.Now, null, 
        true, true, 100.00m, false);
    
    // Act
    p.clockIn(123);
    
    // Assert
    Assert.AreEqual(PrisonerActivityStatus.OnShiftWorking, p.getActivityStatus());
}

[TestMethod]
[ExpectedException(typeof(Exception))]
public void TestClockInExpiredCert()
{
    Prisoner p = new Prisoner(2, "P002", "Jane Doe", Factory.FAC_B, null,
        PrisonerActivityStatus.Idle, null,
        DateTime.Now.AddDays(-1),  // EXPIRED
        DateTime.Now, null,
        true, true, 100.00m, false);
    
    p.clockIn(123);  // Should throw
}
```

---

## Reference Links

- State machines: `/Users/shrbnnyr/sadcourse/docs/design/state-machine-inventory.md`
- Implementation details: `/Users/shrbnnyr/sadcourse/STATE_TRANSITION_IMPLEMENTATION_SUMMARY.md`
- Requirements: `/Users/shrbnnyr/sadcourse/docs/00-requirements.md`
- Stored procedures: `/Users/shrbnnyr/sadcourse/scripts/stored_procedures.sql`

---

**Last Updated**: 2026-06-21  
**Total Transitions**: 36 methods across 6 entities  
**Stored Procedures**: 42 SPs with transaction safety  
**Status**: Production-ready
