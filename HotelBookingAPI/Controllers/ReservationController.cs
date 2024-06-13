using HotelBookingAPI.DTOs.BookingDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Reservation (Booking and Payment) CRUD operations.
    [ApiController]
    [Route("api/[controller]")]
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


        
        
        
        
        
        
        //API Endpoint to get the details of all the Rooms along with their Cost.
        [HttpPost("CalculateRoomCosts")]
        public async Task<APIResponse<RoomCostsResponseDTO>> CalculateRoomCosts([FromBody] RoomCostsDTO model)
        {
            //Log the Request Received for CalculateRoomCosts along with the Request Body.
            _logger.LogInformation("Request Received for CalculateRoomCosts: {@RoomCostsDTO}", model);

            //Check if the Request Body is not valid.
            if (!ModelState.IsValid)
            {
                //Log the Invalid Data in the Request Body.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return 400 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
            }

            //Try to get the Room Costs.
            try
            {
                //Call the Repository Method to Calculate the Room Costs.
                var result = await _reservationRepository.CalculateRoomCostsAsync(model);

                //Check if the Room Costs are calculated successfully.
                if (result.Status)
                {
                    //Return the Success Response with the Room Costs.
                    return new APIResponse<RoomCostsResponseDTO>(result, "Success");
                }
                //Return 400 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.BadRequest, "Failed");
            }
            //Catch the Exception if any error occurred while calculating the Room Costs.
            catch (Exception ex)
            {
                //Log the Error Message.
                _logger.LogError(ex, "Failed to calculate room costs");

                //Return 500 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.InternalServerError, "Failed to calculate room costs", ex.Message);
            }
        }







        
    }
}
