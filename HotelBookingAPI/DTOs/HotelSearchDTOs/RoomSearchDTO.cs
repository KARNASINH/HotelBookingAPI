namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    //This class is used to hold the information of a Room along with RoomType
    public class RoomSearchDTO
    {
        //Holds the RoomID data
        public int RoomID { get; set; }

        //Holds the RoomNumber data
        public string RoomNumber { get; set; }

        //Holds the Price data
        public decimal Price { get; set; }

        //Holds the BedType data
        public string BedType { get; set; }

        //Holds the ViewType data
        public string ViewType { get; set; }

        //Holds the Status data
        public string Status { get; set; }

        //Holds the RoomType data
        public RoomTypeSearchDTO RoomType { get; set; }
    }
}
