namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    //This class is used to hold the response data while updating RoomType information
    public class UpdateRoomTypeResponseDTO
    {
        //Hold the id of the updated RoomType
        public int RoomTypeId { get; set; }

        //Hold the Successful or Unsuccessful message while updating RoomType information
        public string Message { get; set; }

        //Hold the StatusCode while updating RoomType information
        public bool IsUpdated { get; set; }
    }
}