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
    
        public  Task<List<Course>> GetAllCoursesAsync()
        {
            return  _dbContext.Courses.ToListAsync();
        }
    }
}


 