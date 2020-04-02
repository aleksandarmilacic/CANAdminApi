using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CANAdminApi.Data;
using CANAdminApi.Data.Repositories;
using CANAdminApi.Services.Automapper;
using CANAdminApi.Services.Services;
using CANAdminApi.Services.Services.Custom;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CANAdminApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void AddServices(IServiceCollection services)
        {
            var asm = typeof(GroundBaseService).Assembly;
            var serv = asm.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(GroundBaseService)))
                ;
            foreach (var typ in serv)
            {
                services.AddScoped(typ);
            }
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Context>();
            services.AddScoped(typeof(Repository<>));

            AddServices(services);
            services.AddScoped<ColumnMappingService>();
            services.AddHttpContextAccessor();
            services.AddMvc(config =>
            {

               
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CANAdminApi", Version = "v1" });
          
            });
            services.AddCors(o => o.AddPolicy("CSPolicy", builder =>
            {

                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
         
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = new CANAdminApi.Services.Exceptions.GenericException().Invoke
                });
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CSPolicy");
            AutomapperConfig.RegisterMappings();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CANAdminApi V1");
            });
            app.UseAuthentication();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
