namespace Fitbit.Api.Portable
{
    internal class Constants
    {
        public static string BaseApiUrl => "https://api.fitbit.com/";

        /// <summary>
        /// The fitbit api url for oAuth2 token request
        /// </summary>
        public static string OAuth2TokenUrl => $"{BaseApiUrl}oauth2/token";

        public const string FloorsUnsupportedOnDeviceError = "Invalid time series resource path: /activities/floors";

        public class Headers
        {
            public const string XFitbitSubscriberId = "X-Fitbit-Subscriber-Id";   
        }

        public class Formatting
        {
            public const string TrailingSlash = "{0}/";
            public const string LeadingDash = "-{0}";
        }
    }
}
