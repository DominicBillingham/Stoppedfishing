using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class Blogcontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JavascriptBlog()
        {
            return View();
        }
    }
}
