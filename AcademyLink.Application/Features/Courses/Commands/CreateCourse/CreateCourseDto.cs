using System.ComponentModel.DataAnnotations;

namespace AcademyLink.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseDto
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
