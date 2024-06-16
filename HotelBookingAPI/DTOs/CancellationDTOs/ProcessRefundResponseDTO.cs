namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the information of the refund process response
    public class ProcessRefundResponseDTO
    {
        public int RefundID { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
