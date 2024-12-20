using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class GetStudentEnrolledCoursesListQueryHandler : IRequestHandler<GetStudentEnrolledCoursesListQuery, StudentEnrolledCoursesListVm>
    {
        private readonly IAsyncRepository<StudensEnrolledCourse> _studentEnrolledCourseRepository;
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IAsyncRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentEnrolledCoursesListQueryHandler(IMapper mapper, IAsyncRepository<StudensEnrolledCourse> studentEnrolledCourseRepository, IAsyncRepository<Course> courseRepository, IAsyncRepository<Student> studentRepository)
        {
            _mapper = mapper;
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public async Task<StudentEnrolledCoursesListVm> Handle(GetStudentEnrolledCoursesListQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {request.StudentId} not found.");
            }

            var studentEnrolledCourses = await _studentEnrolledCourseRepository.ListAllAsync(e => e.StudentId == request.StudentId);

            if (!studentEnrolledCourses.Any())
            {
                return new StudentEnrolledCoursesListVm
                {
                    StudentId = student.StudentId,
                    FullName = $"{student.FirstName} {student.LastName}",
                    Course = new List<CourseDto>() 
                };
            }

            var courseIds = studentEnrolledCourses.Select(e => e.CourseId).Distinct().ToList();
            var courses = await _courseRepository.ListAllAsync(c => courseIds.Contains(c.CourseId));

            var courseDtos = new List<CourseDto>();
            foreach (var enrolledCourse in studentEnrolledCourses)
            {
                var course = courses.FirstOrDefault(c => c.CourseId == enrolledCourse.CourseId);
                if (course != null)
                {
                    var courseDto = _mapper.Map<CourseDto>(course);

                    courseDto.EnrollmentId = enrolledCourse.EnrollmentId;
                    courseDto.EnrollmentDate = enrolledCourse.EnrollmentDate;
                    courseDto.Progress = enrolledCourse.Progress;
                    courseDto.Status = enrolledCourse.Status;

                    courseDtos.Add(courseDto);
                }
            }

            var studentVm = _mapper.Map<StudentEnrolledCoursesListVm>(student);
            studentVm.Course = courseDtos; 

            return studentVm;
        }
    }
}
