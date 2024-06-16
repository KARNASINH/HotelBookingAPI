using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the information of the request to update the refund status.
    public class UpdateRefundStatusRequestDTO
    {
        [Required(ErrorMessage = "RefundID is required.")]
        public int RefundID { get; set; }
        
        [Required(ErrorMessage = "NewRefundStatus is required.")]
        [StringLength(50, ErrorMessage = "NewRefundStatus length cannot exceed 50 characters.")]
        public string NewRefundStatus { get; set; }
    }
}
