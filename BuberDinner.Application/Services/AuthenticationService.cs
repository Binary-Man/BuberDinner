using System.Text;
using BuberDinner.Application.Common.interfaces.Authentication;
using BuberDinner.Application.Common.interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRespository _userRespository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRespository userRespository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRespository = userRespository;
    }
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRespository.GetUserByEmail(email) is not null)
        {
            throw new Exception("user already exist");
        }
        // 2. Create user (generate unique Id) & persist user to data store
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRespository.Add(user);
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,          
            token
        );
    }
    public AuthenticationResult Login(string email, string password)
    {
        // 1. validate that the user exists
        if (_userRespository.GetUserByEmail(email) is not User user)
            throw new Exception("User does not exist");

        // 2. validate the password is correct
        if (user.Password != password)
            throw new Exception("Invalid password");

        // 3. create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }
}