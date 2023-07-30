using AspNetCore.Controllers;
using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoppedFishing.Services;

namespace StoppedFishing.Controllers
{
    public class MeetingController : Controller
    {
        public ApplicationDbContext _context;
        public IUserService _userService;

        public MeetingController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateMeeting(string meetingName) {

            try
            {

                var meeting = new Meeting();
                meeting.Name = meetingName;
                _context.Meetings.Add(meeting);

                var userId = _userService.GetCurrentUserId();
                var user = _context.Users
                    .Include(user => user.Meetings)
                    .Single(user => user.Id == userId);

                user.Meetings.Add(meeting);
                _context.SaveChanges();

                return Ok();

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult JoinMeeting(int id)
        {
            try
            {

                var meeting = _context.Meetings
                    .Include(meet => meet.Users)
                    .Single(meet => meet.Id == id);

                var user = _userService.GetCurrentUser();

                if (meeting == null || user == null)
                {
                    return BadRequest("Meeting or User not found");
                }

                meeting.Users.Add(user);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult GetUserMeetings()
        {

            try
            {
                var user = _userService.GetCurrentUser();

                if (user == null)
                {
                    return BadRequest("Sign in to be able to see your current meetings");
                }

                var meetings = _context.Meetings
                    .Where(meet => meet.Users.Contains(user))
                    .Select(x => new
                    {
                        x.Name
                    }).ToList();

                if (meetings.Count == 0)
                {
                    return NotFound("You haven't got any meetings... yet!");
                }

                return Json(data: meetings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
    }
}
