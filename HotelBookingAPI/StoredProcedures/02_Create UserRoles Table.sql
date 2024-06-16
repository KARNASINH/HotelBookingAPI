-- Use HotelDB Database
USE HotelDB;
GO

-- This script creates the UserRoles table
CREATE TABLE UserRoles 
(

RoleID INT PRIMARY KEY IDENTITY(1,1),
RoleName NVARCHAR(50),
IsActive BIT DEFAULT 1,
Description NVARCHAR(255)

);

GO