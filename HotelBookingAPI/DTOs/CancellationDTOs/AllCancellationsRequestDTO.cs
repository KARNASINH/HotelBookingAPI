using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the properties required for fetching all Cancellation information based on the provided optional parameters.
    public class AllCancellationsRequestDTO
    {
        [StringLength(50, ErrorMessage = "Status length cannot exceed 50 characters.")]
        public string? Status { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }
    }
}
