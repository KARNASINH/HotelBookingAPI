namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the response for creating a cancellation request
    public class CreateCancellationResponseDTO
    {
        public int CancellationId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
