using PortfolioAPI.DTOs;
using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> VerifyLogin(LoginDTO login);
        Task CreateUserAsync(User userInfo);
    }
}
