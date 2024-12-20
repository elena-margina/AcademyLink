using AcademyLink.Application.Contracts.Persistence;
using FluentValidation;

namespace AcademyLink.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudentCommandValidator(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

            RuleFor(e => e)
                .MustAsync(StudentIsInUseCheck)
                .WithMessage("A course with the same name and email already exists."); ;
        }

        private async Task<bool> StudentIsInUseCheck(DeleteStudentCommand e, CancellationToken token)
        {
            return !await _studentRepository.StudentIsInUseCheck(e.StudentId);
        }
    }
}
