﻿using System.ComponentModel.DataAnnotations;

namespace AcademyLink.Domain.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ICollection<StudensEnrolledCourse>? StudentEnrolledCourses { get; set; } = default!;
    }
}
