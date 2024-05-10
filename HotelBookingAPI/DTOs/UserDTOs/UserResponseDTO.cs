namespace HotelBookingAPI.DTOs.UserDTOs
{
    //UserResponseDTO class is used to return the user data to the client
    public class UserResponseDTO
    {
        //User Id
        public int UserID { get; set; }

        //User Email Address
        public string Email { get; set; }

        //User is active or not
        public bool IsActive { get; set; }

    }
}
