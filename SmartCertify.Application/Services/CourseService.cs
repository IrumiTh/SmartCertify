using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Domain.Entities;

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
        public async Task AddCourseAsync(CreateCourseDto createCourseDto)
        {
            var course = _mapper.Map<Course>(createCourseDto);
            course.CreatedBy = 1;
            course.CreatedOn = DateTime.UtcNow;

            await _courseRepository.AddCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException("Course not found");

            await _courseRepository.DeleteCourseAsync(course);
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
            
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            return course == null ? null : _mapper.Map<CourseDto>(course);
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await _courseRepository.IsTitleDuplicateAsync(title);
        }

        public async Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) throw new KeyNotFoundException("Course not found");

            _mapper.Map(updateCourseDto, course);
            await _courseRepository.UpdateCourseAsync(course);
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            await _courseRepository.UpdateDescriptionAsync(courseId, description);
        }
    }
}
