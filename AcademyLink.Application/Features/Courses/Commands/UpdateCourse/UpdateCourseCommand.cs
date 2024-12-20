
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AcademyLink.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<UpdateCourseCommandResponse>
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int IsAvailable { get; set; } = 1;
    }
}
