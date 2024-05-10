using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class generates a response after creating a new user.
    public class CreateUserResponseDTO
    {
        //UserID of the user
        public int UserId { get; set; }

        //Message to be displayed if any error occurs
        public string Message { get; set; }

        //Flag to check if the user is created
        public bool IsCreated { get; set; }
    }
}
