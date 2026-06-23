-- ============================================================================
-- Data fix: backfill packaging_instructions on pre-existing WorkOrder rows
--
-- migrate_workorder_add_product_packaging.sql backfilled WorkOrder.product_id
-- from the parent ProductionOrder but left packaging_instructions NULL for
-- every row that existed before the multi-line Add Order redesign (the 12
-- seeded WorkOrders). Set them to the same text as the matching Product's
-- own packaging_instructions, mirroring the fix applied to seed_data.sql for
-- fresh installs.
--
-- Guarded by packaging_instructions IS NULL, so it only touches rows that
-- were never given a real value and never overwrites anything a user has
-- since entered through the UI. Safe to re-run.
-- ============================================================================

USE sadcourse3;
GO

UPDATE WorkOrder SET packaging_instructions = 'Pack in 500g bags, seal with tamper-proof label' WHERE work_order_id = 1 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in cartons of 100 units' WHERE work_order_id = 2 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Roll and wrap in protective plastic' WHERE work_order_id = 3 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in 250g containers' WHERE work_order_id = 4 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack individually in cardboard boxes' WHERE work_order_id = 5 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in 500g bags, seal with tamper-proof label' WHERE work_order_id = 6 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in 250g containers' WHERE work_order_id = 7 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in cartons of 100 units' WHERE work_order_id = 8 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in 500g bags, seal with tamper-proof label' WHERE work_order_id = 9 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in cartons of 100 units' WHERE work_order_id = 10 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Roll and wrap in protective plastic' WHERE work_order_id = 11 AND packaging_instructions IS NULL;
UPDATE WorkOrder SET packaging_instructions = 'Pack in 500g bags, seal with tamper-proof label' WHERE work_order_id = 12 AND packaging_instructions IS NULL;
GO
