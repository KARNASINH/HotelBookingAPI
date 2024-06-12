using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.PaymentDTOs
{
    //This class hold the properties required to update the payment status
    public class UpdatePaymentStatusDTO
    {
        [Required]
        public int PaymentID { get; set; }

        //Status of the payment. It can be 'Completed' or 'Failed'
        [Required]
        [RegularExpression("(Completed|Failed)", ErrorMessage = "Status must be one of these : Completed or Failed.")]
        public string NewStatus { get; set; } // 'Completed' or 'Failed'

        //Reason for failure if the payment status is 'Failed'
        public string FailureReason { get; set; }
    }
}
