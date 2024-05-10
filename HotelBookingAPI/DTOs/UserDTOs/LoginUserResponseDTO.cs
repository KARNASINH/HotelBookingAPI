using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class is used to generate a response when a user logs in.
    public class LoginUserResponseDTO
    {
        //Id of the user
        public int UserId { get; set; }

        //Email of the user
        public string Message { get; set; }

        //Is the user logged in
        public bool IsLogin { get; set; }
    }
}
