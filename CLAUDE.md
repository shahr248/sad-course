# CLAUDE.md — Prison Employment Department Information System (PEDIS)

## What This Project Is

This is a **System Analysis and Design (SAD) course project** for the Industrial Engineering and Management program at Ben-Gurion University. It is an academic exercise in requirements analysis, use case design, and system architecture—not a production system.

The project demonstrates the full analysis → design pipeline for a prison employment management system. It covers:

1. **Organizational context** — בית סוהר "אלה" (Prison Elah) in Be'er Sheva: a medium-security facility operating four manufacturing workshops employing ~75 inmates
2. **Problem identification** — 15 documented operational bottlenecks: manual processes, data silos, lack of real-time coordination between facilities
3. **Requirements** — 20 functional requirements (R1–R20) + 5 non-functional requirements, structured in two-layer format (behavioral + implementation notes)
4. **System design** — class diagram (12 core entities), sequence diagrams (7 key scenarios), and UC specifications (to be written)

## Architecture Conventions

This project inherits the shared SAD conventions from [`cloned/PATTERNS.md`](./cloned/PATTERNS.md):
- Entity pattern with self-contained DB methods
- **String primary keys** (not INT) — INTsmateId, orderId, factoryId, etc.; exception: DailyAttendance and ProductionOutput use date-based PKs
- In-memory lists loaded from DB at startup; load order strictly enforced
- Stored procedures only for DB access — no ad-hoc SQL
- Panel navigation (WinForms UserControl, single window)
- Hebrew for UI/Hebrew docs; English for requirements/code/design
- Right-to-left layout for all Hebrew panels

**Do not revisit these decisions without instructor approval.** They are intentional for teaching coherence.

---

## Database

**Database Name**: `sadcourse3`

**Connection String** (for reference):
```
Server=localhost,1433; Database=sadcourse3; User Id=sa; Password=CoursePassword123!; TrustServerCertificate=true;
```

### Critical Rule — USE Statement on Every Query

**Every SQL query executed via `execute_sql` must start with:**
```sql
USE sadcourse3;
```

This applies to all stored procedure definitions, data loading, schema creation, and modifications. Example:

```sql
USE sadcourse3;
CREATE TABLE Factory (
    factoryId NVARCHAR(50) NOT NULL PRIMARY KEY,
    factoryName NVARCHAR(MAX) NOT NULL,
    ...
);
```

**Why**: Ensures all operations target the correct database and prevents accidental schema pollution in the `master` or other databases.

---

## Document Map

| File | Purpose | Owner | Status |
|---|---|---|---|
| `docs/org-analysis/organization.md` | Organizational structure, existing systems, IT infrastructure | Domain | ✅ Complete |
| `docs/org-analysis/02-interviews.md` | Two semi-structured interviews with employment officer and factory manager | Domain | ✅ Complete |
| `docs/org-analysis/03-problems.md` | 15-row problems table: issue, cause, source, desired fix | Domain | ✅ Complete |
| `docs/org-analysis/04-business-analysis.md` | 7 core business processes: order intake, inmate assignment, attendance tracking, inventory, payroll, maintenance, performance eval | Domain | ✅ Complete |
| `docs/00-requirements.md` | 20 functional + 5 non-functional requirements, two-layer format + traceability matrix | Requirements | ✅ Complete |
| `docs/design/05-class-diagram.md` | 12 core entity classes, attributes, methods, relationships | Design | ✅ Complete |
| `docs/design/06-design-sequence.md` | 7 sequence diagrams: order intake, daily scheduling, attendance, inventory, payroll, evaluation, alert escalation | Design | ✅ Complete |
| `docs/00e-use-cases.html` | Interactive UC diagram + VP18 specs (to be generated) | Design | 🔄 Pending |
| `example_project/` | C# WinForms reference implementation (to be written) | Implementation | 🔄 Pending |
| `scripts/create_database.sql` | Full DB creation script (to be written) | Implementation | 🔄 Pending |

---

## Domain Entities and Load Order

### Entity Hierarchy

