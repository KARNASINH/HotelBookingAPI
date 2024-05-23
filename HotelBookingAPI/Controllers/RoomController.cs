using HotelBookingAPI.DTOs.RoomDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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




        //API Endpoint to get all the rooms
        [HttpGet("All")]
        public async Task<APIResponse<List<RoomDetailsResponseDTO>>> GetAllRooms([FromQuery] GetAllRoomsRequestDTO request)
        {
            //Logging the request received
            _logger.LogInformation("Request Received for CreateRoomType: {@GetAllRoomsRequestDTO}", request);

            //Checking if the Data in the Query String is valid and if model binding is successful or not
            if (!ModelState.IsValid)
            {
                //Logging the error if the data in the Query String is invalid
                _logger.LogInformation("Invalid Data in the Request Body");

                //Returning the response with the 400 status code and the error message
                return new APIResponse<List<RoomDetailsResponseDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Query String");
            }

            //Try to get all the rooms
            try
            {
                //Calling the GetAllRoomsAsync method of RoomRepository to get all the rooms
                var rooms = await _roomRepository.GetAllRoomsAsync(request);

                //Returning the response with the 200 status code and the rooms data
                return new APIResponse<List<RoomDetailsResponseDTO>>(rooms, "Retrieved all Room Successfully.");
            }
            //Catch the exception if any error occurs
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Retriving all Room");

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<List<RoomDetailsResponseDTO>>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }






    }
}
