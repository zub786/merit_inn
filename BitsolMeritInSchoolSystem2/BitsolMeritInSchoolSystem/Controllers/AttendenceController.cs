using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BitsolMeritInSchoolSystem.Models;
using BitsolMeritInSchoolSystem.DAL;
using Newtonsoft.Json;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class SingleListObject
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int StudentId { get; set; }
        public char Status { get; set; }
        public DateTime Date { get; set; }

    }
    public class MyModelWithList
    {
        public List<SingleListObject> MyList { get; set; }

        public MyModelWithList()
        {
            MyList = new List<SingleListObject>();

        }
    }


    public class AttendenceController : Controller
    {

        private ContextClasss db = new ContextClasss();



        public JsonResult SubmitAttendence(MyModelWithList details)
        {

            try
            {

                foreach (var s in details.MyList)
                {
                    var csid = db.ClassSections.Include("Class").Include("Section").Single(i => i.Class.ClassId == s.ClassId && i.Section.SectionId == s.SectionId);

                    Attendence at = new Attendence();

                    at.AttendenceId = 0;
                    at.ClassSectionId = csid.ClassSectionId;
                    at.StudentId = s.StudentId;
                    at.Status = s.Status.ToString();
                    at.Date = s.Date;
                    db.Attendence.Add(at);
                    db.SaveChanges();
                }

                return Json("Successfully Submit", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return Json(e.Message.ToString());
            }




        }
        public ActionResult ShowAttendenceSheet(int c, int s)
        {
            try
            {

                var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
                var Students = db.Student.Include(cc => cc.ClassSection).Include(cc => cc.ClassSection.Class).Include(cc => cc.ClassSection.Section).Where(cs => cs.ClassSectionId == csid.ClassSectionId && cs.Closed == false).ToList();


                var data = new List<object>();
                foreach (var temp in Students)
                {
                    data.Add(new { Class = temp.ClassSection.Class.ClassName, Section = temp.ClassSection.Section.SectionName, Name = temp.StudentName, ClassId = temp.ClassSection.Class.ClassId, SectionId = temp.ClassSection.Section.SectionId, StudentId = temp.StudentId });
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }

        public ActionResult ViewAttendenceDetails()
        {
            try
            {

                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");
                ViewBag.StudentId = new SelectList(db.Student.Where(i => i.StudentId == 0), "StudentId", "StudentName");
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }
        public ActionResult GetStudents(int id)
        {
            try
            {

                int ci = Convert.ToInt32(Session["Class"]);

                var Students = db.Student.Include(cc => cc.ClassSection).Where(cs => cs.ClassSection.Class.ClassId == ci && cs.ClassSection.Section.SectionId == id).ToList();



                var data = new List<object>();
                foreach (var temp in Students)
                {
                    data.Add(new { value = temp.StudentId, display = temp.StudentName });
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }

        public string CheckAttendence(DateTime date, int c, int s)
        {
            var csid = db.ClassSections.Include("Class").Include("Section").Single(i => i.Class.ClassId == c && i.Section.SectionId == s);
            var isFound = db.Attendence.Where(i => i.Date.Day == date.Day && i.Date.Month == date.Month && i.Date.Year == date.Year && i.ClassSection.ClassSectionId == csid.ClassSectionId).ToList();


            if (isFound.Count > 0)
            {
                return "Attendnce for this day already submited";
            }

            return "";
        }

        public ActionResult ViewAttendence(int c, int s, int st, int y, int m)
        {
            try
            {

                var Attendence = db.Attendence.Include(i => i.ClassSection).Where(i => i.ClassSection.ClassId == c && i.ClassSection.SectionId == s && i.StudentId == st && i.Date.Year == y && i.Date.Month == m).ToList();

                var data = new List<object>();
                foreach (var temp in Attendence)
                {
                    data.Add(new { Date = temp.Date.Day, Status = temp.Status.ToString() });
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

        }

        public ActionResult GetSections(int id)
        {
            try
            {

                Session["Class"] = id;

                var Sections = db.ClassSections.Include("Class").Include("Section").Where(i => i.ClassId == id).ToList();

                var data = new List<object>();
                foreach (var temp in Sections)
                {
                    data.Add(new { value = temp.Section.SectionId, display = temp.Section.SectionName });
                }
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }




        }




        // GET: /Attendence/


        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                else
                {

                    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                    ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");

                    return View();

                }


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }

        // GET: /Attendence/Details/5
        public ActionResult Details(int? id)
        {
            try
            {


                if (Session["Email"] == null)
                {
                    return View("Login");
                }


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Attendence attendenceregister = db.Attendence.Find(id);
                if (attendenceregister == null)
                {
                    return HttpNotFound();
                }
                return View(attendenceregister);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

        }

        // GET: /Attendence/Create
        public ActionResult Create()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(int ClassId,int SectionId,int StudentId,string Status,DateTime Date)
        //{
        //    if (Session["Email"] == null)
        //    {
        //        return View("Login");
        //    }

        //    var csid = db.ClassSections.Single(i => i.ClassId == ClassId && i.SectionId == SectionId);
        //    if (ModelState.IsValid)
        //    {
        //        db.Attendence.Add(attendenceregister);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", attendenceregister.ClassId);
        //    ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", attendenceregister.SectionId);
        //    return View(attendenceregister);
        //}

        // GET: /Attendence/Edit/5


        // GET: /Attendence/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
