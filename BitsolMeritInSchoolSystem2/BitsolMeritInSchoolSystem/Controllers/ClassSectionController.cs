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

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class ClassSectionController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /ClassSection/
        public ActionResult Index()
        {
            try
            {
                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    var classsections = db.ClassSections.Include(c => c.Class).Include(c => c.Section);
                    return View(classsections.ToList());


                }
                return View("Login");


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
           
        }

        // GET: /ClassSection/Details/5
        public ActionResult Details(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var classsection = db.ClassSections.Include("Class").Include("Section").Where(i => i.ClassSectionId == id).ToList();
                if (classsection == null)
                {
                    return HttpNotFound();
                }
                return View(classsection[0]);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /ClassSection/Create
        public ActionResult Create()
        {
            try
            {

                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName");
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /ClassSection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClassSectionId,ClassId,SectionId")] ClassSection classsection)
        {
            try
            {

                var isFound = db.ClassSections.Where(i => i.ClassId == classsection.ClassId && i.SectionId == classsection.SectionId).ToList();

                if (isFound.Count > 0)
                {
                    ModelState.AddModelError("", "Sorry this section already exist in this class");
                }



                if (ModelState.IsValid)
                {
                    db.ClassSections.Add(classsection);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", classsection.ClassId);
                ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", classsection.SectionId);
                return View(classsection);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /ClassSection/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClassSection classsection = db.ClassSections.Find(id);
                if (classsection == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", classsection.ClassId);
                ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", classsection.SectionId);
                return View(classsection);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /ClassSection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClassSectionId,ClassId,SectionId")] ClassSection classsection)
        {
            try
            {

                var isFound = db.ClassSections.Where(i => i.ClassId == classsection.ClassId && i.SectionId == classsection.SectionId).ToList();

                if (isFound.Count > 0)
                {
                    ModelState.AddModelError("", "Sorry this section already exist in this class");
                }



                if (ModelState.IsValid)
                {
                    db.Entry(classsection).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", classsection.ClassId);
                ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", classsection.SectionId);
                return View(classsection);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
         
        }

        // GET: /ClassSection/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClassSection classsection = db.ClassSections.Find(id);
                if (classsection == null)
                {
                    return HttpNotFound();
                }
                return View(classsection);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /ClassSection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                ClassSection classsection = db.ClassSections.Find(id);
                db.ClassSections.Remove(classsection);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

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
