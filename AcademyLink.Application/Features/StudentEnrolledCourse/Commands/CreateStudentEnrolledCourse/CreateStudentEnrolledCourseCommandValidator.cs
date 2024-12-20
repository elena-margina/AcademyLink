using AcademyLink.Application.Contracts.Persistence;
using FluentValidation;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse
{
    public class CreateStudentEnrolledCourseCommandValidator : AbstractValidator<CreateStudentEnrolledCourseCommand>
    {
        private readonly IStudensEnrolledCourseRepository _studentEnrolledCourseRepository;
        public CreateStudentEnrolledCourseCommandValidator(IStudensEnrolledCourseRepository studentEnrolledCourseRepository)
        {
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;

            RuleFor(p => p.StudentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(CourseAvailabilityCheck)
                .WithMessage("The availability for this course is over.");

            RuleFor(e => e)
                .MustAsync(StudentExistsCheck)
                .WithMessage("The student does not exists.");

            RuleFor(e => e)
                .MustAsync(CourseExistsCheck)
                .WithMessage("The course does not exists.");

            RuleFor(e => e)
                .MustAsync(StudentInCourseExistsCheck)
                .WithMessage("The student is already enrolled in this course.");

        }

        private async Task<bool> CourseAvailabilityCheck(CreateStudentEnrolledCourseCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.CourseAvailabilityCheck(e.CourseId);
        }

        private async Task<bool> StudentExistsCheck(CreateStudentEnrolledCourseCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.StudentExistsCheck(e.StudentId);
        }

        private async Task<bool> CourseExistsCheck(CreateStudentEnrolledCourseCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.CourseExistsCheck(e.CourseId);
        }

        private async Task<bool> StudentInCourseExistsCheck(CreateStudentEnrolledCourseCommand e, CancellationToken token)
        {
            return !await _studentEnrolledCourseRepository.StudentInCourseExistsCheck(e.StudentId, e.CourseId);
        }
    }
}
