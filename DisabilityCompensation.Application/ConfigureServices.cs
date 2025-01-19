using DisabilityCompensation.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using DisabilityCompensation.Application.Mapping;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.OpenApi.Models;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Infrastructure.FileUploaders;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using DisabilityCompensation.Domain.Validators.FileValidators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace DisabilityCompensation.Application
{
    public static class ConfigureServices
    {
        public static void AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddSwagger();
            services.AddAuthorization();
            services.AddAuthenticationService(configuration);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddConfigs(configuration);
            AddServices(services);
            AddValidators(services);

            services.AddScoped<LocalFileUploader>();
            services.AddScoped<IFileUploader>(provider => FileUploadFactory.CreateUploader(provider, configuration));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<ICompensationService, CompensationService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IFileUploaderService, FileUploaderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAuthorityService, UserAuthorityService>();
            services.AddScoped<IAuthorityService, AuthorityService>();
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IFileValidator, MimeTypeValidator>();
            services.AddScoped<IFileValidator, FileExtensionValidator>();
            services.AddScoped<IFileValidator, MaxFileSizeValidator>();
            services.AddScoped<ICompositeFileValidator, CompositeFileValidator>();
        }

        private static void AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings!.Issuer!,
                    ValidAudience = jwtSettings!.Audience!,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.SecretKey!))
                };
            });
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT token'ınızı girin. Örnek: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileUploadSettings>(configuration.GetSection(nameof(FileUploadSettings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.Configure<FileValidatorSettings>(configuration.GetSection(nameof(FileValidatorSettings)));
        }

        public static void UseStaticFiles(this WebApplication app)
        {
            var fileUploadSettings = app.Services.GetRequiredService<IOptions<FileUploadSettings>>().Value;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), fileUploadSettings.LocalTargetPath!);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath),
                RequestPath = fileUploadSettings.RequestPath
            });
        }
    }
}
