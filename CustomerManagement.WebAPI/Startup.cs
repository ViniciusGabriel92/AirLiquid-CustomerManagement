using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using CustomerManagement.Domain.Interfaces.Repositories;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Infra.Repository;
using CustomerManagement.Domain.Behavior;
using FluentValidation.AspNetCore;
using CustomerManagement.Domain.Commands;
using CustomerManagement.Domain.Validators;
using FluentValidation;
using CustomerManagement.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CustomerManagement.WebAPI
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
            #region Configure Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerManagement API", Version = "v1", Description = "API developed for AirLiquid customer management." });
            });
            #endregion

            services.AddControllers(setup => { }).AddFluentValidation();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMemoryCache();
            services.AddControllers();
            services.AddTransient<IValidator<InsertClienteCommand>, InsertClienteValidator>();
            services.AddTransient<IValidator<UpdateClienteCommand>, UpdateClienteValidator>();
            services.AddTransient<IValidator<DeleteClienteCommand>, DeleteClienteValidator>();
            System.Reflection.Assembly assembly = AppDomain.CurrentDomain.Load("CustomerManagement");
            services.AddMediatR(assembly);
            services.AddTransient<IRepository<Cliente>, ClienteRepository>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastBehavior<,>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API CustomerManagement");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
