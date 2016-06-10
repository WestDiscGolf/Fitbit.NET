using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fitbit.Api.Portable.Security
{
    internal class DefaultOAuth2TokenManager : IOAuth2TokenManager
    {
        private static string FitbitOauthPostUrl => "https://api.fitbit.com/oauth2/token";

        public async Task<OAuth2AccessToken> RefreshTokenAsync(FitbitClient client)
        {
            string postUrl = FitbitOauthPostUrl;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", client.AccessToken.RefreshToken),
            });
            
            var httpClient = new HttpClient();

            var clientIdConcatSecret = OAuth2Helper.Base64Encode($"{client.AppCredentials.ClientId}:{client.AppCredentials.ClientSecret}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientIdConcatSecret);

            HttpResponseMessage response = await httpClient.PostAsync(postUrl, content);
            string responseString = await response.Content.ReadAsStringAsync();

            return OAuth2Helper.ParseAccessTokenResponse(responseString);
        }
    }
}
