-- This script creates the RefundMethods table in the HotelBooking database.

CREATE TABLE RefundMethods 
(

MethodID INT PRIMARY KEY IDENTITY(1,1),
MethodName NVARCHAR(50),
IsActive BIT DEFAULT 1,

);

GO