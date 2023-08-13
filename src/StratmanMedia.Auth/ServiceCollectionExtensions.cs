using System.Security.Claims;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StratmanMedia.Auth.UserInfo;

namespace StratmanMedia.Auth;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStratmanMediaAuthentication(
        this IServiceCollection services, Action<StratmanAuthenticationOptions> optionsAction)
    {
        optionsAction = Guard.Against.Null(optionsAction, nameof(optionsAction));
        var options = new StratmanAuthenticationOptions();
        optionsAction.Invoke(options);
        options.ValidateOptions();
        AddAuthentication(services, options);
        AddServices(services, options);

        return services;
    }

    public static IServiceCollection AddStratmanMediaAuthenticationServices(
        this IServiceCollection services,
        Action<StratmanAuthenticationOptions> optionsAction)
    {
        optionsAction = Guard.Against.Null(optionsAction, nameof(optionsAction));
        var options = new StratmanAuthenticationOptions();
        optionsAction.Invoke(options);
        options.ValidateOptions();
        AddServices(services, options);

        return services;
    }

    private static void AddAuthentication(IServiceCollection services, StratmanAuthenticationOptions options)
    {
        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = options.Authority;
                o.Audience = options.Audience;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
                //o.RequireHttpsMetadata = true;
                //o.SaveToken = true;
                //o.JwtBearerEvents.OnTokenValidated = OnTokenValidated;
            });
        services.AddAuthorization(o =>
        {
            foreach (var scope in options.Scopes)
            {
                o.AddPolicy(scope, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new ScopeRequirement(scope, options.Authority));
                });
            }
        });
        services.AddSingleton<IAuthorizationHandler, ScopeHandler>();
    }

    private static void AddServices(IServiceCollection services, StratmanAuthenticationOptions options)
    {
        services.AddScoped<IUserInfoResolver>(s => new UserInfoResolver(options));
    }
}