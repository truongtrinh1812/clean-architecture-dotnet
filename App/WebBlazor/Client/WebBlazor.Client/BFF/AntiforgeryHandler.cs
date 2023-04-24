using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlazor.Client.BFF
{
    public class AntiforgeryHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-CSRF", "1");
            return base.SendAsync(request, cancellationToken);
        }
    }
}