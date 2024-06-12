using HotelBookingAPI.Connection;

namespace HotelBookingAPI.Repository
{
    //This class is used to perform CRUD operations for the Reservations.
    public class ReservationRepository
    {
        //Private field of SqlConnectionFactory class to establish the connection with Database.
        private readonly SqlConnectionFactory _connectionFactory;

        //Injecting SqlConnectionFactory object using DI.
        public ReservationRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
