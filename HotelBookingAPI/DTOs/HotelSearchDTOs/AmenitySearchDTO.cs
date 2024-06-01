namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{ 
    //This class is used to hold the information of the Amenity.
    public class AmenitySearchDTO
    {
        //This is the ID of the Amenity.
        public int AmenityID { get; set; }

        //This is the Name of the Amenity.
        public string Name { get; set; }

        //Description of the Amenity.
        public string Description { get; set; }
    }
}

