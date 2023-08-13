using Ardalis.GuardClauses;

namespace StratmanMedia.Auth;

public class StratmanAuthenticationOptions
{
    public string Authority { get; set; } = "";
    public string Audience { get; set; } = "";
    public IEnumerable<string> Scopes { get; set; } = Array.Empty<string>();

    internal void ValidateOptions()
    {
        Authority = Guard.Against.NullOrWhiteSpace(Authority, nameof(Authority));
        Audience = Guard.Against.NullOrWhiteSpace(Audience, nameof(Audience));
    }
}