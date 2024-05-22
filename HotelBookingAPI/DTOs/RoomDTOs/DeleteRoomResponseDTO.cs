namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //This class is used to return a response when a room is deleted
    public class DeleteRoomResponseDTO
    {
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}