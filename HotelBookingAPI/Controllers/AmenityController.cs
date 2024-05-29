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
    }
}
