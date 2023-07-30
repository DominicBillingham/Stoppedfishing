using AspNetCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace StoppedFishing.Services
{
    public interface IUserService
    {
        public void SetCurrentUserId(int userId);
        public int? GetCurrentUserId();
        public User GetCurrentUser();
        public void SignOutCurrentUser();

    }
}
