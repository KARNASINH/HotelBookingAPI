namespace HotelBookingAPI.DTOs.RoomAmenityDTOs
{
    //This class is used to hold the information about the amenity.
    public class AmenityResponseDTO
    {
        //The ID of the amenity.
        public int AmenityID { get; set; }

        //The name of the amenity.
        public string Name { get; set; }

        //The description of the amenity.
        public string Description { get; set; }

        //The price of the amenity.
        public bool IsActive { get; set; }
    }
}