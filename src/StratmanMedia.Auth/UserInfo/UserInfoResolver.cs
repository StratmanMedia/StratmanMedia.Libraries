using System.Net.Http.Headers;
using Auth0.AuthenticationApi;

namespace StratmanMedia.Auth.UserInfo;

public class UserInfoResolver : IUserInfoResolver
{
    private readonly StratmanAuthenticationOptions _options;

    internal UserInfoResolver(StratmanAuthenticationOptions options)
    {
        _options = options;
    }

    public async Task<UserInfo?> Resolve(string authorizationHeaderValue)
    {
        if (!AuthenticationHeaderValue.TryParse(authorizationHeaderValue, out var headerValue))
        {
            return null;
        }

        var client = new AuthenticationApiClient(new Uri(_options.Authority));
        var userInfo = await client.GetUserInfoAsync(headerValue.Parameter);
        var currentUser = (userInfo != null) ? Map(userInfo) : null;

        return currentUser;
    }

    private UserInfo Map(Auth0.AuthenticationApi.Models.UserInfo user)
    {
        var currentUser = new UserInfo
        {
            Subject = user.UserId,
            GivenName = user.FirstName,
            FamilyName = user.LastName,
            EmailAddress = user.Email
        };

        return currentUser;
    }
}