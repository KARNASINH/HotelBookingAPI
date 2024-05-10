using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.UserDTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //UserRepository class is used to interact with the database to perform CRUD operations on the User table
    public class UserRepository
    {
        //SqlConnectionFactory instance to create a connection to the database
        private readonly SqlConnectionFactory _connectionFactory;

        //This constructor is used to inject the SqlConnectionFactory instance
        public UserRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        //This method is used to add a new user to the database
        public async Task<CreateUserResponseDTO> AddUserAsync(CreateUserDTO user)
        {
            //Create an instance of CreateUserResponseDTO
            CreateUserResponseDTO createUserResponseDTO = new CreateUserResponseDTO();
        
            //Create a connection to the database
            using var connection = _connectionFactory.CreateConnection();
            
            //Create a command to execute the stored procedure spAddUser
            using var command = new SqlCommand("spAddUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            //Add parameters to the command
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.Password);            
            command.Parameters.AddWithValue("@CreatedBy", "System");
            
            //Add an output parameter to get the UserID of the newly created user
            var userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            
            //Add an output parameter to get the error message if any error occurs
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            
            //Add the output parameters to the command
            command.Parameters.Add(userIdParam);            
            command.Parameters.Add(errorMessageParam);
            
            //Open the connection
            await connection.OpenAsync();
            
            //Execute the command
            await command.ExecuteNonQueryAsync();
            
            //Get the UserID and error message from the output parameters
            var UserId = (int)userIdParam.Value;
            var message = errorMessageParam.Value?.ToString();
            
            //Check if the UserID is greater than 0
            if (UserId != -1)
            {
                //Set the properties of the CreateUserResponseDTO
                createUserResponseDTO.UserId = UserId;
                createUserResponseDTO.IsCreated = true;
                createUserResponseDTO.Message = "User Created Successfully";
                
                //Return the CreateUserResponseDTO if the user is created successfully
                return createUserResponseDTO;
            }

            //Set the properties of the CreateUserResponseDTO
            createUserResponseDTO.IsCreated = false;
            createUserResponseDTO.Message = message ?? "An unknown error occurred while creating the user.";
            
            //Return the CreateUserResponseDTO if any error occurs
            return createUserResponseDTO;
        }
    }
}
