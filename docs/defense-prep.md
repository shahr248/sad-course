# PEDIS — Oral Defense Preparation Document

> Quick-navigation cheat sheet for the defense. Each section is self-contained — jump straight to the one you're asked about.

---

## 1. Architecture Overview (Simple Terms)

The system is a **3-layer WinForms desktop app**, single process, no web/API layer:

```
┌─────────────────────────────────────────────────────┐
│ LAYER 1 — UI (Panels & Dialogs)                      │
│ WinForms UserControls (*.cs + *.Designer.cs pairs)   │
│ e.g. PrisonerPanel, AddEditPrisonerDialog            │
└───────────────────────┬───────────────────────────────┘
                         │ calls getters/setters, create()/update()/delete()
┌───────────────────────▼───────────────────────────────┐
│ LAYER 2 — In-Memory Lists + Entity Logic              │
│ Program.cs static List<T> (Prisoners, ProductionOrders…) │
│ Entity classes (Prisoner.cs, ProductionOrder.cs…)     │
│ — hold state, validation, state-machine transitions   │
└───────────────────────┬───────────────────────────────┘
                         │ builds SqlCommand → EXECUTE sp_...
┌───────────────────────▼───────────────────────────────┐
│ LAYER 3 — SQL_CON.cs → SQL Server (sadcourse3)        │
│ Every entity method calls a stored procedure only —   │
│ no inline/ad-hoc SQL anywhere in C#                   │
└─────────────────────────────────────────────────────────┘
```

**The data journey — one concrete trip, start to finish** (using "Add Prisoner" as the example):

1. User opens `PrisonerPanel` → clicks **Add** → `btnAdd_Click` opens `AddEditPrisonerDialog`.
2. User types into text boxes / picks combo box values, clicks **Save**.
3. `btnSave_Click` runs `ValidateInput()` **in C#, before touching the DB**: required fields, numeric ranges, duplicate prisoner-number check against the in-memory `Program.Prisoners` list.
4. If valid → `new Prisoner(id, ..., isNew: true)` is constructed.
5. The constructor calls `this.create()`, which builds a `SqlCommand` with `EXECUTE sp_Prisoner_create @prisoner_id, ...` and parameter values.
6. `SQL_CON.execute_non_query(cmd)` opens the connection (from `app.config`), runs the stored procedure, shows a success/error `MessageBox`, and **always closes the connection in a `finally` block**.
7. Back in the constructor: the new `Prisoner` object is appended to `Program.Prisoners` (the in-memory list).
8. Dialog closes with `DialogResult.OK` → `PrisonerPanel.refreshList()` clears and re-populates the `ListView` **from `Program.Prisoners`** (not from a fresh DB query) — the UI reflects the in-memory list, which is now in sync with the DB.

**Key talking point**: the DB is the source of truth at startup (`Program.initLists()` loads every list from a `sp_X_get_all` stored procedure, in strict FK order) and on every write, but **during a running session the UI reads only from the in-memory `List<T>`** — that's why `Program.cs`'s load order (Employment Department → Customer/Product → Department Management/Prisoner/Contract → Production Order → Work Order → Attendance/Productivity) matters: every later list resolves its foreign keys (`seekById`) against lists that are already loaded.

---

## 2. Design-to-Code Mapping — 3 Core Use Cases

### Use Case A — Prisoner Management (full CRUD + 10-state lifecycle)

| Layer | Name |
|---|---|
| Class Diagram entity (Part 1) | `Inmate` |
| C# class (Part 2) | `Prisoner.cs` |
| SQL table | `Prisoner` |
| Panel / Dialog | `PrisonerPanel.cs`, `AddEditPrisonerDialog.cs` |
| Requirements covered | R1 (persistent profile), R4 (factory assignment), R9 (safety-cert expiry) |

