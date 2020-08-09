using System.Text;
using DemoProje.Business.Abstract;
using DemoProje.Business.Concrete;
using DemoProje.Business.CustomExtensions;
using DemoProje.Core.DataAccess;
using DemoProje.Core.DataAccess.EntityFramework;
using DemoProje.DataAccess.Abstract;
using DemoProje.DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            //services.AddTransient<ITokenSerivce, TokenManager>();
            #endregion

            #region JWT
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false; 
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), 
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            services.AddCors();
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
