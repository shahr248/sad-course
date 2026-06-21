-- ============================================================================
-- Migration: Make Prisoner.department required (NOT NULL)
--
-- department represents the inmate's home wing/department within the prison
-- (independent of the employment program's factory/EmploymentDepartment).
-- It must always be known at intake, so it can no longer be left empty.
--
-- Any existing rows with a NULL department (added before this field was
-- required in the UI) are backfilled with department 1 as a placeholder;
-- update them to the correct wing via the Prisoner edit screen.
--
-- Safe to re-run.
-- ============================================================================

USE sadcourse3;
GO

UPDATE Prisoner SET department = 1 WHERE department IS NULL;
GO

ALTER TABLE Prisoner ALTER COLUMN department INT NOT NULL;
GO
