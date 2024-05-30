using HotelBookingAPI.DTOs.AmenityDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Amenity CRUD operations.
    [ApiController]
    [Route("api/[controller]")]
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







        //APi endpoint to fetch all the amenities.
        [HttpGet("Fetch")]
        public async Task<APIResponse<AmenityFetchResultDTO>> FetchAmenities(bool? isActive = null)
        {
            //Try block to fetch the amenities.
            try
            {
                //Calling the FetchAmenitiesAsync method from the repository to get all the amenities.
                var response = await _amenityRepository.FetchAmenitiesAsync(isActive);

                //Checking if the response is successful.
                if (response.IsSuccess)
                {
                    //Returning the data along with 200 HttpStatusCode.
                    return new APIResponse<AmenityFetchResultDTO>(response, "Retrieved all Room Amenity Successfully.");
                }

                //Returning the error message along with 400 HttpStatusCode.
                return new APIResponse<AmenityFetchResultDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error occurred while fetching amenities.");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<AmenityFetchResultDTO>(HttpStatusCode.InternalServerError, "An error occurred while processing your request.", ex.Message);
            }
        }







        //API endpoint to fetch the amenity by ID.
        [HttpGet("Fetch/{id}")]
        public async Task<APIResponse<AmenityDetailsDTO>> FetchAmenityById(int id)
        {
            //Try block to fetch the amenity by ID.
            try
            {
                //Calling the FetchAmenityByIdAsync method from the repository to get the amenity by ID.
                var response = await _amenityRepository.FetchAmenityByIdAsync(id);

                //Checking if the response is not null.
                if (response != null)
                {
                    //Returning the data along with 200 HttpStatusCode.
                    return new APIResponse<AmenityDetailsDTO>(response, "Retrieved Room Amenity Successfully.");
                }

                //Returning the error message along with 404 HttpStatusCode.
                return new APIResponse<AmenityDetailsDTO>(HttpStatusCode.NotFound, "Amenity ID not found.");
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error occurred while fetching amenity by ID.");
                
                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<AmenityDetailsDTO>(HttpStatusCode.InternalServerError, "An error occurred while processing your request.", ex.Message);
            }
        }







        //API endpoint to create the amenity.
        [HttpPost("Add")]
        public async Task<APIResponse<AmenityInsertResponseDTO>> AddAmenity([FromBody] AmenityInsertDTO amenity)
        {
            //Try block to add the amenity.
            try
            {
                //Checking if the model state is not valid.
                if (!ModelState.IsValid)
                {
                    //Returning the error message along with 400 HttpStatusCode if the model state is not valid.
                    return new APIResponse<AmenityInsertResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
                }

                //Calling the AddAmenityAsync method from the repository to add the amenity.
                var response = await _amenityRepository.AddAmenityAsync(amenity);

                //Checking if the amenity is created successfully.
                if (response.IsCreated)
                {
                    //Returning the data along with 200 HttpStatusCode.
                    return new APIResponse<AmenityInsertResponseDTO>(response, response.Message);
                }

                //Returning the error message along with 400 HttpStatusCode.
                return new APIResponse<AmenityInsertResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch block to handle the exception.
            catch (Exception ex)
            {
                //Logging the error message.
                _logger.LogError(ex, "Error occurred while adding amenity.");

                //Returning the error message along with 500 HttpStatusCode.
                return new APIResponse<AmenityInsertResponseDTO>(HttpStatusCode.InternalServerError, "Amenity Creation Failed.", ex.Message);
            }
        }
    }
}
