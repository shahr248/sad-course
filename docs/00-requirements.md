# System Requirements Specification

## Overview
This document defines the functional and non-functional requirements for the Prison Employment Department Information System (PEDIS) designed for בית סוהר "אלה" (Prison Elah) in Be'er Sheva, Israel. The system aims to integrate employment management, payroll administration, inmate tracking, and production scheduling currently managed through disparate manual processes and legacy systems.

---

# Functional Requirements

## R1: Inmate Registration and Profile Management

### Behavioral
The system shall maintain a persistent database of inmate profiles containing biographical data, employment status, security clearance, training certifications, and work history. Authorized users shall be able to view and update inmate profiles without manual re-entry each month. The system shall enforce role-based access control to ensure that only appropriate personnel can modify sensitive fields.

### Implementation Notes
- Centralized database with normalized schema for inmate master data
- Integration with existing צוהר system to pull biographical and security status in real-time
- Fields to track: inmate ID (number), name, Hebrew name transliteration, assigned factory, employment status (active/inactive/on-hold), security clearance level, skills/certifications, date of hire, expected release date
- Audit logging for all profile modifications
- Read-only access for managers, update access only for employment officers

---

## R2: Order Intake and Tracking

### Behavioral
When a customer (internal or external) submits an order, the system shall accept and record order details through a structured form including: customer name, product description, quantity, delivery deadline, technical specifications, and contact information. The system shall prevent submission of orders with missing critical fields and maintain a complete order history accessible by relevant personnel.

### Implementation Notes
- Web-based order form with mandatory field validation
- Automatic timestamp of order submission
- Order number auto-generation
- Support for file attachments (drawings, specifications)
- Orders stored in relational database with full audit trail
- Search and filter capabilities by customer, date range, status, deadline
- Historical view of all customer orders with ability to identify patterns

---

## R3: Production Capacity Assessment and Job Allocation

### Behavioral
Before assigning an order to a factory, the system shall display current workload status, estimated completion dates for pending orders, and available inmate workforce capacity. The system shall recommend the most suitable factory based on current availability and inmate skillset matching. Job assignments shall be recorded with assignment timestamp, assigned inmate(s), deadline, and expected completion date.

### Implementation Notes
- Real-time dashboard showing current workload per factory
- Algorithm to calculate factory capacity based on: number of available inmates, average productivity rates, current pending orders
- Matching engine between order requirements and inmate skills
- Job assignment form with automatic validation against security status and availability
- Capacity planning tool that projects workload 2-4 weeks forward

---

## R4: Inmate Assignment and Daily Scheduling

### Behavioral
The system shall automatically retrieve inmate availability status each morning by querying צוהר for: security holds, planned absences (court visits, isolation, medical), and changes in security clearance. The system shall present the employment officer with a list of cleared, available inmates and allow rapid job assignment. The system shall prevent assignment of inmates with active security holds or expired safety training certifications.

### Implementation Notes
- Scheduled integration job that syncs with צוהר every morning before work hours
- Displays real-time status: available, on hold, medical, court appearance, isolation
- Automatic blocking of inmates with expired training certificates (tracked in system)
- Visual dashboard with color-coding: available (green), blocked (red), at-risk (yellow)
- Batch assignment interface for assigning same task to multiple inmates
- Record of who assigned whom, when, to which task

---

## R5: Attendance Tracking and Time Recording

### Behavioral
The system shall record inmate attendance automatically through integration with existing facial recognition systems at facility entry/exit points, or through manual checklist if facial recognition is unavailable. The system shall track entry time, exit time, and total hours worked per shift. The system shall flag discrepancies between expected and actual attendance and alert supervisors to unexpected absences.

### Implementation Notes
- APIs to receive attendance events from facial recognition system (if available)
- Fallback to manual entry interface with photo capture for validation
- Recording of: inmate ID, factory, date, entry time, exit time, authorized duration
- Automatic alert if inmate departs before scheduled time or arrives significantly late
- Daily summary of absences with categorization: unexcused, medical, authorized
- Historical attendance record per inmate with weekly/monthly summaries

