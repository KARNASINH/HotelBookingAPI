-- This script creates the Guests table in the HotelBooking database.

CREATE TABLE Guests 
(

GuestID INT PRIMARY KEY IDENTITY(1,1),
UserID INT,
FirstName NVARCHAR(50),
LastName NVARCHAR(50),
Email NVARCHAR(100),
Phone NVARCHAR(15),
AgeGroup NVARCHAR(20) CHECK (AgeGroup IN ('Adult', 'Child', 'Infant')),
Address NVARCHAR(255),
CountryID INT,
StateID INT,
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME,

-- FOREIGN KEY CONSTRAINT
FOREIGN KEY (UserID) REFERENCES Users(UserID),
FOREIGN KEY (CountryID) REFERENCES Countries(CountryID),
FOREIGN KEY (StateID) REFERENCES States(StateID)

);

GO