using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegisterUser.BusinessLayer.Models.User.RequestModels
{
    public class CreateUserRequest : IValidatableObject
    {
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password field is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 40 characters.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password confirmation does not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The country ID field is required.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "The province ID field is required.")]
        public int ProvinceId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (CountryId < 1)
            {
                results.Add(new ValidationResult("The country ID field must be greater than zero."));
            }

            if (ProvinceId < 1)
            {
                results.Add(new ValidationResult("The province ID field must be greater than zero."));
            }

            return results;
        }
    }

}
