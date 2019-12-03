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
    public class StaffMemberAttendenceController : Controller
    {
        private ContextClasss db = new ContextClasss();

        // GET: /StaffMemberAttendence/
        public ActionResult Index()
        {


            if (Session["Email"] == null)
            {

                return RedirectToAction("../Home/Login");
            }
            else
            {
                var staffmemberattendence = db.StaffMemberAttendence.Include(s => s.StaffMember);
                return View(staffmemberattendence.ToList());
            }

           
        }

        // GET: /StaffMemberAttendence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMemberAttendence staffmemberattendence = db.StaffMemberAttendence.Find(id);
            if (staffmemberattendence == null)
            {
                return HttpNotFound();
            }
            return View(staffmemberattendence);
        }

        // GET: /StaffMemberAttendence/Create
        public ActionResult Create()
        {
            ViewBag.StaffMemberId = new SelectList(db.StaffMember, "StaffMemberId", "StaffMemberName");
            return View();
        }

        // POST: /StaffMemberAttendence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StaffMemberAttendenceId,StaffMemberId,Status,Date,InTime,OutTime")] StaffMemberAttendence staffmemberattendence)
        {
            if (ModelState.IsValid)
            {
                db.StaffMemberAttendence.Add(staffmemberattendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffMemberId = new SelectList(db.StaffMember, "StaffMemberId", "StaffMemberName", staffmemberattendence.StaffMemberId);
            return View(staffmemberattendence);
        }

        // GET: /StaffMemberAttendence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMemberAttendence staffmemberattendence = db.StaffMemberAttendence.Find(id);
            if (staffmemberattendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffMemberId = new SelectList(db.StaffMember, "StaffMemberId", "StaffMemberName", staffmemberattendence.StaffMemberId);
            return View(staffmemberattendence);
        }

        // POST: /StaffMemberAttendence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StaffMemberAttendenceId,StaffMemberId,Status,Date,InTime,OutTime")] StaffMemberAttendence staffmemberattendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffmemberattendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffMemberId = new SelectList(db.StaffMember, "StaffMemberId", "StaffMemberName", staffmemberattendence.StaffMemberId);
            return View(staffmemberattendence);
        }

        // GET: /StaffMemberAttendence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMemberAttendence staffmemberattendence = db.StaffMemberAttendence.Find(id);
            if (staffmemberattendence == null)
            {
                return HttpNotFound();
            }
            return View(staffmemberattendence);
        }

        // POST: /StaffMemberAttendence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffMemberAttendence staffmemberattendence = db.StaffMemberAttendence.Find(id);
            db.StaffMemberAttendence.Remove(staffmemberattendence);
            db.SaveChanges();
            return RedirectToAction("Index");
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
