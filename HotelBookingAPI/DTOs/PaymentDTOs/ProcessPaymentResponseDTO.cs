namespace HotelBookingAPI.DTOs.PaymentDTOs
{
    //THis class is used to hold the response wheather the payment is successful or not
    public class ProcessPaymentResponseDTO
    {
        public int PaymentID { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
