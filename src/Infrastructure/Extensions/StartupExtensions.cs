using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using src.Infrastructure.Security;
using src.Infrastructure.Security.Implements;
using src.Infrastructure.Security.Interfaces;
using src.Persistence.Repositories.UnitOfWork;
using src.Persistence.Repositories.Users.Implements;
using src.Persistence.Repositories.Users.Interfaces;
using src.Persistence.Services.Implements.Users;
using src.Persistence.Services.Interfaces.Users;

namespace src.Infrastructure.Extensions
{
    public static class StartupExtensions
    {
        public static void AddConnection<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options => 
                options.UseSqlServer($"{connectionString}"));
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, Infrastructure.Security.Implements.TokenHandler>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            

            services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            
            var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
			services.AddSingleton(signingConfigurations);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(jwtBearerOptions =>
				{
					jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						IssuerSigningKey = signingConfigurations.SecurityKey,
						ClockSkew = TimeSpan.Zero
					};
				});

            return services;
        }
    }
}