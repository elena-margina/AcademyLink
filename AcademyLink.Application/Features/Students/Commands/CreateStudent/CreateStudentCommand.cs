using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Xml.Linq;

namespace AcademyLink.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<CreateStudentCommandResponse>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
