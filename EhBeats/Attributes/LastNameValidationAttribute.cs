using System.ComponentModel.DataAnnotations;
using EhBeats.Models;

namespace EhBeats.Attributes
{
    internal class LastNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string lastName)
            {
                if (lastName.StartsWith("The"))
                {
                    return new ValidationResult($"For band names starting with “The”, put “The” in the {nameof(Artist.FirstName)} field");
                }

                if (!char.IsLetterOrDigit(lastName[0]))
                {
                    // alternatieve optie, als je alle chars in string wilt checken :
                    // if(lastName.Any(c => !char.IsLetterOrDigit(c)))
                    return new ValidationResult("Invalid band name.");
                }

            }
            else
            {
                throw new InvalidDataException("LastNameValidationAttribute can only be used on string properties.");
            }


            return ValidationResult.Success;
        }
    }
}