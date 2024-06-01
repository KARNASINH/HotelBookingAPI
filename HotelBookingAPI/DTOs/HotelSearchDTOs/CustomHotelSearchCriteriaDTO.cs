using HotelBookingAPI.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class is used to search the hotels based on the custom search criteria
    //Here if you can pass any, non or all of the parameters to search the hotels
    public class CustomHotelSearchCriteriaDTO
    {
        //This is the minimum price of the room
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0.")]
        public decimal? MinPrice { get; set; }

        //This is the maximum price of the room
        [Range(1, double.MaxValue, ErrorMessage = "Minimum price must be greater than 1.")]
        [PriceRangeValidation("MinPrice", "MaxPrice")]
        public decimal? MaxPrice { get; set; }

        //This is the Room type name
        [StringLength(50, ErrorMessage = "Room type name length cannot exceed 50 characters.")]
        public string? RoomTypeName { get; set; }

        //This is the Amenity associated with the room
        [StringLength(100, ErrorMessage = "Amenity name length cannot exceed 100 characters.")]
        public string? AmenityName { get; set; }

        //This is the View type name of the room
        [StringLength(50, ErrorMessage = "View type name length cannot exceed 50 characters.")]
        public string? ViewType { get; set; }
    }
}
