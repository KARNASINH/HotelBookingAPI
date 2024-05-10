using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class generates a response when a user is updated.
    public class UpdateUserResponseDTO
    {
        //Id of the user
        public int UserId { get; set; }

        //Message to be displayed if any error occurs
        public string Message { get; set; }

        //Is the user updated
        public bool IsUpdated { get; set; }

    }
}
