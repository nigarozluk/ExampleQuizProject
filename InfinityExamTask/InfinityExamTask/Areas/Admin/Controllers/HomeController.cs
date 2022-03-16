using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Database.Abstract;
using Project.Database.Entity;
using Project.WebUI.Areas.Admin.Library;
using Project.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
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
            ViewBag.ExamCatList = _service.Category.FindByCondition(x=>x.CategoryType==0).ToList();
            ViewBag.QuestionCatList = _service.Category.FindByCondition(x => x.CategoryType!=0).ToList();
            ViewBag.ExamList = _service.Exam.FindAll().ToList();
            return View();
        }
        [HttpPost]
        public JsonResult AddCategory(CategoryDto entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category()
                    {
                        CategoryName = entity.CategoryName,
                        CategoryType = entity.CategoryType
                    };
                    _service.Category.Create(category);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }
               
            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult AddExam(ExamDto entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var exam = new Exam()
                    {
                        ExamName=entity.ExamName,
                        ExamDuration=entity.ExamDuration,
                        ExamText=entity.ExamText,
                        CategoryID=entity.CategoryID,
                        SuccessScore=entity.SuccessScore
                    };
                    _service.Exam.Create(exam);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult AddQuestion(QuestionDto entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

          
                try
                {
                    var question = new Question()
                    {
                        QuestionText = entity.QuestionText,
                        QuestionType = entity.QuestionType,
                        CategoryID = entity.CategoryID,
                    };
                    _service.Question.Create(question);
                    _service.Save();
                    if (entity.QuestionType==QuestionType.WithOptions)
                    {
                        foreach (var item in entity.listOfOptions)
                        {
                            var option = new Option()
                            {
                                QuestionID = question.ID,
                                OptionText = item,
                                IsTrue = (item == entity.selectedValue) ? true : false,

                            };
                            _service.Option.Create(option);
                            _service.Save();
                        }
                    }
                    else if (entity.QuestionType == QuestionType.WithoutOption)
                    {
                        var option = new Option()
                        {
                            QuestionID = question.ID,
                            OptionText = entity.Answer,
                            IsTrue = true,

                        };
                        _service.Option.Create(option);
                        _service.Save();
                    }
                    
                   
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            
           
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetAllQuestions(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter, int examID)
        {
            DatatablesModel.DataTablesResponseParameter<Question> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<Question>();
            //var questions = _service.Question.FindAll().Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).Include(x=>x.ExamQuestions).AsEnumerable();
            var questions = _service.Question.FindAll().Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            dataTablesReponseParameter.recordsTotal = questions.Count();
            List<Question> list = questions.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = list;
            foreach (var item in list)
            {
                item.ExamQuestions = _service.ExamQuestion.FindByCondition(x => x.QuestionID == item.ID && x.ExamID == examID).ToList();
            }
            return Json(dataTablesReponseParameter);

        }
        [HttpPost]
        public JsonResult AddSelectedQuestionToExam(int questionID,int examID)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var questionexam = new ExamQuestion()
                    {
                        ExamID=examID,
                        QuestionID=questionID
                    };
                    _service.ExamQuestion.Create(questionexam);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult RemoveSelectedQuestionFromExam(int questionID, int examID)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var questionexam = new ExamQuestion()
                    {
                        ExamID = examID,
                        QuestionID = questionID
                    };
                    _service.ExamQuestion.Delete(questionexam);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult AssignUserToExam(UserAssignExamDto entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var user_ = _service.User.FindByCondition(x => x.Mail == entity.UserName).FirstOrDefault();
                    if (user_ == null)
                    {
                        user_ = new User()
                        {
                            Mail = entity.UserName,
                            UserName = entity.UserName,
                        };
                        _service.User.Create(user_);
                        _service.Save();
                    }
                  
                    var userexam = new UserExam()
                    {
                        UserID = user_.ID,
                        ExamID = entity.ExamID,
                        StartDate=entity.StartDate,
                        EndDate=entity.EndDate
                    };
                    _service.UserExam.Create(userexam);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetAllExams(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter,int catID)
        {
            DatatablesModel.DataTablesResponseParameter<Exam> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<Exam>();
            IEnumerable<Exam> exs;
            if (catID==0)
            {
                exs = _service.Exam.FindAll().Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            }
            else
            {
                exs = _service.Exam.FindByCondition(y=>y.CategoryID==catID).Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            }
            dataTablesReponseParameter.recordsTotal = exs.Count();
            List<Exam> list = exs.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = list;
            return Json(dataTablesReponseParameter);

        }
        [HttpPost]
        public JsonResult RemoveSelectedExam(int examID)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var exam = new Exam()
                    {
                        ID = examID
                    };
                    _service.Exam.Delete(exam);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetSelectedEditExamInfo(int examID)
        {
                
           Exam exam=  _service.Exam.FindByCondition(X=>X.ID==examID).FirstOrDefault();
              
            return Json(exam);

        }
        [HttpPost]
        public JsonResult EditExam(Exam entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Exam.Update(entity);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetAllExamsCategory(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter, CategoryType type)
        {
            DatatablesModel.DataTablesResponseParameter<Category> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<Category>();
            var cats = _service.Category.FindAll().Where(x=>x.CategoryType==type).Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            dataTablesReponseParameter.recordsTotal = cats.Count();
            List<Category> list = cats.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = list;
            return Json(dataTablesReponseParameter);

        }
        [HttpPost]
        public JsonResult RemoveSelectedCategory(int catID)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var cat = new Category()
                    {
                        ID = catID
                    };
                    _service.Category.Delete(cat);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult EditSelectedCategory(Category entity)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var cat = _service.Category.FindByCondition(x=>x.ID==entity.ID).FirstOrDefault();
                    entity.CategoryType = cat.CategoryType;
                    _service.Category.Update(entity);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetAllUsers(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter)
        {
            DatatablesModel.DataTablesResponseParameter<User> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<User>();
            var users = _service.User.FindAll().Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            dataTablesReponseParameter.recordsTotal = users.Count();
            List<User> list = users.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = list;
            return Json(dataTablesReponseParameter);

        }
        [HttpPost]
        public JsonResult RemoveSelectedQue(int queID)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var question = new Question()
                    {
                        ID = queID
                    };
                    _service.Question.Delete(question);
                    _service.Save();

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetSelectedQueInfo(int queID)
        {

            Question que = _service.Question.FindByCondition(X => X.ID == queID).FirstOrDefault();
            que.Options = _service.Option.FindByCondition(x=>x.QuestionID==queID).ToList();

            return Json(que);

        }
        [HttpPost]
        public JsonResult EditQuestion(QuestionDto que)
        {
            GenericResponse<string> response = new GenericResponse<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    var question = _service.Question.FindByCondition(x => x.ID == que.QueID).FirstOrDefault();

                    var editquestion = new Question()
                    {
                        ID=que.QueID,
                        QuestionText = que.QuestionText,
                        QuestionType = que.QuestionType,
                        CategoryID = que.CategoryID,
                    };
                    _service.Question.Update(editquestion);
                    _service.Save();
                    var options= _service.Option.FindByCondition(x => x.QuestionID == question.ID).ToList();
                    foreach (var item in options)
                    {
                        _service.Option.Delete(item);
                        _service.Save();
                    }
                    foreach (var item in que.listOfOptions)
                    {
                        var option = new Option()
                        {
                            QuestionID = question.ID,
                            OptionText = item,
                            IsTrue = (item == que.selectedValue) ? true : false,

                        };
                        _service.Option.Create(option);
                        _service.Save();
                    }

                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "All Fields are Required!";
            }
            return Json(response);
        }
        [HttpPost]
        public JsonResult GetAllScores(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter, int exID)
        {
            DatatablesModel.DataTablesResponseParameter<ScoreListDto> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<ScoreListDto>();
            IEnumerable<ScoreListDto> data;
            if (exID != 0)
            {                
                data = _service.UserExam.FindAll().Where(x => x.ExamID == exID && x.IsStarted)
                 .Select(k => new ScoreListDto
                 {
                     UserMail = k.User.Mail,
                     ExamName = k.Exam.ExamName,
                     ExamDate=k.TakingExamDate,
                     UserScore=k.UserScore
                 }).ToList();
            }
            else
            {
                data = _service.UserExam.FindAll().Where(x=>x.IsStarted)
                .Select(k => new ScoreListDto
                {
                    UserMail = k.User.Mail,
                    ExamName = k.Exam.ExamName,
                    ExamDate = k.TakingExamDate,
                    UserScore = k.UserScore
                }).ToList();
            }

            data = data.Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).AsEnumerable();
            dataTablesReponseParameter.recordsTotal = data.Count();
            List<ScoreListDto> list = data.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = list;
            return Json(dataTablesReponseParameter);

        }
        



    }
}
