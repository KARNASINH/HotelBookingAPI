using System.ComponentModel.DataAnnotations;
namespace HotelBookingAPI.CustomValidator
{
    //This is the class that will be used to check the endterd price range
    public class PriceRangeValidationAttribute : ValidationAttribute
    {
        //This will store the value entered for PriceRangeValidationAttribute.
        //In this application we have entered words/values "MinPrice" and "MaxPrice" in the PriceRangeValidationAttribute
        private readonly string _minPricePropertyName;
        private readonly string _maxPricePropertyName;

        //This is the constructor of the class
        public PriceRangeValidationAttribute(string minPricePropertyName, string maxPricePropertyName)
        {
            //This will store the value entered in PriceRangeValidationAttribute
            _minPricePropertyName = minPricePropertyName;
            _maxPricePropertyName = maxPricePropertyName;

            //This is the error message that will be displayed if the maximum price is less than the minimum price
            ErrorMessage = "The maximum price must be greater than or equal to the minimum price.";
        }

        //This is the method that will be called to validate the price range
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the Property name from the class where we have used PriceRangeValidationAttribute attribute
            var minPriceProperty = validationContext.ObjectType.GetProperty(_minPricePropertyName);
            var maxPriceProperty = validationContext.ObjectType.GetProperty(_maxPricePropertyName);

            //Check if the properties are aviailable in the class where we have used PriceRangeValidationAttribute attribute
            if (minPriceProperty == null || maxPriceProperty == null)
            {
                //Return ArgumentException if the properties are not found
                throw new ArgumentException("Property not found.");
            }

            //Get the minimum and maximum price values from the Properties/Fields of the class
            var minPrice = minPriceProperty.GetValue(validationContext.ObjectInstance, null) as decimal?;
            var maxPrice = maxPriceProperty.GetValue(validationContext.ObjectInstance, null) as decimal?;

            //Checks if the minimum and maximum price values are null
            //If the values are null, then also return a ValidationResult with a success message, b'cos the main purpose of this validation is to check if the maximum price is greater than the minimum price
            //To check if the values are null, we should use the "Required" attribute on the MinPrice and MaxPrice properties
            if (!minPrice.HasValue || !maxPrice.HasValue)
            {
                //Return a ValidationResult with a success message
                return ValidationResult.Success;
            }

            //Check if the maximum price is less than the minimum price
            if (minPrice.Value > maxPrice.Value)
            {
                //Return a new ValidationResult with the error message
                return new ValidationResult(ErrorMessage);
            }

            //If the maximum price is greater than the minimum price, return a ValidationResult with a success message
            return ValidationResult.Success;
        }
    }
}