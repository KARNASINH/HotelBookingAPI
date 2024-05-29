using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Amenity CRUD operations.
    public class AmenityController : ControllerBase
    {
        //AmenityRepository instance to access the methods in the repository.
        private readonly AmenityRepository _amenityRepository;

        //ILogger instance to log the information or errors.
        private readonly ILogger<AmenityController> _logger;

        //Constructor to initialize the AmenityRepository and Logger using Dependency Injection.
        public AmenityController(AmenityRepository amenityRepository, ILogger<AmenityController> logger)
        {
            _amenityRepository = amenityRepository;
            _logger = logger;
        }
    }
}
