using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.Interfaces.Courses
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();

        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
