using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DisabilityCompensation.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICompensationRepository, CompensationRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IClaimantRepository, ClaimantRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IUserAuthorityRepository, UserAuthorityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorityRepository, AuthorityRepository>();
            services.AddScoped<IRoleAuthorityRepository, RoleAuthorityRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            return services;
        }
    }
}
