namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to return the response of the cancellation request for refund
    public class CancellationForRefundResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<CancellationForRefundDTO> CancellationsToRefund { get; set; }
    }

    //This class is used to hold the details of the cancellation and refund
    public class CancellationForRefundDTO
    {
        public int CancellationRequestID { get; set; }
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public string CancellationType { get; set; }
        public DateTime RequestedOn { get; set; }
        public string Status { get; set; }
        public int RefundID { get; set; }
        public string RefundStatus { get; set; }
    }
}
