using AspNetCore.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoppedFishing.Controllers
{
    public class MeetingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string MeetingName) {
            var meeting = new Meeting();
            meeting.Name = MeetingName;
            return Ok();
        }

        public IActionResult Join(int MeetingId)
        {

            return Ok();
        }
    }
}
