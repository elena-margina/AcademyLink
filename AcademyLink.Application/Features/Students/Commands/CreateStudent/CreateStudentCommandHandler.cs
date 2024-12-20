using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public CreateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var createStudentCommandResponse = new CreateStudentCommandResponse();
              
            var validator = new CreateStudentCommandValidator(_studentRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createStudentCommandResponse.Success = false;
                createStudentCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createStudentCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createStudentCommandResponse.Success)
            {
                var student = new Student() { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, Phone = request.Phone };
                student = await _studentRepository.AddAsync(student);
                createStudentCommandResponse.Student = _mapper.Map<CreateStudentDto>(student);
            }

            return createStudentCommandResponse;
        }
    }
}
