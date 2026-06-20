# System Design - Sequence Diagrams

## Overview
This document presents sequence diagrams for the Prison Employment Department Information System (PEDIS), illustrating key interactions between system components and actors for critical business processes.

---

## Sequence Diagram 1: Order Submission and Capacity Assessment

**Scenario:** A customer submits a new order and the system assesses factory capacity to recommend assignment.

![Order Submission and Capacity Assessment][sequence-order-intake.png]

### Participants
- Customer/Caller (external or internal)
- Employment Officer (user)
- Order Management System
- Factory 1, 2, 3, 4 (production facilities)
- Database (inmate availability, inventory, workload)

### Flow Description
1. Customer contacts employment officer via phone or email with order requirements
2. Employment officer opens order intake form in system
3. Officer enters: customer name, product description, quantity, delivery deadline, technical specs
4. System validates mandatory fields
5. System generates unique order number and records submission timestamp
6. System queries factory workload: for each factory, retrieves current pending orders and inmate availability
7. System calculates factory utilization rates based on current capacity vs. available workforce
8. System displays recommendation: "Factory B has 40% capacity available; can complete by deadline"
9. Employment officer reviews recommendation and selects Factory B
10. System records order assignment, updates Factory B workload projection
11. System confirms order acceptance to employment officer
12. System (optional): sends confirmation email to customer

### Key Validations
- All mandatory fields present
- Delivery deadline is future date
- At least one factory has capacity to meet deadline

---

## Sequence Diagram 2: Morning Inmate Availability Check and Job Assignment

**Scenario:** Each morning, the system syncs with צוהר security system to determine inmate availability, and the factory manager assigns inmates to pending tasks.

![Morning Availability Check and Assignment][sequence-daily-scheduling.png]

### Participants
- Scheduler/Batch Job (system background process)
- צוהר System (external prison security system)
- Inmate Availability Service (system component)
- Factory Manager (user)
- Task Management System
- Database

### Flow Description
1. At 06:00 AM, scheduler triggers daily sync job
2. Sync job connects to צוהר API using secure credentials
3. צוהר returns list of all inmates with status updates: security holds, planned absences (court, medical, isolation), security clearance changes
4. System compares צוהר data against cached inmate records in PEDIS database
5. For each inmate with status change:
   - If new hold detected: mark inmate as "Blocked", send alert to employment officer
   - If absence planned: mark inmate as unavailable for specified dates
   - If security clearance changed: update record and revalidate task assignments
6. Inmate Availability Service updates availability dashboard
7. System generates daily availability report: "75 inmates total, 68 available, 5 blocked, 2 on medical"
8. Factory manager receives availability summary
9. Manager opens task assignment interface, reviews pending tasks
10. Manager reviews available inmate list (filtered by: skills, security clearances, assignments)
11. Manager selects task and assigns available inmates
12. System validates: inmate security clearance ≥ task requirement, no conflicting assignments, safety certifications current
13. If validation passes:
    - System records assignment with timestamp and assigned-by user
    - System updates inmate's daily schedule
    - System sends confirmation to manager
14. If validation fails: System shows error message with reason (e.g., "Certification expires in 5 days")

### Key Validations
- Security hold check against צוהר
- Safety certification not expired
- No conflicting task assignments
- Inmate not in medical/isolation status

---

## Sequence Diagram 3: Attendance Tracking and Productivity Recording

**Scenario:** During the work day, an inmate is tracked entering/leaving the factory, and their production output is recorded.

![Attendance and Production Recording][sequence-attendance-output.png]

### Participants
- Inmate
- Facial Recognition System (or Manual Entry Terminal)
- Attendance Service (system component)
- Production Monitoring System
- Task Management System
- Database

### Flow Description

#### Part A: Attendance Tracking
1. Inmate approaches facility entrance at 08:00 AM
2. (Option 1) Facial recognition system captures face, looks up in database, recognizes inmate ID "12345"
   - (Option 2) If facial recognition unavailable: inmate manually enters ID at tablet/terminal
