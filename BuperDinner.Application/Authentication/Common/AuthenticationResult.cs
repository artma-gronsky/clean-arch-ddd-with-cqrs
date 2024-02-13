using BuperDinner.Domain.Entities;

namespace BuperDinner.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);