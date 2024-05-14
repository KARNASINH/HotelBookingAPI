using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //Data Transfer Object for creating a room
    public class CreateRoomRequestDTO
    {
        //Room number
        [Required]
        [StringLength(10, ErrorMessage = "Room number must be up to 10 characters long.")]

        //Room number
        public string RoomNumber { get; set; }

        //Room type ID, this is a foreign key
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Room Type ID.")]
        public int RoomTypeID { get; set; }

        //Price of the room
        [Required]
        [Range(typeof(decimal), "0.01", "999999.99", ErrorMessage = "Price must be between 0.01 and 999999.99.")]
        public decimal Price { get; set; }

        //Bed type
        [Required]
        [StringLength(50, ErrorMessage = "Bed type must be up to 50 characters long.")]
        public string BedType { get; set; }

        //View type of the room. E.g. Sea view, City view, etc.
        [Required]
        [StringLength(50, ErrorMessage = "View type must be up to 50 characters long.")]
        public string ViewType { get; set; }

        //Status of the room. E.g. Available, Under Maintenance, Occupied
        [Required]
        [RegularExpression("(Available|Under Maintenance|Occupied)", ErrorMessage = "Status must be 'Available', 'Under Maintenance', or 'Occupied'.")]
        public string Status { get; set; }

        //Indicates if the room is active or not
        [Required]
        public bool IsActive { get; set; }
    }
}
