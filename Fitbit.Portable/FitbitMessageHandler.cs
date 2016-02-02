using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Fitbit.Api.Portable
{
    /// <summary>
    /// Fitbit Message Handler which wraps the specified interceptor
    /// </summary>
    internal class FitbitMessageHandler : DelegatingHandler
    {
        private readonly IFitbitClientInterceptor _interceptor;

        internal FitbitMessageHandler(IFitbitClientInterceptor interceptor) : base(new HttpClientHandler())
        {
            _interceptor = interceptor;
        }

        // We override the SendAsync method to intercept both the request and response path
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // interceptor pre
            _interceptor?.InterceptRequest(request, cancellationToken);

            // pass it onto the base
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    // interceptor post
                    _interceptor?.InterceptResponse(task.Result, cancellationToken);
                    return task.Result;
                },
                TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
