-- This script creates the CancellationDetails table.

CREATE TABLE CancellationDetails 
(

CancellationDetailID INT PRIMARY KEY IDENTITY(1,1),
CancellationRequestID INT,
ReservationRoomID INT,

-- Foreign keys
FOREIGN KEY (CancellationRequestID) REFERENCES CancellationRequests(CancellationRequestID),
FOREIGN KEY (ReservationRoomID) REFERENCES ReservationRooms(ReservationRoomID)

);
GO