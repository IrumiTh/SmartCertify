
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartCertify.API.Filters;
using SmartCertify.Application;
using SmartCertify.Application.DTOvalidations;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Application.Interfaces.QuestionChoice;
using SmartCertify.Application.Interfaces.Questions;
using SmartCertify.Application.Services;
using SmartCertify.Infrastructure;

namespace SmartCertify.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Configuring Services - Start
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SmartCertifyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();// add your custom filters
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // disable automate validation
            }
            );
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //add fluentvalidation
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            // Register Question and Choice services and repositories
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IChoiceService, ChoiceService>();
            builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("default", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("default");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My API");
                    options.WithTheme(ScalarTheme.BluePlanet);
                    options.WithSidebar(true);
                });

                app.UseSwaggerUi(options =>
                {
                    options.DocumentPath = "openapi/v1.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            #endregion
        }
    }
}
