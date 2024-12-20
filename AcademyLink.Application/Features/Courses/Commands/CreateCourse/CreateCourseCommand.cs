using MediatR;


namespace AcademyLink.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SeatsAvailable { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
