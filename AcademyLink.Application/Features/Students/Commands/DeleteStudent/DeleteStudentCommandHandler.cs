using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IAsyncRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public DeleteStudentCommandHandler(IMapper mapper, IAsyncRepository<Student> studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToDelete = await _studentRepository.GetByIdAsync(request.StudentId);

            await _studentRepository.DeleteAsync(studentToDelete);
        }
    }
}
