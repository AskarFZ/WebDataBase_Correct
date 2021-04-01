using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDataBase_Correct.Models;


namespace WebDataBase_Correct.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains("User"))
                return RedirectToAction("Login");

            //HttpContext.Session.SetString("UserName", "jgjgh");
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            string a;
          a = HttpContext.Session.GetString("Login");
            ViewBag.a = a;
            return View();
        }
        [HttpPost]
        public IActionResult Login (User user)
        {
            if (user.Name != null && user.Password != null)
            {
                if (user.Name == "Max" && user.Password == "1234")
                {
                    HttpContext.Session.SetString("Login", user.Name);
                    return Redirect("~/Product");


                }
                else ViewBag.Message = "Password is Incorrect";
            }
            return View();
        }
        public IActionResult Logout(string returnTo)
        {
           HttpContext.Session.Remove("Login");
            if (returnTo != null)
                return Redirect(returnTo);
            else
                return Redirect("~/Home");
        }
    }
}
