using PortofollioAPI.DTOs;
using PortofollioAPI.Models;

namespace PortofollioAPI.Repositories.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> VerifyLogin(LoginDTO login);
        Task CreateUserAsync(User userInfo);
    }
}
