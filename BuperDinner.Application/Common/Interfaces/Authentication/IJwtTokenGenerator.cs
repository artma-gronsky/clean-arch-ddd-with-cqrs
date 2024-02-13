using BuperDinner.Domain.Entities;

namespace BuperDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator{
    string GenerateToken(User user);
}