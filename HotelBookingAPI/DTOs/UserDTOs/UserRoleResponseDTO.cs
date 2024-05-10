namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class is used to generate a response when a user role is assigned.
    public class UserRoleResponseDTO
    {
        //Message to be displayed if any error occurs
        public string Message { get; set; }

        //Is the user role assigned
        public bool IsRoleAssigned { get; set; }
    }
}
