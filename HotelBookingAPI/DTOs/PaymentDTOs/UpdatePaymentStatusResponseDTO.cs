namespace HotelBookingAPI.DTOs.PaymentDTOs
{
    //This class hold the properties required to hold the reponse while updating the payment status
    public class UpdatePaymentStatusResponseDTO
    {
        public bool Status { get; set; }

        //Message to be displayed after updating the payment status
        public string Message { get; set; }
    }
}
