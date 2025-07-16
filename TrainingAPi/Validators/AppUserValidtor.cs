using FluentValidation;
using TrainingAPi.ViewModel2;

namespace TrainingAPi.Validators
{
    public class AppUserValidtor : AbstractValidator<AppUserVM>
    {
        public AppUserValidtor()
        {
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(50).
                WithMessage("First Name is required and should not exceed 50 characters");
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(50).WithName("Last Name");
            RuleForEach(user => user.Posts).SetValidator(new PostValidator());

            RuleFor(user => user.Email)
                 .NotEmpty()
                 .EmailAddress() // Uses FluentValidation's built-in email validator
                 .WithMessage("Please provide a valid email address.");

            //RuleFor(user => user.Phone)
            //    .NotEmpty()
            //    .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$") // Simple international phone regex
            //    .WithMessage("Please provide a valid phone number.");
        }
    }
}
