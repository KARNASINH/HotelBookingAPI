namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class is used to return the data for Room, RoomType and Amenities.
    public class RoomDetailsWithAmenitiesSearchDTO
    {
        //Room details along with RoomType details.
        public RoomSearchDTO Room { get; set; }

        //List of Amenities for the Room.
        public List<AmenitySearchDTO> Amenities { get; set; }
    }
}
