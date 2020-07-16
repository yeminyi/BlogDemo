using FluentValidation;

namespace BlogDemo.Infrastructure.Resources
{
    public class PostAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : PostAddOrUpdateResource
    {
        public PostAddOrUpdateResourceValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithName("Title")
                .WithMessage("required|The {PropertyName} must input")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName} max length is {MaxLength}");

            RuleFor(x => x.Body)
                .NotNull()
                .WithName("Body")
                .WithMessage("required|The {{PropertyName}  must input")
                .MinimumLength(100)
                .WithMessage("minlength|{PropertyName} min length is {MinLength}");
        }
    }
}
