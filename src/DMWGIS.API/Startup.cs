using DMWGIS.API.Adapter;
using DMWGIS.API.Adapter.Interface;
using DMWGIS.API.Helper;
using DMWGIS.API.Helper.Interface;
using DMWGIS.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Type = DMWGIS.API.Models.Type;

namespace DMWGIS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "http://dmwgiscalib.southcentralus.cloudapp.azure.com/dmwgisui")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            var mongoDbSettings = services.FirstOrDefault(descripter => descripter.ServiceType == typeof(IConfigureOptions<MongoDbSettings>));
            if (mongoDbSettings == null)
                ConfigureMongoDb(services);

            services.AddControllers();

            services.AddScoped<IVolunteerDetailsAdapter, VolunteerDetailsAdapter>();
            services.AddScoped<IGetUserDataAdapter, GetUserDataAdapter>();
            services.AddScoped<IMongoClientHelper<UserNotification>, MongoClientHelper<UserNotification>>();
            services.AddScoped<IMongoClientHelper<City>, MongoClientHelper<City>>();
            services.AddScoped<IMongoClientHelper<Type>, MongoClientHelper<Type>>();
            services.AddScoped<IMongoClientHelper<Volunteer>, MongoClientHelper<Volunteer>>();
            services.AddScoped<IMongoClientHelper<AssignVolunteer>, MongoClientHelper<AssignVolunteer>>();
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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ConfigureMongoDb(IServiceCollection services)
        {
            var dbSettings = new MongoDbSettings();
            Configuration.GetSection("MongoDbSettings").Bind(dbSettings);
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("MONGO_CONNECTIONSTRING");
                options.DbName = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");
                options.AlertNotificationCollection = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("NOTIFICATION_COLLECTION");
                options.VolunteerAssignCollection = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("ASSIGN_VOLUNTEER_COLLECTION");
                options.VolunteerCollection = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("VOLUNTEER_COLLECTION");
                options.CityMasterCollection = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("CITY_MASTER_COLLECTION");
                options.TypeCollection = dbSettings.ConnectionString ?? Environment.GetEnvironmentVariable("TYPE_COLLECTION");

            });
        }
    }
}
