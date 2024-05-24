using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.AmenityDTOs
{
    //This class is used to update an existing amenity
    public class AmenityUpdateDTO
    {
        //Amenity ID
        [Required]
        public int AmenityID { get; set; }

        //Amenity Name
        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        //Amenity Description
        [StringLength(255, ErrorMessage = "Description length can't be more than 255 characters.")]
        public string Description { get; set; }

        //Amenity is active or not
        [Required]
        public bool IsActive { get; set; }
    }
}
