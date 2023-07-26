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

        public HomeController(IUserService userService)
        {
            _context = new ApplicationDbContext();
            _userService = userService;
        }


        // GET: HomeController
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult UserBlocks(int userId, List<SimpleTimeBlock> blocks)  
        {

            var user = _context.Users.Find(userId);

            if (user == null) 
            { 
                return BadRequest(); 
            }

            foreach (var block in blocks)
            {
                user.SimpleBlocks.Add(block);
            }

            _context.SaveChanges(); 

            return Ok("worked");
        }

    }
    
}
