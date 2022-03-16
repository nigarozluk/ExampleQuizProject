using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Database.Abstract;
using Project.Database.Entity;
using Project.WebUI.Library;
using Project.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Controllers
{
    [TypeFilter(typeof(UserAuth))]
    public class HomeController : Controller
    {
        private ILogicService _service;
        public HomeController(ILogicService service)
        {
            _service = service;
        }
        // GET: HomeController
        public ActionResult Index()
         {
            var user = _service.User.FindByCondition(x => x.Mail == HttpContext.Session.GetString("UserSession")).Include(y=>y.UserExams).FirstOrDefault();
            var exams = user.UserExams.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsStarted==false).ToList();
            if (exams.Count!=0)
            {
                ViewBag.ThereIsExam = 1;
            }
            else
            {
                ViewBag.ThereIsExam = 0;
            }
            return View();
        }

       
        public ActionResult Exam()
        {
            int userId= _service.User.FindByCondition(x => x.Mail == HttpContext.Session.GetString("UserSession")).Include(y => y.UserExams).Select(x=>x.ID).FirstOrDefault();
            var exams = _service.UserExam.FindAll().Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsStarted == false && x.UserID == userId)
                .OrderBy(x=>x.StartDate)
                .Include(y => y.Exam)
                .ThenInclude(y => y.ExamQuestions)
                .ThenInclude(y => y.Question)
                .ThenInclude(y => y.Options)
                .FirstOrDefault();
            if (exams!= null)
            {
                exams.IsStarted = true;
                exams.TakingExamDate = DateTime.Now;
                _service.UserExam.Update(exams);
                _service.Save();
                return View(exams);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SaveExam(int examID)
        {
            int userId = _service.User.FindByCondition(x => x.Mail == HttpContext.Session.GetString("UserSession")).Include(y => y.UserExams).Select(x => x.ID).FirstOrDefault();
            var data = _service.ExamQuestion.FindAll().Where(x => x.ExamID == examID).Include(x => x.Question).ThenInclude(x=>x.Options).ToList();
             int count = 0;
            List<string> list = new List<string>();
            foreach (var item in data)
            {
               string selectedOptionID=(Request.Form["question_" + item.QuestionID].ToString());
                foreach (var option in item.Question.Options)
                {
                    if (option.ID.ToString()==selectedOptionID)
                    {
                        count++;
                    }
                }
               
            }
            var score = count * 10;
            var userexam = _service.UserExam.FindByCondition(x => x.ExamID == examID && x.UserID == userId).FirstOrDefault();
            userexam.UserScore = score;
            _service.UserExam.Update(userexam);
            _service.Save();
            
            ViewBag.Score = score.ToString();
            return View("Index", "Home");
        }




    }
   


}

