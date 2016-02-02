using System.Net.Http;
using System.Threading;

namespace Fitbit.Api.Portable
{
    public interface IFitbitClientInterceptor
    {
        void InterceptRequest(HttpRequestMessage request, CancellationToken cancellationToken);
        void InterceptResponse(HttpResponseMessage response, CancellationToken cancellationToken);
    }
}