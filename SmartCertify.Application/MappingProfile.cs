using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartCertify.Application.DTOs;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Question, QuestionDto>().ReverseMap();

            // CreateQuestionDto -> Question
            CreateMap<CreateQuestionDto, Question>();

            // UpdateQuestionDto -> Question
            CreateMap<UpdateQuestionDto, Question>();

            // Choice <-> ChoiceDto
            CreateMap<Choice, ChoiceDto>().ReverseMap();
            // CreateChoiceDto -> Choice
            CreateMap<CreateChoiceDto, Choice>();

            // UpdateChoiceDto -> Choice
            CreateMap<UpdateChoiceDto, Choice>();
        }
    }
}
