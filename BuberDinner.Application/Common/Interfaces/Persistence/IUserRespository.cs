using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.interfaces.Persistence;
public interface IUserRespository
{
    User? GetUserByEmail(string emails);
    void Add(User user);

}