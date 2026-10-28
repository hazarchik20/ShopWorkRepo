using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Advart;
using Application.Abstractions.Persistence.Order;
using Application.Abstractions.Persistence.User;
using Application.Abstractions.Storage;
using Infrastructure.Authentication;
using Infrastructure.Repositories.Advart;
using Infrastructure.Repositories.Order;
using Infrastructure.Repositories.Users;
using Infrastructure.Services;
using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSecurityRepository, UserSecurityRepository>();
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Authentication
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGoogleTokenVerifier, GoogleIdTokenVerifier>();

            services.AddOptions<GoogleAuthOptions>()
                .Bind(configuration.GetSection(GoogleAuthOptions.SectionName))
                .Validate(options => !string.IsNullOrWhiteSpace(options.ClientId),
                    "GoogleAuth:ClientId is required.")
                .ValidateOnStart();


            services.AddOptions<FileStorageOptions>()
                .Bind(configuration.GetSection(FileStorageOptions.SectionName))
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.UploadPath) &&
                               !Path.IsPathRooted(options.UploadPath) &&
                               !options.UploadPath.Contains(".."),
                    "Invalid upload directory.")
                .Validate(
                    options => options.MaxFileSizeBytes > 0 &&
                               options.MaxFileSizeBytes <= 10 * 1024 * 1024,
                    "Invalid maximum file size.")
                .ValidateOnStart();


            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(JwtOptions.SectionName))
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Issuer),
                    "JWT Issuer is required.")
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Audience),
                    "JWT Audience is required.")
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Secret) && Encoding.UTF8.GetByteCount(options.Secret) >= 32,
                    "JWT Secret must contain at least 32 UTF-8 bytes.")
                .Validate(
                    options => options.ExpirationMinutes > 0,
                    "JWT expiration must be greater than zero.")
                .ValidateOnStart();


            return services;
        }
    }
}
