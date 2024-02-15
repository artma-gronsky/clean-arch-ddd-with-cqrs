namespace BuberDinner.Infrastructure.Authentication;

public class JwtSettings
{
    public required string Secret { get; init; }

    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public int ExpirityMinutes { get; init; }
}