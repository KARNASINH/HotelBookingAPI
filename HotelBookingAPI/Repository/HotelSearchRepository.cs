using HotelBookingAPI.Connection;

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


    }
}
