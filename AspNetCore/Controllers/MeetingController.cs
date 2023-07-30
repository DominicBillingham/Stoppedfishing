﻿using AspNetCore.Controllers;
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

        public IActionResult Create(string MeetingName) {

            var meeting = new Meeting();
            meeting.Name = MeetingName;
            _context.Meetings.Add(meeting);

            var userId = _userService.GetCurrentUserId();
            var user = _context.Users
                .Include(user => user.Meetings)
                .Single(user => user.Id == userId);

            user.Meetings.Add(meeting);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult Join(int Id)
        {
            var meeting = _context.Meetings
                .Include(meet => meet.Users)
                .Single(meet => meet.Id == Id);

            var user = _userService.GetCurrentUser();

            if (meeting == null || user == null) {
                return BadRequest("Meeting or User not found");
            }

            meeting.Users.Add(user);

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult GetUserMeetings()
        {
            var user = _userService.GetCurrentUser();

            if (user == null)
            {
                return BadRequest();
            }

            var meetings = _context.Meetings
                .Where(meet => meet.Users.Contains(user))
                .Select(x => new
                {
                    x.Name
                }).ToList();
               
            return Json (data : meetings);
          
        }
    }
}
