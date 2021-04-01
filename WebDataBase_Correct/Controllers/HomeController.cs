using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebDataBase_Correct.Models;

namespace WebDataBase_Correct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult TestCookie()
        {
            Random rnd = new Random();
            string strCookie;
            int cookie;
            if (Request.Cookies.TryGetValue("RandomValue", out strCookie))
            {
                cookie = Int32.Parse(strCookie);
            }
            else 
            {
                cookie = rnd.Next(0, 100);
                Response.Cookies.Append("RandomValue", cookie.ToString(),
                    new Microsoft.AspNetCore.Http.CookieOptions() { MaxAge = new TimeSpan(0, 0, 30) }); 
            }

            return View(cookie);
        }
    }
}
