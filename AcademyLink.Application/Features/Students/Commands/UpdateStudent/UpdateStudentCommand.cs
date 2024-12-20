using MediatR;

namespace AcademyLink.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<UpdateStudentCommandResponse>
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}

