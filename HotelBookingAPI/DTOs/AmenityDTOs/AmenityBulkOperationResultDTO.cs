namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is ussed to perform the Bulk Insertation operation for Amenity
    public class AmenityBulkOperationResultDTO
    {
        //Response message
        public string Message { get; set; }

        //Status of the operation wheather it is success or not
        public bool IsSuccess { get; set; }
    }
}
