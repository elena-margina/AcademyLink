﻿using AcademyLink.Application.Features.Courses.Commands.DeleteCourse;
using AcademyLink.Domain.Entities;

namespace AcademyLink.Application.Contracts.Persistence
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<bool> IsCourseNameUnique(string name);
        Task<bool> IsCourseNameUnique(int courseId, string name);
        Task<bool> CourseIsInUseCheck(int CourseId);
    }
}
