using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomAmenityDTOs
{
    //This class is used to hold the information of the RoomType.
    public class RoomTypeResponse
    {
        [Required]
        public int RoomTypeID { get; set; }
        [Required]
        public List<int> AmenityIDs { get; set; }
    }
}
