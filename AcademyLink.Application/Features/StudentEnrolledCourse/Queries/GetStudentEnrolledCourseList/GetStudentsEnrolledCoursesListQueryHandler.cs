using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList
{
    public class GetStudentsEnrolledCoursesListQueryHandler :
         IRequestHandler<GetStudentsEnrolledCoursesListQuery, List<StudentsEnrolledCoursesListVm>>
    {
        private readonly IAsyncRepository<StudensEnrolledCourse> _studentEnrolledCourseRepository;
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IAsyncRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsEnrolledCoursesListQueryHandler(IMapper mapper, IAsyncRepository<StudensEnrolledCourse> studentEnrolledCourseRepository, IAsyncRepository<Course> courseRepository, IAsyncRepository<Student> studentRepository)
        {
            _mapper = mapper;
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }


        public async Task<List<StudentsEnrolledCoursesListVm>> Handle(GetStudentsEnrolledCoursesListQuery request, CancellationToken cancellationToken)
        {
            var studentEnrolledCourses = await _studentEnrolledCourseRepository.ListAllAsync();

            var studentEnrroledCourses = _mapper.Map<List<StudentsEnrolledCoursesListVm>>(studentEnrolledCourses);

            foreach (var item in studentEnrroledCourses)
            {
                var student = await _studentRepository.GetByIdAsync(item.StudentId);
                item.Student = _mapper.Map<StudentDto>(student);

                var course = await _courseRepository.GetByIdAsync(item.CourseId);
                item.Course = _mapper.Map<CourseDto>(course);
            }

            return studentEnrroledCourses;
        }      
    }
}
