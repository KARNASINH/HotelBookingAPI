-- This script creates the RoomTypes table

CREATE TABLE RoomTypes 
(

RoomTypeID INT PRIMARY KEY IDENTITY(1,1),
TypeName NVARCHAR(50),
AccessibilityFeatures NVARCHAR(255),
Description NVARCHAR(255),
IsActive BIT DEFAULT 1,
CreatedBy NVARCHAR(100),
CreatedDate DATETIME DEFAULT GETDATE(),
ModifiedBy NVARCHAR(100),
ModifiedDate DATETIME

);

GO