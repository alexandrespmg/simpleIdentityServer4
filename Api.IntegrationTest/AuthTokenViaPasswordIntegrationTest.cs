using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityModel.Client;
using Newtonsoft.Json;
using Xunit;

namespace Api.IntegrationTests
{
    public class AuthTokenViaPasswordIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthTokenViaPasswordIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SignInMustBeSucess()
        {
            var disco = await _client.GetDiscoveryDocumentAsync();

            disco.IsError.Should().BeFalse();

            var tokenResponse = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });

            tokenResponse.IsError.Should().BeFalse();
        }
    }
}
