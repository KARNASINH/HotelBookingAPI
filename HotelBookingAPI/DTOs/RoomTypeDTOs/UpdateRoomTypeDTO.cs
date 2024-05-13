using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    //This class is used to hold the information to perform RoomType Update operation
    public class UpdateRoomTypeDTO
    {
        //This property is used to hold the RoomTypeID
        [Required]
        public int RoomTypeID { get; set; }

        //This property is used to hold the RoomTypeName
        [Required]
        public string TypeName { get; set; }

        //This property is used to hold the RoomType AccessibilityFeatures
        [Required]
        public string AccessibilityFeatures { get; set; }

        //This property is used to hold the RoomType Description
        [Required]
        public string Description { get; set; }

        //This property is used to hold the RoomType Active status
        [Required]
        public bool IsActive { get; set; }
    }
}
