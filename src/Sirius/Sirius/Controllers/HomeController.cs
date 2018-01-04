using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Sirius.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string path)
        {
          return this.File("index.html", "text/html");
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
