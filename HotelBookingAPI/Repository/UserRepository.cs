using HotelBookingAPI.Connection;

namespace HotelBookingAPI.Repository
{
    //UserRepository class is used to interact with the database to perform CRUD operations on the User table
    public class UserRepository
    {
        //SqlConnectionFactory instance to create a connection to the database
        private readonly SqlConnectionFactory _connectionFactory;

        //This constructor is used to inject the SqlConnectionFactory instance
        public UserRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
