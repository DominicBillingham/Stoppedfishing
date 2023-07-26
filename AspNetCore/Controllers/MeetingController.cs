using AspNetCore.Controllers;
using AspNetCore.Data;
using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using StoppedFishing.Services;

namespace StoppedFishing.Controllers
{
    public class MeetingController : Controller
    {
        public ApplicationDbContext _context;
        public IUserService _userService;

        public MeetingController(IUserService userService)
        {
            _context = new ApplicationDbContext();
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string MeetingName) {

            var meeting = new Meeting();
            meeting.Name = MeetingName;
            _context.Meetings.Add(meeting);

            var user = _userService.GetCurrentUser();
            meeting.Users = new List<User>();
            
            return Ok();
        }

        public IActionResult Join(int MeetingId)
        {

            return Ok();
        }
    }
}
