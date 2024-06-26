﻿using HotelBookingAPI.DTOs.RoomDTOs;
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

                //Return Http 500 Internal Server Error if any error occurs during the execution of the Action Method.
                return new APIResponse<List<RoomDetailsResponseDTO>>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }





        //API Endpoint to get a room by ID
        [HttpGet("{id}")]
        public async Task<APIResponse<RoomDetailsResponseDTO>> GetRoomById(int id)
        {
            //Logging the message that request is received for GetRoomById with the ID
            _logger.LogInformation($"Request Received for GetRoomById, id: {id}");

            //Try to get the room by ID
            try
            {
                //Calling the GetRoomByIdAsync method of RoomRepository to get the room by ID
                var response = await _roomRepository.GetRoomByIdAsync(id);

                //Checking if the response is null
                if (response == null)
                {
                    //Returning the response with the 404 status code and the error message
                    return new APIResponse<RoomDetailsResponseDTO>(HttpStatusCode.NotFound, "Room ID not found.");
                }

                //Returning the response with the 200 status code and the room data
                return new APIResponse<RoomDetailsResponseDTO>(response, "Room fetched successfully.");
            }
            //Catch the exception if any error occurs
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Room by ID {id}", id);

                //Return Http 500 Internal Server Error if any error occurs during the execution of the Action Method.
                return new APIResponse<RoomDetailsResponseDTO>(HttpStatusCode.InternalServerError, "Error fetching Room.", ex.Message);
            }
        }





        //API Endpoint to create a new room
        [HttpPost("Create")]
        public async Task<APIResponse<CreateRoomResponseDTO>> CreateRoom([FromBody] CreateRoomRequestDTO request)
        {
            //Logging the request received
            _logger.LogInformation("Request Received for CreateRoom: {@CreateRoomRequestDTO}", request);

            //Checking if the Data in the Request Body is valid and if model binding is successful or not
            if (!ModelState.IsValid)
            {
                //Logging the error if the data in the Request Body is invalid
                _logger.LogInformation("Invalid Data in the Request Body");

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<CreateRoomResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
            }
            //Try to create a new room
            try
            {
                //Calling the CreateRoomAsync method of RoomRepository to create a new room
                var response = await _roomRepository.CreateRoomAsync(request);

                //Logging the response received from the repository
                _logger.LogInformation("CreateRoom Response From Repository: {@CreateRoomResponseDTO}", response);

                //Checking if the room is created successfully
                if (response.IsCreated)
                {
                    //Returning the response with the 200 Http status code and the room data
                    return new APIResponse<CreateRoomResponseDTO>(response, response.Message);
                }

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<CreateRoomResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs
            catch (Exception ex)
            {
                //Logging the error if any error occurs in the try block
                _logger.LogError(ex, "Error adding new Room");

                //Returning the response with the 500 Http status code and the error message
                return new APIResponse<CreateRoomResponseDTO>(HttpStatusCode.InternalServerError, "Room Creation Failed.", ex.Message);
            }
        }






        //API Endpoint to update a room
        [HttpPut("Update/{id}")]
        public async Task<APIResponse<UpdateRoomResponseDTO>> UpdateRoom(int id, [FromBody] UpdateRoomRequestDTO request)
        {
            //Logging the request received
            _logger.LogInformation("Request Received for UpdateRoom {@UpdateRoomRequestDTO}", request);

            //Checking if the Data in the Request Body is valid and if model binding is successful or not
            if (!ModelState.IsValid)
            {
                //Logging the error if the data in the Request Body is invalid
                _logger.LogInformation("UpdateRoom Invalid Request Body");

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<UpdateRoomResponseDTO>(HttpStatusCode.BadRequest, "Invalid Request Body");
            }

            //Checking if the ID in the URL and the ID in the Request Body are same
            if (id != request.RoomID)
            {
                //Logging the error if the ID in the URL and the ID in the Request Body are different
                _logger.LogInformation("UpdateRoom Mismatched Room ID");

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<UpdateRoomResponseDTO>(HttpStatusCode.BadRequest, "Mismatched Room ID.");
            }

            //Try to update the room
            try
            {
                //Calling the UpdateRoomAsync method of RoomRepository to update the room
                var response = await _roomRepository.UpdateRoomAsync(request);

                //Checks if the room is updated successfully
                if (response.IsUpdated)
                {
                    //Returning the response with the 200 Http status code and the room data
                    return new APIResponse<UpdateRoomResponseDTO>(response, response.Message);
                }

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<UpdateRoomResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs
            catch (Exception ex)
            {
                //Logging the error if any error occurs in the try block
                _logger.LogError(ex, "Error Updating Room {id}", id);

                //Returning the response with the 500 Http status code and the error message
                return new APIResponse<UpdateRoomResponseDTO>(HttpStatusCode.InternalServerError, "Update Room Failed.", ex.Message);
            }
        }





        //API Endpoint to delete a room
        [HttpDelete("Delete/{id}")]
        public async Task<APIResponse<DeleteRoomResponseDTO>> DeleteRoom(int id)
        {
            //Logging the request received
            _logger.LogInformation($"Request Received for DeleteRoom, id: {id}");

            //Try to delete the room
            try
            {
                //Calling the DeleteRoomAsync method of RoomRepository to get the room id if present into the database
                var room = await _roomRepository.GetRoomByIdAsync(id);

                //Checking the room is in the database or not
                if (room == null)
                {
                    //Returning the response with the 404 Http status code and the error message
                    return new APIResponse<DeleteRoomResponseDTO>(HttpStatusCode.NotFound, "Room not found.");
                }
                //Calling the DeleteRoomAsync method of RoomRepository to delete the room
                var response = await _roomRepository.DeleteRoomAsync(id);

                //Checking if the room is deleted successfully
                if (response.IsDeleted)
                {
                    //Returning the response with the 200 Http status code and the room data
                    return new APIResponse<DeleteRoomResponseDTO>(response, response.Message);
                }

                //Returning the response with the 400 Http status code and the error message
                return new APIResponse<DeleteRoomResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs
            catch (Exception ex)
            {
                //Logging the error if any error occurs in the try block
                _logger.LogError(ex, "Error deleting Room {id}", id);

                //Returning the response with the 500 Http status code and the error message
                return new APIResponse<DeleteRoomResponseDTO>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }
    }

}