The in-memory list load order **must** follow this sequence to respect foreign key dependencies:

```
1. Factory                  (no FK)
2. Inmate                   (FK → Factory)
3. SafetyCertification      (FK → Inmate)
4. Order                    (FK → Factory + external customer)
5. Task                     (FK → Order + Factory)
6. DailyAttendance          (FK → Inmate + Task)
7. ProductionOutput         (FK → Task + Inmate)
8. Inventory                (FK → Factory)
9. PayrollRecord            (FK → Inmate)
10. PerformanceEvaluation   (FK → Inmate)
11. User                    (no FK; for authentication/audit)
12. Alert                   (polymorphic: relates to any entity)
```

**Rationale**: Base entities first (Factory, Inmate, Order), then entities with FK references, then transactional records (Attendance, Output, Payroll) that depend on historical data.

### Primary Key Strategy — String PKs (Deviation from PATTERNS.md)

**This project uses String primary keys, not INT.**

- DDL: all non-temporal entities have PKs as `NVARCHAR(50) NOT NULL PRIMARY KEY`
- Exceptions: `DailyAttendance` and `ProductionOutput` use date-based PKs (see below)
- Rationale: Human-readable keys (inmateId "P123456", factoryId "FAC-A") are more meaningful in a correctional facility context and match the class diagram specification

#### Primary Key Formats by Entity

| Entity | PK Format | Example | Comment |
|---|---|---|---|
| Inmate | inmateId (String) | "P123456" | Prison inmate number |
| Factory | factoryId (String) | "FAC-A", "FAC-B" | Fixed facilities |
| Order | orderId (String) | "ORD-2026-0001" | Auto-generated sequential |
| Task | taskId (String) | "TASK-2026-0001" | Auto-generated sequential |
| DailyAttendance | attendanceId (date + inmateId) | "20260620-P123456" | Date-based composite |
| ProductionOutput | outputId (date + inmateId + taskId) | "20260620-P123456-TASK-0001" | Date-based composite |
| Inventory | inventoryId (String) | "INV-20260620-001" | Date + sequence |
| SafetyCertification | certificationId (String) | "CERT-2026-0001" | Auto-generated |
| PayrollRecord | payrollId (String) | "PAYROLL-202606-P123456" | Period + inmate |
| PerformanceEvaluation | evaluationId (String) | "EVAL-2026-Q1-P123456" | Quarterly + inmate |
| User | userId (String) | "USER-001" | System user |
| Alert | alertId (String) | "ALERT-2026-0001" | Auto-generated |

**Implementation in C#**: 
- For sequential String PKs, create a helper method in Program class or in each entity: `static string getNextXYZId()` that retrieves max order number from `Program.XYZs` list, parses suffix, and increments
- Alternative: read from a `Sequences` table (one row per entity type, tracking next available ID)

---

## Relationship Patterns

### Task ↔ Inmate: Managed Collection (Not Association Class)

**Decision**: `Task` owns `List<Inmate>` for assigned workers. This is **not** modeled as an association class.

```csharp
class Task {
    public List<Inmate> assignedInmates { get; set; }
    public void assignInmate(Inmate inmate) { ... }
    public void removeInmate(Inmate inmate) { ... }
}
```

**Rationale**:
- Simplicity: task-level attributes (quantity, deadline) are sufficient; no need for per-assignment metadata (hoursAssigned, reassignmentReason)
- The business process (R4: Inmate Assignment) treats assignment as a simple pointing operation, not a rich entity
- PerformanceEvaluation and PayrollRecord separately track inmate-level history if needed

**DB Mapping**: A join table `Task_Inmate` with columns `task_id, inmate_id` (no entity class, used only for queries and CRUD).

### Inmate ↔ SafetyCertification: One-to-Many

```
Inmate (1) ←—— (N) SafetyCertification
```

- An inmate can hold multiple certifications (Fire Safety, Equipment Operation, etc.)
- Each certification record has expiration logic (R9)
- Loaded after Inmate: `Program.initSafetyCertifications()` populates `Program.Inmate.certifications`

