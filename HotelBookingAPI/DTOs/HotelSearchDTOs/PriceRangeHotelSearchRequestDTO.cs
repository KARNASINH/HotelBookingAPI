using HotelBookingAPI.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class is used to hold the data that will be used to search for hotels by price range
    public class PriceRangeHotelSearchRequestDTO
    {
        //This is the minimum price that the user wants to search for
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0.")]
        public decimal MinPrice { get; set; }

        //This is the maximum price that the user wants to search for
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Maximum price must be greater than 0.")]
        [PriceRangeValidation("MinPrice", "MaxPrice")]
        public decimal MaxPrice { get; set; }
    }
}
