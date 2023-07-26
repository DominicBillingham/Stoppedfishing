using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AspNetCore.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Create(string UserName, string DisplayName)
        {

            if (UserName.Length < 4)
            {
                return BadRequest("Username is too short");
            }

            var user = new User();
            user.UserName = UserName;
            user.DisplayName = DisplayName ?? UserName;
            _context.Users.Add(user);
            _context.SaveChanges();

            UserService.SetCurrentUser(user.Id);

            return Ok(user.Id);

        }
    }
}
