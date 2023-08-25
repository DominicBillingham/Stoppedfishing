using AspNetCore.Data;

namespace StoppedFishing.Services
{
    public class TimezoneService
    {
        private ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext _httpContext;

        public TimezoneService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }

        public int? GetUserUTCOffset()
        {
            var localZone = TimeZoneInfo.Local.BaseUtcOffset.TotalHours;
            return Convert.ToInt32(localZone);
        }

        public void ConvertTimezoneToUTC(int userId)
        {
            _httpContext.Session.SetInt32("UserId", userId);
        }



    }
}
