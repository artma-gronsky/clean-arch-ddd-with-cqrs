using BuperDinner.Domain.Entities;

namespace BuperDinner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);