using AspNetCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace StoppedFishing.Services
{
    public interface IUserService
    {
        public void SetCurrentUser(int userId);
        public User GetCurrentUser();
        public int? GetCurrentUserId();
    }
}
