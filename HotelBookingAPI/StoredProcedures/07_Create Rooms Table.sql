-- This script creates the Rooms table in the HotelBooking database

CREATE TABLE Rooms 
(

RoomID INT PRIMARY KEY IDENTITY(1,1),
RoomNumber NVARCHAR(10) UNIQUE,
RoomTypeID INT,
Price DECIMAL(10,2),
BedType NVARCHAR(50),
ViewType NVARCHAR(50),
Status NVARCHAR(50) CHECK (Status IN ('Available', 'Under Maintenance', 'Occupied')),
IsActive BIT DEFAULT 1,
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME,

-- FOREIGN KEY CONSTRAINT
FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID)

);

GO