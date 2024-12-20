using AcademyLink.Application.Contracts.Persistence;
using FluentValidation;

namespace AcademyLink.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentCommandValidator(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.") 
                .NotNull() 
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.") 
                .EmailAddress().WithMessage("Invalid email address."); 

            RuleFor(e => e)
                .MustAsync(StudentNameAndEmailUnique)
                .WithMessage("A student with the same name and email already exists.");
        }

        private async Task<bool> StudentNameAndEmailUnique(UpdateStudentCommand e, CancellationToken token)
        {
            return !await _studentRepository.IsStudentNameAndEmailUnique(e.StudentId, e.FirstName + " " + e.LastName, e.Email);
        }
    }
}
