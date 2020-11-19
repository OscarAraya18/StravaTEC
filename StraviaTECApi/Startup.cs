using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StraviaTECApi.Models;

namespace StraviaTECApi
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
            services.AddControllers().AddNewtonsoftJson(
                s => {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            services.AddMvc().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddEntityFrameworkNpgsql().AddDbContext<StraviaContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("StraviaTECConnection")));

            // Customize our CORS policy
            services.AddCors(o => o.AddPolicy(
                "CORS_POLICY",
                builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));


            // se inyectan las dependencias
            services.AddScoped<CarreraRepo>();
            services.AddScoped<CategoriaRepo>();
            services.AddScoped<DeportistaRepo>();
            services.AddScoped<GrupoRepo>();
            services.AddScoped<InscripcionRepo>();
            services.AddScoped<PatrocinadorRepo>();
            services.AddScoped<RetoRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Apply CORS Policy to every request
            app.UseCors("CORS_POLICY");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
