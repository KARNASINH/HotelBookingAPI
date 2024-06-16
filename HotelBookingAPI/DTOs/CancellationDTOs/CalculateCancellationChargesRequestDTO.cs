using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.CancellationDTOs
{
    //This class is used to hold the request for calculating the cancellation charges
    public class CalculateCancellationChargesRequestDTO
    {
        [Required(ErrorMessage = "ReservationID is required.")]
        public int ReservationID { get; set; }
        
        [Required(ErrorMessage = "RoomsCancelled list cannot be empty.")]
        [MinLength(1, ErrorMessage = "At least one room must be cancelled.")]
        public List<int> RoomsCancelled { get; set; }
    }
}
