# State Machine Inventory for PEDIS

## Overview
This document catalogs all entities with non-trivial state machines, including states, transitions, triggers, guards, and side effects.

---

## 1. Prisoner (PrisonerActivityStatus)

### Entity
`Prisoner` — Inmate in the employment program with activity status

### States (10 total)

| State | Description |
|-------|-------------|
| **PendingPrisonAdministrationApproval** | Initial state when prisoner enrolled; awaiting prison admin review |
| **PendingDepartmentManagerApproval** | Prison admin approved; awaiting employment dept manager review |
| **PendingPlacement** | Dept manager approved; awaiting factory assignment |
| **Idle** | Assigned to factory but not currently working; available for tasks |
| **OnShiftWorking** | Actively working on assigned task |
| **WaitingForMaterials** | Assigned to task but materials unavailable; paused state |
| **InProfessionalTraining** | Enrolled in job skills training; unavailable for regular work |
| **InSafetyTraining** | Enrolled in safety/compliance training; unavailable for work |
| **TemporarilyUnavailable** | Medical leave, court appearance, or security hold; unavailable |
| **Archived** | Program ended (released, transferred, or completed term) |

### Transitions

| Source | Target | Trigger | Guard | Side Effects |
|--------|--------|---------|-------|--------------|
| PendingPrisonAdministrationApproval | PendingDepartmentManagerApproval | Prison admin approval | Valid prisoner record | Update approval timestamp |
| PendingPrisonAdministrationApproval | Archived | Rejection or policy change | - | Archive prisoner record |
| PendingDepartmentManagerApproval | PendingPlacement | Dept manager approval | Valid prisoner record, no security holds | Notify factory managers |
| PendingDepartmentManagerApproval | Archived | Rejection | - | Archive prisoner record |
| PendingPlacement | Idle | Factory assignment (R4) | Factory has capacity, inmate available | Update Prisoner.factory FK, notify inmate |
| Idle | OnShiftWorking | Start shift (clock in to task) | Task exists, inmate assigned, safety certs current (R9) | Create DailyAttendance entry_time |
| OnShiftWorking | WaitingForMaterials | Materials unavailable alert (R7) | Task pending on inventory | Create Alert, notify factory manager |
| OnShiftWorking | Idle | End shift (clock out) | - | Update DailyAttendance exit_time, calc hours worked (R5) |
| WaitingForMaterials | OnShiftWorking | Materials restocked (R7) | Inventory item available | Clear Alert, resume task |
| WaitingForMaterials | Idle | Shift ends / task cancelled | - | Update DailyAttendance |
| Idle | InProfessionalTraining | Enroll in training | Available slot | Create training record, pause assignments |
| InProfessionalTraining | Idle | Training complete | - | Update certification, resume availability |
| Idle | InSafetyTraining | Enroll in safety cert training | Safety cert required | Pause assignments, create training record |
| InSafetyTraining | Idle | Safety cert acquired | - | Add to Prisoner.certifications (R9), resume availability |
| Any | TemporarilyUnavailable | צוהר security hold OR medical leave OR court summons (R12) | External trigger from צוהר or manual entry | Create Alert, pause all tasks |
| TemporarilyUnavailable | Idle | Hold released / medical cleared | צוהר sync OR manual clearance | Resume previous assignments, clear Alert |
| TemporarilyUnavailable | Archived | Permanent unavailability | - | Archive prisoner |
| Any | Archived | Release date reached OR program termination | - | Archive record, cease all PayrollRecords (R10) |

### Non-Trivial Aspects
- **10-state lifecycle** with multiple entry/exit paths
- **External system dependency** on צוהר (R12) for security holds (sync daily 06:00 AM per sequence diagram 2)
- **Concurrent parallel states** (e.g., can be in "OnShiftWorking" and simultaneously under "TemporarilyUnavailable" if security hold issued mid-shift)
- **Cascading side effects** on child entities:
  - OnShiftWorking → creates/updates AttendanceRecord (R5)
  - OnShiftWorking → can create ProductivityRecord (R6)
  - TemporarilyUnavailable → creates Alert (R16)
  - Archived → ceases PayrollRecord generation (R10)

---

## 2. ProductionOrder (ProductionOrderStatus)

### Entity
`ProductionOrder` — Customer order for manufactured products

### States (6 total)

| State | Description |
|-------|-------------|
| **Received** | Order submitted; awaiting review and factory assignment |
| **InProduction** | Factory assigned, work orders created (R2), inmates assigned (R4) |
| **ReadyForPickup** | All units produced and QA passed; awaiting customer collection |
| **Delivered** | Order delivered to customer; order complete |
| **Cancelled** | Order cancelled by customer or system (insufficient capacity, deadline unreachable) |
| **OnHold** | Paused (e.g., awaiting materials R7, security issue, quality rework needed) |

