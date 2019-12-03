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
    public class ClassController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /Class/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View(db.Class.ToList());


                }
                return View("Login");

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /Class/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Class @class = db.Class.Find(id);
                if (@class == null)
                {
                    return HttpNotFound();
                }
                return View(@class);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /Class/Create
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

        // POST: /Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClassId,ClassName")] Class @class)
        {
            try
            {
                var cl = db.Class.Where(i => i.ClassName == @class.ClassName).ToList();
                if (cl.Count > 0)
                {
                    ModelState.AddModelError("ClassName","Sorry this class already exist");
                }

                if (ModelState.IsValid)
                {
                    db.Class.Add(@class);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(@class);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /Class/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Class @class = db.Class.Find(id);
                if (@class == null)
                {
                    return HttpNotFound();
                }
                return View(@class);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClassId,ClassName")] Class @class)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(@class).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(@class);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /Class/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Class @class = db.Class.Find(id);
                if (@class == null)
                {
                    return HttpNotFound();
                }
                return View(@class);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Class @class = db.Class.Find(id);
                db.Class.Remove(@class);
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
