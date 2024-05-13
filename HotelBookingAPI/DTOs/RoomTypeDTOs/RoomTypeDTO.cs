namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    // This class is used to represent RoomType's information.
    public class RoomTypeDTO
    {
        //The RoomType's ID.
        public int RoomTypeID { get; set; }

        //The RoomType's name.
        public string TypeName { get; set; }
        
        //Accessibility features available to this RoomType.
        public string AccessibilityFeatures { get; set; }

        //The RoomType's description.
        public string Description { get; set; }

        //The RoomType is acrive or not.
        public bool IsActive { get; set; }
    }
}
