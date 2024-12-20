using AcademyLink.Application.Contracts.Persistence;
using FluentValidation;

namespace AcademyLink.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        public CreateCourseCommandValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.SeatsAvailable)
                .NotEmpty().WithMessage("{PropertyName} is required.") 
                .NotNull(); 

            RuleFor(e => e)
                .MustAsync(CourseNameUnique)
                .WithMessage("A course with the same name and email already exists.");

            RuleFor(course => course.DateFrom)
                .NotEmpty().WithMessage("DateFrom is required.")
                .Must(date => date != default).WithMessage("DateFrom must be a valid DateTime.");

            RuleFor(course => course.DateTo)
                .NotEmpty().WithMessage("DateTo is required.")
                .Must(date => date != default).WithMessage("DateTo must be a valid DateTime.");

            RuleFor(course => course)
                .Must(course => course.DateFrom < course.DateTo)
                .WithMessage("DateFrom must be earlier than DateTo.");
        }

        private async Task<bool> CourseNameUnique(CreateCourseCommand e, CancellationToken token)
        {
            return !await _courseRepository.IsCourseNameUnique(e.Name);
        }
    }
}
