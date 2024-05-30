using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.RoomAmenityDTOs;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //This class is used to interact with the database and perform CRUD operations on the RoomAmenity table
    public class RoomAmenityRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;






        public RoomAmenityRepository(SqlConnectionFactory context)
        {
            _connectionFactory = context;
        }







        //This method is used to fetch all the amenities based on  RoomTypeID from the database.
        public async Task<List<AmenityResponseDTO>> FetchRoomAmenitiesByRoomTypeIdAsync(int roomTypeId)
        {
            //Creating a list of AmenityResponseDTO to store the response from the database.
            var response = new List<AmenityResponseDTO>();

            //Creating a connection object using the CreateConnection method from the SqlConnectionFactory class.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a command object to execute the stored procedure spFetchRoomAmenitiesByRoomTypeID.
            using var command = new SqlCommand("spFetchRoomAmenitiesByRoomTypeID", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Adding the RoomTypeID as a parameter to the command object.
            command.Parameters.AddWithValue("@RoomTypeID", roomTypeId);


            //Opening the connection.
            await connection.OpenAsync();

            //Executing the command and storing the response in a reader object.
            using var reader = await command.ExecuteReaderAsync();

            //Reading the response from the reader object and storing it in the response list.
            while (await reader.ReadAsync())
            {
                //Adding the response to the response list.
                response.Add(new AmenityResponseDTO
                {
                    AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                });
            }

            //Returning the response list.
            return response;
        }







        //This method is used to fetch all the RoomTypes based on AmenityID from the database.
        public async Task<List<RoomTypeResponse>> FetchRoomTypesByAmenityIdAsync(int amenityId)
        {
            //Creating a list of RoomTypeResponse to store the response from the database.
            var response = new List<RoomTypeResponse>();

            //Creating a connection object using the CreateConnection method from the SqlConnectionFactory class.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a command object to execute the stored procedure spFetchRoomTypesByAmenityID.
            using var command = new SqlCommand("spFetchRoomTypesByAmenityID", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the AmenityID as a parameter to the command object.
            command.Parameters.AddWithValue("@AmenityID", amenityId);

            //Opening the connection.
            await connection.OpenAsync();

            //Executing the command and storing the response in a reader object.
            using var reader = await command.ExecuteReaderAsync();

            //Reading the response from the reader object and storing it in the response list.
            while (await reader.ReadAsync())
            {
                //Adding the response to the response list.
                response.Add(new RoomTypeResponse
                {
                    //Reading the response from the reader object and storing it in the response list.
                    RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                    TypeName = reader.GetString(reader.GetOrdinal("TypeName")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    AccessibilityFeatures = reader.GetString(reader.GetOrdinal("AccessibilityFeatures")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                });
            }

            //Returning the response list.
            return response;
        }







        //This method is used to add a new RoomAmenity to the database.
        public async Task<RoomAmenityResponseDTO> AddRoomAmenityAsync(RoomAmenityDTO input)
        {
            //Creating a connection object using the CreateConnection method from the SqlConnectionFactory class.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a command object to execute the stored procedure spAddRoomAmenity.
            using var command = new SqlCommand("spAddRoomAmenity", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the RoomTypeID and AmenityID as parameters to the command object.
            command.Parameters.AddWithValue("@RoomTypeID", input.RoomTypeID);
            command.Parameters.AddWithValue("@AmenityID", input.AmenityID);

            //Adding the output parameters to the command object.
            var statusParam = new SqlParameter("@Status", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

            //Adding the output parameters to the command object.
            command.Parameters.Add(statusParam);
            command.Parameters.Add(messageParam);

            //Opening the connection.
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            //Returning the response from the database.
            return new RoomAmenityResponseDTO
            {
                //Setting up the properties of the response object.
                IsSuccess = (bool)statusParam.Value,
                Message = (string)messageParam.Value
            };
        }








        //This method is used to delete a RoomAmenity from the database.
        public async Task<RoomAmenityResponseDTO> DeleteRoomAmenityAsync(RoomAmenityDTO input)
        {
            //Creating a connection object using the CreateConnection method from the SqlConnectionFactory class.
            using var connection = _connectionFactory.CreateConnection();

            //Creating a command object to execute the stored procedure spDeleteRoomAmenity.
            using var command = new SqlCommand("spDeleteSingleRoomAmenity", connection)
            {
                //Setting the command type to stored procedure.
                CommandType = CommandType.StoredProcedure
            };

            //Adding the RoomTypeID and AmenityID as parameters to the command object.
            command.Parameters.AddWithValue("@RoomTypeID", input.RoomTypeID);
            command.Parameters.AddWithValue("@AmenityID", input.AmenityID);

            //Adding the output parameters to the command object.
            var statusParam = new SqlParameter("@Status", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

            //Adding the output parameters to the command object.
            command.Parameters.Add(statusParam);
            command.Parameters.Add(messageParam);

            //Opening the connection.
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            //Returning the response from the database.
            return new RoomAmenityResponseDTO
            {
                //Setting up the properties of the response object.
                IsSuccess = (bool)statusParam.Value,
                Message = (string)messageParam.Value
            };
        }
    }
}
