using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class holds the information to update the user role
    public class UserRoleDTO
    {
        //UserID of the user
        [Required(ErrorMessage = "UserID is required")]       
        public int UserID { get; set; }

        //RoleID of the user
        [Required(ErrorMessage = "RoleID is required")]
        public int RoleID { get; set; }
    }
}
