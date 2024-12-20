using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse
{
    public class CreateStudentEnrolledCourseCommandHandler : IRequestHandler<CreateStudentEnrolledCourseCommand, CreateStudentEnrolledCourseCommandResponse>
    {
        private readonly IStudensEnrolledCourseRepository _studentEnrolledCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public CreateStudentEnrolledCourseCommandHandler(IMapper mapper, IStudensEnrolledCourseRepository studentEnrolledCourseRepository, ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }
        public async Task<CreateStudentEnrolledCourseCommandResponse> Handle(CreateStudentEnrolledCourseCommand request, CancellationToken cancellationToken)
        {
            var createStudentEnrolledCourseCommandResponse = new CreateStudentEnrolledCourseCommandResponse();

            var validator = new CreateStudentEnrolledCourseCommandValidator(_studentEnrolledCourseRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createStudentEnrolledCourseCommandResponse.Success = false;
                createStudentEnrolledCourseCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createStudentEnrolledCourseCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createStudentEnrolledCourseCommandResponse.Success)
            {
                var student = await _studentRepository.GetByIdAsync(request.StudentId);

                var studentEnrolledCourse = new StudensEnrolledCourse
                {
                    StudentId = request.StudentId,
                    CourseId = request.CourseId,
                    Progress = 0.0m, // Default Progress (0%)
                    Status = Domain.Enums.EnrollmentStatus.Enrolled, // Default Status
                    EnrollmentDate = DateTime.UtcNow, // Set the enrollment date
                    Student = student
                };

                await _studentEnrolledCourseRepository.AddAsync(studentEnrolledCourse);
                createStudentEnrolledCourseCommandResponse.CreateStudentEnrolledCourse = _mapper.Map<CreateStudentEnrolledCourseDto>(studentEnrolledCourse);
            }

            return createStudentEnrolledCourseCommandResponse;
        }
    }
}
