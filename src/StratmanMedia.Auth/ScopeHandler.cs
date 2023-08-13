using Microsoft.AspNetCore.Authorization;

namespace StratmanMedia.Auth;

public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Authority))
            return Task.CompletedTask;

        var scopes = context.User.FindFirst(c => c.Type == "permissions" && c.Issuer == requirement.Authority)?.Value.Split(' ');
        if (scopes != null && scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}