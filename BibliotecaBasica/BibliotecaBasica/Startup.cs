using AutoMapper;
using BibliotecaBasica.Context;
using BibliotecaBasica.Entities;
using BibliotecaBasica.Helpers;
using BibliotecaBasica.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BibliotecaBasica
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
            // Configure automapper
            services.AddAutoMapper(
                configuration => {
                    configuration.CreateMap<Autor, AutorDTO>(); // Autor -> AutorDTO
                    configuration.CreateMap<AutorCreacionDTO, Autor>().ReverseMap(); // AutorCreacionDTO -> <- Autor
                },
                typeof(Startup));
            // Configuration DB
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.AddControllers()
                .AddNewtonsoftJson(
                    // Configuración para ingnorar referencia ciclica
                    option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            // Configuracion para poder obtener IUrlHelper en GenerarEnlaces de una clase que no hereda ControllerBase
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            // Registrar servicios HATEOAS
            services.AddScoped<HATEOASAuthorFilterAttribute>(); // Configuracion HATEOAS Autor
            services.AddScoped<HATEOASAuthorsFilterAttribute>(); // Configuracion HATEOAS Autores
            services.AddScoped<GeneradorEnlaces>();

            // Configuracion swagger [Swashbuckle.AspNetCore]
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new OpenApiInfo { Version = "V1", Title = "Biblioteca Web Api" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Config swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>{
                //https://localhost:44306/swagger/index.html
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca Api V1");
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
