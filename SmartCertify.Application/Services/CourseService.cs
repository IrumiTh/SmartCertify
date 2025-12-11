using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository , IMapper mapper)
        {
            this._courseRepository = courseRepository;
            this._mapper = mapper;
        }
        public Task<CourseDto> AddCourseAsync(CreateCourseDto createCourseDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourseAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
            
        }

        public Task<CourseDto?> GetCourseByIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTitleDuplicateAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDescriptionAsync(int courseId, CourseUploadDescriptionDto descriptionDto)
        {
            throw new NotImplementedException();
        }
    }
}
