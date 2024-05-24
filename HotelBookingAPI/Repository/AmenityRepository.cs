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





    }
}
