namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the response for fetching all Cancellation information.
    public class AllCancellationsResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<CancellationRequestDTO> Cancellations { get; set; }
    }

    //This class holds the cancellation request details
    public class CancellationRequestDTO
    {
        public int CancellationRequestID { get; set; }
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public string CancellationType { get; set; }
        public DateTime RequestedOn { get; set; }
        public string Status { get; set; }
    }
}
