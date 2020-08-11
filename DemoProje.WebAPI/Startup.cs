using System;
using System.Text;
using DemoProje.Business.Abstract;
using DemoProje.Business.Concrete;
using DemoProje.Business.CustomExtensions;
using DemoProje.Core.DataAccess;
using DemoProje.Core.DataAccess.EntityFramework;
using DemoProje.DataAccess.Abstract;
using DemoProje.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DemoProje.WebAPI
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
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Proje Api", Version = "v1" });
            });
            #endregion

            #region DbContext
            services.AddTransient<DbContext, DemoProjeDbContext>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<DemoProjeDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Connection")));
            services.AddScoped(typeof(IEntityRepository<>), typeof(efRepositoryBase<>));
            #endregion

            #region DAL
            services.AddTransient<IActionTypeDal, efActionTypeDal>();
            services.AddTransient<IMaintenanceDal, efMaintenanceDal>();
            services.AddTransient<IMaintenanceHistoryDal, efMaintenanceHistoryDal>();
            services.AddTransient<IPictureGroupDal, efPitcureGroupDal>();
            services.AddTransient<IStatusDal, efStatusDal>();
            services.AddTransient<IUserDal, efUserDal>();
            services.AddTransient<IVehicleDal, efVehicleDal>();
            services.AddTransient<IVehicleTypeDal, efVehicleTypeDal>();
            #endregion

            #region Services
            services.AddTransient<IActionTypeService, ActionTypeManager>();
            services.AddTransient<IMaintenanceService, MaintenanceManager>();
            services.AddTransient<IMaintenanceHistoryService, MaintenanceHistoryManager>();
            services.AddTransient<IPictureGroupService, PictureGroupManager>();
            services.AddTransient<IStatusService, StatusManager>();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IVehicleService, VehicleManager>();
            services.AddTransient<IVehicleTypeService, VehicleTypeManager>();
            #endregion

            services.AddCors();
            services.AddControllers();

            #region Auth

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            #endregion
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

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Proje Web Api");
            });
        }
    }
}
