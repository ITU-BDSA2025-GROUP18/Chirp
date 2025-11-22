using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;

namespace Chirp.Web.Tests;

public sealed class TestOAuthPostConfigure : IPostConfigureOptions<OAuthOptions>
{
    public void PostConfigure(string? name, OAuthOptions options)
    {
        options.ClientId ??= "test-client-id";
        options.ClientSecret ??= "test-secret";
        options.SignInScheme ??= options.SignInScheme ?? "Identity.External";

        // Beskyt mod andre obligatoriske felter der kan mangle
        options.AuthorizationEndpoint ??= "https://example.com/oauth/authorize";
        options.TokenEndpoint ??= "https://example.com/oauth/token";
        options.UserInformationEndpoint ??= "https://example.com/oauth/userinfo";
        if (!options.CallbackPath.HasValue)
            options.CallbackPath = "/signin-test";
    }
}