3. System records: entry event, timestamp (08:00:15), associated task ID
4. System updates daily attendance record: entry_time = 08:00
5. System verifies against expected schedule: "Inmate assigned to Task #1001 in Factory B"
6. If match: system marks attendance as "Confirmed"
7. If no match: system flags discrepancy, sends alert to factory manager
8. At 12:00 PM (lunch): inmate exits temporarily, facial recognition/manual capture at exit
9. System records: exit event, timestamp
10. System calculates: hours worked so far = 4.0 hours
11. At 13:00 PM: inmate re-enters facility after lunch
12. System records: entry event, resumes tracking
13. At 16:30 (end of shift): inmate exits facility permanently
14. System records: exit event, timestamp
15. System calculates: total_hours_worked = 8.25 hours (accounting for lunch break)
16. System records: attendance status "Present", hours worked "8.25", task completed

#### Part B: Production Output Recording
1. During the shift, factory worker (or tablet interface) records units produced
2. (Primary method) Worker scans barcode/enters task ID on tablet at workstation
3. Tablet form displays:
   - Task: "Assembly of Product X, Qty 500"
   - Current progress: 250 units completed
   - Worker ID: same inmate we're tracking
4. Worker inputs: "Completed 50 units in last hour"
5. Worker inputs quality check: "3 units defective"
6. System calculates: quality rate = (50 - 3) / 50 = 94%
7. System records: ProductionOutput record with timestamp
8. System updates Task progress: 250 + 50 = 300 units completed (60% complete)
9. System calculates productivity rate: 50 units / 1 hour = 50 units/hour
10. System compares against target rate (e.g., 45 units/hour): "EXCEEDS TARGET by 11%"
11. (Optional) System sends positive alert to manager: "Inmate 12345 productivity is high"
12. At end of shift, system generates daily summary:
    - Hours worked: 8.25
    - Units produced: 125 (assuming multiple entries throughout day)
    - Quality rate: 94.5% average
    - Productivity: 15.2 units/hour

### Key Calculations
- Total hours: exit_time - entry_time - lunch_breaks
- Productivity: units_produced / hours_worked
- Quality rate: good_units / total_units_produced
- Task progress: total_units_to_date / order_quantity

---

## Sequence Diagram 4: Inventory Management and Reorder Alert

**Scenario:** Factory manager checks inventory status; system detects low stock and auto-generates purchase requisition.

![Inventory Management and Reorder][sequence-inventory-reorder.png]

### Participants
- Factory Manager (user)
- Inventory Management System
- Material Tracking Database
- Procurement System
- Supplier Database
- Employment Officer (escalation)

### Flow Description
1. Factory manager logs into system, views Dashboard
2. Manager selects Inventory tab for Factory B
3. System displays current inventory items with status:
   - Item #1 (Raw Material A): 150 units, Status: OK
   - Item #2 (Raw Material B): 25 units, Status: LOW STOCK (minimum threshold: 50)
   - Item #3 (Component C): 5 units, Status: CRITICAL (minimum: 100, reorder point: 200)
4. System auto-highlights Item #3 in red and displays alert: "CRITICAL STOCK ALERT: Component C will be depleted in 2 days at current usage rate"
5. Manager clicks on Item #3 to view details:
   - Current quantity: 5 units
   - Usage rate (last 7 days): 25 units/day
   - Minimum required: 100 units
   - Reorder point: 200 units
   - Reorder quantity: 500 units
   - Supplier: "ABC Components Ltd"
   - Lead time: 5 business days
6. System displays: "If no action taken, stock depletes by [date]. Current pending orders require [estimated usage]"
7. Manager clicks "Issue Purchase Requisition"
8. System displays purchase order form pre-filled with:
   - Supplier: ABC Components Ltd
   - Material: Component C
   - Requested quantity: 500 units
   - Estimated delivery date: [today + 5 days]
   - Cost estimate: 2500 NIS
   - Urgency: Standard (or manager selects "Expedited" for premium)
9. Manager reviews and clicks "Submit"
10. System records purchase requisition with status: "Approved"
11. System (optional) sends email to procurement officer: "New requisition #PO-5432 for Component C from ABC Components Ltd, requested delivery [date]"
12. System sends email to supplier (if integration available): "Purchase Order #PO-5432"
13. System generates tracking alert: "Component C PO-5432 expected delivery [date]. Manager will receive reminder 1 day before"
14. System updates inventory forecast: "Assuming delivery on [date], inventory will reach 505 units"

