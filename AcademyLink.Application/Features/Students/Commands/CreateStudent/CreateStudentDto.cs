﻿using System.ComponentModel.DataAnnotations;

namespace AcademyLink.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentDto
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
