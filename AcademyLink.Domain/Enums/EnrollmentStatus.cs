using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyLink.Domain.Enums
{
    public enum EnrollmentStatus
    {
        Enrolled,      // Student is actively enrolled
        InProgress,    // Student has started the course
        Completed,     // Student has completed the course
        Cancelled      // Enrollment was cancelled
    }
}
