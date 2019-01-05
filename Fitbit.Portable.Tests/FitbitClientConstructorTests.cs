using Fitbit.Api.Portable;
using Fitbit.Api.Portable.OAuth2;
using NUnit.Framework;
using System.Net.Http;

namespace Fitbit.Portable.Tests
{
    [TestFixture]
    public class FitbitClientConstructorTests
    {
        [Test]
        [Category("constructor")]
        public void Most_Basic()
        {
            var accessToken = new OAuth2AccessToken() { Token = "", TokenType = "", ExpiresIn = 3600, RefreshToken = ""};

            var sut = new FitbitClient(accessToken);

            Assert.IsNotNull(sut.HttpClient);
        }

        [Test]
        [Category("constructor")]
        public void Use_Custom_HttpClient_Factory()
        {
            var sut = new FitbitClient(mh => { return new HttpClient(); });

            Assert.IsNotNull(sut.HttpClient);
        }

        [Test]
        [Category("constructor")]
        public void Can_Instantiate_Without_Any_Interceptors()
        {
            var accessToken = new OAuth2AccessToken() { Token = "", TokenType = "", ExpiresIn = 3600, RefreshToken = "" };

            //Ensure not even the auto-token-refresh interceptor is active
            var sut = new FitbitClient(accessToken, enableOAuth2TokenRefresh: false);

            Assert.IsNotNull(sut.HttpClient);
        }

        [Test]
        [Category("constructor")]
        public void Can_Use_Interceptors_Without_Autorefresh()
        {
            var accessToken = new OAuth2AccessToken() { Token = "", TokenType = "", ExpiresIn = 3600, RefreshToken = "" };

            //Register an interceptor, but disable the auto-token-refresh interceptor
            var sut = new FitbitClient(accessToken, new InterceptorCounter(), false);

            Assert.IsNotNull(sut.HttpClient);
        }
    }
}
