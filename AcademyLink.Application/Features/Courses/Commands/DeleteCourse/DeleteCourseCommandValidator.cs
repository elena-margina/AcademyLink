using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Application.Features.Courses.Commands.CreateCourse;
using FluentValidation;

namespace AcademyLink.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseCommandValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

            RuleFor(e => e)
                .MustAsync(CourseIsInUseCheck)
                .WithMessage("The course that you want to delete is alrady enrolled and can not be deleted!");;
        }

        private async Task<bool> CourseIsInUseCheck(DeleteCourseCommand e, CancellationToken token)
        {
            return !await _courseRepository.CourseIsInUseCheck(e.CourseId);
        }
    }
}
