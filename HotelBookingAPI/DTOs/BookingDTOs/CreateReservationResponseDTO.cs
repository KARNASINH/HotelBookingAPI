namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //This class is used to hold the reponse while creating a reservation
    public class CreateReservationResponseDTO
    {
        //Reservation ID
        public int ReservationID { get; set; }

        //Status of the response
        public bool Status { get; set; }

        //Message of the response
        public string Message { get; set; }
    }
}
