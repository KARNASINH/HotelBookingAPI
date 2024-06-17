namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the information of the response while updating the refund status.
    public class UpdateRefundStatusResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
