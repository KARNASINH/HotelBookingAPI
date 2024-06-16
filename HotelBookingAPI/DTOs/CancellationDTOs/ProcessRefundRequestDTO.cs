using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the information required to process a refund
    public class ProcessRefundRequestDTO
    {
        [Required(ErrorMessage = "CancellationRequestID is required.")]
        public int CancellationRequestID { get; set; }
        
        [Required(ErrorMessage = "ProcessedByUserID is required.")]
        public int ProcessedByUserID { get; set; }
        
        [Required(ErrorMessage = "RefundMethodID is required.")]
        public int RefundMethodID { get; set; }
    }
}
