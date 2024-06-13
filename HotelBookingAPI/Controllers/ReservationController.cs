using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Reservation (Booking and Payment) CRUD operations.
    public class ReservationController : Controller
    {
        //Repository object to interact with the database.
        private readonly ReservationRepository _reservationRepository;

        //Logger object to log the information, warning and error messages.
        private readonly ILogger<ReservationController> _logger;

        //Constructor to initialize the Repository and Logger objects.
        public ReservationController(ReservationRepository reservationRepository, ILogger<ReservationController> logger)
        {
            //Initialize the Repository and Logger objects.
            _reservationRepository = reservationRepository;

            //Initialize the Logger object.
            _logger = logger;
        }
    }
}
