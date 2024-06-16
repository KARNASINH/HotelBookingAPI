-- This script creates the Amenities table

CREATE TABLE Amenities 
(

AmenityID INT PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(100),
Description NVARCHAR(255),
IsActive BIT DEFAULT 1,
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME

);
GO