### Key Thresholds
- Minimum stock: 100 units (below this is critical)
- Reorder point: 200 units (system suggests reorder if current < this)
- Lead time: 5 business days (time to receive after ordering)
- Usage rate: calculated from last 30 days history

---

## Sequence Diagram 5: Monthly Payroll Calculation and Submission to Tamar

**Scenario:** At month-end, the system aggregates attendance and productivity data, calculates wages, and submits to תמר payroll system.

![Monthly Payroll Calculation][sequence-payroll-calculation.png]

### Participants
- Payroll Processor (system component or user)
- Attendance Database
- Production Output Database
- Payroll Calculation Engine
- תמר System (external payroll system)
- Employment Officer (approval)
- Accountant (export)

### Flow Description
1. On June 30 (end of month), payroll processor initiates month-end run
2. System retrieves all attendance records for June for all inmates
3. For each inmate, system calculates:
   - Total hours worked in June = SUM(daily hours for June)
   - Example: Inmate 12345 = 176 hours (22 working days × 8 hours)
4. System retrieves all production output records for June for all inmates
5. For each inmate, system calculates:
   - Total units produced in June = SUM(units_produced daily for June)
   - Example: Inmate 12345 = 4,500 units
   - Quality rate average = AVG(quality_rate daily for June)
   - Example: Inmate 12345 = 96.2%
6. System retrieves wage rate configuration for inmate's employment category
   - Example: Inmate 12345 category "Standard Worker"
   - Base rate: 15 NIS/hour
   - Productivity bonus: 0.5 NIS/unit if quality ≥ 95%
7. System calculates gross pay components:
   - Base pay: 176 hours × 15 NIS/hour = 2,640 NIS
   - Productivity bonus: 4,500 units × 0.5 NIS/unit = 2,250 NIS (quality ≥ 95% met)
   - Family support: 300 NIS (fixed per inmate)
   - Total gross: 2,640 + 2,250 + 300 = 5,190 NIS
8. System applies deductions (if any):
   - Example: Inmate 12345 has no deductions
   - Example: Inmate 12346 has 500 NIS deduction (disciplinary)
9. System calculates net pay:
   - Inmate 12345: 5,190 NIS (no deductions)
   - Inmate 12346: 5,190 - 500 = 4,690 NIS
10. System records PayrollRecord for each inmate with status: "Draft"
11. Employment officer receives notification: "Payroll calculation complete. Review and approve."
12. Officer opens Payroll Review screen, sees summary:
    - Total inmates paid: 75
    - Total wages: 389,250 NIS
    - Anomalies detected: 2 inmates with unusually high wages (red flag)
13. Officer clicks on anomaly to investigate:
    - Inmate 12789: 7,500 NIS (150% above monthly average)
    - Reason: Worked overtime 20 hours due to rush order
    - Officer approves (overtime justified by rush order)
14. Officer clicks "Approve All"
15. System updates PayrollRecord status to "Approved"
16. System initiates export to תמר system
17. System converts payroll data to תמר format (CSV/XML) with fields:
    - Inmate ID, name, base pay, bonus, deductions, net pay, account number
18. System connects to תמר API using secure credentials
19. System submits payroll batch for all 75 inmates
20. תמר responds: "Batch #PYR-202606-01 accepted. Processing..."
21. System records: PayrollRecord status updated to "Submitted"
22. System generates audit log: "Payroll submitted to תמר at [timestamp] by [user]"
23. Accountant receives confirmation: "Payroll for June submitted to תמר"
24. תמר processes payroll (external process) and transfers funds to inmate accounts

### Key Calculations
- Base pay = hours_worked × hourly_rate
- Productivity bonus = units_produced × unit_rate (conditional on quality)
- Family support = fixed amount per inmate
- Deductions = disciplinary or court-ordered
- Net pay = (base + bonus + family_support) - deductions
- Anomaly detection: flag if wage > 200% or < 50% of inmate's 3-month average

---

## Sequence Diagram 6: Quarterly Performance Evaluation for Parole Board

