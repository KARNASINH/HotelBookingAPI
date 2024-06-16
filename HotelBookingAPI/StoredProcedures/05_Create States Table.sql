-- This script will create the States table

CREATE TABLE States 
(

StateID INT PRIMARY KEY IDENTITY(1,1),
StateName NVARCHAR(50),
CountryID INT,
IsActive BIT DEFAULT 1,

-- FOREIGN KEY CONSTRAINT
FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)

);

GO