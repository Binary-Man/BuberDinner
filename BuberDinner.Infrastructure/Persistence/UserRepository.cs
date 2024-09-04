using BuberDinner.Application.Common.interfaces.Persistence;
using BuberDinner.Domain.Entities;
namespace BuberDinner.Infrastructure.Persistence;

public class UserRespository : IUserRespository
{
    private static readonly List<User> _users = new();

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}