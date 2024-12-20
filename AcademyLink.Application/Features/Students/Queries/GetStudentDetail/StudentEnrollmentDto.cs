using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentDetail
{
    public class StudentEnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
