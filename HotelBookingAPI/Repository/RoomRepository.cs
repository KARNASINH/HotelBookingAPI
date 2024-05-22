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
        private readonly SqlConnectionFactory _context;

        //This constructor is used to inject the SqlConnectionFactory instance
        public RoomRepository(SqlConnectionFactory context)
        {
            _context = context;
        }

        
    }

}