---

## Core Use Cases (High-Level Grouping)

The 20 functional requirements map to these use case clusters (detailed UC specs to follow):

| UC Group | Requirements | Actors | Priority |
|---|---|---|---|
| **Order Management** | R1, R2, R3, R8 | Employment Officer, Customer | HIGH |
| **Daily Scheduling** | R4, R9, R14 | Employment Officer, Factory Manager | HIGH |
| **Attendance & Productivity** | R5, R6 | Factory Floor, Task Management | CRITICAL |
| **Material Management** | R7, R15 | Factory Manager, Procurement | HIGH |
| **Payroll & Compensation** | R10 | Payroll Processor | CRITICAL |
| **Performance Evaluation** | R11 | Factory Manager, Parole Board | MEDIUM |
| **System Coordination** | R12, R13, R16, R17, R18, R19, R20 | System, Admin | CRITICAL |

---

## MVP Scope — Phase 1 Implementation

For the MVP + all CRUDs, implement these entities and use cases:

**Tier 1 — Core (Must-Have)**
- ✅ Factory (CRUD) — list, create, view, edit
- ✅ Inmate (CRUD) — with availability status
- ✅ Order (CRUD) — intake, track, complete
- ✅ Task (CRUD) — assign to order, manage inmate assignments
- ✅ DailyAttendance (Create, Read) — record entry/exit, display daily roster
- ✅ ProductionOutput (Create, Read) — record units, calculate quality metrics

**Tier 2 — Extended (Should-Have for MVP+)**
- ✅ SafetyCertification (CRUD) — track and validate certifications
- ✅ Inventory (CRUD) — stock levels, reorder alerts
- ✅ PayrollRecord (Create, Read, Approve) — calculate wages, export to תמר

**Tier 3 — Later Phases**
- Performance Evaluation (Report generation for parole board)
- User Management (multi-user auth, role-based access)
- Alert System (background monitoring and escalation)
- External Integrations (צוהר sync, תמר export automation)

---

## Known Conflicts and Design Notes

### 1. Factory Manager vs. Inmate Assignment Authority
**Issue**: Org analysis mentions three factory managers (one per facility), but class diagram has no `FactoryManager` entity.

**Resolution**: Factory managers are system users (`User` entity) with role = "FactoryManager" and `factory_id` FK. Access control is role-based, not entity-based. Store factory manager in `User` table, not as separate entity.

### 2. External System Integration Scope
**Issue**: Requirements R12 (צוהר sync) and R10 (תמר export) involve integration with external systems. Scope unclear.

**Resolution for MVP**:
- צוהר (Security Status) → Mock data provider for now. Assumption: צוהר API unavailable; manually seeded availability status.
- תמר (Payroll) → Generate export file (CSV/XML) only. Assumption: actual תמר integration deferred to Phase 2.
- Document integration points in code but do not implement API calls in initial release.

### 3. Attendance Recording: Automated vs. Manual
**Issue**: Sequence diagram assumes facial recognition for entry/exit. Not available in MVP.

**Resolution**: Manual attendance entry via UI panel. Inmate ID + date + time fields. Batch record from paper logs later. Facial recognition as future enhancement.

### 4. Payroll Export Format
**Issue**: תמר system's expected input format not specified in requirements.

**Resolution**: Generate a standardized CSV with columns: inmate_id, first_name, last_name, hours_worked, base_pay, productivity_bonus, deductions, net_pay. Format documented in `scripts/payroll_export_template.csv` (to be created).

### 5. Silent Conflict: Inmate Status Enums
**Issue**: Class diagram lists `employmentStatus` (Active/Inactive/OnHold) but sequence diagram and interviews also mention security holds from צוהר (security status), absence types (medical/court/isolation).

**Resolution**: 
- `employmentStatus` = assignment status (Active/Inactive/OnHold) for employment program only
- `securityStatus` = fetched from צוהר daily; stored as separate field (Security Hold / No Hold)
- `dailyAbsenceReason` = recorded in DailyAttendance (Medical/Court/Isolation/Unexcused)
These are three independent dimensions, not conflicting.

