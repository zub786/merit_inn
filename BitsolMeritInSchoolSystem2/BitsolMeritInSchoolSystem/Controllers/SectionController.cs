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
    public class SectionController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /Section/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View(db.Section.ToList());


                }
                return View("Login");

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /Section/Details/5
        public ActionResult Details(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = db.Section.Find(id);
                if (section == null)
                {
                    return HttpNotFound();
                }
                return View(section);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /Section/Create
        public ActionResult Create()
        {
            try
            {

                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        
        }

        // POST: /Section/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SectionId,SectionName")] Section section)
        {
            try
            {
                var sc = db.Section.Where(i => i.SectionName == section.SectionName).ToList();
                if (sc.Count > 0)
                {
                    ModelState.AddModelError("SectionName", "Sorry this Section already exist");
                }


                if (ModelState.IsValid)
                {
                    db.Section.Add(section);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(section);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        
        }

        // GET: /Section/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = db.Section.Find(id);
                if (section == null)
                {
                    return HttpNotFound();
                }
                return View(section);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /Section/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SectionId,SectionName")] Section section)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(section).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(section);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /Section/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Section section = db.Section.Find(id);
                if (section == null)
                {
                    return HttpNotFound();
                }
                return View(section);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Section section = db.Section.Find(id);
                db.Section.Remove(section);
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
