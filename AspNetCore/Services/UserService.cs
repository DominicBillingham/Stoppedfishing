using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Http;

namespace StoppedFishing.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext _httpContext;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }

        public void SetCurrentUserId(int userId)
        {
            _httpContext.Session.SetInt32("UserId", userId);
        }
        public int? GetCurrentUserId()
        {
            return _httpContext.Session.GetInt32("UserId");
        }
        public User GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            return _context.Users.Find(userId);
        }

        public void SignOutCurrentUser() {
            _httpContext.Session.Remove("UserId");
        }

    }
}