### Transitions

| Source | Target | Trigger | Guard | Side Effects |
|--------|--------|---------|-------|--------------|
| Received | InProduction | Accept order & assign factory (R2, R3) | ≥1 factory has capacity; deadline achievable | Create WorkOrder(s), assign inmates (R4), notify Factory Manager |
| Received | Cancelled | Reject order OR insufficient capacity | - | Send rejection notice to customer (R12), archive ProductionOrder |
| InProduction | OnHold | Materials unavailable (R7) OR quality issue OR security incident | External trigger | Create Alert (R16), notify Factory Manager |
| InProduction | ReadyForPickup | All units completed (R6) & quality pass (R6) | completed_quantity ≥ quantity AND avg quality ≥ threshold | Notify customer (R12) "Order ready" |
| InProduction | Cancelled | Customer cancellation request | - | Cease all WorkOrders, notify inmates, refund/credit |
| OnHold | InProduction | Issue resolved (materials restocked OR quality rework complete) | - | Resume WorkOrders, notify Factory Manager |
| OnHold | Cancelled | Hold cannot be resolved | - | Notify customer, archive |
| ReadyForPickup | Delivered | Customer pickup confirmed (R8) | - | Generate final report, calculate PayrollRecords (R10) for all inmates worked on order |
| ReadyForPickup | Cancelled | Customer cancels pickup (rare) | - | Archive |
| Cancelled | (terminal) | - | - | Permanent; no further transitions |
| Delivered | (terminal) | - | - | Permanent; archive after retention period |

### Non-Trivial Aspects
- **6 states** with **OnHold as a pause state** (recoverable vs. terminal cancellation)
- **Cascading creation** on transition Received→InProduction: creates multiple WorkOrder child records (R2, R3)
- **Quality validation dependency** (R6): transition ReadyForPickup requires production quality metrics
- **Payroll cascade** on delivery (R10): finalizes compensation for all inmates who contributed
- **External notification** to customers on key transitions (R12)

---

## 3. WorkOrder (WorkOrderStatus)

### Entity
`WorkOrder` — Specific task/job assigned to factory for a ProductionOrder

### States (3 total)

| State | Description |
|-------|-------------|
| **HasntEnteredIntoProductionYet** | Created but no inmates assigned or work started |
| **InProcess** | Inmates assigned (R4), work underway, AttendanceRecords/ProductivityRecords being created (R5, R6) |
| **Completed** | All units produced, task marked complete |

### Transitions

| Source | Target | Trigger | Guard | Side Effects |
|--------|--------|---------|-------|--------------|
| HasntEnteredIntoProductionYet | InProcess | Assign inmates to task (R4) | Inmates available, security clearance ≥ requirement (R4) | Create first AttendanceRecord entry, notify assigned inmates |
| InProcess | Completed | Production complete (completed_quantity ≥ required_quantity) | QA sign-off on last unit | Update ProductionOrder.completed_quantity, check if ProductionOrder ready for pickup |
| InProcess | HasntEnteredIntoProductionYet | Inmate reassignment / task reset | - | Clear current assignments, reset completed_quantity to 0 |
| HasntEnteredIntoProductionYet | (abandoned) | ProductionOrder cancelled | - | Mark WorkOrder cancelled; don't transition further |

### Non-Trivial Aspects
- **Simple 3-state machine** (linear progression with possible reset)
- **Parent-dependent transitions**: WorkOrder tied to ProductionOrder; if ProductionOrder cancelled, WorkOrder status is overridden
- **Child record generation** on InProcess state: creates AttendanceRecord entries (R5) and ProductivityRecord entries (R6) as inmates work

---

## 4. Contract (ContractStatus)

### Entity
`Contract` — Legal agreement between prison and customer for product manufacturing

### States (3 total)

| State | Description |
|-------|-------------|
| **Active** | Contract in effect; orders under this contract can be accepted (R3) |
| **Inactive** | Contract paused; no new orders accepted, but existing orders continue (R3) |
| **Expired** | Contract end date reached; archived, no new orders |

### Transitions

| Source | Target | Trigger | Guard | Side Effects |
|--------|--------|---------|-------|--------------|
| Active | Inactive | Manual suspension by Dept Manager | - | Prevent new ProductionOrders under this contract (R3) |
| Inactive | Active | Manual reactivation by Dept Manager | - | Re-enable ProductionOrder acceptance (R3) |
| Active/Inactive | Expired | End date reached (daily check) | TODAY ≥ contract.end_date | Archive contract, block new orders, notify customer (R12) |
| Expired | (terminal) | - | - | No transitions; archived state |

