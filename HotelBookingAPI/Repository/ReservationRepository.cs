using HotelBookingAPI.Connection;
using HotelBookingAPI.DTOs.BookingDTOs;
using Microsoft.Data.SqlClient;
using System.Data;

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






        //This method is used to calculate the room costs based on the check-in and check-out dates and the room IDs.
        public async Task<RoomCostsResponseDTO> CalculateRoomCostsAsync(RoomCostsDTO model)
        {
            //Creating an object of RoomCostsResponseDTO class to hold the response.
            RoomCostsResponseDTO roomCostsResponseDTO = new RoomCostsResponseDTO();

            //Try block to execute the code and catch the exceptions if any.
            try
            {
                //Creating a connection object using the CreateConnection method of SqlConnectionFactory class.
                using var connection = _connectionFactory.CreateConnection();

                //Creating a command object to execute the stored procedure.
                using var command = new SqlCommand("spCalculateRoomCosts", connection);

                //Setting the command type to stored procedure.
                command.CommandType = CommandType.StoredProcedure;

                //Setting up the parameters for the stored procedure.
                command.Parameters.AddWithValue("@CheckInDate", model.CheckInDate);
                command.Parameters.AddWithValue("@CheckOutDate", model.CheckOutDate);


                //Creating a DataTable object to hold the RoomIDs.
                var table = new DataTable();

                //Adding the column to the DataTable.
                table.Columns.Add("RoomID", typeof(int));

                //Adding the RoomIDs to the DataTable.
                model.RoomIDs.ForEach(id => table.Rows.Add(id));

                //Adding the DataTable as a parameter to the stored procedure.
                command.Parameters.AddWithValue("@RoomIDs", table).SqlDbType = SqlDbType.Structured;
                
                //Adding the output parameters to the stored procedure.
                command.Parameters.Add("@Amount", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                command.Parameters.Add("@GST", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                //Opening the connection.
                await connection.OpenAsync();

                //Executing the command and storing the result in a reader object.
                using var reader = await command.ExecuteReaderAsync();

                //Reading the data from the reader object.
                while (reader.Read())
                {
                    //Adding the room details to the RoomDetails list of RoomCostsResponseDTO object.
                    roomCostsResponseDTO.RoomDetails.Add(new RoomCostDetailDTO
                    {
                        //Reading the data from the reader object and adding it to the RoomCostDetailDTO object.
                        RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                        RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                        RoomPrice = reader.GetDecimal(reader.GetOrdinal("RoomPrice")),
                        TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                        NumberOfNights = reader.GetInt32(reader.GetOrdinal("NumberOfNights"))
                    });
                }

                // Ensuring the reader is closed before accessing output parameters
                await reader.CloseAsync();

                //Setting the output parameters to the RoomCostsResponseDTO object.
                roomCostsResponseDTO.Amount = (decimal)command.Parameters["@Amount"].Value;
                roomCostsResponseDTO.GST = (decimal)command.Parameters["@GST"].Value;
                roomCostsResponseDTO.TotalAmount = (decimal)command.Parameters["@TotalAmount"].Value;
                roomCostsResponseDTO.Status = true;
                roomCostsResponseDTO.Message = "Sucess";
            }
            //Catch block to catch the exceptions if any.
            catch (Exception ex)
            {
                //Setting the status to false and the message to the exception message.
                roomCostsResponseDTO.Status = false;
                roomCostsResponseDTO.Message = ex.Message;
            }

            //Returning the RoomCostsResponseDTO object.
            return roomCostsResponseDTO;
        }






        //This method is used to create a reservation.
        public async Task<CreateReservationResponseDTO> CreateReservationAsync(CreateReservationDTO reservation)
        {
            //Creating an object of CreateReservationResponseDTO class to hold the
            CreateReservationResponseDTO createReservationResponseDTO = new CreateReservationResponseDTO();
            try
            {
                //Creating a connection object using the CreateConnection method of SqlConnectionFactory class.
                using var connection = _connectionFactory.CreateConnection();

                //Creating a command object to execute the stored procedure.
                using var command = new SqlCommand("spCreateReservation", connection);

                //Setting the command type to stored procedure.
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", reservation.UserID);
                command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                command.Parameters.AddWithValue("@CreatedBy", reservation.UserID);

                //Creating a DataTable object to hold the RoomIDs.
                var table = new DataTable();

                //Adding the column to the DataTable.
                table.Columns.Add("RoomID", typeof(int));

                //Adding the RoomIDs to the DataTable.
                reservation.RoomIDs.ForEach(id => table.Rows.Add(id));

                //Adding the DataTable as a parameter to the stored procedure.
                command.Parameters.AddWithValue("@RoomIDs", table).SqlDbType = SqlDbType.Structured;

                //Adding the output parameters to the stored procedure.
                command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Status", SqlDbType.Bit).Direction = ParameterDirection.Output;
                command.Parameters.Add("@ReservationID", SqlDbType.Int).Direction = ParameterDirection.Output;

                //Opening the connection.
                await connection.OpenAsync();

                //Executing the command and storing the result in a reader object.
                await command.ExecuteNonQueryAsync();

                //Setting the output parameters to the CreateReservationResponseDTO object.
                createReservationResponseDTO.Message = command.Parameters["@Message"].Value.ToString();
                createReservationResponseDTO.Status = (bool)command.Parameters["@Status"].Value;
                createReservationResponseDTO.ReservationID = (int)command.Parameters["@ReservationID"].Value;
            }
            //Catch block to catch the exceptions if any.
            catch (Exception ex)
            {
                //Setting the status to false and the message to the exception message.
                createReservationResponseDTO.Message = ex.Message;
                createReservationResponseDTO.Status = false;
            }
            //Returning the CreateReservationResponseDTO object.
            return createReservationResponseDTO;
        }





        //This method is used to add guests to a reservation.
        public async Task<AddGuestsToReservationResponseDTO> AddGuestsToReservationAsync(AddGuestsToReservationDTO details)
        {
            //Creating an object of AddGuestsToReservationResponseDTO class to hold the response.
            AddGuestsToReservationResponseDTO addGuestsToReservationResponseDTO = new AddGuestsToReservationResponseDTO();

            //Try block to execute the code and catch the exceptions if any.
            try
            {
                //Creating a connection object using the CreateConnection method of SqlConnectionFactory class.
                using var connection = _connectionFactory.CreateConnection();

                //Creating a command object to execute the stored procedure.
                using var command = new SqlCommand("spAddGuestsToReservation", connection);

                //Setting the command type to stored procedure.
                command.CommandType = CommandType.StoredProcedure;

                //Setting up the parameters for the stored procedure.
                command.Parameters.AddWithValue("@UserID", details.UserID);
                command.Parameters.AddWithValue("@ReservationID", details.ReservationID);

                //Creating a DataTable object to hold the GuestDetails.
                var table = new DataTable();
                table.Columns.Add("FirstName", typeof(string));
                table.Columns.Add("LastName", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.Columns.Add("Phone", typeof(string));
                table.Columns.Add("AgeGroup ", typeof(string));
                table.Columns.Add("Address", typeof(string));
                table.Columns.Add("CountryId", typeof(int));
                table.Columns.Add("StateId", typeof(int));
                table.Columns.Add("RoomID", typeof(int));
                
                //Adding the GuestDetails to the DataTable.
                details.GuestDetails.ForEach(guest =>
                {
                    table.Rows.Add(guest.FirstName, guest.LastName, guest.Email, guest.Phone,
                    guest.AgeGroup, guest.Address, guest.CountryId, guest.StateId, guest.RoomID);
                });

                //Adding the DataTable as a parameter to the stored procedure.
                command.Parameters.AddWithValue("@GuestDetails", table).SqlDbType = SqlDbType.Structured;
                command.Parameters.Add("@Status", SqlDbType.Bit).Direction = ParameterDirection.Output;
                command.Parameters.Add("@Message", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output;

                //Opening the connection.
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                //Setting the output parameters to the AddGuestsToReservationResponseDTO object.
                addGuestsToReservationResponseDTO.Status = (bool)command.Parameters["@Status"].Value;
                addGuestsToReservationResponseDTO.Message = command.Parameters["@Message"].Value.ToString();
            }
            //Catch block to catch the exceptions if any.
            catch (Exception ex)
            {
                //Setting the status to false and the message to the exception message.
                addGuestsToReservationResponseDTO.Message = ex.Message;
                addGuestsToReservationResponseDTO.Status = false;
            }
            //Returning the AddGuestsToReservationResponseDTO object.
            return addGuestsToReservationResponseDTO;
        }
    }
}
