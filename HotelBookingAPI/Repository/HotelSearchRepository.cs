using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.HotelSearchDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repository
{
    //This class is used to interact with the database and executes the Search queries to get the various details of the Hotel Rooms.
    public class HotelSearchRepository
    {
        //SqlConnectionFactory object to get the connection object.
        private readonly SqlConnectionFactory _connectionFactory;






        //Constructor to initialize the SqlConnectionFactory object using dependency injection.
        public HotelSearchRepository(SqlConnectionFactory connectionFactory)
        {
            //Assigning the SqlConnectionFactory object to the private variable.
            _connectionFactory = connectionFactory;
        }







        //This method is used to fetch the Room details from the database based on the CheckIn and CheckOut dates.
        public async Task<List<RoomSearchDTO>> SearchByAvailabilityAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchByAvailability.
                using (var command = new SqlCommand("spSearchByAvailability", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@CheckInDate", checkInDate));
                    command.Parameters.Add(new SqlParameter("@CheckOutDate", checkOutDate));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to create the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }







        //This method is used to fetch the Room details from the database based on the Minumum and Maximum Price range provided by the user.
        public async Task<List<RoomSearchDTO>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchByPriceRange.
                using (var command = new SqlCommand("spSearchByPriceRange", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@MinPrice", minPrice));
                    command.Parameters.Add(new SqlParameter("@MaxPrice", maxPrice));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }






        //This method is used to fetch the Room details from the database based on the RoomTypeName provided by the user.
        public async Task<List<RoomSearchDTO>> SearchByRoomTypeAsync(string roomTypeName)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchByRoomType.
                using (var command = new SqlCommand("spSearchByRoomType", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@RoomTypeName", roomTypeName));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }






        //This method is used to fetch the Room details from the database based on the ViewType provided by the user.
        public async Task<List<RoomSearchDTO>> SearchByViewTypeAsync(string viewType)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchByViewType.
                using (var command = new SqlCommand("spSearchByViewType", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@ViewType", viewType));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }






        //This method is used to fetch the Room details from the database based on the Amenity Name provided by the user.
        public async Task<List<RoomSearchDTO>> SearchByAmenitiesAsync(string amenityName)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchByAmenities.
                using (var command = new SqlCommand("spSearchByAmenities", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@AmenityName", amenityName));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }







        //This method is used to fetch the Room details from the database based on the RoomTypeID provided by the user.
        public async Task<List<RoomSearchDTO>> SearchRoomsByRoomTypeIDAsync(int roomTypeID)
        {
            //List to store the Room fetched from the database.
            var rooms = new List<RoomSearchDTO>();

            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spSearchRoomsByRoomTypeID.
                using (var command = new SqlCommand("spSearchRoomsByRoomTypeID", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@RoomTypeID", roomTypeID));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Reading the data from the reader object.
                        while (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and adding it to the list.
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }

            //Returning the list of RoomSearchDTO objects.
            return rooms;
        }







        //This method is used to fetch the Room details from the database based on the RoomID provided by the user.
        //This method fetches the two result sets from the database, one for Room details and another for Amenities details.
        //1st result set will have only one record and taht is why we are directly casting to RoomSearchDTO object using CreateRoomSearchDTO Helper method.
        //2nd result set will have multiple records and we are iterating through the records and creating the AmenitySearchDTO objects and adding them to the list.
        public async Task<RoomDetailsWithAmenitiesSearchDTO> GetRoomDetailsWithAmenitiesByRoomIDAsync(int roomID)
        {
            //Creating a RoomDetailsWithAmenitiesSearchDTO object to store the Room and Amenities details.
            //This object has two complex object, RoomDetails and Amenities list.
            RoomDetailsWithAmenitiesSearchDTO roomDetails = new RoomDetailsWithAmenitiesSearchDTO();
            
            //Creating a connection object using the SqlConnectionFactory object.
            using (var connection = _connectionFactory.CreateConnection())
            {
                //Creating a SqlCommand object to execute the stored procedure spGetRoomDetailsWithAmenitiesByRoomID.
                using (var command = new SqlCommand("spGetRoomDetailsWithAmenitiesByRoomID", connection))
                {
                    //Setting the command type to stored procedure.
                    command.CommandType = CommandType.StoredProcedure;

                    //Adding the parameters to the stored procedure.
                    command.Parameters.Add(new SqlParameter("@RoomID", roomID));

                    //Opening the connection.
                    connection.Open();

                    //Executing the command and fetching the data using ExecuteReaderAsync method.
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //Checking if the reader has any data.
                        if (await reader.ReadAsync())
                        {
                            //Calling helper method to get the RoomSearchDTO object and assigning it to the Room property of RoomDetails object.
                            roomDetails.Room = CreateRoomSearchDTO(reader);

                            //Checking if the reader has more result sets.
                            roomDetails.Amenities = new List<AmenitySearchDTO>();

                            //Checking if the reader has more result sets.
                            if (await reader.NextResultAsync())
                            {
                                //Iterating through the records and creating the AmenitySearchDTO objects and adding them to the list.
                                while (await reader.ReadAsync())
                                {
                                    //Creating the AmenitySearchDTO object and adding it to the list.
                                    roomDetails.Amenities.Add(new AmenitySearchDTO
                                    {
                                        //Reading the data from the reader object.
                                        AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                                        Name = reader.GetString(reader.GetOrdinal("Name")),
                                        Description = reader.GetString(reader.GetOrdinal("Description"))
                                    });
                                }
                            }
                        }
                    }
                }
            }

            //Returning the RoomDetailsWithAmenitiesSearchDTO object.
            return roomDetails;
        }








        //This would be a Helper method which takes SqlDataReader object as input and returns the RoomSearchDTO object.
        //In all the methods in the Repository class, we would be using this method to create and to return the RoomSearchDTO object.
        private RoomSearchDTO CreateRoomSearchDTO(SqlDataReader reader)
        {
            return new RoomSearchDTO
            {
                RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                BedType = reader.GetString(reader.GetOrdinal("BedType")),
                ViewType = reader.GetString(reader.GetOrdinal("ViewType")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                RoomType = new RoomTypeSearchDTO
                {
                    RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                    TypeName = reader.GetString(reader.GetOrdinal("TypeName")),
                    AccessibilityFeatures = reader.GetString(reader.GetOrdinal("AccessibilityFeatures")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                }
            };
        }
    }
}
