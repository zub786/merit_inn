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
    public class UserController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /User/
        public ActionResult Index()
        {
            try
            {
                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                return View(db.User.ToList());
              

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
            
        }

        // GET: /User/Details/5
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
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,Password,UserRole")] User user)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(user);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
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
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,Password,UserRole")] User user)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
       
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
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
                User user = db.User.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                User user = db.User.Find(id);
                db.User.Remove(user);
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
