using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("All", "Book");
            }
            return View();
        }


    }
}