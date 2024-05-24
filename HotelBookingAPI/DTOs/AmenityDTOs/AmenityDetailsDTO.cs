namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to return the details of an amenity
    public class AmenityDetailsDTO
    {
        //The ID of the amenity
        public int AmenityID { get; set; }

        //The name of the amenity
        public string Name { get; set; }

        //The description of the amenity
        public string Description { get; set; }

        //Flag to determine if the amenity is active
        public bool IsActive { get; set; }
    }
}