### 6. Enum Values — Keep All Enumerators
**Decision**: Per user feedback ("keep enumerators as enumerators"), do not simplify enum values. Example:

```csharp
enum OrderStatus { Submitted, Approved, InProgress, Completed, Cancelled }
enum TaskStatus { Assigned, InProgress, Completed, Blocked }
enum AttendanceStatus { Present, Absent, Excused, Medical }
enum AlertSeverity { Info, Warning, Critical }
```

Do not collapse into single numeric `status` field and mapping table.

---

## Database Schema Considerations

### No Inheritance Tables (per decision: no subtypes)
All 12 entities are independent tables. No table-per-subclass needed.

### Audit Logging
All transactional tables (DailyAttendance, ProductionOutput, PayrollRecord, Alert) must include:
- `recordedAt` (DateTime) — when record was created
- `recordedBy` (user_id or string) — who created it
- `modifiedAt` (DateTime, nullable) — last update time
- `modifiedBy` (user_id or string, nullable) — who last modified

These support compliance and debugging (requirement NFR5: Compliance).

### No Soft Deletes
Records are not marked as deleted; they are removed. Requirement NFR5 (Compliance) can be satisfied with audit logs and backup retention instead.

---

## Deliverables Checklist — SAD Course Submission

- [x] Organizational context (`docs/org-analysis/organization.md`)
- [x] Problem identification (`docs/org-analysis/03-problems.md`)
- [x] Requirements specification (`docs/00-requirements.md`)
- [x] Class diagram (`docs/design/05-class-diagram.md`)
- [x] Sequence diagrams (`docs/design/06-design-sequence.md`)
- [ ] UC diagram + specs (to be generated from requirements)
- [ ] Database schema (`scripts/create_database.sql`)
- [ ] C# WinForms implementation (`example_project/`)
- [ ] Stored procedures (`scripts/*.sql`)

---

## Key Decisions — Do Not Revisit Without Discussion

1. **String primary keys** (not INT) for human readability in correctional facility context
2. **List<Inmate> on Task** (not association class) — simplicity over metadata richness
3. **No entity subtypes** — all 12 entities independent, no table-per-subclass
4. **Manual attendance** (not facial recognition) for MVP
5. **Mock צוהר integration** (not live) — external API assumed unavailable for course project
6. **All enumerators kept** — no numeric status fields with mapping tables
7. **Hebrew UI labels, English code** — per PATTERNS.md language conventions
8. **Panel navigation only** — no dialogs or additional forms beyond WinForms UserControl pattern

---

## How to Use This Document

**For developers**: 
- Entity classes inherit the patterns from `cloned/PATTERNS.md` (entity pattern, constructor logic, DB methods)
- This document adds domain-specific load order (§ Domain Entities), relationship patterns (§ Relationship Patterns), and confliction resolutions (§ Known Conflicts)
- All PKs are String except temporal records; implement `getNextXYZId()` for auto-incrementing sequences

**For designers/architects**:
- Class diagram is in `docs/design/05-class-diagram.md`
- Sequence diagrams detail key interactions in `docs/design/06-design-sequence.md`
- Requirements traceability matrix in `docs/00-requirements.md` links each requirement to entity/UC

**For instructors**:
- This document is the analysis-to-design bridge for the course submission
- Appendix shows how human decisions (interviews, problem identification) flow into architecture (entity design, load order, UC grouping)
- Conflicts resolved transparently; deviations from PATTERNS.md (String PKs) justified and approved

---

## Appendix: How We Got Here

### From Problems to Requirements
The 15-row problems table (org-analysis/03-problems.md) drove the 20 functional requirements (00-requirements.md). Each requirement solves ≥1 problem:

