using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace WebBlazor.Client.BFF
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        public ValueTask<AccessTokenResult> RequestAccessToken()
        {
            throw new NotImplementedException();
        }

        public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}