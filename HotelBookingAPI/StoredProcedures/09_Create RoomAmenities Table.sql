-- This script creates the RoomAmenities table in the HotelBooking database

CREATE TABLE RoomAmenities 
(

RoomTypeID INT,
AmenityID INT,

-- Foreign Key Constraints
FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID),
FOREIGN KEY (AmenityID) REFERENCES Amenities(AmenityID),

-- Primary Key Constraint
PRIMARY KEY (RoomTypeID, AmenityID) -- Composite Primary Key to avoid duplicates

);
GO