-- This script creates the Feedbacks table in the HotelBooking database.

CREATE TABLE Feedbacks 
(
FeedbackID INT PRIMARY KEY IDENTITY(1,1),
ReservationID INT,
GuestID INT,
-- Rating is a number between 1 and 5.
Rating INT CHECK (Rating BETWEEN 1 AND 5),
-- Comment is optional and can be up to 1000 characters long.
Comment NVARCHAR(1000),
FeedbackDate DATETIME,

-- Foreign keys pointing to other tables.
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)

);

GO