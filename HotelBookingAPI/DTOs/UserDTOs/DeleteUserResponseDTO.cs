namespace HotelBookingAPI.DTOs.UserDTOs
{
    // DeleteUserResponseDTO class is used to return the response when the User is deleted from the database.
    public class DeleteUserResponseDTO
    {
        //UserId to hold the id of the user that is deleted.
        public int UserId { get; set; }

        // Message to hold the response while perdorming the delete operation.
        public string Message { get; set; }
    }
}
