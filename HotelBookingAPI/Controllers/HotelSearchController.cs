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

                    //Return the  Bad Request response with the error message.
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







        //This endpoint is used to search the Hotels based on the ViewType.
        [HttpGet("ViewType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByViewType(string viewType)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the ViewType is empty or not.
                if (string.IsNullOrEmpty(viewType))
                {
                    //Log the error message.
                    _logger.LogInformation("ViewType is Empty.");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "ViewType is Empty.");
                }

                //Call the SearchByViewTypeAsync method to get the rooms based on the ViewType.
                var rooms = await _hotelSearchRepository.SearchByViewTypeAsync(viewType);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by ViewType Successful.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Rooms found for the given ViewType name.");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms by view type");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by ViewType.", ex.Message);
            }
        }





        //This endpoint is used to search the Hotels based on the Amenity Name.
        [HttpGet("Amenities")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByAmenities(string amenityName)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the AmenityName is empty or not.
                if (string.IsNullOrEmpty(amenityName))
                {
                    //Log the error message.
                    _logger.LogInformation("Amenity Name is Empty");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Amenity Name is Empty");
                }

                //Call the SearchByAmenitiesAsync method to get the rooms based on the Amenity Name.
                var rooms = await _hotelSearchRepository.SearchByAmenitiesAsync(amenityName);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {   
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Successfully get the rooms by amenit name.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Record Found.");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms by amenity name.");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by amenity name.", ex.Message);
            }
        }





        //This endpoint is used to search the Hotels based on the RoomType ID.
        [HttpGet("RoomsByType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchRoomsByRoomTypeID(int roomTypeID)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the RoomTypeID is less than or equal to 0.
                if (roomTypeID <= 0)
                {
                    //Log the error message.
                    _logger.LogInformation($"Invalid Room Type ID, {roomTypeID}");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Room Type ID, {roomTypeID}");
                }

                //Call the SearchRoomsByRoomTypeIDAsync method to get the rooms based on the RoomType ID.
                var rooms = await _hotelSearchRepository.SearchRoomsByRoomTypeIDAsync(roomTypeID);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by room type ID Successful.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Record Found.");
            }

            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms by Room Type ID.");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by room type ID.", ex.Message);
            }
        }





        //This endpoint is used to get the Room Details for the given RoomID.
        [HttpGet("RoomDetails")]
        public async Task<APIResponse<RoomDetailsWithAmenitiesSearchDTO>> GetRoomDetailsWithAmenitiesByRoomID(int roomID)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the RoomID is less than or equal to 0.
                if (roomID <= 0)
                {
                    //Log the error message.
                    _logger.LogInformation($"Invalid Room ID, {roomID}");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.BadRequest, $"Invalid Room ID, {roomID}");
                }

                //Call the GetRoomDetailsWithAmenitiesByRoomIDAsync method to get the room details based on the RoomID.
                var roomDetails = await _hotelSearchRepository.GetRoomDetailsWithAmenitiesByRoomIDAsync(roomID);

                //Check if the room details are available or not.
                if (roomDetails != null)
                {
                    //Returning the response with the room details data and 200 status code.
                    return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(roomDetails, "Fetched the Room Details with Amenities for the given RoomID.");
                }
                else
                {
                    //Returning the response with the 404 status code if no room details are available.
                   return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.NotFound, "No Record Found");
                }
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, $"Failed to get room details with amenities for RoomID {roomID}");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.InternalServerError, "An error occurred while fetching room details with amenities.", ex.Message);
            }
        }





        //This endpoint is used to get the Amenities for the given RoomID.
        [HttpGet("RoomAmenities")]
        public async Task<APIResponse<List<AmenitySearchDTO>>> GetRoomAmenitiesByRoomID(int roomID)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the RoomID is less than or equal to 0.
                if (roomID <= 0)
                {
                    //Log the error message.
                    _logger.LogInformation($"Invalid Room ID, {roomID}");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Room ID, {roomID}");
                }

                //Call the GetRoomAmenitiesByRoomIDAsync method to get the amenities based on the RoomID.
                var amenities = await _hotelSearchRepository.GetRoomAmenitiesByRoomIDAsync(roomID);

                //Check if the amenities are available or not.
                if (amenities != null && amenities.Count > 0)
                {
                    //Returning the response with the amenities data and 200 status code.
                    return new APIResponse<List<AmenitySearchDTO>>(amenities, "Fetch Amenities for the given RoomID.");
                }

                //Returning the response with the 404 status code if no amenities are available.
                return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.NotFound, "No Record Found");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, $"Failed to get amenities for RoomID {roomID}");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching room amenities.", ex.Message);
            }
        }






        //This endpoint is used to get the Rooms based on the Minimum Rating.
        [HttpGet("ByRating")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByMinRating(float minRating)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the MinRating is less than or equal to 0 and greater than 5.
                if (minRating <= 0 && minRating > 5)
                {
                    //Log the error message.
                    _logger.LogInformation($"Invalid Rating: {minRating}");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Rating: {minRating}");
                }

                //Call the SearchByMinRatingAsync method to get the rooms based on the Minimum Rating.
                var rooms = await _hotelSearchRepository.SearchByMinRatingAsync(minRating);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Successfully fetched rooms by minimum rating.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Record Found");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to get rooms by minimum rating");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by minimum rating.", ex.Message);
            }
        }






        //This endpoint is used to get the Rooms based on the different combinations of the Search Criteria.
        //User can none, any or all of the search criteria to get the Room details.
        //These are the search criteria: MinPrice, MaxPrice, RoomTypeName, AmenityName, ViewType.
        [HttpGet("CustomSearch")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchCustomCombination([FromQuery] CustomHotelSearchCriteriaDTO criteria)
        {
            //Trying to execute the code inside the try block.
            try
            {
                //Check if the Model is valid or not.
                if (!ModelState.IsValid)
                {
                    //Log the error message.
                    _logger.LogInformation("Invalid Data in the Request Body");

                    //Return the response with 400 Bad Request and the error message.
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body.");
                }

                //Call the SearchCustomCombinationAsync method to get the rooms based on the Custom Search Criteria.
                var rooms = await _hotelSearchRepository.SearchCustomCombinationAsync(criteria);

                //Check if the rooms are available or not.
                if (rooms != null && rooms.Count > 0)
                {
                    //Returning the response with the rooms data and 200 status code.
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch Room By Custom Search Successful.");
                }

                //Returning the response with the 404 status code if no rooms are available.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.NotFound, "No Record Found");
            }
            //Catch the exception if any error occurs during the execution of the try block.
            catch (Exception ex)
            {
                //Log the error message.
                _logger.LogError(ex, "Failed to perform custom search");

                //Return Http 500 Internal Server Error if any error occurs during the execution of the try block.
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred during the custom search.", ex.Message);
            }
        }
    }
}