**Scenario:** Quarterly performance evaluation is triggered; system aggregates quantitative data and manager provides qualitative assessment; report generated for parole board.

![Performance Evaluation and Parole Report][sequence-performance-eval.png]

### Participants
- Factory Manager (user)
- Evaluation System
- Metrics Aggregation Engine
- Database (attendance, productivity, incidents)
- Report Generation Service
- Parole Board (recipient)
- Employment Officer (submission)

### Flow Description
1. On March 31 (end of Q1), evaluation scheduler sends reminder to all factory managers
2. Factory manager logs in and clicks "Q1 Performance Evaluations"
3. System displays list of inmates managed by this factory in Q1
4. Manager selects Inmate 12345
5. System auto-populates quantitative metrics for Q1 (Jan-Mar):
   - Total hours worked: 520 hours (22 working days/month × 3 months)
   - Attendance rate: 98% (1 unexcused absence out of 66 working days)
   - Average daily productivity: 45.2 units/hour
   - Quality rate average: 96.1%
   - Safety incidents: 0
   - Certifications current: Yes (training expires July 2026)
   - Disciplinary incidents: 1 (minor infraction in February)
6. Manager reviews auto-populated data and agrees with metrics
7. Manager clicks "Add Qualitative Assessment"
8. Form displays fields for manager input:
   - Work quality: [narrative field]
   - Collaboration: [narrative field]
   - Discipline and behavior: [narrative field]
   - Skill improvement: [narrative field]
   - Overall recommendation: [dropdown: Advance, Maintain, Demote, Parole-Consideration]
9. Manager types narrative assessment:
   - Work quality: "Inmate produces consistently high-quality output, rarely requires rework. Very attentive to detail."
   - Collaboration: "Works well with other inmates. Occasionally assists others with tasks. Positive peer influence."
   - Discipline: "Generally well-behaved. One minor infraction in February (minor tool misplacement). Responds well to correction."
   - Skill improvement: "Has mastered assembly task. Cross-trained on packaging. Demonstrates aptitude for advancement."
   - Recommendation: "Advance"
