using System.Threading.Tasks;

namespace Fitbit.Api.Portable.Security
{
    public interface IOAuth2TokenManager
    {
        Task<OAuth2AccessToken> RefreshTokenAsync(FitbitClient client);
    }
}