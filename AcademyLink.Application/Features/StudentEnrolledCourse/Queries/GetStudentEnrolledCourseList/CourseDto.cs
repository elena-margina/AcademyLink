using AcademyLink.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class CourseDto
    {
        public int EnrollmentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public decimal Progress { get; set; } 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnrollmentStatus Status { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Duration { get; set; }
        public string IsAvailable { get; set; } = string.Empty;
    }
}
