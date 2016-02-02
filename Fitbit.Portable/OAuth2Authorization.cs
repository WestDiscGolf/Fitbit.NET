using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Fitbit.Api.Portable
{
    public class OAuth2Authorization : IAuthorization
    {
        internal string BearerToken { get; private set; }
        internal string RefreshToken { get; private set; }

        public OAuth2Authorization(string bearerToken, string refreshToken)
        {
            RefreshToken = refreshToken;
            BearerToken = bearerToken;
        }

        public void InterceptRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
        }

        public void InterceptResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            // nothing to see here
        }
    }
}
