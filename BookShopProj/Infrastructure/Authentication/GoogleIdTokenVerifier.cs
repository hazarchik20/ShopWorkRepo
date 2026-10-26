using Application.Abstractions.Authentication;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;

public sealed class GoogleIdTokenVerifier : IGoogleTokenVerifier
{
    private readonly GoogleAuthOptions _options;

    public GoogleIdTokenVerifier(IOptions<GoogleAuthOptions> options)
    {
        _options = options.Value;
    }

    public async Task<VerifiedGoogleUser?> VerifyAsync(
        string idToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(idToken) || idToken.Length > 10_000)
            return null;

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _options.ClientId }
                });

            if (!payload.EmailVerified ||
                string.IsNullOrWhiteSpace(payload.Subject) ||
                string.IsNullOrWhiteSpace(payload.Email))
                return null;

            return new VerifiedGoogleUser(
                payload.Subject,
                payload.Email.Trim().ToLowerInvariant(),
                payload.Name);
        }
        catch (InvalidJwtException)
        {
            return null;
        }
        catch (FormatException)
        {
            return null;
        }
    }
}
