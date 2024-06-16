-- This script creates the CancellationCharges table in the HotelBooking database

CREATE TABLE CancellationCharges
(
CancellationRequestID INT PRIMARY KEY,
TotalCost DECIMAL(10,2),
CancellationCharge DECIMAL(10,2),
CancellationPercentage DECIMAL(10,2),
MinimumCharge DECIMAL(10,2),
PolicyDescription NVARCHAR(255),

-- Foreign keys
FOREIGN KEY (CancellationRequestID) REFERENCES CancellationRequests(CancellationRequestID)
);

GO