using AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList;
using AcademyLink.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse
{
    public class CreateStudentEnrolledCourseDto
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? EnrollmentDate { get; set; }
        public decimal Progress { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnrollmentStatus Status { get; set; }
        public CourseDto Course { get; set; } = default!;
    }
}
