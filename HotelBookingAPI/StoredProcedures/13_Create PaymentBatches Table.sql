 --THis script creates the PaymentBatches table

CREATE TABLE PaymentBatches 
(
PaymentBatchID INT PRIMARY KEY IDENTITY(1,1),
UserID INT,
PaymentDate DATETIME,
TotalAmount DECIMAL(10,2),
PaymentMethod NVARCHAR(50),

-- Foreign key to Users table
FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO