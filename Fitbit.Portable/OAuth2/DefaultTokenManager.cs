using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fitbit.Api.Portable.OAuth2
{
    public class DefaultTokenManager : ITokenManager
    {
        private readonly FitbitAppCredentials _credentials;

        public DefaultTokenManager(FitbitAppCredentials credentials)
        {
            _credentials = credentials;
        }

        public async Task<OAuth2AccessToken> RefreshTokenAsync(FitbitClient client)
        {
            string postUrl = Constants.OAuth2TokenUrl;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", client.AccessToken.RefreshToken),
            });
            
            HttpClient httpClient;
            if (client.HttpClient == null)
            {
                httpClient = new HttpClient();
            }
            else
            {
                httpClient = client.HttpClient;
            }

            var clientIdConcatSecret = OAuth2Helper.Base64Encode(_credentials.ClientId + ":" + _credentials.ClientSecret);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", clientIdConcatSecret);

            HttpResponseMessage response = await httpClient.PostAsync(postUrl, content);
            string responseString = await response.Content.ReadAsStringAsync();

            return OAuth2Helper.ParseAccessTokenResponse(responseString);
        }
    }
}
