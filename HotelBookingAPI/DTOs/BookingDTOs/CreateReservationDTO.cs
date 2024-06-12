using HotelBookingAPI.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //This class is used to hold the data that are required to create a reservation.
    public class CreateReservationDTO
    {
        //User Id who is creating the reservation
        [Required]
        public int UserID { get; set; }
        
        //List of Room IDs for the reservation
        [Required]
        [MinLength(1, ErrorMessage = "At least one room ID must be provided.")]
        public List<int> RoomIDs { get; set; }
        
        //Check-in date for the reservation
        [Required]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "Check-in date must be in the future.")]
        public DateTime CheckInDate { get; set; }
        
        //Check-out date for the reservation
        [Required]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "Check-out date must be in the future.")]
        [DateGreaterThanValidation("CheckInDate", ErrorMessage = "Check-out date must be after check-in date.")]
        public DateTime CheckOutDate { get; set; }
    }
}
