namespace HotelBookingAPI.DTOs.BookingDTOs
{
    //Class to hold the response of the rooms along with the cost details
    public class RoomCostsResponseDTO
    {
        //List of RoomCostDetailDTO to hold the cost details of each room
        public List<RoomCostDetailDTO> RoomDetails { get; set; } = new List<RoomCostDetailDTO>();

        //Base cost of the room before tax
        public decimal Amount { get; set; }

        //GST amount based on 18% tax
        public decimal GST { get; set; }

        //Total amount including GST
        public decimal TotalAmount { get; set; }

        //Status of the response
        public bool Status { get; set; }

        //Message to be displayed in case of any error
        public string Message { get; set; }
    }
}
