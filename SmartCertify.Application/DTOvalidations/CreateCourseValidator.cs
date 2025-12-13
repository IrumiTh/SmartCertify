using FluentValidation;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.Application.DTOvalidations
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        public CreateCourseValidator(ICourseRepository courseRepository)
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100)
                .MustAsync(async (title, cancellation) => !await courseRepository.IsTitleDuplicateAsync(title))
                .WithMessage("The Course Title Must be unique.");
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(500);
            this._courseRepository = courseRepository;
        }
    }
}
