using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.RoomDTOs;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //This class is used to interact with the database and perform CRUD operations on the Room table 
    public class RoomRepository
    {
        //SqlConnectionFactory instance to create a connection to the database
        private readonly SqlConnectionFactory _connectionFactory;




        //This constructor is used to inject the SqlConnectionFactory instance
        public RoomRepository(SqlConnectionFactory context)
        {
            _connectionFactory = context;
        }





        //This method is used to create a new room in the database using a stored procedure
        public async Task<CreateRoomResponseDTO> CreateRoomAsync(CreateRoomRequestDTO request)
        {
            //Create a new SqlConnection instance using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a new SqlCommand instance to execute the stored procedure
            using var command = new SqlCommand("spCreateRoom", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add the parameters required by the stored procedure
            command.Parameters.AddWithValue("@RoomNumber", request.RoomNumber);
            command.Parameters.AddWithValue("@RoomTypeID", request.RoomTypeID);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@BedType", request.BedType);
            command.Parameters.AddWithValue("@ViewType", request.ViewType);
            command.Parameters.AddWithValue("@Status", request.Status);
            command.Parameters.AddWithValue("@IsActive", request.IsActive);
            command.Parameters.AddWithValue("@CreatedBy", "System");

            //Add the output parameters to retrieve the response data
            command.Parameters.Add("@NewRoomID", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add("@StatusCode", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
            
            //Open the connection and execute the stored procedure
            try
            {
                //Open the connection asynchronously
                await connection.OpenAsync();

                //Execute the stored procedure asynchronously
                await command.ExecuteNonQueryAsync();

                //Retrieve the output parameters and create a response object
                var outputRoomID = command.Parameters["@NewRoomID"].Value;             

                //Safely handle potential DBNull values
                var newRoomID = outputRoomID != DBNull.Value ? Convert.ToInt32(outputRoomID) : 0;

                //Return the CreateRoomResponseDTO object with the response data
                return new CreateRoomResponseDTO
                {
                    //Set the properties of the response object
                    RoomID = newRoomID,
                    IsCreated = (int)command.Parameters["@StatusCode"].Value == 0,
                    Message = (string)command.Parameters["@Message"].Value
                };
            }
            //Catch any exceptions that occur during the execution of the stored procedure
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error creating room: {ex.Message}", ex);
            }            
        }





        //This method is used to update an existing room in the database using a stored procedure
        public async Task<UpdateRoomResponseDTO> UpdateRoomAsync(UpdateRoomRequestDTO request)
        {
            //Create a new SqlConnection instance using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a new SqlCommand instance to execute the stored procedure
            using var command = new SqlCommand("spUpdateRoom", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add the parameters required by the stored procedure
            command.Parameters.AddWithValue("@RoomID", request.RoomID);
            command.Parameters.AddWithValue("@RoomNumber", request.RoomNumber);
            command.Parameters.AddWithValue("@RoomTypeID", request.RoomTypeID);
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@BedType", request.BedType);
            command.Parameters.AddWithValue("@ViewType", request.ViewType);
            command.Parameters.AddWithValue("@Status", request.Status);
            command.Parameters.AddWithValue("@IsActive", request.IsActive);
            command.Parameters.AddWithValue("@ModifiedBy", "System");

            //Add the output parameters to retrieve the response data
            command.Parameters.Add("@StatusCode", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            //Open the connection and execute the stored procedure
            try
            {
                //Open the connection asynchronously
                await connection.OpenAsync();

                //Execute the stored procedure asynchronously
                await command.ExecuteNonQueryAsync();

                //Return the UpdateRoomResponseDTO object with the response data
                return new UpdateRoomResponseDTO
                {
                    //Set the properties of the response object
                    RoomId = request.RoomID,
                    IsUpdated = (int)command.Parameters["@StatusCode"].Value == 0,
                    Message = (string)command.Parameters["@Message"].Value
                };
            }
            //Catch any exceptions that occur during the execution of the stored procedure
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error updating room: {ex.Message}", ex);
            }
        }






        //This method is used to delete an existing room in the database using a stored procedure
        public async Task<DeleteRoomResponseDTO> DeleteRoomAsync(int roomId)
        {
            //Create a new SqlConnection instance using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure
            using var command = new SqlCommand("spDeleteRoom", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add the parameter required by the stored procedure
            command.Parameters.AddWithValue("@RoomID", roomId);

            //Add the output parameters to retrieve the response data
            command.Parameters.Add("@StatusCode", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            //Try to open the connection and execute the stored procedure
            try
            {
                //Open the connection asynchronously
                await connection.OpenAsync();

                //Execute the stored procedure asynchronously
                await command.ExecuteNonQueryAsync();

                //Return the DeleteRoomResponseDTO object with the response data
                return new DeleteRoomResponseDTO
                {
                    //Set the properties of the response object
                    IsDeleted = (int)command.Parameters["@StatusCode"].Value == 0,
                    Message = (string)command.Parameters["@Message"].Value
                };
            }
            //Catch any exceptions that occur during the execution of Try block
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error deleting room: {ex.Message}", ex);
            }
        }






        //This method is used to retrieve a room by its ID from the database using a stored procedure
        public async Task<RoomDetailsResponseDTO> GetRoomByIdAsync(int roomId)
        {
            //Create a new SqlConnection instance using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure
            using var command = new SqlCommand("spGetRoomById", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add the parameter required by the stored procedure
            command.Parameters.AddWithValue("@RoomID", roomId);

            //Try to open the connection and execute the stored procedure
            try
            {
                //Open the connection asynchronously
                await connection.OpenAsync();

                //Execute the stored procedure asynchronously
                using var reader = await command.ExecuteReaderAsync();

                //Check if the reader has any rows
                if (await reader.ReadAsync())
                {
                    //Return a new RoomDetailsResponseDTO object with the data from the reader
                    return new RoomDetailsResponseDTO
                    {
                        //Set the properties of the response object
                        RoomID = reader.GetInt32("RoomID"),
                        RoomNumber = reader.GetString("RoomNumber"),
                        RoomTypeID = reader.GetInt32("RoomTypeID"),
                        Price = reader.GetDecimal("Price"),
                        BedType = reader.GetString("BedType"),
                        ViewType = reader.GetString("ViewType"),
                        Status = reader.GetString("Status"),
                        IsActive = reader.GetBoolean("IsActive")
                    };
                }
                //If the reader has no rows, return null
                else
                {
                    return null;
                }
            }
            //Catch any exceptions that occur during the execution of Try block
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error retrieving room by ID: {ex.Message}", ex);
            }
        }






        //This method is used to retrieve all rooms from the database using a stored procedure
        //Here, you can omit the optional parameters which are RoomTypeID and Status
        public async Task<List<RoomDetailsResponseDTO>> GetAllRoomsAsync(GetAllRoomsRequestDTO request)
        {
            //Create a new SqlConnection instance using the SqlConnectionFactory
            using var connection = _connectionFactory.CreateConnection();

            //Create a command to execute the stored procedure
            using var command = new SqlCommand("spGetAllRoom", connection)
            {
                //Set the command type to stored procedure
                CommandType = CommandType.StoredProcedure
            };

            //Add the RoomTypeID parameters to the command
            command.Parameters.Add(new SqlParameter("@RoomTypeID", SqlDbType.Int)
            {
                //Check if the RoomTypeID is null or not
                Value = request.RoomTypeID.HasValue ? (object)request.RoomTypeID.Value : DBNull.Value
            });

            //Add the Status parameter to the command
            command.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50)
            {
                //Check if the Status is null or empty
                Value = string.IsNullOrEmpty(request.Status) ? DBNull.Value : (object)request.Status
            });

            //Try to open the connection and execute the stored procedure
            try
            {
                //Open the connection asynchronously
                await connection.OpenAsync();

                //Execute the stored procedure asynchronously
                var rooms = new List<RoomDetailsResponseDTO>();

                //Read the results from the stored procedure
                using var reader = await command.ExecuteReaderAsync();

                //Iterate over the results and create a list of RoomDetailsResponseDTO objects
                while (await reader.ReadAsync())
                {
                    //Add all available RoomDetailsResponseDTO object to the list of rooms
                    rooms.Add(new RoomDetailsResponseDTO
                    {
                        RoomID = reader.GetInt32("RoomID"),
                        RoomNumber = reader.GetString("RoomNumber"),
                        RoomTypeID = reader.GetInt32("RoomTypeID"),
                        Price = reader.GetDecimal("Price"),
                        BedType = reader.GetString("BedType"),
                        ViewType = reader.GetString("ViewType"),
                        Status = reader.GetString("Status"),
                        IsActive = reader.GetBoolean("IsActive")
                    });
                }
                //Return the list of RoomDetailsResponseDTO objects
                return rooms;
            }
            //Catch any exceptions that occur during the execution of Try block
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error retrieving all rooms: {ex.Message}", ex);
            }
        }
    }
}