---

## R6: Production Output Recording and Productivity Metrics

### Behavioral
Workers shall record production output at point of work using tablet devices or paper forms. The system shall accept output data (units produced, waste, rework) and automatically calculate productivity metrics per inmate per shift. The system shall flag inmates whose output falls below 70% of their target productivity and alert supervisors. The system shall maintain historical productivity trends to enable performance evaluation.

### Implementation Notes
- Mobile data entry interface (tablet-based) for shop floor with offline capability
- Data schema for: inmate ID, product type, units completed, defect rate, start time, end time
- Automatic calculation of: units/hour, variance from standard, daily/weekly trends
- Performance alerts: red flag if output < 70% of expected, yellow if 70-85%
- Historical database allowing analysis of: productivity trends, learning curves, anomalies
- Export functionality for performance reports

---

## R7: Raw Material and Inventory Management

### Behavioral
The system shall maintain a perpetual inventory of raw materials and work-in-progress items per factory. The system shall track minimum stock levels and automatically alert supervisors when stock falls below threshold. The system shall record material receipts, usage, and identify stock-outs before they impact production. The system shall support manual inventory adjustments with justification and audit trail.

### Implementation Notes
- Inventory master data: SKU, description, current quantity, minimum threshold, reorder point
- Tracking of receipts with date, supplier, quantity, quality check status
- Usage recording per job/order with date and quantity consumed
- Automatic notification (email/SMS) when quantity ≤ threshold
- Predictive alert: "Stock will be depleted in X days based on current usage rate"
- Inventory variance reports highlighting unexplained discrepancies
- Barcode scanning capability for receipt and issue transactions

---

## R8: Delivery Schedule and Customer Communication

### Behavioral
The system shall maintain and display expected delivery dates for all active orders. The system shall provide visual representations (dashboard, calendar view) of upcoming delivery commitments grouped by urgency and date. The system shall automatically notify relevant personnel if an order is at risk of missing its deadline and recommend corrective actions. The system shall maintain a log of all communications with customers (emails, phone calls, delivery confirmations).

### Implementation Notes
- Order timeline view with color-coded urgency (green = on-track, yellow = at-risk, red = critical)
- Predictive analysis: if current progress and productivity remain constant, will deadline be met?
- Alert trigger: if risk of miss detected, notify employment officer and factory manager automatically
- Communication log linked to each order: timestamp, party contacted, method, message summary
- Integration with Outlook for automatic email logging
- Confirmation of delivery with customer signature/proof and system timestamp

---

## R9: Safety Training Certification Tracking

### Behavioral
The system shall maintain a record of each inmate's safety training certifications with: training type, certification date, expiration date, and trainer identity. The system shall automatically flag inmates whose certifications are expiring within 30 days and prevent assignment to relevant work without retraining. The system shall generate a monthly report of expiring certifications and recommended retraining schedule.

### Implementation Notes
- Training master data: training type, duration, required frequency, current attendees
- Certification record: inmate ID, training type, certification date, expiry date, certificate number
- Automatic alert when certification ≤ 30 days from expiry
- Assignment blocking: prevent job assignment if required certification missing or expired
- Training schedule generator based on expiry dates and production calendar
- Audit compliance report for regulatory inspections

---

## R10: Payroll Calculation and Wage Processing

### Behavioral
The system shall automatically calculate wages based on: recorded hours worked, productivity output, applicable wage rates, and deductions. The system shall populate all necessary fields in the existing תמר payroll system without manual re-entry. The system shall perform wage calculations only once monthly (end of month) and provide audit trails showing calculation components (base hours, bonus, deductions). The system shall flag wage anomalies (unusually high/low wages) for review before payment.

