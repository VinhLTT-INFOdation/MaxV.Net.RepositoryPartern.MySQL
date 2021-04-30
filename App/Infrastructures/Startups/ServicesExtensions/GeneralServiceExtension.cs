using Microsoft.Extensions.DependencyInjection;
using App.Infrastructures.Dbcontexts;
using App.Data.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using App.Mapper;
using App.Services.Interface;
using App.Services;
using App.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Startup.ServicesExtensions
{
    public static class GeneralServiceExtension
    {
        public static void AddGeneralConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableDetailedErrors(true);

                options.UseMySQL(configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
                options.UseSnakeCaseNamingConvention();
            }); 
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            //DI
            services.AddSingleton(mapper);
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();


            services.AddDistributedMemoryCache();
            var jwtSection = configuration.GetSection("JWT");
            services.Configure<JwtOptions>(jwtSection);
            var appSettings = jwtSection.Get<JwtOptions>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidAudience,
                    ValidIssuer = appSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Example Token: 'Bearer {Token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oath2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}