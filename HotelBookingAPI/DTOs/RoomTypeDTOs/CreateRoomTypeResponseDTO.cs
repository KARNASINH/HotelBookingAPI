namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    //This class is used to hold the response data while creating a new RoomType
    public class CreateRoomTypeResponseDTO
    {
        //Hold the id of the newly created RoomType
        public int RoomTypeId { get; set; }

        //Hold the Successful or Unsuccessful message while creating a new RoomType
        public string Message { get; set; }

        //Hold the StatusCode while creating a new RoomType
        public bool IsCreated { get; set; }
    }
}
