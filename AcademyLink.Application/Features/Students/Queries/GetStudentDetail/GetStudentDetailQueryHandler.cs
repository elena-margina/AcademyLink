using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentDetail
{
    public class GetStudentDetailQueryHandler : IRequestHandler<GetStudentDetailQuery, StudentDetailVm>
    {
        private readonly IAsyncRepository<Student> _studentRepository;
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IAsyncRepository<StudensEnrolledCourse> _studentEnrollmentRepository;
        private readonly IMapper _mapper;

        public GetStudentDetailQueryHandler(
            IMapper mapper,
            IAsyncRepository<Student> studentRepository,
            IAsyncRepository<Course> courseRepository,
            IAsyncRepository<StudensEnrolledCourse> studentEnrollmentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _studentEnrollmentRepository = studentEnrollmentRepository;
        }

        public async Task<StudentDetailVm> Handle(GetStudentDetailQuery request, CancellationToken cancellationToken)
        {
            var @student = await _studentRepository.GetByIdAsync(request.Id);
            var studentDetailDto = _mapper.Map<StudentDetailVm>(@student);

            var studentEnrollment = await _studentEnrollmentRepository.GetByIdAsync(@student.StudentId);
            //var course = await _courseRepository.GetByIdAsync(studentEnrollment.CourseId);

            studentDetailDto.StudentEnrollment = _mapper.Map<StudentEnrollmentDto>(studentEnrollment);

            return studentDetailDto;
        }
    }
}
