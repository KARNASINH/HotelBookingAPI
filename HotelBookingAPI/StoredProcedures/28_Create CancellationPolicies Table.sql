-- This script creates the CancellationPolicies table.

CREATE TABLE CancellationPolicies 
(
PolicyID INT PRIMARY KEY IDENTITY(1,1),
Description NVARCHAR(255),
CancellationChargePercentage DECIMAL(5,2),
MinimumCharge DECIMAL(10,2),
EffectiveFromDate DATETIME,
EffectiveToDate DATETIME
);
GO