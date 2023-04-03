using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using AutoMapper;
using RegisterUser.API.Middlewares;
using RegisterUser.BusinessLayer.Interfaces;
using RegisterUser.BusinessLayer.Services;
using Microsoft.OpenApi.Models;
using RegisterUser.Repositories.Repositories;
using RegisterUser.DAL.Models;
using RegisterUser.Repositories.Repositories.Interfaces;

namespace RegisterUser.API
{
    public class Startup
    {
        #region Constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        private IConfiguration Configuration { get; set; }

        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddControllers().AddNewtonsoftJson();

            #region Configure Swagger  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Registration User API", Version = "v1", Description = "API Documentation" });
            });
            #endregion

            services.AddCors();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<RegisterUserContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RegisterUserConnectionString")));

            DependenceInjection(services);
        }

        private void DependenceInjection(IServiceCollection services)
        {
            services.AddScoped<RegisterUserContext>();
            services.AddScoped<IRegisterUserServices, RegisterUserServices>();
            services.AddScoped<DbContext, RegisterUserContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUserDetail, UserDetailBl>();
            services.AddScoped<ICountry, CountryBl>();
            services.AddScoped<IProvince, ProvinceBl>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                          pattern: "{controller=Default}/{action=Index}/{id?}");
            });

            //Seed database
            DbInitializer.Seed(app);
        }
    }
}
