using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using StoppedFishing.Services;
using System.Net;

namespace AspNetCore.Controllers
{
    public class UserController : Controller
    {
        public ApplicationDbContext _context;
        public IUserService _userService;

        public UserController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public ActionResult Create(string UserName, string DisplayName)
        {

            if (UserName == null || UserName.Length < 4)
            {
                return BadRequest("Username is too short");
            }

            var user = new User();
            user.UserName = UserName;
            user.DisplayName = DisplayName ?? UserName;
            _context.Users.Add(user);
            _context.SaveChanges();

            _userService.SetCurrentUser(user.Id);

            return Ok(user.Id);

        }

        public ActionResult SignIn(int Id)
        {
            var user = _context.Users.Find(Id);

            if (user == null) 
                return BadRequest("User not found!");

            _userService.SetCurrentUser(user.Id);

            return RedirectToAction("~/Home/Index");

        }

        public ActionResult LogIn(string UserName)
        {
            var user = _context.Users.FirstOrDefault(user => user.UserName == UserName);

            if (user == null)
                return BadRequest("User not found!");

            _userService.SetCurrentUser(user.Id);

            return Redirect("~/Home/Index");

        }

        public ActionResult GetCurrentUserName() {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user.UserName);
        }
    }
}
