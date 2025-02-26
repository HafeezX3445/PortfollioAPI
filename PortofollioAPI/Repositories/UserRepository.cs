using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;
using PortfolioAPI.Repositories.Interfaces;
using PortfolioAPI.Services;

namespace PortfolioAPI.Repositories
{
    public class UserRepository(ApplicationDbContext _context, RedisCacheService cacheService) : IUser
    {
        private const string CacheKey = "AllUsers";

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var cachedUsers = await cacheService.GetCacheAsync<IEnumerable<User>>(CacheKey);
            if (cachedUsers != null)
            {
                return cachedUsers; // Return cached data if available
            }
            var users = await _context.Users.ToListAsync();

            // Store data in cache for future requests
            await cacheService.SetCacheAsync(CacheKey, users, TimeSpan.FromMinutes(10));

            return users;
        }

        public async Task CreateUserAsync(User userInfo)
        {
            _context.Users.Add(userInfo);
            await _context.SaveChangesAsync();

            // Remove cache since data has changed
            await cacheService.RemoveCacheAsync(CacheKey);
        }

        public async Task<User> VerifyLogin(LoginDTO login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
            return user;
        }



    }
}