- **CRUD**: Add / Edit / Delete / View all wired in `PrisonerPanel.cs` (`btnAdd_Click`, `btnEdit_Click`, `btnDelete_Click` with a Yes/No confirm `MessageBox`, `btnView_Click`).
- **Validation example**: duplicate `prisoner_number` rejected in `AddEditPrisonerDialog.ValidateInput()`; release date must be after work-start date; a release date >7 years in the past is refused outright (can't backfill ancient records).
- **Edge case / state machine**: `PrisonerActivityStatus` has **10 states** (`Prisoner.cs`), each transition is its own guarded method — e.g. `clockIn()` throws if the prisoner's safety certification has expired:
  ```csharp
  if (this.safetyTrainingValidity.HasValue && this.safetyTrainingValidity.Value < DateTime.Now)
      throw new Exception("Error: prisoner's safety certification has expired");
  ```
  `PrisonerPanel.updateAvailableActions()` only **shows the buttons valid for the prisoner's current state** (e.g. Approve/Reject only while pending approval) — the UI itself enforces the state machine, not just the backend.
- **Association example (FK)**: `Prisoner` (1) → `AttendanceRecord` (N).
  - **DB**: `create_database.sql` → `CONSTRAINT FK_AttendanceRecord_Prisoner FOREIGN KEY (prisoner_id) REFERENCES Prisoner(prisoner_id)`.
  - **Code**: `Prisoner.cs` holds `List<AttendanceRecord> attendanceRecords` with `getAttendanceRecords()`/`addAttendanceRecord()`; `PrisonerPanel.btnClockIn_Click` creates the child row directly: `new AttendanceRecord(nextId, prisoner.getId(), now.Date, prisoner.getFactory().Value, newEntry, null, true)`.

### Use Case B — Production Order Management (full CRUD + 6-state lifecycle + cascading children)

| Layer | Name |
|---|---|
| Class Diagram entity (Part 1) | `Order` |
| C# class (Part 2) | `ProductionOrder.cs` |
| SQL table | `ProductionOrder` |
| Panel / Dialog | `ProductionOrderPanel.cs`, `AddEditProductionOrderDialog.cs` |
| Requirements covered | R2 (order intake), R3 (capacity/factory assignment), R8 (delivery tracking) |

- **CRUD**: same Add/Edit/Delete/View pattern in `ProductionOrderPanel.cs`; double-clicking a row opens `OrderWorkOrdersDialog` (drill-down to child Work Orders).
- **Validation / edge case**: state-machine guards throw Hebrew-language exceptions if violated, e.g. `markReadyForPickup()`:
  ```csharp
  if (this.completedQuantity < this.quantity)
      throw new Exception("כשגיאה: לא הושלמה כמות יחידות מספקת");
  ```
- **Association example (3 FKs + 1 managed collection)**: `ProductionOrder` has FKs to `CustomerCompany`, `Product`, and `Contract`, and owns a `List<WorkOrder>`.
  - **DB**: `FK_ProductionOrder_CustomerCompany`, `FK_ProductionOrder_Product`, `FK_ProductionOrder_Contract` in `create_database.sql`.
  - **Code**: the constructor resolves all three immediately (`this.customerCompany = CustomerCompany.seekById(custCompId)`, etc.); `addWorkOrder()`/`getWorkOrders()` mirror the **exact same "managed collection, not association class" pattern** the class diagram specified for `Task ↔ Inmate` — here applied to `ProductionOrder ↔ WorkOrder`.

### Use Case C — Attendance Recording (Create/Read/Update/Delete + business-rule validation in C#, not SQL)

| Layer | Name |
|---|---|
| Class Diagram entity (Part 1) | `DailyAttendance` |
| C# class (Part 2) | `AttendanceRecord.cs` |
| SQL table | `AttendanceRecord` |
| Panel / Dialog | `AttendanceRecordPanel.cs`, `AddEditAttendanceDialog.cs` |
| Requirements covered | R5 (attendance tracking) |

- **CRUD**: full CRUD (Add/Edit/Delete) — note this is *richer* than originally scoped (see Gap #3 below).
- **Validation / edge case**: at least one of entry/exit time required; exit must be after entry; **overlap detection** — a prisoner cannot have two attendance records on the same date with overlapping hours. This rule is **not** a DB constraint — it's enforced purely in C# (`AddEditAttendanceDialog.CheckForOverlap()`), by looping the in-memory `Program.AttendanceRecords` list and throwing if any existing record's time range intersects the new one. Good example to cite if asked "where does validation live, app or DB?" — answer: **both**, but business rules that need cross-row comparison live in C#; type/range rules live in SQL `CHECK` constraints.
- **Association example (FK)**: same `Prisoner ↔ AttendanceRecord` FK as Use Case A, viewed from the child side — `AttendanceRecord.cs` stores `prisonerId` and resolves the parent on demand.

---

## 3. Gap Analysis & Deviations — Be Ready For These Questions

### Deviation 1 — Primary Key strategy: **String (planned) → INT (built)**
- **Plan**: class diagram and design conventions specified human-readable String PKs (e.g. `"P123456"`, `"FAC-A"`), each entity getting its own `getNextXYZId()` sequence helper.
- **Built**: every table uses `INT NOT NULL PRIMARY KEY`, assigned in the C# layer with a simple "scan the in-memory list for the max ID, add 1" pattern (see `AddEditPrisonerDialog.btnSave_Click`: `int maxId = ...; int nextId = maxId + 1;`).
- **Why we changed it**: one uniform ID-generation pattern works identically across all 10 entities — no per-entity string-formatting/parsing helper needed, no risk of ID-format typos. Trade-off knowingly accepted: lost the human-readability of string IDs.

### Deviation 2 — `Factory`: full entity (planned) → enum + CHECK constraint column (built)
- **Plan**: class diagram modeled `Factory` as its own entity/table (name, manager, capacity, etc.).
- **Built**: no `Factory` table exists. `Factory` is a C# `enum { MaimonSpices, Technosak, SewingWorkshop, TzitzitWorkshop }`, stored as `NVARCHAR(30)` with a `CHECK (factory IN (...))` constraint on every table that references it (`Prisoner`, `CustomerCompany`, `AttendanceRecord`, `DepartmentManagement`).
- **Why we changed it**: there are exactly **4 fixed factories** in the real organization and that number never changes at runtime — giving it a full CRUD-able table/panel would have added a screen for data nobody ever edits. An enum encodes the same domain constraint with far less code, and is consistent with our explicit decision to keep all enumerators as enumerators rather than collapsing statuses into lookup tables.

### Deviation 3 — Four Part-1 entities never became tables: `Inventory`, `PayrollRecord`, `PerformanceEvaluation`, `Alert` (+ `SafetyCertification` folded into a column)
- **Plan**: all five appear in the 12-entity class diagram and in dedicated sequence diagrams (inventory/reorder alert, monthly payroll → Tamar export, quarterly parole-board evaluation, exception escalation).
- **Built**: none of the four have a table. `SafetyCertification` was simplified from its own entity into a single `safety_training_validity DATE` column directly on `Prisoner`.
- **Why we changed it**: this was a **deliberate MVP scope cut**, not an oversight — these were explicitly tiered as "later phases" because they depend on integrations (Tamar payroll, Tzohar security sync) that are mocked/out-of-scope for the course project. The team chose to deliver **fully working CRUD + state machines** for the core entities (Prisoner, Orders, Work Orders, Attendance, Productivity) rather than partial stubs for all twelve.
- **One scope change ran the *other* direction, worth mentioning if asked for a 4th**: `AttendanceRecord` was originally planned as Create+Read only, but the built version has full Update/Delete too — an example of scope *expanding*, not just shrinking.

---

## 4. Reverse Tracing (Code → Design)

### `ProductionOrderPanel.cs` / `AddEditProductionOrderDialog.cs`
- **Justified by**: R2 (order intake & tracking), R3 (factory capacity assessment), R8 (delivery-deadline tracking) — and directly implements **Sequence Diagram #1, "Order Submission and Capacity Assessment."**
- **Traces back to a real interview pain point**: factory manager Yehuda Bromi described orders being tracked "three ways at once" (paper form, personal Excel sheet, Outlook email) with no searchable history — Problem #4 in the problems table ("no fast way to find a customer's order history"). The panel's filterable order list with status/date columns is the direct answer to that complaint.

### `PrisonerPanel.cs` / `AddEditPrisonerDialog.cs`
- **Justified by**: R1 (persistent inmate profile), R4 (daily assignment/scheduling), R9 (safety-certification expiry tracking) — implements **Sequence Diagram #2, "Morning Inmate Availability Check and Job Assignment,"** and the full `Prisoner` 10-state lifecycle.
- **Traces back to two real pain points**: (1) Problem #10 — the Tamar system doesn't retain prisoner data between months, forcing re-entry every month — solved by one persistent `Prisoner` record that lives for the prisoner's whole employment history; (2) Problem #12 — no digital tracking of safety-training validity — solved by the `safety_training_validity` field plus the dedicated "expiring safety training" filter button (`btnFilterExpiringSafety_Click`) in the panel.

---

## 5. One-Page Cheat Sheet (say this if asked "walk me through the system")

> "We started from 15 documented operational problems at Prison Elah's employment department — manual Excel sheets, no order history, no payroll memory between months, no link to the prison's security system. We turned those into 20 functional requirements, designed a 12-entity class diagram and 7 sequence diagrams, then built an MVP that implements **10 of those entities** with full CRUD and explicit state machines, deliberately deferring four entities (Inventory, Payroll, Performance Evaluation, Alert) whose sequence diagrams depend on external integrations we mocked or scoped out for this course project. Every screen talks to an in-memory list that's loaded once at startup in strict foreign-key order, and every write goes through a stored procedure — never raw SQL from the UI."

**If you forget everything else, remember the 3 deviations**: (1) String PKs → INT PKs, (2) Factory entity → enum, (3) Inventory/Payroll/PerformanceEvaluation/Alert deferred as MVP scope cuts.
