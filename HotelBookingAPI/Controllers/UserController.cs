using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //API Controller for User which holds all the endpoints related to User operations.
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        //UserRepository instance to access the User data.
        private readonly UserRepository _userRepository;

        //Logger instance to log the information or errors.
        private readonly ILogger<UserController> _logger;

        //Constructor to initialize the UserRepository and Logger objects.
        public UserController(UserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
    }
}
