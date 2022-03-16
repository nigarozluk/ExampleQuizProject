using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebUI.Areas.Admin.Models;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
           if (login.email == "admin@example.com" && login.password=="123456")
           {
                HttpContext.Session.SetString("AdminSession", login.email);
                return RedirectToAction("Index", "Home", "Admin");
           }

            TempData["msg"] = "User Not Exist!";
            return RedirectToAction("Index", "Account","Admin");
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Account","Admin");
        }
    }
   
}
