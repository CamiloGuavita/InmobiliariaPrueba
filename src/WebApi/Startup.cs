using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Aplicacion.Interfaz.Repositorio;
using Aplicacion.Interfaz.Servicios;
using Aplicacion.Servicios;
using Infraestructura.Persistencia;
using Infraestructura.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using FluentValidation.AspNetCore;
using FluentValidation;
using Aplicacion.Comun.Request;

namespace WebApi
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
            services.AddAutoMapper(typeof(Aplicacion.Comun.Mappings.AutoMapping));
            services.AddAutoMapper(typeof(Infraestructura.Mappings.AutoMapping));

            services.AddDbContext<InmobiliariaContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOwnerServicio, OwnerServicio>();
            services.AddScoped<IOwnerRepositorio, OwnerRepositorio>();
            services.AddScoped<IPropertyServicio, PropertyServicio>();
            services.AddScoped<IPropertyRepositorio, PropertyRepositorio>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
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
