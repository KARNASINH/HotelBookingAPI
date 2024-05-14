using HotelBookingAPI.DTOs.RoomTypeDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to RoomType CRUD operations.
    [Route("api/[controller]")]
    [ApiController]
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


        //API endpoint to get all the RoomTypes from the database based on the IsActive status.
        [HttpGet("AllRoomTypes")]
        public async Task<APIResponse<List<RoomTypeDTO>>> GetAllRoomTypes(bool? IsActive = null)
        {
            //Log the request received.
            _logger.LogInformation($"Request Received for GetAllRoomTypes, IsActive: {IsActive}");

            //Try to get all the RoomTypes.
            try
            {
                //Retrieve all the RoomTypes from the database using the RoomTypeRepository's RetrieveAllRoomTypesAsync method.
                var users = await _roomTypeRepository.RetrieveAllRoomTypesAsync(IsActive);

                //Return the retrieved RoomTypes.
                return new APIResponse<List<RoomTypeDTO>>(users, "Retrieved all Room Types Successfully.");
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Error Retriving all Room Types");

                //Return the Internal server error response if any error occurs in the Try block.
                return new APIResponse<List<RoomTypeDTO>>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }
    }
}
