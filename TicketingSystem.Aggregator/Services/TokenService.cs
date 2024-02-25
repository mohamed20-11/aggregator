namespace DynamicWorkflow.Aggregator.Services
{
    public class TokenService : ITokenService
    {
        private IHttpContextAccessor _contextAccessor;

        public TokenService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<CallOptions> AcquireToken()
        {
            string jwtToken = _contextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return new CallOptions();
            }

            if (jwtToken.StartsWith("Bearer "))
            {
                jwtToken = jwtToken.Substring("Bearer ".Length);
            }
            var metadata = new Metadata
            {
               { "Authorization", $"Bearer {jwtToken}" }
            };
            var callOptions = new CallOptions(metadata);
            return await Task.FromResult(callOptions);
        }


        public async Task<CallOptions> GetAccessTokenAsync()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7152");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "AggregatorClient",
                ClientSecret = "secret",
                Scope = "CPAggregatorAPI"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Error getting access token: {tokenResponse.Error}");
            }

            var jwtToken = tokenResponse.AccessToken;

            var metadata = new Metadata
            {
               { "Authorization", $"Bearer {jwtToken}" }
            };
            var callOptions = new CallOptions(metadata);
            return await Task.FromResult(callOptions);
        }
    }
}
