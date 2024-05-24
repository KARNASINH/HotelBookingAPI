namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to update the status of an amenity
    public class AmenityStatusDTO
    {
        //Amenity ID
        public int AmenityID { get; set; }

        //Amenity status
        public bool IsActive { get; set; }
    }
}
