using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ApiGateway.API.Authentication;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "Basic";

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        if (!AuthenticationHeaderValue.TryParse(authorizationHeader.ToString(), out var headerValue)
            || !string.Equals(headerValue.Scheme, "Basic", StringComparison.OrdinalIgnoreCase)
            || string.IsNullOrWhiteSpace(headerValue.Parameter))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }

        string username;
        string password;

        try
        {
            var credentialBytes = Convert.FromBase64String(headerValue.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (credentials.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            username = credentials[0];
            password = credentials[1];
        }
        catch (FormatException)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }

        if (!string.Equals(username, Options.Username, StringComparison.Ordinal)
            || !string.Equals(password, Options.Password, StringComparison.Ordinal))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers.WWWAuthenticate = "Basic realm=\"ApiGateway\"";
        return base.HandleChallengeAsync(properties);
    }
}
