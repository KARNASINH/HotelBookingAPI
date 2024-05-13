using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    // Data Transfer Object for creating a new RoomType
    public class CreateRoomTypeDTO
    {
        //Hold the name of the room type
        [Required]
        public string TypeName { get; set; }

        //Hold the Accessibility features of the room type
        [Required]
        public string AccessibilityFeatures { get; set; }

        //Hold the description of the room type
        [Required]
        public string Description { get; set; }

        //Hold the Active status of the room type
        [Required]
        public bool IsActive { get; set; }
    }
}
