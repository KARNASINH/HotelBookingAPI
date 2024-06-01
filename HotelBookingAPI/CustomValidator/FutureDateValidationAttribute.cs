using System.ComponentModel.DataAnnotations;
namespace HotelBookingAPI.CustomValidator
{
    //This is the class that will be used to validate the date
    public class FutureDateValidationAttribute : ValidationAttribute
    {
        //This is the constructor of the class
        public FutureDateValidationAttribute()
        {
            //This is the error message that will be displayed if the date is not in the future
            ErrorMessage = "The date must be in the future.";
        }

        //This is the method that will be called to validate the date
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the date value and store it in a variable
            var date = value as DateTime?;


            //If the date is null or the date is less than or equal to today's date, return an error message
            if (!date.HasValue || date.Value.Date <= DateTime.Today)
            {
                //Return a new ValidationResult with the error message
                return new ValidationResult(ErrorMessage);
            }

            //If the date is in the future, return a ValidationResult with a success message
            return ValidationResult.Success;
        }
    }
}