using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.RoomTypeDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //This class is used to interact with the database and perform CRUD operations on the RoomType table
    public class RoomTypeRepository
    {
        //Injecting SqlConnectionFactory object using DI.
        private readonly SqlConnectionFactory _connectionFactory;




        //Constructor to initialize SqlConnectionFactory object.
        public RoomTypeRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }




        
        //This method is used to retrieve all RoomTypes from the database.
        public async Task<List<RoomTypeDTO>> RetrieveAllRoomTypesAsync(bool? IsActive)
        {
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a new SqlCommand object to execute the stored procedure spGetAllRoomTypes.
            var command = new SqlCommand("spGetAllRoomTypes", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the IsActive parameter to the command.
            command.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);

            //Opening the connection.
            await connection.OpenAsync();

            //Executing the command and storing the result in a SqlDataReader object.
            using var reader = await command.ExecuteReaderAsync();

            //Creating a new list of RoomTypeDTO objects to store the result for RoomTypes.
            var roomTypes = new List<RoomTypeDTO>();

            //Reading the result from the SqlDataReader object and storing it in the list of RoomTypeDTO objects.
            while (reader.Read())
            {
                //Creating a new RoomTypeDTO object and setting its properties using the values from the SqlDataReader object.
                roomTypes.Add(new RoomTypeDTO
                {
                    RoomTypeID = reader.GetInt32("RoomTypeID"),
                    TypeName = reader.GetString("TypeName"),
                    AccessibilityFeatures = reader.GetString("AccessibilityFeatures"),
                    Description = reader.GetString("Description"),
                    IsActive = reader.GetBoolean("IsActive")
                });
            }

            //Returning the list of RoomTypeDTO objects.
            return roomTypes;
        }





        //This method is used to retrieve a RoomType by its ID from the database.
        public async Task<RoomTypeDTO> RetrieveRoomTypeByIdAsync(int RoomTypeID)
        {
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a new SqlCommand object to execute the stored procedure spGetRoomTypeById.
            var command = new SqlCommand("spGetRoomTypeById", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the RoomTypeID parameter to the command.
            command.Parameters.AddWithValue("@RoomTypeID", RoomTypeID);

            //Opening the connection.
            await connection.OpenAsync();

            //Executing the command and storing the result in a SqlDataReader object.
            using var reader = await command.ExecuteReaderAsync();

            //Checking if the SqlDataReader object has any rows.
            if (!reader.Read())
            {
                //Returning null if no rows are found.
                return null;
            }

            //Creating a new RoomTypeDTO object and setting its properties using the values from the SqlDataReader object.
            var roomType = new RoomTypeDTO
            {
                //Setting the RoomTypeDTO properties using the values from the SqlDataReader object.
                RoomTypeID = RoomTypeID,
                TypeName = reader.GetString("TypeName"),
                AccessibilityFeatures = reader.GetString("AccessibilityFeatures"),
                Description = reader.GetString("Description"),
                IsActive = reader.GetBoolean("IsActive")
            };

            //Returning the RoomTypeDTO object.
            return roomType;
        }





        //This method is used to create a new RoomType in the database.
        public async Task<CreateRoomTypeResponseDTO> CreateRoomType(CreateRoomTypeDTO request)
        {
            //Creating a new CreateRoomTypeResponseDTO object to store the response.
            CreateRoomTypeResponseDTO createRoomTypeResponseDTO = new CreateRoomTypeResponseDTO();
        
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();
            
            //Creating a new SqlCommand object to execute the stored procedure spCreateRoomType.
            var command = new SqlCommand("spCreateRoomType", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };
            
            //Adding the parameters to the command.
            command.Parameters.Add(new SqlParameter("@TypeName", request.TypeName));
            command.Parameters.Add(new SqlParameter("@AccessibilityFeatures", request.AccessibilityFeatures));
            command.Parameters.Add(new SqlParameter("@Description", request.Description));
            command.Parameters.Add(new SqlParameter("@CreatedBy", "System"));
            
            //Adding the output parameters to the command.
            var outputId = new SqlParameter("@NewRoomTypeID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var statusCode = new SqlParameter("@StatusCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
            
            //Adding the output parameters to the command.
            command.Parameters.Add(outputId);
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);

            //Trying to create a RoomType into the database.        
            try
            {
                //Opening the connection.
                await connection.OpenAsync();
                
                //Executing the command asynchronously to create a new RoomType.
                await command.ExecuteNonQueryAsync();
                
                //Checking the status code to determine if the RoomType was created successfully.
                if ((int)statusCode.Value == 0)
                {
                    //Setting the response properties.
                    createRoomTypeResponseDTO.Message = message.Value.ToString();
                    createRoomTypeResponseDTO.IsCreated = true;
                    createRoomTypeResponseDTO.RoomTypeId = (int)outputId.Value;
                
                    //Returning the response.
                    return createRoomTypeResponseDTO;
                }

                //Setting the response properties if the RoomType was not created successfully.
                createRoomTypeResponseDTO.Message = message.Value.ToString();
                createRoomTypeResponseDTO.IsCreated = false;
                
                //Returning the unsuccessful RoomTye creation response.
                return createRoomTypeResponseDTO;
            }
            //Catching any SqlException that may occur during the RoomType creation process.
            catch (SqlException ex)
            {
                //Setting the response properties if an exception occurs.
                createRoomTypeResponseDTO.Message = ex.Message;
                
                //Setting the response properties if the RoomType was not created successfully.
                createRoomTypeResponseDTO.IsCreated = false;
                
                //Returning the unsuccessful RoomTye creation response.
                return createRoomTypeResponseDTO;
            }
        }





        //This method is used to update an existing RoomType in the database.
        public async Task<UpdateRoomTypeResponseDTO> UpdateRoomType(UpdateRoomTypeDTO request)
        {
            //Creating a new UpdateRoomTypeResponseDTO object to store the response while updating the RoomType.
            UpdateRoomTypeResponseDTO updateRoomTypeResponseDTO = new UpdateRoomTypeResponseDTO()
            {
                //Setting the RoomTypeId property of the response object.
                RoomTypeId = request.RoomTypeID
            };
         
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();
            
            //Creating a new SqlCommand object to execute the stored procedure spUpdateRoomType.
            var command = new SqlCommand("spUpdateRoomType", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };
            
            //Adding the parameters to the command.
            command.Parameters.Add(new SqlParameter("@RoomTypeID", request.RoomTypeID));
            command.Parameters.Add(new SqlParameter("@TypeName", request.TypeName));
            command.Parameters.Add(new SqlParameter("@AccessibilityFeatures", request.AccessibilityFeatures));
            command.Parameters.Add(new SqlParameter("@Description", request.Description));
            command.Parameters.Add(new SqlParameter("@ModifiedBy", "System"));
            
            //Adding the output parameters to the command.
            var statusCode = new SqlParameter("@StatusCode", SqlDbType.Int)
            {
                //Setting the direction of the parameter to output.
                Direction = ParameterDirection.Output
            };
            
            //Adding the output parameters to the command.
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
            {
                //Setting the direction of the parameter to output.
                Direction = ParameterDirection.Output
            };
            
            //Adding the output parameters to the command.
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);
            
            //Trying to update the RoomType in the database.
            try
            {
                //Opening the connection.
                await connection.OpenAsync();

                //Executing the command asynchronously to update the RoomType.
                await command.ExecuteNonQueryAsync();
            
                //Setting the response properties using the output parameters.
                updateRoomTypeResponseDTO.Message = message.Value.ToString();
                updateRoomTypeResponseDTO.IsUpdated = (int)statusCode.Value == 0;
                
                //Returning the response.
                return updateRoomTypeResponseDTO;
            }
            //Catching any SqlException that may occur during the RoomType update process.
            catch (SqlException ex)
            {
                //Setting the response properties if an exception occurs.
                updateRoomTypeResponseDTO.Message = ex.Message;

                //Setting the response properties if the RoomType was not updated successfully.
                updateRoomTypeResponseDTO.IsUpdated = false;
                
                //Returning the unsuccessful RoomType update response.
                return updateRoomTypeResponseDTO;
            }
        }




        //This method is used to delete a RoomType from the database.
        public async Task<DeleteRoomTypeResponseDTO> DeleteRoomType(int RoomTypeID)
        {
            //Creating a new DeleteRoomTypeResponseDTO object to store the response while deleting the RoomType.
            DeleteRoomTypeResponseDTO deleteRoomTypeResponseDTO = new DeleteRoomTypeResponseDTO();
            
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();
            
            //Creating a new SqlCommand object to execute the stored procedure spToggleRoomTypeActive.
            var command = new SqlCommand("spToggleRoomTypeActive", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };
            
            //Adding the parameters to the command.
            command.Parameters.Add(new SqlParameter("@RoomTypeID", RoomTypeID));
            command.Parameters.AddWithValue("@IsActive", false);
            
            //Adding the output parameters to the command.
            var statusCode = new SqlParameter("@StatusCode", SqlDbType.Int)
            {
                //Setting the direction of the parameter to output.
                Direction = ParameterDirection.Output
            };
            
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            
            //Adding the output parameters to the command.
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);
            
            //Trying to delete the RoomType from the database.
            try
            {
                //Opening the connection.
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                
                //Setting the response properties using the output parameters.
                deleteRoomTypeResponseDTO.Message = "Room Type Deleted Successfully";
                deleteRoomTypeResponseDTO.IsDeleted = (int)statusCode.Value == 0;
                
                //Returning the response.
                return deleteRoomTypeResponseDTO;
            }
            //Catching any SqlException that may occur during the RoomType deletion process execution.
            catch (SqlException ex)
            {
                //Setting the response properties if an exception occurs.
                deleteRoomTypeResponseDTO.Message = ex.Message;
                deleteRoomTypeResponseDTO.IsDeleted = false;
                
                //Returning the unsuccessful RoomType deletion response.
                return deleteRoomTypeResponseDTO;
            }
        }




        //This method is used to toggle Active status of a RoomType in the database.
        public async Task<(bool Success, string Message)> ToggleRoomTypeActiveAsync(int RoomTypeID, bool IsActive)
        {
            //Creating a new SqlConnection object using the CreateConnection method of SqlConnectionFactory.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a new SqlCommand object to execute the stored procedure spToggleRoomTypeActive.
            using var command = new SqlCommand("spToggleRoomTypeActive", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the parameters to the command.
            command.Parameters.Add(new SqlParameter("@RoomTypeID", RoomTypeID));
            command.Parameters.AddWithValue("@IsActive", IsActive);

            //Adding the output parameters to the command.
            var statusCode = new SqlParameter("@StatusCode", SqlDbType.Int)
            {
                //Setting the direction of the parameter to output.
                Direction = ParameterDirection.Output
            };
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            //Adding the output parameters to the command.
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);

            //Opening the connection.
            await connection.OpenAsync();

            //Executing the command asynchronously to toggle the RoomType's active status.
            await command.ExecuteNonQueryAsync();

            //Getting the value of the message parameter.
            var ResponseMessage = message.Value.ToString();

            //Checking the status code to determine if the RoomType's active status was toggled successfully
            var success = (int)statusCode.Value == 0;

            //Returning the success status and message.
            return (success, ResponseMessage);
        }
    }
}
