-- This script creates the ReservationGuests table

CREATE TABLE ReservationGuests 
(
ReservationGuestID INT PRIMARY KEY IDENTITY(1,1),
ReservationID INT,
GuestID INT,

-- Foreign keys to Reservations and Guests tables
FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
);

GO