| Problem | Requirement | How It Helps |
|---|---|---|
| P1 (data entry errors) | R1 (inmate profiles), R2 (order form validation) | Structured forms prevent re-entry; dropdown selections reduce typos |
| P3 (Excel complexity) | R13 (dashboard) | Visual dashboard replaces manual Excel queries |
| P4 (lost order history) | R2 (order tracking), R8 (delivery schedule) | Centralized order DB, searchable history |
| P9 (manual attendance) | R5 (attendance tracking), R12 (צוהר integration) | Automatic/manual entry replaces paper forms |
| P10 (monthly re-entry) | R1 (persistent profiles), R10 (payroll calc) | Database remembers inmate data; payroll automated |
| P11 (security risk) | R4 (daily scheduling), R12 (צוהר sync) | Real-time security status check before assignment |

### From Requirements to Entities
Requirements 1–20 map to entities:

- **R1**: Inmate (profiles persist)
- **R2, R3, R8**: Order (track, capacity assess, delivery schedule)
- **R4, R14**: Task, assignment logic, inmate reallocation
- **R5, R6**: DailyAttendance, ProductionOutput
- **R7, R15**: Inventory, procurement
- **R9**: SafetyCertification (expiration tracking)
- **R10**: PayrollRecord (wage calculation, export)
- **R11**: PerformanceEvaluation (parole board report)
- **R12**: צוהר integration (mock in MVP)
- **R13**: Dashboard (queries across all entities)
- **R16**: Alert (exception handling, escalation)
- **R17**: User (authentication, audit logging)
- **R18–NFR5**: Infrastructure (backup, compliance)

### Load Order Justification
```
Factory (0 deps) 
  ↓ (owns inmates)
Inmate (1 dep: Factory)
  ↓ (has certifications)
SafetyCertification (2 deps: Inmate)
  ↓ (assigned to factory)
Order (1 dep: Factory + external customer ref)
  ↓ (broken into)
Task (2 deps: Order, Factory)
  ↓ (records daily work)
DailyAttendance (2 deps: Inmate, Task)
ProductionOutput (2 deps: Task, Inmate)
  ↓ (both depend on Task before they exist)
Inventory (1 dep: Factory)
  ↓ (independent, but depends on Factory existing)
PayrollRecord (1 dep: Inmate + historical Attendance data)
PerformanceEvaluation (1 dep: Inmate + historical ProductionOutput data)
  ↓ (read-only use of historical records)
User (0 deps; for audit fields in other entities)
Alert (polymorphic; no direct FK, used for notifications)
```

This ensures no missing foreign key references during initialization.

---

## Entry Flow — WinForms Navigation Architecture

### Authentication Model

**Single Credential Entity**: `DepartmentManagement`
- `username` (unique login ID)
- `password` (plaintext for course project)
- `role` (3 values: departmentManager, deputyOfDepartmentManager, factoryManager)
- `factory` (assigned facility for Factory Manager; NULL for Employment Officers)

### Human Actors & Role-Based Routing

| Actor | Role | Home Panel | Responsibilities |
|---|---|---|---|
| Employment Officer | departmentManager, deputyOfDepartmentManager | EmploymentOfficerHome | Prisoner mgmt, order tracking, contract mgmt, reporting |
| Factory Manager | factoryManager | FactoryManagerHome | Assignment, attendance, productivity, inventory, work orders |

### Entry Point Architecture

```
mainForm (entry point)
  ├── Program.Main() 
  │   └── Program.initLists() → Load all entities from DB
  │
  └── LoginPanel (first screen)
      ├── Input: username, password
      ├── Validate: seek DepartmentManagement record matching credentials
      └── On success: route to role-specific home panel
          ├── departmentManager → EmploymentOfficerHome
          ├── deputyOfDepartmentManager → EmploymentOfficerHome
          └── factoryManager → FactoryManagerHome

EmploymentOfficerHome
├── Button: Prisoners (placeholder)
├── Button: Orders (placeholder)
├── Button: Contracts (placeholder)
├── Button: Work Orders (placeholder)
├── Button: Attendance (placeholder)
├── Button: Reports (placeholder)
└── Button: Logout → return to LoginPanel

FactoryManagerHome
├── Button: Prisoners (placeholder)
├── Button: Attendance (placeholder)
├── Button: Productivity (placeholder)
├── Button: Work Orders (placeholder)
├── Button: Inventory (placeholder)
├── Button: Reports (placeholder)
└── Button: Logout → return to LoginPanel
```

