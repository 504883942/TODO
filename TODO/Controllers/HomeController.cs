using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace TODO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TODOEntities db = new TODOEntities();
            ViewBag.list= db.Task_t.ToList();
            return View();
        }
        public ActionResult Add(string description) {
            Task_t task = new Task_t();
            task.Description = description;
            task.Uid = Guid.NewGuid();
            task.State = 0;
            task.UpdateTime = DateTime.Now;
            TODOEntities db = new TODOEntities();
            db.Task_t.Add(task);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public JsonResult Save(Guid[] todo, Guid[] progress, Guid[] complete) {
            TODOEntities db = new TODOEntities();
            JsonResult ret = new JsonResult();
            if (todo != null)
            {
                db.Task_t.Where(p => todo.Contains(p.Uid)).ForEach(p =>
                {
                    p.State = 0;
                });
            }
            if (progress != null)
            {
                db.Task_t.Where(p => progress.Contains(p.Uid)).ForEach(p =>
                {
                    p.State = 1;
                });
            }
            if (complete != null)
            {
                db.Task_t.Where(p => complete.Contains(p.Uid)).ForEach(p =>
                {
                    p.State = 2;
                });
            }

            db.SaveChanges();
            ret.Data =new {state="success" };
            return ret;
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}