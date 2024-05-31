using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //This controller is responsible for handling requests related to room amenities.
    public class RoomAmenityController : ControllerBase
    {
        //Created private fields for RoomAmenityRepository and ILogger.
        private readonly RoomAmenityRepository _roomAmenityRepository;
        private readonly ILogger<RoomAmenityController> _logger;

        //Constructor to initialize the RoomAmenityRepository and ILogger.
        public RoomAmenityController(RoomAmenityRepository roomAmenityRepository, ILogger<RoomAmenityController> logger)
        {
            _roomAmenityRepository = roomAmenityRepository;
            _logger = logger;
        }

    }
}
