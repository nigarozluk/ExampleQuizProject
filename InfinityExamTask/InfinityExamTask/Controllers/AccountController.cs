using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Database.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ILogicService _service;
        public AccountController(ILogicService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string email)
        {
            
             var user = _service.User.FindByCondition(x => x.Mail == email).FirstOrDefault();
            if (user==null)
            {
                TempData["msg"] = "User Not Exist!";
                return RedirectToAction("Index", "Account");
            }
            HttpContext.Session.SetString("UserSession", email);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Account");
        }
    }
}
