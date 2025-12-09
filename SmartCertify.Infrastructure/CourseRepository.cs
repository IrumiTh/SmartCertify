using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SmartCertifyContext _dbContext;

        public CourseRepository(SmartCertifyContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<Course> GetAllCourses()
        {
            var data = _dbContext.Courses.ToList();
            return data;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }
    }
}
