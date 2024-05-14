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



        //API endpoint to get the RoomType by RoomTypeID.
        [HttpGet("GetRoomType/{RoomTypeID}")]
        public async Task<APIResponse<RoomTypeDTO>> GetRoomTypeById(int RoomTypeID)
        {
            //Log the request received to get the RoomType by RoomTypeID.
            _logger.LogInformation($"Request Received for GetRoomTypeById, RoomTypeID: {RoomTypeID}");

            //Try to get the RoomType by RoomTypeID.
            try
            {
                //Retrieve the RoomType by RoomTypeID using the RoomTypeRepository's RetrieveRoomTypeByIdAsync method.
                var roomType = await _roomTypeRepository.RetrieveRoomTypeByIdAsync(RoomTypeID);

                //Check if the RoomType is null.
                if (roomType == null)
                {
                    //Return the Not Found response if the RoomType is not found along with 404 status code.
                    return new APIResponse<RoomTypeDTO>(HttpStatusCode.NotFound, "RoomTypeID not found.");
                }

                //Return the RoomType if found.
                return new APIResponse<RoomTypeDTO>(roomType, "RoomType fetched successfully.");
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message if any error occurs during the Try block.
                _logger.LogError(ex, "Error getting Room Type by ID {RoomTypeID}", RoomTypeID);

                //Return the Bad Request response if any error occurs in the Try block along with 400 status code.
                return new APIResponse<RoomTypeDTO>(HttpStatusCode.BadRequest, "Error fetching Room Type .", ex.Message);
            }
        }




        //API endpoint to Create a new RoomType with the provided details.
        [HttpPost("AddRoomType")]
        public async Task<APIResponse<CreateRoomTypeResponseDTO>> CreateRoomType([FromBody] CreateRoomTypeDTO request)
        {
            //Log the request received to create a new RoomType.
            _logger.LogInformation("Request Received for CreateRoomType: {@CreateRoomTypeDTO}", request);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                //Log the error message if the request body is invalid.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return the Bad Request response if the request body is invalid along with 400 status code.
                return new APIResponse<CreateRoomTypeResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
            }

            //Try to create a new RoomType.
            try
            {
                //Create a new RoomType using the RoomTypeRepository's CreateRoomType method.
                var response = await _roomTypeRepository.CreateRoomType(request);

                //Log the response received from the repository.
                _logger.LogInformation("CreateRoomType Response From Repository: {@CreateRoomTypeResponseDTO}", response);

                //Check if the RoomType is created successfully.
                if (response.IsCreated)
                {
                    //Return the RoomType created response if the RoomType is created successfully along with 200 status code.
                    return new APIResponse<CreateRoomTypeResponseDTO>(response, response.Message);
                }

                //Return the Bad Request response if the RoomType is not created successfully along with 400 status code.
                return new APIResponse<CreateRoomTypeResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message if any error occurs during the Try block.
                _logger.LogError(ex, "Error adding new Room Type with Name {TypeName}", request.TypeName);

                //Return the Internal Server Error response if any error occurs in the Try block along with 500 status code.
                return new APIResponse<CreateRoomTypeResponseDTO>(HttpStatusCode.InternalServerError, "Room Type Creation Failed.", ex.Message);
            }
        }




        //API endpoint to Update the RoomType with the provided details.
        [HttpPut("Update/{RoomTypeId}")]
        public async Task<APIResponse<UpdateRoomTypeResponseDTO>> UpdateRoomType(int RoomTypeId, [FromBody] UpdateRoomTypeDTO request)
        {
            //Log the request received to update the RoomType.
            _logger.LogInformation("Request Received for UpdateRoomType {@UpdateRoomTypeDTO}", request);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                //Log the error message if the request body is invalid.
                _logger.LogInformation("UpdateRoomType Invalid Request Body");

                //Return the Bad Request response if the request body is invalid along with 400 status code.
                return new APIResponse<UpdateRoomTypeResponseDTO>(HttpStatusCode.BadRequest, "Invalid Request Body");
            }

            //Check if the RoomTypeId in the request body and the RoomTypeId in the URL are the same.
            if (RoomTypeId != request.RoomTypeID)
            {
                //Log the error message if the RoomTypeId in the request body and the RoomTypeId in the URL are not the same.
                _logger.LogInformation("UpdateRoomType Mismatched Room Type ID");

                //Return the Bad Request response if the RoomTypeId in the request body and the RoomTypeId in the URL are not the same along with 400 status code.
                return new APIResponse<UpdateRoomTypeResponseDTO>(HttpStatusCode.BadRequest, "Mismatched Room Type ID.");
            }

            //Try to update the RoomType.
            try
            {
                //Update the RoomType using the RoomTypeRepository's UpdateRoomType method.
                var response = await _roomTypeRepository.UpdateRoomType(request);

                //Log the response received from the repository.
                if (response.IsUpdated)
                {
                    //Return the RoomType updated response if the RoomType is updated successfully along with 200 status code.
                    return new APIResponse<UpdateRoomTypeResponseDTO>(response, response.Message);
                }

                //Return the Bad Request response if the RoomType is not updated successfully along with 400 status code.
                return new APIResponse<UpdateRoomTypeResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message if any error occurs during the Try block.
                _logger.LogError(ex, "Error Updating Room Type {RoomTypeId}", RoomTypeId);

                //Return the Internal Server Error response if any error occurs in the Try block along with 500 status code.
                return new APIResponse<UpdateRoomTypeResponseDTO>(HttpStatusCode.InternalServerError, "Update Room Type Failed.", ex.Message);
            }
        }



        //API endpoint to Delete the RoomType by RoomTypeId.
        [HttpDelete("Delete/{RoomTypeId}")]
        public async Task<APIResponse<DeleteRoomTypeResponseDTO>> DeleteRoomType(int RoomTypeId)
        {
            //Log the request received to delete the RoomType by RoomTypeId.
            _logger.LogInformation($"Request Received for DeleteRoomType, RoomTypeId: {RoomTypeId}");

            //Try to delete the RoomType by RoomTypeId.
            try
            {
                //Retrieve the RoomType by RoomTypeId using the RoomTypeRepository's RetrieveRoomTypeByIdAsync method.
                var roomType = await _roomTypeRepository.RetrieveRoomTypeByIdAsync(RoomTypeId);

                //Check if the RoomType is null.
                if (roomType == null)
                {
                    //Return the Not Found response if the RoomType is not found along with 404 status code.
                    return new APIResponse<DeleteRoomTypeResponseDTO>(HttpStatusCode.NotFound, "RoomType not found.");
                }

                //Delete the RoomType by RoomTypeId using the RoomTypeRepository's DeleteRoomType method.
                var response = await _roomTypeRepository.DeleteRoomType(RoomTypeId);

                //Checks if the RoomType is deleted successfully using the value stored in IsDeleted property.
                if (response.IsDeleted)
                {
                    //Return the RoomType deleted response if the RoomType is deleted successfully along with 200 status code.
                    return new APIResponse<DeleteRoomTypeResponseDTO>(response, response.Message);
                }

                //Return the Bad Request response if the RoomType is not deleted successfully along with 400 status code.
                return new APIResponse<DeleteRoomTypeResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                //Log the error message if any error occurs during the Try block.
                _logger.LogError(ex, "Error deleting RoomType {RoomTypeId}", RoomTypeId);

                //Return the Internal Server Error response if any error occurs in the Try block along with 500 status code.
                return new APIResponse<DeleteRoomTypeResponseDTO>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }
    }
}
