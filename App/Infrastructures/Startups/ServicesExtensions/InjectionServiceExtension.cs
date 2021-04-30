using App.Data.Entities;
using App.Mapper;
using App.Repositories.BaseRepository;
using App.Repositories.Interface;
using App.Services;
using App.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace S4.Infrastructures.Startup.ServicesExtensions
{
    public static class InjectionServiceExtension
    {
        public static void AddInjectedServices(this IServiceCollection services)
        {
            //services.AddTransient<IWebhookEventRepository<WebhookEvent>, WebhookEventRepository>();
            //services.AddTransient<IWebhookEventService, WebhookEventService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            //DI
            services.AddSingleton(mapper);
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<UserManager<User>, UserManager<User>>();
            //services.AddTransient<IBaseRepository<Department>, BaseRepository<Department>>();
            //services.AddTransient<IBaseRepository<Staff>, BaseRepository<Staff>>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStaffService, StaffService>();

        }
    }
}