-- This script creates the Refunds table in the HotelBooking database

CREATE TABLE Refunds 
(

RefundID INT PRIMARY KEY IDENTITY(1,1),
PaymentID INT,
RefundAmount DECIMAL(10,2),
RefundDate DATETIME DEFAULT GETDATE(),
RefundReason NVARCHAR(255),
RefundMethodID INT,
ProcessedByUserID INT,
RefundStatus NVARCHAR(50),

-- Foreign keys pointing to other tables.
FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
FOREIGN KEY (RefundMethodID) REFERENCES RefundMethods(MethodID),
FOREIGN KEY (ProcessedByUserID) REFERENCES Users(UserID)

);

GO