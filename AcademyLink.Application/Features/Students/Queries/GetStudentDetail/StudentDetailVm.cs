using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentDetail
{
    public class StudentDetailVm
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int CourseId { get; set; }
        //public int EnrollmentId { get; set; }
        public StudentEnrollmentDto StudentEnrollment { get; set; } = default!;
    }
}
