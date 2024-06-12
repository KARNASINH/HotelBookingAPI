using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.PaymentDTOs
{
    //This class is used to process payment for a reservation
    public class ProcessPaymentDTO
    {
        [Required]
        public int ReservationID { get; set; }

        //The total amount to be paid for the reservation and it should be greater than zero
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
    }
}
