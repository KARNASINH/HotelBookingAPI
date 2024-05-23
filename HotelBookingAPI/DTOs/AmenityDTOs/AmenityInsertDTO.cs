using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to insert a new amenity into the database
    public class AmenityInsertDTO
    {
        //Name of the amenity
        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        //Description of the amenity
        [StringLength(255, ErrorMessage = "Description length can't be more than 255 characters.")]
        public string Description { get; set; }
    }
}