### Implementation Notes

**mainForm.cs**
- Single MDI container (ShowDialog pattern with panel swapping)
- `showLoginPanel()` — displays login screen on startup
- `handleLoginSuccess(DepartmentManagement user)` — routes to home based on role
- `handleLogout()` — clears current user, returns to login

**LoginPanel.cs (UserControl)**
- Credential validation via `Program.DepartmentManagements` list
- Event: `onLoginSuccess` fires with authenticated DepartmentManagement object
- Keyboard: Enter key triggers login

**EmploymentOfficerHome.cs & FactoryManagerHome.cs (UserControl)**
- Placeholder buttons route to future CRUD/feature panels
- Display current user info (username + role)
- Event: `onLogout` fires to return to login

### Security Notes
- Authentication is **NOT** a formal requirement; login flow is for demonstration only
- Passwords stored plaintext (course project, not production)
- No session management; single user per window
- Role-based access control via enum `DepartmentManagementRole`

---

## Visual Design Language (Phase 10.1 Polish — ProductionOrderPanel, AttendanceRecordPanel, ProductivityRecordPanel)

**Color palette**

| Role | Color | Hex |
|---|---|---|
| Title text | `(0,51,102)` | `#003366` |
| Separator line | `(189,195,199)` | `#BDC3C7` |
| Primary action (View / Apply Filters) | `(52,152,219)` | `#3498DB` |
| Create / Add | `(46,204,113)` | `#2ECC71` |
| Edit | `(241,196,15)` | `#F1C40F` |
| Delete | `(231,76,60)` | `#E74C3C` |
| Back / Clear / neutral | `(149,165,166)` | `#95A5A6` |
| Secondary/tertiary action (e.g. Daily Totals) | `(230,126,34)` | `#E67E22` |
| Panel background | `(236,240,241)` | `#ECF0F1` |
| Filter-section backdrop (optional) | `(248,249,250)` | `#F8F9FA` |
| Filter label text | `(50,50,50)` | `#323232` |
| ListView background | White | `#FFFFFF` |

**Fonts** (all Segoe UI)
- Page title: 18pt Bold
- Section/filter labels: 10pt Bold
- ListView grid content: 9.5pt Regular
- Primary buttons (View/Add/Edit/Delete/Back): 10pt Bold
- Secondary buttons (Apply/Clear Filters, utility actions): 9pt Bold

**Spacing rules**
- 20px left margin for all primary controls; content width = 984px (1024 panel width − 20px margins on each side).
- 2px title separator spans the full 984px content width, directly under the title.
- 20px vertical gap between major sections: grid → CRUD button row → Back button row.
- CRUD button row: View/Add/Edit/Delete at x = 20, 130, 240, 350 (100px wide, 10px gaps); Back always at x=20 on its own row.

**Button styling rules**
- Standard size 100×35 for the primary row; 100×25 for filter-action buttons.
- Fixed color-to-action mapping per the palette above; white text; Bold font; `UseVisualStyleBackColor = false`.

**ListView styling rules**
- `BorderStyle = FixedSingle`, `Font = Segoe UI 9.5pt`, `BackColor = White`, `FullRowSelect = true`, `GridLines = true`, `View = Details`.
- Fixed width 984px; height adapts to available vertical space.

**Section/header styling**
- Title: 18pt Bold, `#003366`, anchored at (20,15).
- 2px `#BDC3C7` separator line directly beneath the title.
- Filter/section labels: 10pt Bold, `#323232`.

**Panel background style**
- Every top-level panel: `BackColor = #ECF0F1`.
- An optional `FixedSingle`-bordered `#F8F9FA` backdrop panel may be used to visually group a cluster of filter controls — added first in z-order so it never intercepts input. Not mandatory on every panel; applied case-by-case (currently only AttendanceRecordPanel).

