using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //This class is used to hold the information for adding guests to a reservation
    public class AddGuestsToReservationDTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ReservationID { get; set; }
        [Required]
        
        //Guest details inbcluding room association
        [MinLength(1, ErrorMessage = "At least one guest detail must be provided.")]
        public List<GuestDetail> GuestDetails { get; set; }
    }
    public class GuestDetail
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string AgeGroup { get; set; }
        public string Address { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int StateId { get; set; }

        //Room Id associated with the guest
        [Required]
        public int RoomID { get; set; }
    }
}
