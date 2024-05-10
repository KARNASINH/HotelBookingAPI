using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class holds the information about the user that is trying to log in
    public class LogInUserDTO
    {
        //Email of the user
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //Password of the user
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
