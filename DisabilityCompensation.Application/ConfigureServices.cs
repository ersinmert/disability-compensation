using DisabilityCompensation.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using DisabilityCompensation.Application.Mapping;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Services;

namespace DisabilityCompensation.Application
{
    public static class ConfigureServices
    {
        public static void AddInjectionApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICompensationService, CompensationService>();
            services.AddScoped<IParameterService, ParameterService>();
        }
    }
}
