using AspNetCore.Data;
using AspNetCore.Data.Enums;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoppedFishing.Services;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller 
    {

        public ApplicationDbContext _context;
        public IUserService _userService;

        public HomeController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Index()
        {
            try
            {
                var user = _userService.GetCurrentUser();
                return View(user);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public void ClearData()
        {
            var Users = _context.Users.ToList();

            foreach (var user in Users)
            {
                var SimpleTimeBlocks = user.SimpleBlocks.ToList();

                foreach (var block in  SimpleTimeBlocks)
                {
                    user.SimpleBlocks.Remove(block);
                    _context.SaveChanges();
                }
            }

            _context.Users.RemoveRange(Users);

            var Meetings = _context.Meetings.ToList();
            _context.Meetings.RemoveRange(Meetings);

            _context.SaveChanges();
        }

    }
    
}
