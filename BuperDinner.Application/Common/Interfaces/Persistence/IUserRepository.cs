using BuperDinner.Domain.Entities;

namespace BuperDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository{
    User? GetUserByEmail(string email);

    void AddUser(User user);
}