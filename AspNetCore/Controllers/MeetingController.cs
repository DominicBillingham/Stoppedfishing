using AspNetCore.Controllers;
using AspNetCore.Data;
using AspNetCore.Data.Enums;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoppedFishing.Services;
using static System.Net.WebRequestMethods;

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

                return Ok("Meeting created");

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

                return Redirect("~/Meeting/Index");

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
                        x.Name,
                        x.Id
                    }).ToList();

                if (meetings.Count == 0)
                {
                    return NotFound("You haven't got any meetings... yet!");
                }

                var meetingsUpdate = meetings.Select(x => new
                {
                    MeetingLink = $"<a href='/Meeting/MeetingDetails/{x.Id}'>{x.Name}</a>",
                    InviteLink = "https://localhost:44349/Meeting/JoinMeeting/" + x.Id
                });

                return Json(data: meetingsUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        public IActionResult MeetingDetails(int id)
        {
            try
            {
                var meeting = _context.Meetings
                    .Include(meet => meet.Users)
                    .Single(meet => meet.Id == id);

                if (meeting == null)
                {
                    return BadRequest("Meeting not found");
                }

                return View(meeting);


            } catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        public IActionResult FindMeetingTime(int id)
        {
            try
            {
                var meeting = _context.Meetings
                    .Include(meet => meet.Users)
                    .Single(meet => meet.Id == id);

                if (meeting == null)
                {
                    return BadRequest("Meeting not found");
                }

                var dayValues = Enum.GetValues(typeof(Days)).Cast<Days>().ToList().Select(e => (int)e);
                var simpleBlockValues = Enum.GetValues(typeof(SimpleBlocks)).Cast<SimpleBlocks>().ToList().Select(e => (int)e);

                List<SimpleTimeBlock> allMeetingBlocks = new List<SimpleTimeBlock>();

                foreach(var user in meeting.Users)
                {
                    allMeetingBlocks.AddRange(user.SimpleBlocks);
                }

                var meetingTime = allMeetingBlocks.Select(x => new { 
                    x.Day,
                    x.SimpleBlock
                });

                var count = meetingTime
                    .GroupBy(e => e)
                    .Where(e => e.Count() == 2)
                    .Select(e => e.First()).First();

                var data = new
                {
                    Day = count.Day.ToString(),
                    Block = count.SimpleBlock.ToString()

                };

                return Json(data: data);
                //return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
