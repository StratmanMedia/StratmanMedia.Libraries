using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;

namespace StratmanMedia.Auth;

public class ScopeRequirement : IAuthorizationRequirement
{
    public string Scope { get; set; }
    public string Authority { get; set; }

    public ScopeRequirement(string scope, string authority)
    {
        Scope = Guard.Against.NullOrWhiteSpace(scope, nameof(scope));
        Authority = Guard.Against.NullOrWhiteSpace(authority, nameof(authority));
    }
}