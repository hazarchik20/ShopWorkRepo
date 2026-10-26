namespace Application.Abstractions.Authentication;

public sealed record VerifiedGoogleUser(string Subject, string Email, string? Name);

public interface IGoogleTokenVerifier
{
    Task<VerifiedGoogleUser?> VerifyAsync(string idToken, CancellationToken cancellationToken = default);
}
