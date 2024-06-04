﻿using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Configurations
{
    public static class AppConfiguration
    {
        public static void AddDependenceInjection(this IServiceCollection services)
        {
            // Service life time DI
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.Net 8.0 - Happy Milk 88", Description = "APIs Service", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                 });
            });
        }
    }
}
