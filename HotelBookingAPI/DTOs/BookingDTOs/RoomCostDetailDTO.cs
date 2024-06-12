namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //Class to hold the cost details of each room
    public class RoomCostDetailDTO
    {
        //Room ID
        public int RoomID { get; set; }

        //Room Number
        public string RoomNumber { get; set; }

        //Price of the room for a single night
        public decimal RoomPrice { get; set; }

        //Number of nights the room is booked for
        public int NumberOfNights { get; set; }

        //Total price of the room based on the number of nights it is booked for
        public decimal TotalPrice { get; set; }
    }
}
