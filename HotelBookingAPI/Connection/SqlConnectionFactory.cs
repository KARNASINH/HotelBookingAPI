using Microsoft.Data.SqlClient;

namespace HotelBookingAPI.Connection
{
    //This class get a connection string from appsettings.json file to establish the connection with Database.
    public class SqlConnectionFactory
    {
        //Creating IConfiguration object to get value based on the key from appsettings.json file.
        private readonly IConfiguration _configuration;

        //Injecting IConfiguration objerct using DI.
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //
        public SqlConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("DefaultConnection");           
       
    }
}
