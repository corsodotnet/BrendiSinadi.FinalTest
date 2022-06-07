using LawyerTravelAPI.Models;
using LawyerTravelAPI.Persistence.Configuration;
using LawyerTravelAPI.Utils;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LawyerTravelAPI { 
public class Startup
{
    public IConfiguration Configuration { get; }
    public IApplicationBuilder _app;
    public IWebHostEnvironment _env;
    public string _connStrn;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;

        var builder = new ConfigurationBuilder()
            .SetBasePath(_env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {

        OptionsConfigurationServiceCollectionExtensions
          .Configure<AppSettings>(services, Configuration.GetSection("MySettings"));

        _connStrn = Configuration["ConnectionString"];

            if (_env.IsProduction())
            {
                services.AddDbContext<DatabaseCxt>(options =>
                options.UseInMemoryDatabase(databaseName: "TravelDb"));
            }
            else if (_env.IsStaging())
            {
                // for staging 
            }
            else if (_env.IsDevelopment())
            {
                
               services.AddDbContext<DatabaseCxt>(options => options.UseSqlServer(_connStrn));

                
            }
            services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LawyerTravel", Version = "v1" });
        });
    }

   
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LawyerTravel v1"));


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

