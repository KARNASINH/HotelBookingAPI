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






        //This method is used to fetch the details of the Amenity by AmenityID from the database.
        public async Task<AmenityDetailsDTO?> FetchAmenityByIdAsync(int amenityId)
        {
            //Creating SqlConnection object to establish connection with Database.
            using var connection = _connectionFactory.CreateConnection();

            //Creating SqlCommand object to execute the stored procedure spFetchAmenityByID.
            using var command = new SqlCommand("spFetchAmenityByID", connection);

            //Setting the CommandType of the SqlCommand object to StoredProcedure.
            command.CommandType = CommandType.StoredProcedure;

            //Adding parameters to the SqlCommand object.
            command.Parameters.AddWithValue("@AmenityID", amenityId);

            //Try block to catch any exceptions that occur during the execution of the code inside the block.
            try
            {
                //Creating SqlParameter object to get the output parameters from the stored procedure.
                await connection.OpenAsync();

                //Executing the SqlCommand object to fetch the details of the Amenity by AmenityID from the database.
                var reader = await command.ExecuteReaderAsync();

                //Reading the data from the SqlDataReader object.
                if (await reader.ReadAsync())
                {
                    //Returning the AmenityDetailsDTO object.
                    return new AmenityDetailsDTO
                    {
                        AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                }
                else
                {
                    return null;
                }
            }
            //Catch any exceptions that occur during the execution of Try block
            catch (Exception ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error retrieving Amenity by ID: {ex.Message}", ex);
            }

        }






        //This method is used to add a new Amenity to the database.
        public async Task<AmenityInsertResponseDTO> AddAmenityAsync(AmenityInsertDTO amenity)
        {
            //Creating AmenityInsertResponseDTO object to store the response of the AddAmenity operation.
            AmenityInsertResponseDTO amenityInsertResponseDTO = new AmenityInsertResponseDTO();

            //Creating SqlConnection object to establish connection with Database.
            using var connection = _connectionFactory.CreateConnection();

            //Creating SqlCommand object to execute the stored procedure spAddAmenity.
            using var command = new SqlCommand("spAddAmenity", connection);

            //Setting the CommandType of the SqlCommand object to StoredProcedure.
            command.CommandType = CommandType.StoredProcedure;

            //Adding parameters to the SqlCommand object.
            command.Parameters.AddWithValue("@Name", amenity.Name);
            command.Parameters.AddWithValue("@Description", amenity.Description);
            command.Parameters.AddWithValue("@CreatedBy", "System");

            //Creating SqlParameter object to get the output parameters from the stored procedure.
            command.Parameters.Add("@AmenityID", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.Parameters.Add("@Status", SqlDbType.Bit).Direction = ParameterDirection.Output;
            command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

            //Try block to catch any exceptions that occur during the execution of the code inside the block.
            try
            {
                //Opening the connection with the Database.
                await connection.OpenAsync();

                //Executing the SqlCommand object to add a new Amenity to the database.
                await command.ExecuteNonQueryAsync();

                //Checking the status of the operation.
                if (Convert.ToBoolean(command.Parameters["@Status"].Value))
                {
                    //Setting the response of the operation.
                    amenityInsertResponseDTO.Message = Convert.ToString(command.Parameters["@Message"].Value);
                    amenityInsertResponseDTO.IsCreated = true;
                    amenityInsertResponseDTO.AmenityID = Convert.ToInt32(command.Parameters["@AmenityID"].Value);

                    //Returning the response of the operation.
                    return amenityInsertResponseDTO;
                }

                //Setting the response of the operation.
                amenityInsertResponseDTO.Message = Convert.ToString(command.Parameters["@Message"].Value);
                amenityInsertResponseDTO.IsCreated = false;

                //Returning the response of the operation.
                return amenityInsertResponseDTO;
            }
            //Catch any exceptions that occur during the execution of Try block
            catch (SqlException ex)
            {
                //Throw a new exception with a custom message and the original exception
                throw new Exception($"Error while adding Amenity to the database : {ex.Message}", ex);
            }
        }

    }
}
