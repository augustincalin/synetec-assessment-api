using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SynetecAssessmentApi.Domain.Interfaces;
using SynetecAssessmentApi.Domain.Model;
using SynetecAssessmentApi.Domain.Services;
using SynetecAssessmentApi.Persistence.Data;

namespace SynetecAssessmentApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SynetecAssessmentApi", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "HrDb"));

            services.AddTransient<DbContext, AppDbContext>();
            services.AddTransient<IRepository<Employee>, EFRepository<Employee>>();
            services.AddTransient<IBonusPoolService, BonusPoolService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SynetecAssessmentApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
