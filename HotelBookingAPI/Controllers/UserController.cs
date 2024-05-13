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
    public class UserController : Controller
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

    }
}
