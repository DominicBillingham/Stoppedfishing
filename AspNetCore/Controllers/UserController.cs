using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using StoppedFishing.Data.Models;
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

        public IActionResult CreateNewUser(string userName, string displayName)
        {

            try
            {
                if (userName == null || userName.Length < 4)
                {
                    return BadRequest("Username is too short");
                }

                var user = new User();
                user.UserName = userName;
                user.DisplayName = displayName ?? userName;
                _context.Users.Add(user);
                _context.SaveChanges();

                _userService.SetCurrentUserId(user.Id);

                return Ok(user.Id);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult LogIn(string userName)
        {

            try
            {
                var user = _context.Users.FirstOrDefault(user => user.UserName == userName);

                if (user == null)
                {
                    return BadRequest("User not found!");
                }

                _userService.SetCurrentUserId(user.Id);

                return Redirect("~/Home/Index");

            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        public IActionResult SignOut()
        {

            try
            {
                _userService.SignOutCurrentUser();
                return Redirect("~/Home/Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult GetCurrentUserName() {

            try
            {
                var user = _userService.GetCurrentUser();
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                return Ok(user.UserName);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        public IActionResult UpdateUserTimeBlocks(int userId, List<SimpleTimeBlock> blocks)
        {
            try
            {
                var user = _context.Users.Find(userId);

                if (user == null)
                {
                    return BadRequest();
                }

                user.SimpleBlocks = new List<SimpleTimeBlock>();

                foreach (var block in blocks)
                {
                    user.SimpleBlocks.Add(block);
                }

                _context.SaveChanges();

                return Redirect("~/Meeting/Index");
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        public IActionResult ComposeTimeBlocks(List<HourBlock> blocks)
        {
            try
            {   

                var days = blocks
                    .GroupBy(e => e.Day)
                    .ToList();

                foreach (var day in days)
                {

                    foreach(var block in day)
                    {

                        var fish = 0;




                    }



                }

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
