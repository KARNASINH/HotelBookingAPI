namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class is used to hold the infromation that is returned when a room type is searched for
    public class RoomTypeSearchDTO
    {
        //The ID of the room type
        public int RoomTypeID { get; set; }

        //The name of the room type
        public string TypeName { get; set; }

        //The Accessibility features of the room type
        public string AccessibilityFeatures { get; set; }

        //The description of the room type
        public string Description { get; set; }
    }
}
