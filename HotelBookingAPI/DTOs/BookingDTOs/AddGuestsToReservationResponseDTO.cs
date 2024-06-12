namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //This class is used to return the response of adding guests to a reservation
    public class AddGuestsToReservationResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
