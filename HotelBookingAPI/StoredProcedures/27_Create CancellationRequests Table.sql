-- This script creates the CancellationRequests table in the HotelBooking database

CREATE TABLE CancellationRequests 
(
CancellationRequestID INT PRIMARY KEY IDENTITY(1,1),
ReservationID INT,
UserID INT,
CancellationType NVARCHAR(50),
RequestedOn DATETIME DEFAULT GETDATE(),
Status NVARCHAR(50),
AdminReviewedByID INT,
ReviewDate DATETIME,
CancellationReason NVARCHAR(255),

-- Foreign keys for the ReservationID, UserID, and AdminReviewedByID columns
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
FOREIGN KEY (UserID) REFERENCES Users(UserID),
FOREIGN KEY (AdminReviewedByID) REFERENCES Users(UserID)
);

GO