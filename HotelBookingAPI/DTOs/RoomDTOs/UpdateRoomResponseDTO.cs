namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //This class is used to hold the response data while updating a room using Store procedure.
    public class UpdateRoomResponseDTO
    {
        public int RoomId { get; set; }
        public string Message { get; set; }
        public bool IsUpdated { get; set; }
    }
}