10. Manager clicks "Submit for Review"
11. System records evaluation with status: "Draft"
12. Employment officer receives notification: "New evaluations submitted for review"
13. Officer reviews manager's assessments and agrees
14. Officer clicks "Approve"
15. System updates evaluation status to "Approved"
16. System initiates parole board report generation
17. Report generation service creates formatted report:
    - Inmate name, ID, incarceration details
    - Quantitative metrics (formatted as tables/charts)
    - Qualitative assessment (manager's narrative)
    - Overall recommendation: "ADVANCE - Recommend for parole consideration"
    - Signatures: Factory Manager, Employment Officer, Date
18. System converts report to PDF format
19. System archives report in database with status: "Report-Generated"
20. System sends email to parole board administration: "Performance evaluation for inmate 12345 ready for review"
21. Parole board accesses report and uses it in parole board meeting
22. (Optional) If parole decision made, board notifies prison; system records decision in inmate record

### Key Metrics Auto-Populated
- Attendance rate: days_present / working_days_in_period
- Productivity: units_produced / hours_worked (averaged daily)
- Quality rate: good_units / total_units (averaged daily)
- Safety incidents: count of recorded incidents
- Disciplinary incidents: count of infractions
- Certification status: cross-check against expiration dates

---

## Sequence Diagram 7: Exception Alert and Escalation - Missed Deadline Risk

**Scenario:** System detects that an order is at risk of missing its delivery deadline; alert is created, escalated to management, and corrective actions are recommended.

![Exception Alert and Escalation][sequence-alert-escalation.png]

### Participants
- Alert Detection Engine (background process)
- Task Progress Tracker
- Productivity Analytics
- Alert Management System
- Factory Manager (first escalation)
- Employment Officer (second escalation)
- Database (orders, tasks, alerts)

### Flow Description
1. Alert detection engine runs every 4 hours (e.g., 08:00, 12:00, 16:00, 20:00)
2. Engine queries all active tasks/orders with delivery deadlines in next 7 days
3. For each task, engine calculates:
   - Current completion percentage: units_completed / order_quantity
   - Days remaining until deadline
   - Current productivity rate: units_per_hour (average last 7 days)
   - Estimated completion date = current_date + (remaining_units / productivity_rate) / 8 hours
4. For Order #2050 (Delivery deadline: July 3):
   - Current completion: 60% (300/500 units)
   - Days remaining: 5 days
   - Remaining units: 200 units
   - Current productivity: 40 units/hour
   - Time needed: 200 units / 40 units/hour = 5 hours
   - Estimated completion: Today (June 28) at 13:00
   - Status: ON TRACK ✓
5. For Order #2051 (Delivery deadline: July 2):
   - Current completion: 20% (50/250 units)
   - Days remaining: 4 days
   - Remaining units: 200 units
   - Current productivity: 30 units/hour (slower than normal due to material shortage)
   - Time needed: 200 units / 30 units/hour = 6.67 hours
   - Days of work needed: 6.67 hours / 8 hours/day = 0.83 days (less than 1 day)
   - Estimated completion: June 29 at 17:00
   - Status: ON TRACK (barely) ⚠
6. For Order #2052 (Delivery deadline: June 30):
   - Current completion: 10% (20/200 units)
   - Days remaining: 2 days
   - Remaining units: 180 units
   - Current productivity: 25 units/hour (hampered by equipment downtime)
   - Time needed: 180 units / 25 units/hour = 7.2 hours
   - Days of work needed: 7.2 hours / 8 hours/day = 0.9 days
   - BUT: Only 2 workers assigned (should be 3)
   - Estimated completion: July 1 at 14:00
   - Status: MISSED DEADLINE ✗ (1 day late)
7. Alert engine generates CRITICAL alert for Order #2052:
   - Alert Type: OPERATIONAL
   - Severity: CRITICAL
   - Message: "Order #2052 at risk of missing deadline (June 30). Estimated completion: July 1. Recommended actions: (1) Add 1 more worker, (2) Reduce other assignments, (3) Check material availability"
   - RelatedEntityId: Order #2052
   - CreatedAt: June 28, 16:00
   - AssignedTo: Factory Manager (Factory A)
8. System stores alert in database with status: "New"
9. System sends real-time notification to Factory Manager (browser pop-up, email, SMS)
10. Factory Manager sees alert and clicks to view details
11. System displays:
    - Order details: #2052, customer, product, deadline
    - Current progress: 10% complete, 180 units remaining
    - Bottleneck: 2 workers assigned, productivity rate 25 units/hour (below target of 35)
    - Recommended actions: (1) Assign additional worker, (2) Reduce material shortage issue
12. Manager checks current inmate availability and sees 3 available inmates
13. Manager reassigns 1 additional inmate to Order #2052
14. System updates task and recalculates:
    - New team: 3 workers
    - Estimated new productivity: 35 units/hour (target rate)
    - Time needed: 180 / 35 = 5.14 hours
    - Estimated completion: June 29 at 13:00
    - Status: ON TRACK ✓
15. System updates alert status to "In-Progress" and records manager's action: "Assigned additional worker"
16. System continues monitoring; next check at 20:00 will verify progress
17. If progress meets revised estimate, alert status automatically changes to "Resolved"
18. If progress still lags, system escalates alert to Employment Officer after 4 hours without improvement

### Alert Calculation Logic
- Estimated completion date = current_date + (remaining_units / (productivity_rate × workers)) / 8
- Risk threshold: if estimated_completion > deadline, set severity to CRITICAL
- Escalation: if alert unacknowledged for 4 hours, escalate to next level (Employment Officer)

---

## Summary of Key Interactions

The sequence diagrams illustrate:
1. **Order Processing**: Customer request → System assessment → Factory assignment
2. **Daily Operations**: Morning sync → Inmate availability → Task assignment → Attendance tracking → Production recording
3. **Inventory Management**: Stock monitoring → Reorder triggers → Supplier coordination
4. **Payroll**: Attendance aggregation → Productivity calculation → Wage computation → תמר submission
5. **Performance Management**: Metrics aggregation → Manager assessment → Parole board report
6. **Exception Management**: Alert detection → Escalation → Recommended actions → Resolution

These flows demonstrate how the PEDIS system integrates multiple functions to provide comprehensive employment management and support informed decision-making for prison operations.
