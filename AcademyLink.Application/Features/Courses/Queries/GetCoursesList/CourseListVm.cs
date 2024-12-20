
namespace AcademyLink.Application.Features.Courses.Queries.GetCoursesList
{
    public class CourseListVm
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        //public TimeSpan? Duration { get; set; }
        public int IsAvailable { get; set; } = 1;
    }
}
