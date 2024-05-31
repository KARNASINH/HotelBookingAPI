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








        //This Endpoint is responsible for fetching all the RoomTypes based on the given Amenity ID.
        [HttpGet("FetchRoomTypesByAmenityId/{amenityId}")]
        public async Task<APIResponse<List<RoomTypeResponse>>> FetchRoomTypesByAmenityId(int amenityId)
        {
            //Try block to fetch the RoomTypes based on the given Amenity ID.
            try
            {
                //Fetching the RoomTypes using FetchRoomTypesByAmenityIdAsync of RoomAmenityRepository.
                var roomTypes = await _roomAmenityRepository.FetchRoomTypesByAmenityIdAsync(amenityId);

                //If roomTypes are not null and count is greater than 0, then return the roomTypes.
                if (roomTypes != null && roomTypes.Count > 0)
                {
                    //Returning the List of RoomType along with 200 HttpStatusCode.
                    return new APIResponse<List<RoomTypeResponse>>(roomTypes, "Fetch Room Types By AmenityId Successfully.");
                }

                //If roomTypes are null or count is 0, then return message No Record Found along with 400 HttpStatusCode.
                return new APIResponse<List<RoomTypeResponse>>(HttpStatusCode.NotFound, "No Record Found");
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error fetching room types by amenity ID");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<List<RoomTypeResponse>>(HttpStatusCode.InternalServerError, "Error fetching room types by amenity ID", ex.Message);
            }
        }







        //This Endpoint is responsible for adding a new RoomAmenity.
        [HttpPost("AddRoomAmenity")]
        public async Task<APIResponse<RoomAmenityResponseDTO>> AddRoomAmenity([FromBody] RoomAmenityDTO input)
        {
            //Try block to add a new RoomAmenity.
            try
            {
                //If the ModelState is not valid, then return the message Invalid Data in the Request Body along with 400 HttpStatusCode.
                if (!ModelState.IsValid)
                {
                    //Returning the message along with 400 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }

                //Adding the RoomAmenity using AddRoomAmenityAsync method of RoomAmenityRepository class.
                var response = await _roomAmenityRepository.AddRoomAmenityAsync(input);

                //If the response is successful, then return the response.
                if (response.IsSuccess)
                {
                    //Returning the RoomAmenityResponseDTO along with 200 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(response, response.Message);
                }

                //If the response is not successful, then return the message along with 400 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error adding room amenity");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.InternalServerError, "Error adding room amenity", ex.Message);
            }
        }







        //This Endpoint is responsible for Deleting an existing RoomAmenity.
        [HttpDelete("DeleteRoomAmenity")]
        public async Task<APIResponse<RoomAmenityResponseDTO>> DeleteRoomAmenity([FromBody] RoomAmenityDTO input)
        {
            //Try block to delete an existing RoomAmenity.
            try
            {
                //If the ModelState is not valid, then return the message Invalid Data in the Request Body along with 400 HttpStatusCode.
                if (!ModelState.IsValid)
                {
                    //Returning the message along with 400 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }

                //Deleting the RoomAmenity using DeleteRoomAmenityAsync method of RoomAmenityRepository class.
                var response = await _roomAmenityRepository.DeleteRoomAmenityAsync(input);

                //If the response is successful, then return the response.
                if (response.IsSuccess)
                {
                    //Returning the RoomAmenityResponseDTO along with 200 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(response, response.Message);
                }

                //If the response is not successful, then return the message along with 400 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error deleting room amenity");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.InternalServerError, "Error deleting room amenity", ex.Message);
            }
        }






        //This Endpoint is responsible for bulk inserting the RoomAmenities.
        [HttpPost("BulkInsertRoomAmenities")]
        public async Task<APIResponse<RoomAmenityResponseDTO>> BulkInsertRoomAmenities([FromBody] RoomAmenitiesBulkInsertUpdateDTO input)
        {
            //Try block to bulk insert the RoomAmenities.
            try
            {
                //If the ModelState is not valid, then return the message Invalid Data in the Request Body along with 400 HttpStatusCode.
                if (!ModelState.IsValid)
                {
                    //Returning the message along with 400 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }

                //Bulk inserting the RoomAmenities using BulkInsertRoomAmenitiesAsync method of RoomAmenityRepository class.
                var response = await _roomAmenityRepository.BulkInsertRoomAmenitiesAsync(input);

                //If the response is successful, then return the response.
                if (response.IsSuccess)
                {
                    //Returning the RoomAmenityResponseDTO along with 200 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(response, response.Message);
                }

                //If the response is not successful, then return the message along with 400 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error performing bulk insert of room amenities");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.InternalServerError, "Error performing bulk insert of room amenities", ex.Message);
            }
        }






        //This Endpoint is responsible for bulk updating the RoomAmenities.
        [HttpPost("BulkUpdateRoomAmenities")]
        public async Task<APIResponse<RoomAmenityResponseDTO>> BulkUpdateRoomAmenities([FromBody] RoomAmenitiesBulkInsertUpdateDTO input)
        {
            //Try block to bulk update the RoomAmenities.
            try
            {
                //If the ModelState is not valid, then return the message Invalid Data in the Request Body along with 400 HttpStatusCode.
                var response = await _roomAmenityRepository.BulkUpdateRoomAmenitiesAsync(input);

                //If the response is successful, then return the response.
                if (response.IsSuccess)
                {
                    //Returning the RoomAmenityResponseDTO along with 200 HttpStatusCode.
                    return new APIResponse<RoomAmenityResponseDTO>(response, response.Message);
                }

                //If the response is not successful, then return the message along with 400 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error performing bulk update of room amenities");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<RoomAmenityResponseDTO>(HttpStatusCode.InternalServerError, "Error performing bulk update of room amenities", ex.Message);
            }
        }

    }
}
