-- This script is used to drop and create the constraint.

-- Check if the constraint exists
SELECT
CONSTRAINT_NAME
FROM
INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE
TABLE_NAME = 'Reservations' AND
CONSTRAINT_TYPE = 'CHECK';

-- Drop the constraint
ALTER TABLE Reservations
DROP CONSTRAINT CK__Reservati__Statu__60A75C0F;

-- Create the constraint
ALTER TABLE Reservations
ADD CONSTRAINT CK_Reservations_Status CHECK (Status IN ('Reserved', 'Checked-in', 'Checked-out', 'Cancelled', 'Partially Cancelled'));