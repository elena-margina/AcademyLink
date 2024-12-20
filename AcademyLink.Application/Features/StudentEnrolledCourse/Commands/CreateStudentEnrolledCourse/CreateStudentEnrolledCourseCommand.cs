using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse
{
    public class CreateStudentEnrolledCourseCommand : IRequest<CreateStudentEnrolledCourseCommandResponse>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
