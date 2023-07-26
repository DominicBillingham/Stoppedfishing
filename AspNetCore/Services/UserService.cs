using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Http;

namespace StoppedFishing.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCurrentUser(int userId)
        {
            HttpContext _httpContext = _httpContextAccessor.HttpContext;
            _httpContext.Session.SetInt32("UserId", userId);
        }
        public User GetCurrentUser()
        {
            HttpContext _httpContext = _httpContextAccessor.HttpContext;
            var userId = _httpContext.Session.GetInt32("UserId");
            return _context.Users.Find(userId);
        }
    }
}
