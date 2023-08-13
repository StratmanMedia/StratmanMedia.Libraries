namespace StratmanMedia.Auth.UserInfo;

public interface IUserInfoResolver
{
    Task<UserInfo?> Resolve(string authorizationHeaderValue);
}