using HotelBookingAPI.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class holds the data which are required to search the Hotel based on the CheckIn and CheckOut dates.
    public class AvailabilityHotelSearchRequestDTO
    {
        //Checkin date for the hotel search
        [Required]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "Check-in date must be in the future.")]
        public DateTime CheckInDate { get; set; }
     

        [Required]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "Check-out date must be in the future.")]
        [DateGreaterThanValidation("CheckInDate", ErrorMessage = "Check-out date must be after check-in date.")]
        public DateTime CheckOutDate { get; set; }
    }
}
