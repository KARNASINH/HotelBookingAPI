using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomAmenityDTOs
{
    //This class is used to hold the information of the RoomType.
    public class RoomTypeResponse
    {
        //This property is used to store the RoomTypeID.
        public int RoomTypeID { get; set; }

        //This property is used to store the RoomTypeName.
        public string TypeName { get; set; }

        //This property is used to store the RoomTypeDescription.
        public string Description { get; set; }

        //This property is used to store the RoomTypeImage.
        public string AccessibilityFeatures { get; set; }

        //This property is used to store flag to check if the RoomType is active or not.
        public bool IsActive { get; set; }
    }
}
