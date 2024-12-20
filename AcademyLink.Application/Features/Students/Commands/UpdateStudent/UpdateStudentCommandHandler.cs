using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<UpdateStudentCommandResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var updateStudentCommandResponse = new UpdateStudentCommandResponse();
            var studentToUpdate = await _studentRepository.GetByIdAsync(request.StudentId);

            var validator = new UpdateStudentCommandValidator(_studentRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                updateStudentCommandResponse.Success = false;
                updateStudentCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    updateStudentCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (updateStudentCommandResponse.Success)
            {
                _mapper.Map(request, studentToUpdate, typeof(UpdateStudentCommand), typeof(Student));

                await _studentRepository.UpdateAsync(studentToUpdate);
            }

            return updateStudentCommandResponse;
        }
    }
}
