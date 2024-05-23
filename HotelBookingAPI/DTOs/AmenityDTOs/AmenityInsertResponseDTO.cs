namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to return the response while inserting a new amenity into the database
    public class AmenityInsertResponseDTO
    {
        //ID of the amenity
        public int AmenityID { get; set; }

        //Message to be returned
        public string Message { get; set; }

        //Flag to check if the amenity is created
        public bool IsCreated { get; set; }
    }
}
