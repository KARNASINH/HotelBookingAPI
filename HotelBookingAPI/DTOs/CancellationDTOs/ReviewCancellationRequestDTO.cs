using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the request for reviewing a cancellation request.
    public class ReviewCancellationRequestDTO
    {
        [Required(ErrorMessage = "CancellationRequestID is required.")]
        public int CancellationRequestID { get; set; }
        
        [Required(ErrorMessage = "AdminUserID is required.")]
        public int AdminUserID { get; set; }
        
        [Required(ErrorMessage = "ApprovalStatus is required.")]
        [StringLength(50, ErrorMessage = "ApprovalStatus length cannot exceed 50 characters.")]
        public string ApprovalStatus { get; set; }
    }
}
