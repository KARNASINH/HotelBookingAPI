using HotelBookingAPI.Connection;

namespace HotelBookingAPI.Repository
{
    //This class is used to handle the database operations related to the cancellation.
    public class CancellationRepository
    {
        //This is the connection factory class instance.
        private readonly SqlConnectionFactory _connectionFactory;

        //This is the constructor of the class.
        public CancellationRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


    }
}
