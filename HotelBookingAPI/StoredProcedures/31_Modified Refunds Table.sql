-- First, if the Refunds table already exists, delete it.
IF OBJECT_ID('Refunds', 'U') IS NOT NULL
DROP TABLE Refunds;
GO

-- Create the Refunds table
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
CancellationCharge DECIMAL(10,2) DEFAULT 0,
NetRefundAmount DECIMAL(10,2),
CancellationRequestID INT,

FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
FOREIGN KEY (RefundMethodID) REFERENCES RefundMethods(MethodID),
FOREIGN KEY (ProcessedByUserID) REFERENCES Users(UserID),
FOREIGN KEY (CancellationRequestID) REFERENCES CancellationRequests(CancellationRequestID)
);

GO