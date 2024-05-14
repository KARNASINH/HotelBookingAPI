using HotelBookingAPI.DTOs.UserDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    //API Controller for User which holds all the endpoints related to User operations.
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        //UserRepository instance to access the User data.
        private readonly UserRepository _userRepository;

        
        
        //Logger instance to log the information or errors.
        private readonly ILogger<UserController> _logger;



        //Constructor to initialize the UserRepository and Logger objects.
        public UserController(UserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        //API Endpoint to add a new user.
        [HttpPost("AddUser")]
        public async Task<APIResponse<CreateUserResponseDTO>> AddUser(CreateUserDTO createUserDTO)
        {
            //Log the request received for AddUser.
            _logger.LogInformation("Request Received for AddUser: {@CreateUserDTO}", createUserDTO);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid Data in the Requrest Body");

                //Return Bad Request if the request body is invalid.
                return new APIResponse<CreateUserResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
            }

            //Try to add the user to the database.
            try
            {
                //Call the AddUserAsync method from UserRepository to add the user.
                var response = await _userRepository.AddUserAsync(createUserDTO);

                _logger.LogInformation("AddUser Response From Repository: {@CreateUserResponseDTO}", response);

                //Check if the user is created successfully.
                if (response.IsCreated)
                {
                    //Return the response with the user details.
                    return new APIResponse<CreateUserResponseDTO>(response, response.Message);
                }

                //Return the response with the error message.
                return new APIResponse<CreateUserResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs.
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new user with email {Email}", createUserDTO.Email);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<CreateUserResponseDTO>(HttpStatusCode.InternalServerError, "Registration Failed.", ex.Message);
            }
        }



        //API Endpoint to assign a role to a user.
        [HttpPost("AssignRole")]
        public async Task<APIResponse<UserRoleResponseDTO>> AssignRole(UserRoleDTO userRoleDTO)
        {
            //Log the request received for AssignRole.
            _logger.LogInformation("Request Received for AssignRole: {@UserRoleDTO}", userRoleDTO);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                //Return Bad Request if the request body is invalid.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return Bad Request if the request body is invalid.
                return new APIResponse<UserRoleResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
            }
            try
            {
                //Call the AssignRoleToUserAsync method from UserRepository to assign the role to the user.
                var response = await _userRepository.AssignRoleToUserAsync(userRoleDTO);

                //Log the response received from the repository.
                _logger.LogInformation("AssignRole Response From Repository: {@UserRoleResponseDTO}", response);

                //Check if the role is assigned successfully.
                if (response.IsAssigned)
                {
                    //Returb the response with Data and Message.
                    return new APIResponse<UserRoleResponseDTO>(response, response.Message);
                }

                //Return the response with the error message.
                return new APIResponse<UserRoleResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error assigning role {RoleID} to user {UserID}", userRoleDTO.RoleID, userRoleDTO.UserID);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<UserRoleResponseDTO>(HttpStatusCode.InternalServerError, "Role Assigned Failed.", ex.Message);
            }
        }



        //API Endpoint to get all the users.
        [HttpGet("AllUsers")]
        public async Task<APIResponse<List<UserResponseDTO>>> GetAllUsers(bool? isActive = null)
        {
            //Log the request received for GetAllUsers.
            _logger.LogInformation($"Request Received for GetAllUsers, IsActive: {isActive}");

            //Try to get all the users from the database.
            try
            {
                //Call the ListAllUsersAsync method from UserRepository to get all the users.
                var users = await _userRepository.ListAllUsersAsync(isActive);

                //Return the response with the list of users and meaningful message.
                return new APIResponse<List<UserResponseDTO>>(users, "Retrieved all Users Successfully.");
            }

            //Catch the exception if any error occurs during the execution of the Action Method.
            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error listing users");

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<List<UserResponseDTO>>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }



        //API Endpoint to update the User details based on the User ID.
        [HttpPut("Update/{id}")]
        public async Task<APIResponse<UpdateUserResponseDTO>> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            //Log the request received for UpdateUser.
            _logger.LogInformation("Request Received for UpdateUser {@UpdateUserDTO}", updateUserDTO);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                //Log the Bad Request details if the request body is invalid.
                _logger.LogInformation("UpdateUser Invalid Request Body");

                //Return Bad Request if the request body is invalid.
                return new APIResponse<UpdateUserResponseDTO>(HttpStatusCode.BadRequest, "Invalid Request Body");
            }

            //Check if the User ID in the request URL and the User ID in the request body are same.
            if (id != updateUserDTO.UserID)
            {
                //Log the Mismatched User ID message.
                _logger.LogInformation("UpdateUser Mismatched User ID.");

                //Return Bad Request if the User ID in the request URL and the User ID in the request body are different.
                return new APIResponse<UpdateUserResponseDTO>(HttpStatusCode.BadRequest, "Mismatched User ID.");
            }
            try
            {
                //Call the UpdateUserAsync method from UserRepository to update the user.
                var response = await _userRepository.UpdateUserAsync(updateUserDTO);

                //Check if the user is updated successfully.
                if (response.IsUpdated)
                {
                    //Return the response with the updated user details and meaningful message.
                    return new APIResponse<UpdateUserResponseDTO>(response, response.Message);
                }

                //Return the response with the error message if the user is not updated.
                return new APIResponse<UpdateUserResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }
            //Catch the exception if any error occurs during the execution of the Action Method.
            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error updating user {UserID}", updateUserDTO.UserID);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<UpdateUserResponseDTO>(HttpStatusCode.InternalServerError, "Update Failed.", ex.Message);
            }
        }


        //API Endpoint to delete the User based on the User ID.
        [HttpDelete("Delete/{id}")]
        public async Task<APIResponse<DeleteUserResponseDTO>> DeleteUser(int id)
        {
            //Log the request received for DeleteUser.
            _logger.LogInformation($"Request Received for DeleteUser, Id: {id}");

            //Try to delete the user from the database.
            try
            {
                //Call the DeleteUserAsync method from UserRepository to delete the user.
                var user = await _userRepository.GetUserByIdAsync(id);

                //Check if the user is found in the database.
                if (user == null)
                {
                    //Return Not Found if the user is not found in the database.
                    return new APIResponse<DeleteUserResponseDTO>(HttpStatusCode.NotFound, "User not found.");
                }

                //Call the DeleteUserAsync method from UserRepository to delete the user.
                var response = await _userRepository.DeleteUserAsync(id);

                //Check if the user is deleted successfully.
                if (response.IsDeleted)
                {
                    //Return the response with the deleted user details and meaningful message.
                    return new APIResponse<DeleteUserResponseDTO>(response, response.Message);
                }

                //Return the response with the error message if the user is not deleted.
                return new APIResponse<DeleteUserResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }

            //Catch the exception if any error occurs during the execution of the Action Method.
            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error deleting user {UserID}", id);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<DeleteUserResponseDTO>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }


        
        
        //APi Endpoint to login the user.
        [HttpPost("Login")]
        public async Task<APIResponse<LoginUserResponseDTO>> LoginUser([FromBody] LoginUserDTO loginUserDTO)
        {
            //Log the request received for LoginUser.
            _logger.LogInformation("Request Received for LoginUser {@LoginUserDTO}", loginUserDTO);

            //Check if the request body is valid.
            if (!ModelState.IsValid)
            {
                //Log the Bad Request details if the request body is invalid.
                return new APIResponse<LoginUserResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Requrest Body");
            }

            //Try to login the user.
            try
            {
                //Call the LoginUserAsync method from UserRepository to login the user.
                var response = await _userRepository.LoginUserAsync(loginUserDTO);

                //Check if the user is logged in successfully.
                if (response.IsLogin)
                {
                    //Return the response with the user details and meaningful message.
                    return new APIResponse<LoginUserResponseDTO>(response, response.Message);
                }

                //Return the response with the error message if the user is not logged in suffessfully.
                return new APIResponse<LoginUserResponseDTO>(HttpStatusCode.BadRequest, response.Message);
            }

            //Catch the exception if any error occurs during the execution of the Action Method.
            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error logging in user with email {Email}", loginUserDTO.Email);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return new APIResponse<LoginUserResponseDTO>(HttpStatusCode.InternalServerError, "Login failed.", ex.Message);
            }
        }




        //API Endpoint to make user Active or Inactive based on the given boolean value.
        //Pass 0 to make the user Inactive.
        //Pass 1 to make the user Active.
        [HttpPost("ToggleActive")]
        public async Task<IActionResult> ToggleActive(int userId, bool isActive)
        {
            //Try to toggle the active status of the user.
            try
            {
                //Call the ToggleUserActiveAsync method from UserRepository to toggle the active status of the user.
                var result = await _userRepository.ToggleUserActiveAsync(userId, isActive);

                //Check if the active status is updated successfully.
                if (result.Success)
                    //Return the response with Http Status Code 200 and meaningful message.
                    return Ok(new { Message = "User activation status updated successfully." });
                else
                    //Return the response with Http Status Code 400 and error message.
                    return BadRequest(new { Message = result.Message });
            }

            catch (Exception ex)
            {
                //Log the error if any error occurs during the execution of the Action Method.
                _logger.LogError(ex, "Error toggling active status for user {UserID}", userId);

                //Return Internal Server Error if any error occurs during the execion of the Action Method.
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
