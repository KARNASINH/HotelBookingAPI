using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Room Search operations.

    [ApiController]
    [Route("[controller]")]
    public class HotelSearchController : ControllerBase
    {
        //Repository object to access the methods of HotelSearchRepository.
        private readonly HotelSearchRepository _hotelSearchRepository;

        //Logger object to log the information or errors.
        private readonly ILogger<HotelSearchController> _logger;

        //Constructor to initialize the Repository and Logger objects.
        public HotelSearchController(HotelSearchRepository hotelSearchRepository, ILogger<HotelSearchController> logger)
        {
            //Initialize the Repository object.
            _hotelSearchRepository = hotelSearchRepository;
            //Initialize the Logger object.
            _logger = logger;
        }

    }
}
