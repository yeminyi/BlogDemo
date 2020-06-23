using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class PostResourceValidator : AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            RuleFor(x => x.Author)
                .NotNull()
                .WithName("Auther")
                .WithMessage("{PropertyName}must input")
                .MaximumLength(50)
                .WithMessage("{PropertyName}max length is {MaxLength}");
        }
    }
}
