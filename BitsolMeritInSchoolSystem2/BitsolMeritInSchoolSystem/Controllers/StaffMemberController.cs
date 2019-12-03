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
using System.Text.RegularExpressions;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class StaffMemberController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /StaffMember/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                return View(db.StaffMember.ToList());

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /StaffMember/Details/5
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
                StaffMember staffmember = db.StaffMember.Find(id);
                if (staffmember == null)
                {
                    return HttpNotFound();
                }
                return View(staffmember);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /StaffMember/Create
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

        // POST: /StaffMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StaffMemberId,StaffMemberName,StaffMemberFatherName,StaffMemberContactNo,StaffMemberCNIC,HiringDate,StaffMemberEmail,DateOfBirth,Designation,Qualification,Salary")] StaffMember staffmember)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                //Regex Cno = new Regex(@"^[0-9]");





                //if (Cno.IsMatch(staffmember.StaffMemberContactNo))
                //{

                //}
                //else
                //{
                //    ModelState.AddModelError("ContactNo", "Contact No is not in correct format");
                //}

                var isFound = db.StaffMember.Where(i => i.StaffMemberCNIC == staffmember.StaffMemberCNIC).ToList();
                if (isFound.Count != 0)
                {
                    ModelState.AddModelError("StaffMemberCNIC", "Sorry this Employee already exist");

                }


                if (ModelState.IsValid)
                {
                    db.StaffMember.Add(staffmember);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(staffmember);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /StaffMember/Edit/5
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
                StaffMember staffmember = db.StaffMember.Find(id);

                Session["ForEditStaffCNIC"] = staffmember.StaffMemberCNIC.ToString();
                if (staffmember == null)
                {
                    return HttpNotFound();
                }
                return View(staffmember);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /StaffMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StaffMemberId,StaffMemberName,StaffMemberFatherName,StaffMemberContactNo,StaffMemberCNIC,HiringDate,StaffMemberEmail,DateOfBirth,Designation,Qualification,Salary")] StaffMember staffmember)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                Regex Cno = new Regex(@"^[0-9]");

                if (staffmember.StaffMemberCNIC != (string)Session["ForEditStaffCNIC"])
                {
                    var isFoundStd = db.StaffMember.Where(i => i.StaffMemberCNIC == staffmember.StaffMemberCNIC).ToList();
                    if (isFoundStd.Count > 0)
                    {
                        ModelState.AddModelError("StaffMemberCNIC", "Sorry this Staff Member already exist");
                    }
                }
                if (Cno.IsMatch(staffmember.StaffMemberContactNo))
                {

                }
                else
                {
                    ModelState.AddModelError("ContactNo", "Contact No is not in correct format");
                }
             
                if (ModelState.IsValid)
                {
                    db.Entry(staffmember).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(staffmember);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /StaffMember/Delete/5
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
                StaffMember staffmember = db.StaffMember.Find(id);
                if (staffmember == null)
                {
                    return HttpNotFound();
                }
                return View(staffmember);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /StaffMember/Delete/5
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
                StaffMember staffmember = db.StaffMember.Find(id);
                db.StaffMember.Remove(staffmember);
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
