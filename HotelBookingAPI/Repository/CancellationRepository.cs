using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.CancellationDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

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





        //This method is used to get the cancellation policies from the database.
        public async Task<CancellationPoliciesResponseDTO> GetCancellationPoliciesAsync()
        {
            //This is the response object.
            var response = new CancellationPoliciesResponseDTO
            {
                //This is the list of cancellation policies.
                Policies = new List<CancellationPolicyDTO>()
            };

            //This is the try block.
            try
            {
                //This is the connection object.
                using var connection = _connectionFactory.CreateConnection();

                //This is the command object.
                using var command = new SqlCommand("spGetCancellationPolicies", connection);

                //This is the command type.
                command.CommandType = CommandType.StoredProcedure;

                //Output parameters.
                var statusParam = new SqlParameter("@Status", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                //Adding parameters to the command.
                command.Parameters.Add(statusParam);
                command.Parameters.Add(messageParam);

                //Opening the connection.
                await connection.OpenAsync();

                //This is the reader object.
                using (var reader = await command.ExecuteReaderAsync())
                {
                    //This is the loop to read the data from the reader.
                    while (await reader.ReadAsync())
                    {
                        response.Policies.Add(new CancellationPolicyDTO
                        {
                            PolicyID = reader.GetInt32(reader.GetOrdinal("PolicyID")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            CancellationChargePercentage = reader.GetDecimal(reader.GetOrdinal("CancellationChargePercentage")),
                            MinimumCharge = reader.GetDecimal(reader.GetOrdinal("MinimumCharge")),
                            EffectiveFromDate = reader.GetDateTime(reader.GetOrdinal("EffectiveFromDate")),
                            EffectiveToDate = reader.GetDateTime(reader.GetOrdinal("EffectiveToDate"))
                        });
                    }
                }

                //Setting the response properties.
                response.Status = (bool)statusParam.Value;
                response.Message = messageParam.Value as string;
            }
            //This is the catch block.
            catch (SqlException ex)
            {
                //Setting the response properties.
                response.Status = false;
                response.Message = $"Database error occurred: {ex.Message}";
            }

            //Returning the response.
            return response;
        }
    }
}
