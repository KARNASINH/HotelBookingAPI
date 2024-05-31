using HotelBookingAPI.DTOs.RoomAmenityDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //This controller is responsible for handling requests related to room amenities.
    public class RoomAmenityController : ControllerBase
    {
        //Created private fields for RoomAmenityRepository and ILogger.
        private readonly RoomAmenityRepository _roomAmenityRepository;
        private readonly ILogger<RoomAmenityController> _logger;






        //Constructor to initialize the RoomAmenityRepository and ILogger.
        public RoomAmenityController(RoomAmenityRepository roomAmenityRepository, ILogger<RoomAmenityController> logger)
        {
            _roomAmenityRepository = roomAmenityRepository;
            _logger = logger;
        }






        //This Endpoint is responsible for fetching all the amenities based on the given RoomType ID.
        [HttpGet("FetchAmenitiesByRoomTypeId/{roomTypeId}")]
        public async Task<APIResponse<List<AmenityResponseDTO>>> FetchAmenitiesByRoomTypeId(int roomTypeId)
        {
            //Try block to fetch the amenities based on the given RoomType ID.
            try
            {
                //Fetching the amenities using FetchRoomAmenitiesByRoomTypeIdAsync of RoomAmenityRepository.
                var amenities = await _roomAmenityRepository.FetchRoomAmenitiesByRoomTypeIdAsync(roomTypeId);

                //If amenities are not null and count is greater than 0, then return the amenities.
                if (amenities != null && amenities.Count > 0)
                {
                    //Returning the List of Amenity along with 200 HttpStatusCode.
                    return new APIResponse<List<AmenityResponseDTO>>(amenities, "Fetch Amenities By Room Type Id Successfully.");
                }

                //If amenities are null or count is 0, then return message No Record Found along with 400 HttpStatusCode.
                return new APIResponse<List<AmenityResponseDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error fetching amenities by room type ID");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<List<AmenityResponseDTO>>(HttpStatusCode.InternalServerError, "Error fetching amenities by room type ID", ex.Message);
            }
        }

    }
}
