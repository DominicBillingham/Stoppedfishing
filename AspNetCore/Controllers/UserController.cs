using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public IActionResult UpdateUserTimeBlocks(List<HourBlock> blocks)
        {

            try
            {
                var user = _userService.GetCurrentUser();
                var timeBlocks = ComposeTimeBlocks(blocks);


                if (user == null)
                {
                    return BadRequest();
                }

                //user.SimpleBlocks = new List<SimpleTimeBlock>();

                //foreach (var block in blocks)
                //{
                //    user.SimpleBlocks.Add(block);
                //}

                //_context.SaveChanges();

                return Redirect("~/Meeting/Index");


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //public IActionResult LogIn(string userName)
        //{

        //    try
        //    {
        //        var user = _context.Users.FirstOrDefault(user => user.UserName == userName);

        //        if (user == null)
        //        {
        //            return BadRequest("User not found!");
        //        }

        //        _userService.SetCurrentUserId(user.Id);

        //        return Redirect("~/Home/Index");

        //    } catch (Exception ex) 
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
        //public IActionResult SignOut()
        //{

        //    try
        //    {
        //        _userService.SignOutCurrentUser();
        //        return Redirect("~/Home/Index");

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        //public IActionResult GetCurrentUserName() {

        //    try
        //    {
        //        var user = _userService.GetCurrentUser();
        //        if (user == null)
        //        {
        //            return BadRequest("User not found");
        //        }
        //        return Ok(user.UserName);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}


        public IActionResult xxxUpdateUserTimeBlocks(int userId, List<SimpleTimeBlock> blocks)
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

        private List<TimeBlock> ComposeTimeBlocks(List<HourBlock> blocks)
        {

            var days = blocks
                .GroupBy(e => e.Day)
                .ToList();

            var blockList = new List<TimeBlock>();

            foreach (var day in days)
            {

                var hourList = day.Select(x => x.Hour).ToList();
                var length = 0;

                if (hourList.Count == 1)
                {
                    var timeBlock = new TimeBlock();
                    timeBlock.FinalHour = hourList.First();
                    timeBlock.StartHour = hourList.First();
                    timeBlock.Day = day.First().Day;
                    blockList.Add(timeBlock);
                    length = 0;
                }

                for (int i = 0; i < hourList.Count-1; i++)
                {

                    int current = hourList[i];
                    int next = hourList[i + 1];

                    if (current + 1 == next)
                    {
                        length++;
                    }
                    else
                    {
                        var timeBlock = new TimeBlock();
                        timeBlock.FinalHour = current;
                        timeBlock.StartHour = current - length;
                        timeBlock.Day = day.First().Day;
                        blockList.Add(timeBlock);
                        length = 0;
                    }

                    if (i == hourList.Count - 2)
                    {

                        var timeBlock = new TimeBlock();
                        timeBlock.FinalHour = next;
                        timeBlock.StartHour = next - length;
                        timeBlock.Day = day.First().Day;
                        blockList.Add(timeBlock);
                        length = 0;

                    }


                }

            }

            return blockList;

        }


        public IActionResult DecomposeTimeBlocks(List<TimeBlock> blocks)
        {
            try
            {

                var days = blocks
                   .GroupBy(e => e.Day)
                   .ToList();

                var hourBlockList = new List<HourBlock>();

                foreach (var day in days)
                {
                    foreach (var block in day)
                    {

                        var start = block.StartHour;
                        var final = block.FinalHour;

                        for (int i = start;  i <= final; i++) {

                            var hourBlock = new HourBlock();
                            hourBlock.Hour = i;
                            hourBlock.Day = block.Day;
                            hourBlockList.Add(hourBlock);
                        }


                    }


                }

                FindOverlap(hourBlockList);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult FindOverlap(List<HourBlock> blocks)
        {
            try
            {

                var meetingTime = blocks.Select(x => new {
                    Day = x.Day.ToString(),
                    Hour = x.Hour
                });

                var matchingBlocks = meetingTime
                    .GroupBy(e => e)
                    .Where(e => e.Count() == 2)
                    .Select(e => e.First())
                    .ToList();

                return Ok();
                //return Json(data: matchingBlocks);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