### Implementation Notes
- Wage calculation engine with configurable formulas per inmate category
- Input data sources: attendance records, productivity metrics, approved deductions
- Wage components: base pay (hourly × hours), productivity bonus (units × rate), family support, deductions
- Interface to תמר: automated data export in required format (XML/CSV)
- Calculation audit trail: wage statement showing all components per inmate
- Anomaly detection: flag wages outside normal range (e.g., > 200% of monthly average)
- Approval workflow: employment officer reviews and approves before תמר submission

---

## R11: Performance Evaluation and Recommendation to Parole Board

### Behavioral
The system shall support structured evaluation of inmate work performance on a periodic basis (e.g., quarterly or upon parole review request). The system shall collect quantitative metrics (productivity, attendance, safety incidents) and qualitative assessments (work attitude, behavior, skill improvement) from factory managers. The system shall generate a comprehensive performance report suitable for submission to the parole board (וועדת שליש).

### Implementation Notes
- Evaluation form with both quantitative fields (pulled automatically) and qualitative narrative fields
- Quantitative data auto-populated: average daily productivity, attendance %, days absent, safety incidents
- Qualitative fields: manager assessment of work quality, collaboration, discipline, rehabilitation progress
- Version control for evaluations (no retroactive changes to submitted reports)
- Report generation in formats suitable for parole board (PDF, HTML)
- Archival of all submitted evaluations with timestamps

---

## R12: Integration with צוהר Security System

### Behavioral
The system shall integrate with the existing צוהר database to automatically retrieve: inmate security status, current holds/restrictions, planned absences (court visits, medical, isolation), and changes in classification. The system shall refresh this data at least once daily, preferably multiple times per day. The system shall immediately block job assignment if a newly retrieved hold is identified and notify relevant personnel.

