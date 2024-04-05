

namespace TagManage.Domain.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> GenerateAccessToken(string username);
        bool IsValidUser(string username, string password);
    }
}
