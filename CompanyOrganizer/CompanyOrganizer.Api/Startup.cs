using AutoMapper;
using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Repositories;
using CompanyOrganizer.Infrastructure;
using CompanyOrganizer.Infrastructure.Helpers;
using CompanyOrganizer.Infrastructure.Mappers;
using CompanyOrganizer.Infrastructure.Repositories;
using CompanyOrganizer.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyOrganizer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheCodeBuzz-Service", Version = "v1" });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            services.AddDbContext<CompanyContext>(o =>
            {
                o.UseSqlServer(Configuration["ConnectionStrings:SqlConnectionString"]);
            });

            services.AddScoped<ICompanyRepository, CompanyRepository>()
                    .AddScoped<ICompanyService, CompanyService>()
                    .AddScoped<IUserService, UserService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CompanyProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddCors();
            services.AddControllers();

            services.AddAuthentication("BasicAuthentication")
                      .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);        
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanyContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompanyOrganizer.Api v1"));
            }

            app.UseHttpsRedirection();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.Database.Migrate();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
