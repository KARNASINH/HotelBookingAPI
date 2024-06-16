-- This script creates the Countries table

CREATE TABLE Countries
(

CountryID INT PRIMARY KEY IDENTITY(1,1),
CountryName NVARCHAR(50),
-- CountryCode is the ISO Code for the country
CountryCode NVARCHAR(10),
IsActive BIT DEFAULT 1

);

GO