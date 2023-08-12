using AspNetCore.Controllers;
using AspNetCore.Data;
using AspNetCore.Data.Enums;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoppedFishing.Data.Models;
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

        public IActionResult CreateMeeting(string meetingName, int startHour, int endHour) {

            try
            {

                var meeting = new Meeting();
                meeting.Id = GenerateToken();
                meeting.Name = meetingName;
                meeting.startHour = startHour;
                meeting.endHour = endHour;
                _context.Meetings.Add(meeting);
                _context.SaveChanges();

                return RedirectToAction("MeetingDetails", new { id = meeting.Id});

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult JoinMeeting(string id)
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

                return RedirectToAction("MeetingDetails", new { id = meeting.Id });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IActionResult MeetingDetails(string id)
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

        public IActionResult FindOverlappingTimes(string id)
        {
            try
            {

                var meeting = _context.Meetings
                    .Include(meet => meet.Users)
                    .Single(meet => meet.Id == id);

                var userCount = meeting.Users.Count();

                if (meeting == null)
                {
                    return BadRequest("Meeting not found");
                }

                List<TimeBlock> timeBlocks = new List<TimeBlock>();

                foreach (var user in meeting.Users)
                {
                    timeBlocks.AddRange(user.TimeBlocks);
                }

                List<HourBlock> hourBlocks = DecomposeTimeBlocks(timeBlocks);

                var filtered = hourBlocks.Select(x => new
                {
                    x.Day,
                    x.Hour
                }).ToList();


                var overlappingBlocks = filtered
                    .GroupBy(e => e)
                    .Where(e => e.Count() == userCount)
                    .Select(e => e.First())
                    .ToList();

                return Json(data: overlappingBlocks);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private List<HourBlock> DecomposeTimeBlocks(List<TimeBlock> blocks)
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

                    for (int i = start; i <= final; i++)
                    {

                        var hourBlock = new HourBlock();
                        hourBlock.Hour = i;
                        hourBlock.Day = block.Day;
                        hourBlockList.Add(hourBlock);
                    }


                }

            }

            return hourBlockList;

        }

        private String GenerateToken()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 6)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            string authToken = resultToken.ToString();

            return authToken;
        }

    }

}
