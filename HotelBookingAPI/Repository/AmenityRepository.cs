using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.AmenityDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //This class is used to interact with the database and perform CRUD operations on the Amenity table
    public class AmenityRepository
    {
        //Creating SqlConnectionFactory object to establish connection with Database.
        private readonly SqlConnectionFactory _connectionFactory;




        //Injecting SqlConnectionFactory object using DI.
        public AmenityRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }





        //This method is used to fetch the details of the Amenity from the database.
        public async Task<List<AmenityDetailsDTO>> FetchAmenitiesAsync(bool? isActive)
        {
            //Creating SqlConnection object to establish connection with Database.
            using var connection = _connectionFactory.CreateConnection();

            //Creating SqlCommand object to execute the stored procedure spFetchAmenities.
            using var command = new SqlCommand("spFetchAmenities", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Adding parameters to the SqlCommand object.
            command.Parameters.AddWithValue("@IsActive", (object)isActive ?? DBNull.Value);

            //Creating SqlParameter object to get the output parameters from the stored procedure.
            var statusCode = new SqlParameter("@Status", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

            //Adding output parameters to the SqlCommand object.
            command.Parameters.Add(statusCode);
            command.Parameters.Add(message);

            try
            {
                //Opening the connection with the Database.
                await connection.OpenAsync();

                //Creating a list of AmenityDetailsDTO object to store the details of the Amenity.
                var amenities = new List<AmenityDetailsDTO>();

                //Executing the SqlCommand object to fetch the details of the Amenity from the database.
                using var reader = await command.ExecuteReaderAsync();

                //Reading the data from the SqlDataReader object.
                while (await reader.ReadAsync())
                {
                    //Adding the details of the Amenity to the list.
                    amenities.Add(new AmenityDetailsDTO
                    {
                        //Reading the data from the SqlDataReader object and storing it in the AmenityDetailsDTO object.
                        AmenityID = reader.GetInt32("AmenityID"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        IsActive = reader.GetBoolean("IsActive")
                    });
                }

                //Returning the list of AmenityDetailsDTO object.
                return amenities;

            }
            //Catching the exception if any error occurs while fetching the details of the Amenity from the database.
            catch (Exception ex)
            {
                //Throwing the exception.
                throw new Exception($"Error fetching amenities: {ex.Message}");
            }
        }

    }
}
