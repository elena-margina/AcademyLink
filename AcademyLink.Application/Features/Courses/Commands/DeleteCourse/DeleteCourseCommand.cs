using MediatR;

namespace AcademyLink.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public int CourseId { get; set; }
    }
}
