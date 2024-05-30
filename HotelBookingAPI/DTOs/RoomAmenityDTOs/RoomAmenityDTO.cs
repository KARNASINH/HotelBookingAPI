using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomAmenityDTOs
{
    //This class is used to hold the RoomAmenity.
    public class RoomAmenityDTO
    {
        [Required]
        public int RoomTypeID { get; set; }
        [Required]
        public int AmenityID { get; set; }
    }
}
