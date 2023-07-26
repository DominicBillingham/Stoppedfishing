using AspNetCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext _context;

        public BaseController()
        {
            _context = new ApplicationDbContext();
        }

    }
}
