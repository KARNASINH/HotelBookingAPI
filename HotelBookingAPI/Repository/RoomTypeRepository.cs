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

    }
}
