-- This script creates the Cancellations table in the HotelBooking database.

CREATE TABLE Cancellations 
(

CancellationID INT PRIMARY KEY IDENTITY(1,1),
ReservationID INT,
CancellationDate DATETIME,
Reason NVARCHAR(255),
CancellationFee DECIMAL(10,2),
CancellationStatus NVARCHAR(50) CHECK (CancellationStatus IN ('Pending', 'Approved', 'Denied')),
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME,

-- Foreign keys pointing to other tables.
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)

);

GO