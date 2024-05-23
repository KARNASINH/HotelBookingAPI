using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Room CRUD operations.
    public class RoomController : ControllerBase
    {
        //Repository object to access the methods of RoomRepository
        private readonly RoomRepository _roomRepository;

        //Logger object to log the exceptions
        private readonly ILogger<RoomController> _logger;

        //Constructor to initialize the RoomRepository and Logger objects
        public RoomController(RoomRepository roomRepository, ILogger<RoomController> logger)
        {
            //Assigning the RoomRepository object to the private variable
            _roomRepository = roomRepository;

            //Assigning the Logger object to the private variable
            _logger = logger;
        }
    }
}
