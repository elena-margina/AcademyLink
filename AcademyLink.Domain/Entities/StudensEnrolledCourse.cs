using AcademyLink.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcademyLink.Domain.Entities
{
    public class StudensEnrolledCourse
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public decimal Progress { get; set; } 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnrollmentStatus Status { get; set; }
        public Student Student { get; set; } = default!;
        public Course Course { get; set; } = default!;
    }
}
