
namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class StudentsEnrolledCoursesListVm
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public decimal Progress { get; set; } 
        public string Status { get; set; } = string.Empty;

        public StudentDto Student { get; set; } = default!;
        public CourseDto Course { get; set; } = default!;
    }
}
