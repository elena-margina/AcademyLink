using AcademyLink.Application.Features.Students.Queries.GetStudentsList;
using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class GetStudentEnrolledCoursesListQuery : IRequest<StudentEnrolledCoursesListVm>
    {
        public int StudentId { get; set; }
    }
}
