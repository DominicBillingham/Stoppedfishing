using AspNetCore.Data;
using Microsoft.AspNetCore.Mvc;
using StoppedFishing.Services;

namespace AspNetCore.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext _context;
        public UserService UserService;

        public BaseController()
        {
            _context = new ApplicationDbContext();
            UserService = new UserService();
        }

    }
}
