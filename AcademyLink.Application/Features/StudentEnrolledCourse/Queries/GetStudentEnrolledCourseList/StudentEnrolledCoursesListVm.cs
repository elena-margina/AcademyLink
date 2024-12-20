using AcademyLink.Domain.Enums;
using System.Text.Json.Serialization;


namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class StudentEnrolledCoursesListVm
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<CourseDto> Course { get; set; } = default!;
    }
}
