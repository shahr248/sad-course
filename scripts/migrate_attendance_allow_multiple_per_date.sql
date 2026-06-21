-- ============================================================================
-- Migration: Allow multiple AttendanceRecord rows per prisoner/date/factory
--
-- Previously, AttendanceRecord had a UNIQUE constraint on
-- (prisoner_id, attendance_date, factory), blocking a second clock-in/out
-- entry on the same day even when the hours didn't overlap (e.g. a morning
-- shift and a separate afternoon shift). Overlap is now checked in the
-- application layer (AddEditAttendanceDialog.ValidateInput) instead.
--
-- Safe to re-run.
-- ============================================================================

USE sadcourse3;
GO

IF EXISTS (
    SELECT 1 FROM sys.key_constraints
    WHERE name = 'UQ_AttendanceRecord_PrisonerDateFactory'
      AND parent_object_id = OBJECT_ID('AttendanceRecord')
)
BEGIN
    ALTER TABLE AttendanceRecord DROP CONSTRAINT UQ_AttendanceRecord_PrisonerDateFactory;
END;
GO
