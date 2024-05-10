namespace HotelBookingAPI.DTOs.UserDTOs
{
    // DeleteUserResponseDTO class is used to return the response when the User is deleted from the database.
    public class DeleteUserResponseDTO
    {
        // Message to hold the response while perdorming the delete operation.
        public string Message { get; set; }

        // IsDeleted is a boolean property to set the User is deleted or not.
        public bool IsDeleted { get; set; }
    }
}
