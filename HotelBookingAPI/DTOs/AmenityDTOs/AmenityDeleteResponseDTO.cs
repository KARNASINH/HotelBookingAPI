namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to return a response when an amenity is deleted
    public class AmenityDeleteResponseDTO
    {
        //The message to be returned
        public string Message { get; set; }

        //A boolean to indicate if the amenity was deleted
        public bool IsDeleted { get; set; }
    }
}
