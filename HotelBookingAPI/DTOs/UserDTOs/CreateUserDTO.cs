using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class is used to create a new user.
    //It contains the email and password of the user.
    public class CreateUserDTO
    {
        //Email of the user
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //Password of the user
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } //Before sending password to the server, it should be hashed.
    }
}
