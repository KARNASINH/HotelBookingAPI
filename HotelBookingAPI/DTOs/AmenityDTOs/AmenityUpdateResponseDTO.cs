namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to send the response while perforning the Amenity update operation
    public class AmenityUpdateResponseDTO
    {
        //Amenity ID
        public int AmenityID { get; set; }

        //Message to be sent
        public string Message { get; set; }

        //Flag to check if the Amenity is updated
        public bool IsUpdated { get; set; }
    }
}
