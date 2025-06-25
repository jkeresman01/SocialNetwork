using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SocialNetwork.Domain.Validation
{
    public class AlgebraEmailAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@algebra\.hr$", RegexOptions.IgnoreCase);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email && EmailRegex.IsMatch(email))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Email must be a valid @algebra.hr address.");
        }
    }
}

