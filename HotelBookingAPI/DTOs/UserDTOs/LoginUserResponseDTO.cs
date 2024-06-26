﻿using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.UserDTOs
{
    //This class is used to generate a response when a user logs in.
    public class LoginUserResponseDTO
    {
        //Id of the user
        public int UserId { get; set; }

        //Message to be displayed if any error occurs
        public string Message { get; set; }

        //Is the user logged in
        public bool IsLogin { get; set; }
    }
}