### Non-Trivial Aspects
- **Simple 3-state machine** with minimal complexity
- **Date-driven transition** (Expired) requires daily/periodic check
- **Order-gating effect**: Contract status directly controls ProductionOrder creation (R3)
- **Minimal side effects**: No cascading entity changes except notification

---

## 5. AttendanceRecord (Implicit State Machine)

### Entity
`AttendanceRecord` — Daily work attendance log for inmate

### Implicit States (2-3 logical states, not explicit enum)

| Implicit State | Condition |
|---|---|
| **Pending** | Record created; entry_time filled, exit_time NULL |
| **In Progress** | entry_time filled, possible temporary exits recorded |
| **Complete** | exit_time filled; hours_worked calculated |

### Key Transitions (implicit)

| Source | Target | Trigger | Side Effects |
|--------|--------|---------|--------------|
| (Create) | Pending | Prisoner clocks in (R5) | Record entry_time = NOW, update Prisoner status to OnShiftWorking |
| Pending | Pending | Prisoner exits temporarily (lunch) | Record temp exit, resume entry |
| Pending | Complete | Prisoner final clock out (R5) | Record exit_time, calc total hours, update Prisoner status to Idle |
| Complete | (archived) | Day ends or record finalized | Contribute to PayrollRecord generation (R10) |

### Non-Trivial Aspects
- **No explicit state enum** — state inferred from entry_time/exit_time values
- **Tightly coupled to Prisoner.activity_status** (R5): controls on/off shift status
- **Feeds into Payroll** (R10): hours_worked used for wage calculation
- **Implicit time-based state transitions**: can auto-complete at end of shift or manual override

---

## 6. ProductivityRecord (Implicit State Machine)

### Entity
`ProductivityRecord` — Hourly/periodic units produced by inmate on WorkOrder

### Implicit States (2 logical states, not explicit enum)

| Implicit State | Condition |
|---|---|
| **Pending** | Record created; units_produced filled, but not yet approved/verified |
| **Verified** | QA or supervisor reviewed; units counted toward WorkOrder progress (R6) |

### Key Transitions (implicit)

| Source | Target | Trigger | Side Effects |
|--------|--------|---------|--------------|
| (Create) | Pending | Worker inputs units produced during shift (R6) | Record quantity_produced, calc productivity rate, create ProductivityRecord |
| Pending | Verified | Supervisor QA review (manual or auto-check quality) | Update WorkOrder.completed_quantity, check if WorkOrder complete (R6) |
| Verified | (archived) | Shift ends or WorkOrder complete | Contribute to PerformanceEvaluation (R11) and PayrollRecord (R10) |

### Non-Trivial Aspects
- **No explicit state enum** — state implicit in verification status
- **Quality-dependent transitions**: productivity count only applied after QA approval
- **Cascades to two downstream entities** (R6, R11):
  - Verified records update WorkOrder progress
  - Feed into PerformanceEvaluation for parole board (R11)
- **Payroll integration** (R10): high productivity can trigger bonuses

---

## Summary Table: Complexity by Entity

| Entity | Complexity | Num States | External Dependencies | Cascading Effects |
|--------|-----------|-----------|----------------------|-------------------|
| **Prisoner** | 🔴 High | 10 | צוהר (daily sync R12) | Alert, AttendanceRecord, PayrollRecord |
| **ProductionOrder** | 🟡 Medium | 6 | Customer notifications (R12) | WorkOrder, PayrollRecord (on delivery) |
| **WorkOrder** | 🟢 Low | 3 | Parent ProductionOrder | AttendanceRecord, ProductivityRecord |
| **Contract** | 🟢 Low | 3 | None | ProductionOrder gating (R3) |
| **AttendanceRecord** | 🟡 Medium* | 2-3 (implicit) | Prisoner.activity_status (R5) | PayrollRecord (R10) |
| **ProductivityRecord** | 🟡 Medium* | 2 (implicit) | WorkOrder, Prisoner (R6) | PerformanceEvaluation, PayrollRecord (R11, R10) |

*Implicit states inferred from field values, not explicit enum.

---

## State Machine Diagram Notes

For each non-trivial entity above, state diagrams can be drawn showing:
1. States as circles/rectangles
2. Transitions as labeled arrows with guards [condition] / side_effects
3. Initial state (→) and terminal states (⊗)
4. Parallel states (e.g., Prisoner can be OnShiftWorking AND TemporarilyUnavailable)

---

## Next Steps
- **Do not implement yet** — this inventory serves as the design baseline
- Confirm state machines align with requirements (R1–R20 in `docs/00-requirements.md`)
- Validate state transitions against sequence diagrams (`docs/design/06-design-sequence.md`)
- Identify missing transitions or guards (e.g., error states, edge cases)
