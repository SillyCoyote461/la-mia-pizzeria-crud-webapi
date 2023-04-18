using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Attributes
{
    public class GreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext _)
        {
            decimal input = (decimal) value;
            if (value is null || input <= 0)
            {
                return new ValidationResult("Price must be greater than 0.");
            }

            return ValidationResult.Success;
        }
    }
}