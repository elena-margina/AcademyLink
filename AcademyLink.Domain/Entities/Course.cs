using System.ComponentModel.DataAnnotations;

namespace AcademyLink.Domain.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IsAvailable { get; set; } = 1;

        public ICollection<StudensEnrolledCourse>? StudentEnrolledCourses { get; set; } = default!;
    }
}
