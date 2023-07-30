using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using StoppedFishing.Services;

namespace StoppedFishing.Controllers
{
    public class TimeblockController : Controller
    {
        public ApplicationDbContext _context;
        public IUserService _userService;

        public TimeblockController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


    }
}