### Implementation Notes
- API or batch data pull from צוהר (interface to be defined with שב"ס IT)
- Scheduled sync jobs: minimum daily (morning), preferably 3-4 times daily
- Fields to sync: inmate ID, current block status, security level, planned absence dates/reasons
- Comparison logic: identify new holds/changes and trigger notifications
- Fallback mechanism if צוהר integration is temporarily unavailable
- Error logging and retry logic for failed integration attempts
- Real-time query capability for urgent verification (not relying on cached data)

---

## R13: Dashboard and Business Intelligence

### Behavioral
The system shall provide role-based dashboards displaying key metrics relevant to each user type:
- Employment Officer: total inmate count, factory utilization, order status summary, risk items (missed deadlines, low productivity)
- Factory Manager: current workforce, pending orders, productivity trends, material status, safety alerts
- Management: profitability per factory/order, cost per unit, revenue trends, workforce efficiency ratios

The system shall support drill-down capability to view detailed data underlying summary metrics.

### Implementation Notes
- Separate dashboard layouts for each role (employment officer, factory manager, management)
- Metrics calculated in real-time or near-real-time (< 5 minute latency acceptable)
- Charts and visualizations: bar charts (productivity), pie charts (order distribution), line charts (trends)
- Filters by: date range, factory, customer, inmate, order status
- Export functionality (PDF, Excel) for reports
- Configurable dashboard: users can select metrics to display
- Performance KPIs tracked: average productivity rate, on-time delivery %, workforce utilization %, wage cost per unit produced

---

## R14: Multi-Facility Workforce Reallocation (Dynamic Assignment)

### Behavioral
When production bottlenecks or shortages occur, the employment officer shall be able to temporarily reassign inmates from one factory to another. The system shall validate security clearances and skills against the destination factory's requirements before confirming reassignment. The system shall record the reassignment with justification, duration, and timestamp. The system shall automatically restore assignments at the end of the specified period unless extended.

### Implementation Notes
- Reassignment form: source factory, destination factory, inmate list, reason, estimated duration
- Validation: security clearances valid for destination, no conflicting holds, skills match
- Record created in database: original assignment, reassignment date/time, estimated return date
- System-generated reminder when return date approaches
- Audit trail showing all reassignments and rationale
- Prevent assignment to conflicting tasks during reallocation

---

## R15: Material Procurement and Supplier Coordination

### Behavioral
The system shall support ordering of raw materials with the ability to: enter quantity needed, select supplier, request expedited delivery if needed, track order status with supplier. The system shall generate purchase requisitions automatically if inventory falls below minimum threshold. The system shall integrate with accounting/finance systems to track procurement costs.

### Implementation Notes
- Supplier master file: name, contact, materials supplied, typical lead time, pricing
- Purchase order form: supplier, material, quantity, requested delivery date, cost
- Material requisition auto-generation when: (current inventory - pending orders) < reorder point
- Status tracking: requisition approved, ordered, in transit, received, rejected
- Integration with צוהר/accounting for budget tracking
- Notification to employment officer when expected delivery date approaches
- Receipt confirmation with quality check option

---

## R16: Exception Handling and Alert Management

### Behavioral
The system shall identify and escalate operational exceptions (missed deadlines, productivity anomalies, absent workers, security holds, material shortages) through automated alerts. The system shall allow users to acknowledge alerts, add notes, and track resolution. The system shall maintain an exception log showing frequency and patterns of recurring issues.

### Implementation Notes
- Alert types: safety (security hold), operational (missed deadline), financial (low productivity), material (stock-out), quality (defect rate)
- Alert conditions: configurable thresholds and triggers
- Notification methods: email, SMS, in-system notification, visual dashboard highlighting
- Alert escalation: if not resolved within X hours, escalate to higher authority
- Exception log: timestamp, type, severity, assigned to, resolution status, resolution notes
- Trend analysis: identify repeat issues, patterns in specific factories or time periods

---

## R17: User Authentication and Role-Based Access Control

### Behavioral
The system shall authenticate all users with username/password credentials and support two-factor authentication if available. The system shall enforce role-based access control with the following minimum roles: Employment Officer (full access), Factory Manager (limited to their factory), Data Entry (limited to specific functions), Management (read-only dashboards), Accountant (payroll export access). The system shall enforce the principle of least privilege: users can only access and modify data relevant to their role.

### Implementation Notes
- User accounts managed in centralized directory (AD/LDAP integration preferred)
- Password policy: minimum 8 characters, complexity requirements, expiration every 90 days
- Role assignments: managed by employment officer or designated administrator
- Audit log of all login attempts, access denied events, and data modifications
- Session timeout: 15 minutes of inactivity, explicit logout available
- Two-factor authentication via SMS or authenticator app (optional enhancement)

---

## R18: Data Backup and Disaster Recovery

### Behavioral
The system shall maintain automatic daily backups with at least 30 days of backup retention. Recovery Time Objective (RTO) shall be 4 hours (system back online within 4 hours of failure). Recovery Point Objective (RPO) shall be 1 hour (maximum 1 hour of data loss). The system shall be tested for recoverability at least quarterly.

### Implementation Notes
- Backup frequency: daily full, hourly incremental
- Backup storage: geographically separate from primary data center
- Backup verification: automated test restores weekly
- Disaster recovery plan documented and tested quarterly
- Off-site encrypted backup storage (cloud or alternate facility)
- Recovery runbook available and regularly updated

---

## R19: System Performance and Scalability

### Behavioral
The system shall support concurrent access by up to 50 users without performance degradation. Response times for common operations shall not exceed 2 seconds under normal load (< 10 seconds for complex queries). The system shall scale to support up to 500 additional inmates without infrastructure changes.

### Implementation Notes
- Performance testing conducted before production deployment
- Database indexing strategy optimized for frequent queries
- Caching layer (Redis or similar) for frequently accessed data
- Load balancing across multiple application servers if needed
- Database connection pooling to manage concurrent requests
- Query optimization and slow query monitoring
- Scalability plan for future growth

---

## R20: System Integration and Data Exchange

### Behavioral
The system shall integrate with: צוהר (read security status), תמר (write payroll data), Outlook (email logging), and optionally facial recognition systems for attendance. The system shall provide APIs for future integrations with other שב"ס systems. Data exchange shall be secure, authenticated, and logged.

### Implementation Notes
- API definitions and documentation for external integrations
- Standard data formats for exchange (JSON/XML)
- Error handling and retry logic for failed integrations
- Secure authentication (OAuth 2.0 or API keys) for API access
- Audit log of all integration calls and data transfers
- Test environment for integration testing before production
- Version control for API contracts

---

# Non-Functional Requirements

## NFR1: Security
The system shall encrypt data in transit (HTTPS/TLS) and at rest (AES-256). All user actions shall be logged with audit trails. The system shall comply with Israeli data protection regulations (Privacy Law). Access controls shall prevent unauthorized viewing of inmate personal data or financial information.

## NFR2: Availability
The system shall achieve 99% uptime (8.8 hours downtime per month acceptable). Scheduled maintenance windows shall not exceed 4 hours per month, coordinated outside working hours.

## NFR3: Usability
The interface shall be intuitive and require minimal training for prison staff. The system shall support Hebrew as primary language. Response times for user-initiated actions shall not exceed 2 seconds under normal load.

## NFR4: Maintainability
The system shall be designed with modular architecture to allow updates and bug fixes without affecting other components. Code shall follow documented standards and be reviewable by prison IT staff.

## NFR5: Compliance
The system shall comply with שב"ס data governance policies, Israeli regulations regarding prisoner data handling, and international standards for correctional facility information systems.

---

# Traceability Matrix

| Requirement ID | Requirement Name | Type | Priority | Related Business Process |
|---|---|---|---|---|
| R1 | Inmate Registration and Profile Management | Functional | High | All processes |
| R2 | Order Intake and Tracking | Functional | High | Process 1: Order Intake |
| R3 | Production Capacity Assessment and Job Allocation | Functional | High | Process 1: Order Intake |
| R4 | Inmate Assignment and Daily Scheduling | Functional | High | Process 2: Inmate Assignment |
| R5 | Attendance Tracking and Time Recording | Functional | Critical | Process 3: Attendance & Payroll |
| R6 | Production Output Recording | Functional | High | Process 3: Attendance & Payroll |
| R7 | Raw Material and Inventory Management | Functional | High | Process 4: Material Management |
| R8 | Delivery Schedule and Customer Communication | Functional | High | Process 1: Order Intake |
| R9 | Safety Training Certification Tracking | Functional | Critical | Process 2: Inmate Assignment |
| R10 | Payroll Calculation and Wage Processing | Functional | Critical | Process 5: Wage Processing |
| R11 | Performance Evaluation and Parole Board Report | Functional | Medium | Process 7: Parole Board Report |
| R12 | Integration with צוהר Security System | Functional | Critical | Process 2: Inmate Assignment |
| R13 | Dashboard and Business Intelligence | Functional | Medium | All processes |
| R14 | Multi-Facility Workforce Reallocation | Functional | Medium | Process 2: Inmate Assignment |
| R15 | Material Procurement and Supplier Coordination | Functional | Medium | Process 4: Material Management |
| R16 | Exception Handling and Alert Management | Functional | High | All processes |
| R17 | User Authentication and Role-Based Access Control | Functional | Critical | All processes |
| R18 | Data Backup and Disaster Recovery | Functional | Critical | All processes |
| NFR1 | Security | Non-Functional | Critical | All processes |
| NFR2 | Availability | Non-Functional | High | All processes |
| NFR3 | Usability | Non-Functional | High | All processes |
| NFR4 | Maintainability | Non-Functional | Medium | All processes |
| NFR5 | Compliance | Non-Functional | Critical | All processes |

---

# Assumptions and Constraints

- The system shall operate on hardware provided by שב"ס IT (servers, workstations)
- Integration with צוהר is dependent on API availability from צוהר team
- Facial recognition system for attendance is optional; manual entry is fallback
- Hebrew language support is required; English translations provided for reference
- System shall not require changes to תמר (read-only integration preferred)
- Initial deployment for Prison Elah only; potential for replication to other facilities
