using Microsoft.EntityFrameworkCore;
using PortofollioAPI.Data;
using PortofollioAPI.DTOs;
using PortofollioAPI.Models;
using PortofollioAPI.Repositories.Interfaces;

namespace PortofollioAPI.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUserAsync(User userInfo)
        {
            _context.Users.Add(userInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<User> VerifyLogin(LoginDTO login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
            return user;
        }
    }
}
