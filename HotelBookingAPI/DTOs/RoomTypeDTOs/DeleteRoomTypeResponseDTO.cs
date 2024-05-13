namespace HotelBookingAPI.DTOs.RoomTypeDTOs
{
    //This class is used to hold the response while Deleting a RoomType
    public class DeleteRoomTypeResponseDTO
    {
        //This property is used to hold the message while performing the Delete operation
        public string Message { get; set; }

        //This property is used to hold the status of the deletion operation
        public bool IsDeleted { get; set; }
    }
}
