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
    }
}
