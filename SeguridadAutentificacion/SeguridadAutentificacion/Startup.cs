using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SeguridadAutentificacion.Context;
using SeguridadAutentificacion.Models;
using SeguridadAutentificacion.Service;
using System;
using System.Text;

namespace SeguridadAutentificacion
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
            // Configuracion de Hash
            services.AddScoped<HashService>();
            // Configuracion encriptacion de datos
            services.AddDataProtection();

            // Configuracion de CORS
            //services.AddCors(); // Aplica para todos los controladores
            services.AddCors(options => 
                        options.AddPolicy("PermitirApiRequest",
                                          builder=>builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader())); // Aplica para controladores o metodos especificos que se le asigne la plitica de seguridad
            // Configuration DB
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            // Configuracion default sistema de usuario
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            // Configurar interpretacion tokens
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["jwt:key"])
                            ),
                            ClockSkew = TimeSpan.Zero
                        }
                    );

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // configuration middleware authenticacion

            app.UseAuthorization();

            // Cors a nivel midleware, aplica para todos los controladores
            //app.UseCors(builder => builder.WithOrigins("https://www.apirequest.io").WithMethods("GET","POST").AllowAnyHeader());
            app.UseCors(); //Cors nivel politica seguridad, Aplica para controladores o metodos especificos que se le asigne la plitica de seguridad
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
