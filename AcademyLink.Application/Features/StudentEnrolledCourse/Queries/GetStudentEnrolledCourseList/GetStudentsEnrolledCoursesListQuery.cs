using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class GetStudentsEnrolledCoursesListQuery : IRequest<List<StudentsEnrolledCoursesListVm>>
    {
        public int StudentId { get; set; }
    }
}
