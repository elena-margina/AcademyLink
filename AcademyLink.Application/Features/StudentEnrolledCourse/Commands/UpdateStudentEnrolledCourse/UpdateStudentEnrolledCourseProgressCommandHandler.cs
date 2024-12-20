using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.UpdateStudentEnrolledCourse
{
    public class UpdateStudentEnrolledCourseProgressCommandHandler : IRequestHandler<UpdateStudentEnrolledCourseProgressCommand, UpdateStudentEnrolledCourseProgressCommandResponse>
    {
        private readonly IStudensEnrolledCourseRepository _studentEnrolledCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public UpdateStudentEnrolledCourseProgressCommandHandler(IMapper mapper, IStudensEnrolledCourseRepository studentEnrolledCourseRepository, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public async Task<UpdateStudentEnrolledCourseProgressCommandResponse> Handle(UpdateStudentEnrolledCourseProgressCommand request, CancellationToken cancellationToken)
        {
            var studentEnrolledCourseProgressCommandResponse = new UpdateStudentEnrolledCourseProgressCommandResponse();

            var validator = new UpdateStudentEnrolledCourseProgressCommandValidator(_studentEnrolledCourseRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                studentEnrolledCourseProgressCommandResponse.Success = false;
                studentEnrolledCourseProgressCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    studentEnrolledCourseProgressCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (studentEnrolledCourseProgressCommandResponse.Success)
            {
                var studentEnrolledCourse = await _studentEnrolledCourseRepository.GetByPredicateAsync(e => e.StudentId == request.StudentId && e.CourseId == request.CourseId);
                if (studentEnrolledCourse != null)
                {
                    studentEnrolledCourse.Status = request.Status;
                    studentEnrolledCourse.Progress = request.Progress;

                    await _studentEnrolledCourseRepository.UpdateAsync(studentEnrolledCourse, x => x.Progress, x => x.Status);

                }
            }

            return studentEnrolledCourseProgressCommandResponse;
        }
    }
}
