using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class is used to update a user.
    public class UpdateUserDTO
    {
        //UserID of the user
        [Required(ErrorMessage ="UserID is required")]
        public int UserID { get; set; }

        //Email of the user
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //Password of the user
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
