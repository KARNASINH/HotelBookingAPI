using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
using System.Reflection.Metadata.Ecma335;

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




        //This method is used to assign a role to a user
        public async  Task<UserRoleResponseDTO> AssignRoleToUserAsync(UserRoleDTO userRole)
        {
            //Create an instance of UserRoleResponseDTO
            UserRoleResponseDTO userRoleResponseDTO = new UserRoleResponseDTO();

            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spAssignRoleToUser
            using var command = new SqlCommand("spAssignUserRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Add parameters to the command
            command.Parameters.AddWithValue("@UserID", userRole.UserID);
            command.Parameters.AddWithValue("@RoleID", userRole.RoleID);

            //Add an output parameter to get the error message if any error occurs
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            //Add the output parameter to the command
            command.Parameters.Add(errorMessageParam);

            //Open the connection
            await connection.OpenAsync();

            //Execute the command
            await command.ExecuteNonQueryAsync();

            //Get the error message from the output parameter
            var message = errorMessageParam.Value?.ToString();

            //Check if the error message is null or empty
            if (string.IsNullOrEmpty(message))
            {
                //Set the properties of the UserRoleResponseDTO
                userRoleResponseDTO.IsAssigned = true;
                userRoleResponseDTO.Message = "Role Assigned Successfully";

                //Return the UserRoleResponseDTO if the role is assigned successfully
                return userRoleResponseDTO;
            }
            else
            {
                //Set the properties of the UserRoleResponseDTO
                userRoleResponseDTO.IsAssigned = false;
                userRoleResponseDTO.Message = message;
                
                //Return the UserRoleResponseDTO if any error occurs
                return userRoleResponseDTO;
            }
        }




        //This method is used to list all the users from the database
        public async Task<List<UserResponseDTO>> ListAllUsersAsync(bool? isActive)
        {
            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();
        
            //Create a command to execute the stored procedure spListAllUsers
            using var command = new SqlCommand("spListAllUsers", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add a parameter to the command
            command.Parameters.AddWithValue("@IsActive", (object)isActive ?? DBNull.Value);
            
            //Open the connection
            await connection.OpenAsync();

            //Execute the command and get the data from the database
            using var reader = await command.ExecuteReaderAsync();

            //Create a list of UserResponseDTO
            var users = new List<UserResponseDTO>();

            //Read the data from the reader and add it to the list of users
            while (reader.Read())
            {
                //Create an instance of UserResponseDTO
                users.Add(new UserResponseDTO
                {
                    //Set the properties of the UserResponseDTO
                    UserID = reader.GetInt32("UserID"),
                    Email = reader.GetString("Email"),
                    IsActive = reader.GetBoolean("IsActive")
                });
            }

            //Return the list of users
            return users;
        }




        //This method is used to get a user by its ID
        public async Task<UserResponseDTO> GetUserByIdAsync(int userId)
        {
            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spGetUserByID
            using var command = new SqlCommand("spGetUserByID", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add a parameter to the command
            command.Parameters.AddWithValue("@UserID", userId);

            //Add an output parameter to get the error message if any error occurs
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255) 
            {
                Direction = ParameterDirection.Output
            };
            
            //Add the output parameter to the command
            command.Parameters.Add(errorMessageParam);

            //Open the connection
            await connection.OpenAsync();

            //Execute the command and get the data from the database
            using var reader = await command.ExecuteReaderAsync();

            //Read the data from the reader
            if (!reader.Read())
            {
                //Return null if the user is not found
                return null;
            }

            //Create an instance of UserResponseDTO
            var user = new UserResponseDTO
            {
                //Set the properties of the UserResponseDTO
                UserID = reader.GetInt32("UserID"),
                Email = reader.GetString("Email"),
                IsActive = reader.GetBoolean("IsActive")
            };

            //Return the User with data
            return user;
        }




        //This method is used to update the user details
        public async Task<UpdateUserResponseDTO> UpdateUserAsync(UpdateUserDTO user)
        {
            //Create an instance of UpdateUserResponseDTO
            UpdateUserResponseDTO updateUserResponseDTO = new UpdateUserResponseDTO()
            {
                //Set the UserID of the user form the passed UserDTO object
                UserId = user.UserID
            };

            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spUpdateUserInformation
            using var command = new SqlCommand("spUpdateUserInformation", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add parameters to the command
            command.Parameters.AddWithValue("@UserID", user.UserID);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.Password);
            command.Parameters.AddWithValue("@ModifiedBy", "System");

            //Add an output parameter to get the error message if any error occurs
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                //Set the direction of the parameter to output
                Direction = ParameterDirection.Output
            };

            //Add the output parameter to the command
            command.Parameters.Add(errorMessageParam);

            //Open the connection
            await connection.OpenAsync();

            //Execute the command
            await command.ExecuteNonQueryAsync();

            //Get the error message from the output parameter
            var message = errorMessageParam.Value?.ToString();

            //Check if the error message is null or empty
            if(string.IsNullOrEmpty(message))
            {
                //Set the properties of the UpdateUserResponseDTO
                updateUserResponseDTO.IsUpdated = true;
                updateUserResponseDTO.Message = "User Updated Successfully";                
            }
            else
            {
                //Set the properties of the UpdateUserResponseDTO
                updateUserResponseDTO.IsUpdated = false;
                updateUserResponseDTO.Message = message;                
            }

            //Return the UpdateUserResponseDTO
            return updateUserResponseDTO;
        }



        //This method is used to delete a user from the database.
        public async Task<DeleteUserResponseDTO> DeleteUserAsync(int userId)
        {
            //Create an instance of DeleteUserResponseDTO
            DeleteUserResponseDTO deleteUserResponseDTO = new DeleteUserResponseDTO();

            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spToggleUserActive
            using var command = new SqlCommand("spToggleUserActive", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add parameters to the command
            command.Parameters.AddWithValue("@UserID", userId);
            command.Parameters.AddWithValue("@IsActive", false);

            //Add an output parameter to get the error message if any error occurs duruing the execution of the stored procedure
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                //Set the direction of the parameter to output
                Direction = ParameterDirection.Output
            };

            //Add the output parameter to the command
            command.Parameters.Add(errorMessageParam);

            //Open the connection
            await connection.OpenAsync();

            //Execute the command
            await command.ExecuteNonQueryAsync();

            //Get the error message from the output parameter
            var message = errorMessageParam.Value?.ToString();

            //Check if the error message is null or empty
            if(!string.IsNullOrEmpty(message)) 
            {
                //Set the properties of the DeleteUserResponseDTO
                deleteUserResponseDTO.IsDeleted = false;
                deleteUserResponseDTO.Message = message;
            }
            else
            {
                //Set the properties of the DeleteUserResponseDTO
                deleteUserResponseDTO.IsDeleted = true;
                deleteUserResponseDTO.Message = "User Deleted Successfully";
            }

            //Return the DeleteUserResponseDTO
            return deleteUserResponseDTO;
        }


        //This method is used to login a user
        public async Task<LoginUserResponseDTO> LoginUserAsync(LoginUserDTO login)
        {
            //Create an instance of LoginUserResponseDTO
            LoginUserResponseDTO userLoginResponseDTO = new LoginUserResponseDTO();

            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spLoginUser
            using var command = new SqlCommand("spLoginUser", connection);

            //Set the command type to stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //Add parameters to the command
            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@PasswordHash", login.Password); // Ensure password is hashed

            //Add an output parameter to get the UserID of the user
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

            //Checking if the user is successfully logged in or not based on the value of the output parameter
            var success = userIdParam.Value != DBNull.Value && (int)userIdParam.Value > 0;

            //Get the error message from the output parameter
            var message = errorMessageParam.Value?.ToString();


            var userId = success ? Convert.ToInt32(userIdParam.Value) : -1;
            
            //Check if the user is successfully logged in
            if (success)
            {
                //Set the properties of the LoginUserResponseDTO
                userLoginResponseDTO.UserId = userId;
                userLoginResponseDTO.IsLogin = true;
                userLoginResponseDTO.Message = "Login Successful";
            }
            else
            {
            userLoginResponseDTO.IsLogin = false;
            userLoginResponseDTO.Message = message;
            }

            //Return the LoginUserResponseDTO
            return userLoginResponseDTO;
        }

        
        //This method is used to toggle the active status of a user
        //Only Admin access can have access to this feture.
        public async Task<(bool Success, string Message)> ToggleUserActiveAsync(int userId, bool isActive)
        {
            //Create a connection to the database using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure spToggleUserActive
            using var command = new SqlCommand("spToggleUserActive", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Add parameters to the command
            command.Parameters.AddWithValue("@UserID", userId);
            command.Parameters.AddWithValue("@IsActive", isActive);

            //Add an output parameter to get the error message if any error occurs
            var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

            //Add the output parameter to the command
            command.Parameters.Add(errorMessageParam);

            //Open the connection
            await connection.OpenAsync();

            //Execute the command
            await command.ExecuteNonQueryAsync();

            //Get the error message from the output parameter
            var message = errorMessageParam.Value?.ToString();

            //Check if the error message is null or empty
            var success = string.IsNullOrEmpty(message);

            //Return the success and message
            return (success, message);
        }
    }
}
