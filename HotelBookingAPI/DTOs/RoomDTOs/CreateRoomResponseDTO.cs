namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //This class is used to hold the response data while creating a room using Store procedure.
    public class CreateRoomResponseDTO
    {
        public int RoomID { get; set; }
        public string Message { get; set; }
        public bool IsCreated { get; set; }
    }
}
