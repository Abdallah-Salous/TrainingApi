using FluentValidation;
using TrainingAPi.ViewModel2;

namespace TrainingAPi.Validators
{
    public class PostValidator : AbstractValidator<PostVM>
    {
        public PostValidator()
        {
            RuleFor(post => post.Title).NotEmpty().MaximumLength(250);
            RuleFor(post => post.Body).NotEmpty();
        }
    }
}
