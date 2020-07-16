using FluentValidation;

namespace BlogDemo.Infrastructure.Resources
{
    public class PostImageResourceValidator : AbstractValidator<PostImageResource>
    {
        public PostImageResourceValidator()
        {
            RuleFor(x => x.FileName)
                .NotNull()
                .WithName("Filename")
                .WithMessage("required|{PropertyName} is required")
                .MaximumLength(100)
                .WithMessage("maxlength|{PropertyName} max length is {MaxLength}");
        }
    }
}
