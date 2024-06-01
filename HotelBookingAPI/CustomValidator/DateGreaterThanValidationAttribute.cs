using System.ComponentModel.DataAnnotations;
namespace HotelBookingAPI.CustomValidator
{
    //This is the class that will be used to check if the date is greater than the comparison date
    public class DateGreaterThanValidationAttribute : ValidationAttribute
    {
        //This is the name of the property whoes will be used for comparison
        private readonly string _comparisonPropertyName;

        //This is the constructor of the class
        public DateGreaterThanValidationAttribute(string comparisonPropertyName)
        {
            //This is the name of the property whoes will be used for comparison
            _comparisonPropertyName = comparisonPropertyName;
            ErrorMessage = "The date must be greater than the comparison date.";
        }

        //This is the method that will be called to validate the date
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the user entered date value and store it in a variable
            var currentDate = value as DateTime?;

            // Get the comparison date value and store it in a variable
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonPropertyName);

            // Get the comparison date value and store it in a variable
            var comparisonDate = comparisonProperty?.GetValue(validationContext.ObjectInstance, null) as DateTime?;

            //Check if the current date is greater than the comparison date
            if (currentDate.HasValue && comparisonDate.HasValue && currentDate.Value <= comparisonDate.Value)
            {
                //Return a new ValidationResult with the error message
                return new ValidationResult(ErrorMessage);
            }

            //If the current date is greater than the comparison date, return a ValidationResult with a success message
            return ValidationResult.Success;
        }
    }
}