namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //Data Transfer Object to send response after executing the create room stored procedure
    public class CreateRoomResponseDTO
    {
        //Room ID
        public int RoomID { get; set; }

        //To store the error message if any
        public string Message { get; set; }

        //To store the status of the room creation
        public bool IsCreated { get; set; }

    }
}
