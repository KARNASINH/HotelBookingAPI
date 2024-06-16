-- This script creates the Payments table in the database

CREATE TABLE Payments 
(

PaymentID INT PRIMARY KEY IDENTITY(1,1),
ReservationID INT,
Amount DECIMAL(10,2),
PaymentBatchID INT,

-- Foreign keys pointing to othe tables. 
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
FOREIGN KEY (PaymentBatchID) REFERENCES PaymentBatches(PaymentBatchID)

);

GO