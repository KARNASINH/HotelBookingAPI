using HotelBookingAPI.DTOs.BookingDTOs;
using HotelBookingAPI.DTOs.PaymentDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    //API Controller which holds all the endpoints related to Reservation (Booking and Payment) CRUD operations.
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        //Repository object to interact with the database.
        private readonly ReservationRepository _reservationRepository;

        //Logger object to log the information, warning and error messages.
        private readonly ILogger<ReservationController> _logger;

        //Constructor to initialize the Repository and Logger objects.
        public ReservationController(ReservationRepository reservationRepository, ILogger<ReservationController> logger)
        {
            //Initialize the Repository and Logger objects.
            _reservationRepository = reservationRepository;

            //Initialize the Logger object.
            _logger = logger;
        }


        
        
        
        
        
        
        //API Endpoint to get the details of all the Rooms along with their Cost.
        [HttpPost("CalculateRoomCosts")]
        public async Task<APIResponse<RoomCostsResponseDTO>> CalculateRoomCosts([FromBody] RoomCostsDTO model)
        {
            //Log the Request Received for CalculateRoomCosts along with the Request Body.
            _logger.LogInformation("Request Received for CalculateRoomCosts: {@RoomCostsDTO}", model);

            //Check if the Request Body is not valid.
            if (!ModelState.IsValid)
            {
                //Log the Invalid Data in the Request Body.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return 400 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
            }

            //Try to get the Room Costs.
            try
            {
                //Call the Repository Method to Calculate the Room Costs.
                var result = await _reservationRepository.CalculateRoomCostsAsync(model);

                //Check if the Room Costs are calculated successfully.
                if (result.Status)
                {
                    //Return the Success Response with the Room Costs.
                    return new APIResponse<RoomCostsResponseDTO>(result, "Success");
                }
                //Return 400 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.BadRequest, "Failed");
            }
            //Catch the Exception if any error occurred while calculating the Room Costs.
            catch (Exception ex)
            {
                //Log the Error Message.
                _logger.LogError(ex, "Failed to calculate room costs");

                //Return 500 Http status code with the error message.
                return new APIResponse<RoomCostsResponseDTO>(HttpStatusCode.InternalServerError, "Failed to calculate room costs", ex.Message);
            }
        }







        //API Endpoint to Create a Reservation.
        [HttpPost("CreateReservation")]
        public async Task<APIResponse<CreateReservationResponseDTO>> CreateReservation([FromBody] CreateReservationDTO reservation)
        {
            //Log the Request Received for CreateReservation along with the Request Body.
            _logger.LogInformation("Request Received for CreateReservation: {@CreateReservationDTO}", reservation);

            //Check if the Request Body is not valid.
            if (!ModelState.IsValid)
            {
                //Log the Invalid Data in the Request Body.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return 400 Http status code with the error message.
                return new APIResponse<CreateReservationResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body.");
            }

            //Try to create a reservation.
            try
            {
                //Call the Repository Method to Create a Reservation.
                var result = await _reservationRepository.CreateReservationAsync(reservation);

                //Check if the Reservation is created successfully.
                if (result.Status)
                {
                    //Return the Success Response with the Reservation Details.
                    return new APIResponse<CreateReservationResponseDTO>(result, result.Message);
                }
                //Return 400 Http status code with the error message.
                return new APIResponse<CreateReservationResponseDTO>(HttpStatusCode.BadRequest, result.Message);
            }
            //Catch the Exception if any error occurred while creating the Reservation.
            catch (Exception ex)
            {
                //Log the Error Message.
                _logger.LogError(ex, "Failed to create reservation");

                //Return 500 Http status code with the error message.
                return new APIResponse<CreateReservationResponseDTO>(HttpStatusCode.InternalServerError, "Failed to create reservation", ex.Message);
            }
        }








        //API Endpoint to add the Guests  the Reservation.
        [HttpPost("AddGuestsToReservation")]
        public async Task<APIResponse<AddGuestsToReservationResponseDTO>> AddGuestsToReservation([FromBody] AddGuestsToReservationDTO details)
        {
            //Log the Request Received for AddGuestsToReservation along with the Request Body.
            _logger.LogInformation("Request Received for AddGuestsToReservation: {@AddGuestsToReservationDTO}", details);

            //Check if the Request Body is not valid.
            if (!ModelState.IsValid)
            {
                //Log the Invalid Data in the Request Body.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return 400 Http status code with the error message.
                return new APIResponse<AddGuestsToReservationResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
            }
            //Try to add guests to the reservation.
            try
            {
                //Call the Repository Method to Add Guests to the Reservation.
                var result = await _reservationRepository.AddGuestsToReservationAsync(details);

                //Check if the Guests are added to the Reservation successfully.
                if (result.Status)
                {
                    //Return 200 Http status code with the Success Response and the Reservation Details.
                    return new APIResponse<AddGuestsToReservationResponseDTO>(result, result.Message);
                }

                //Return 400 Http status code with the error message.
                return new APIResponse<AddGuestsToReservationResponseDTO>(HttpStatusCode.BadRequest, result.Message);
            }
            //Catch the Exception if any error occurred while adding the Guests to the Reservation.
            catch (Exception ex)
            {
                //Log the Error Message.
                _logger.LogError(ex, "Failed to add guests to reservation");

                //Return 500 Http status code with the error message.
                return new APIResponse<AddGuestsToReservationResponseDTO>(HttpStatusCode.InternalServerError, "Failed to add guests to reservation", ex.Message);
            }
        }






        //API Endpoint to Process the Payment.
        [HttpPost("ProcessPayment")]
        public async Task<APIResponse<ProcessPaymentResponseDTO>> ProcessPayment([FromBody] ProcessPaymentDTO payment)
        {
            //Log the Request Received for ProcessPayment along with the Request Body.
            _logger.LogInformation("Request Received for ProcessPayment: {@ProcessPaymentDTO}", payment);

            //Check if the Request Body is not valid.
            if (!ModelState.IsValid)
            {
                //Log the Invalid Data in the Request Body.
                _logger.LogInformation("Invalid Data in the Request Body");

                //Return 400 Http status code with the error message.
                return new APIResponse<ProcessPaymentResponseDTO>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
            }
            //Try to process the payment.
            try
            {
                //Call the Repository Method to Process the Payment.
                var result = await _reservationRepository.ProcessPaymentAsync(payment);

                //Check if the Payment is processed successfully.
                if (result.Status)
                {
                    //Return 200 Http status code with the Success Response and the Payment Details.
                    return new APIResponse<ProcessPaymentResponseDTO>(result, result.Message);
                }
                //Return 400 Http status code with the error message.
                return new APIResponse<ProcessPaymentResponseDTO>(HttpStatusCode.BadRequest, result.Message);
            }
            //Catch the Exception if any error occurred while processing the Payment.
            catch (Exception ex)
            {
                //Log the Error Message.
                _logger.LogError(ex, "Failed to Process Payment");

                //Return 500 Http status code with the error message.
                return new APIResponse<ProcessPaymentResponseDTO>(HttpStatusCode.InternalServerError, "Failed to Process Payment", ex.Message);
            }
        }
    }
}
