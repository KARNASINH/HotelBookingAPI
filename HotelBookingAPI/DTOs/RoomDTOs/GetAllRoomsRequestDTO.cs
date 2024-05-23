using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HotelBookingAPI.DTOs.RoomDTOs
{
    //This class is used to hold the information of all rooms
    public class GetAllRoomsRequestDTO
    {
        //Optional filtering by RoomTypeID
        //Range attribute ensures that the RoomTypeID is a positive integer
        [Range(1, int.MaxValue, ErrorMessage = "Room Type ID must be a positive integer.")]
        public int? RoomTypeID { get; set; }
        
        //Optional filtering by Status
        //RegularExpression attribute ensures that the Status is one of the following: Available, Under Maintenance, Occupied, or All
        [RegularExpression("(Available|Under Maintenance|Occupied|All)", ErrorMessage = "Invalid status. Valid statuses are 'Available', 'Under Maintenance', 'Occupied', or 'All' for no filter.")]
        public string? Status { get; set; }
    }
}
