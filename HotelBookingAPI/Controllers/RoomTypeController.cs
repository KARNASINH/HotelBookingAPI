using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{ 
    //API Controller which holds all the endpoints related to RoomType CRUD operations.
    public class RoomTypeController : ControllerBase
    {
        //UserRepository instance to access the RoomType data.
        private readonly RoomTypeRepository _roomTypeRepository;

        //Logger instance to log the information or errors.
        private readonly ILogger<RoomTypeController> _logger;

        //Constructor to initialize the RoomTypeRepository and Logger objects.
        public RoomTypeController(RoomTypeRepository roomTypeRepository, ILogger<RoomTypeController> logger)
        {
            _roomTypeRepository = roomTypeRepository;
            _logger = logger;
        }
    }
}
