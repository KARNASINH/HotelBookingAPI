-- This script creates the Reservations table in the HotelBooking database.

CREATE TABLE Reservations 
(

ReservationID INT PRIMARY KEY IDENTITY(1,1),
UserID INT,
RoomID INT,
BookingDate DATE,
CheckInDate DATE,
CheckOutDate DATE,
NumberOfGuests INT,
Status NVARCHAR(50) CHECK (Status IN ('Reserved', 'Checked-in', 'Checked-out', 'Cancelled')),
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME,

-- FOREIGN KEY CONSTRAINT
FOREIGN KEY (UserID) REFERENCES Users(UserID),
FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),

-- CHECK CONSTRAINT
CONSTRAINT CHK_CheckOutDate CHECK (CheckOutDate > CheckInDate)

);

GO