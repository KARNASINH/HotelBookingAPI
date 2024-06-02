using HotelBookingAPI.DTOs.HotelSearchDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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








        //This endpoint is used to search the Hotels based on the CheckIn and CheckOut dates.
        //checkInDate=2024-06-18
        //checkOutDate=2024-06-20
        [HttpGet("Availability")]        
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByAvailability([FromQuery] AvailabilityHotelSearchRequestDTO request)
        {
            //Trying to execute the code.
            try
            {
                //Check if the Model is valid or not.
                if (!ModelState.IsValid)
                {
                    //Log the error message.
                    _logger.LogInformation("Invalid Data in the Request Body");

                    //Return the response with the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }

                //Call the SearchByAvailabilityAsync method to get the rooms based on the CheckIn and CheckOut dates.
                var rooms = await _hotelSearchRepository.SearchByAvailabilityAsync(request.CheckInDate, request.CheckOutDate);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Rooms are Available for the given dates.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Roomd Found for the given dates.");
            }

            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get the rooms for the given dates.");

                // Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "Failed to get rooms for the given dates.", ex.Message);
            }
        }








        //This endpoint is used to search the Hotels based on the Price Range that is MinPrice and MaxPrice.
        [HttpGet("PriceRange")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByPriceRange([FromQuery] PriceRangeHotelSearchRequestDTO request)
        {
            //Trying to execute the code.
            try
            {
                //Check if the Model is valid or not.
                if (!ModelState.IsValid)
                {
                    //Log the error message.
                    _logger.LogInformation("Invalid Price Range in the Request Body.");

                    //Return the response with the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body.");
                }

                //Call the SearchByPriceRangeAsync method to get the rooms based on the Price Range.
                var rooms = await _hotelSearchRepository.SearchByPriceRangeAsync(request.MinPrice, request.MaxPrice);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Got rooms for the given price range.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Rooms Found for the given price range.");
            }
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms for the given price range.");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by price range.", ex.Message);
            }
        }






        
        //This endpoint is used to search the Hotels based on the RoomType Name.
        [HttpGet("RoomType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByRoomType(string roomTypeName)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the RoomTypeName is empty or not.
                if (string.IsNullOrEmpty(roomTypeName))
                {
                    //Log the error message.
                    _logger.LogInformation("RoomType Name is Empty.");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "RoomType Name is Empty.");
                }

                //Call the SearchByRoomTypeAsync method to get the rooms based on the RoomType Name.
                var rooms = await _hotelSearchRepository.SearchByRoomTypeAsync(roomTypeName);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Got the rooms for the given RoomType name.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Rooms found for the given RoomType name.");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms by RoomType name.");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by RoomType.", ex.Message);
            }
        }









    }
}
