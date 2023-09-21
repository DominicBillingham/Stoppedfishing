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
                return View();

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult Join(string id)
        {
            try
            {
                var meeting = _context.Meetings.Find(id);

                if (meeting == null)
                {
                    return BadRequest("Meeting not found!");
                }

                return View(meeting);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
    
}
