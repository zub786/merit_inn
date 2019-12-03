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
    public class AnnualChargesController : Controller
    {
        private ContextClasss db = new ContextClasss();

        public ActionResult PaidUnPaid()
        {
            try{
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
        public int GetEditCSId(int c, int s)
        {

            var CSID = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
            return CSID.ClassSectionId;


        }

        public ActionResult Print()
        {
            try{
            if (Session["Email"] == null)
            {
                return View("Login");
            }

            var csid = GetEditCSId(Convert.ToInt32(Session["c"]), Convert.ToInt32(Session["s"]));

            int cc = Convert.ToInt32(Session["c"]);
            int ss = Convert.ToInt32(Session["s"]);

            var ccc = db.Class.Where(i => i.ClassId == cc).ToList();

            ViewBag.classs = ccc[0].ClassName.ToString();

            var sss = db.Section.Where(i => i.SectionId == ss).ToList();

            ViewBag.section = sss[0].SectionName.ToString();


            var Students = db.Student.Where(i => i.ClassSectionId == csid).ToList();
            int yyyy = Convert.ToInt32(Session["y"]);
            var AnnualPaidStudents = db.AnnualCharges.Include(i => i.Student).Where(i => i.Date.Year == yyyy && i.RemainingChargesAmount == 0).ToList();

            ICollection<AnnualCharges> Paid = new List<AnnualCharges>();
            int pc = 0, upc = 0;


            ICollection<Student> UnPaid = new List<Student>();
            ICollection<AnnualCharges> UnPaidDue = new List<AnnualCharges>();

            foreach (var Student in Students)
            {

                var isPaid = db.AnnualCharges.Include(k => k.Student).Where(i => i.StudentId == Student.StudentId).ToList();
                if (isPaid.Count() == 0)
                {

                    UnPaid.Add(Student);

                    upc++;
                }


                else
                {
                    if (isPaid[0].RemainingChargesAmount > 0)
                    {
                        UnPaidDue.Add((AnnualCharges)isPaid[0]);
                        upc++;
                    }

                }


            }

           


            foreach (var pStudent in AnnualPaidStudents)
            {

                Paid.Add(pStudent);
                pc++;

            }


            ViewBag.Paids = Paid;
            ViewBag.UnPaidsDue = UnPaidDue;
            ViewBag.UnPaids = UnPaid;


            ViewBag.PaidCount = pc;
            ViewBag.UnPaidCount = upc;

            pc = 0;
            upc = 0;


            return View();


            
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }

        public ActionResult ShowPaidUnpaid(int c, int s, int y)
        {
            try{

            if (Session["Email"] == null)
            {
                return View("Login");
            }

            var csid = GetEditCSId(c, s);


            Session["c"] = c;

            Session["s"] = s;

            Session["y"] = y;



            var Students = db.Student.Where(i => i.ClassSectionId == csid && i.Closed == false).ToList();

            var AnnualPaidStudents = db.AnnualCharges.Include(i => i.Student).Where(i => i.Date.Year == y && i.RemainingChargesAmount == 0).ToList();

            ICollection<AnnualCharges> Paid = new List<AnnualCharges>();
            int pc = 0, upc = 0;


            ICollection<AnnualCharges> UnPaidDue = new List<AnnualCharges>();

            ICollection<Student> UnPaid = new List<Student>();

            foreach (var Student in Students)
            {

                var isPaid = db.AnnualCharges.Include(k => k.Student).Where(i => i.StudentId == Student.StudentId).ToList();
                if(isPaid.Count() == 0){

                    UnPaid.Add(Student);

                    upc++;
                }


                else
                {
                    if (isPaid[0].RemainingChargesAmount > 0)
                    {
                        UnPaidDue.Add((AnnualCharges)isPaid[0]);
                        upc++;
                    }
             
                }


            }


            foreach (var pStudent in AnnualPaidStudents)
            {

                Paid.Add(pStudent);
                pc++;

            }


            ViewBag.Paids = Paid;
            ViewBag.UnPaidsDue = UnPaidDue;
            ViewBag.UnPaids = UnPaid;


            ViewBag.PaidCount = pc;
            ViewBag.UnPaidCount = upc;

            pc = 0;
            upc = 0;


            return PartialView();

            
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

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                Session["Class"] = id;
                var Sections = db.ClassSections.Include("Class").Include("Section").Where(i => i.Class.ClassId == id).ToList();
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

      

        public ActionResult GetStudents(int c, int s)
        {
            if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
            {
                return View("Login");
            }
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                int ci = Convert.ToInt32(Session["Class"]);
                var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
                var Students = db.Student.Where(i => i.ClassSectionId == csid.ClassSectionId && i.Closed == false).ToList();
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



        // GET: /AnnualCharges/
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
               
                return RedirectToAction("../Home/Login");
            }
            else
            {
                var annualcharges = db.AnnualCharges.Include(a => a.Student);
                return View(annualcharges.ToList());
            }
        }

        // GET: /AnnualCharges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualCharges annualcharges = db.AnnualCharges.Include(s => s.Student).FirstOrDefault(k => k.AnnualChargesId == id);
            if (annualcharges == null)
            {
                return HttpNotFound();
            }
            return View(annualcharges);
        }

        // GET: /AnnualCharges/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName");
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");
            return View();
        }

        // POST: /AnnualCharges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnualChargesId,AnnualChargesDescription,PaidAmount,RemainingChargesAmount,Date,StudentId")] AnnualCharges annualcharges)
        {
            if (Session["Email"] == null)
            {

                return RedirectToAction("../Home/Login");
            }
            else
            {
                if (annualcharges.RemainingChargesAmount == null || annualcharges.RemainingChargesAmount == 0)
                {
                    annualcharges.RemainingChargesAmount = 0;
                }

                if (ModelState.IsValid)
                {
                    db.AnnualCharges.Add(annualcharges);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName", annualcharges.StudentId);

                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");
                return View(annualcharges);
            }



        }

        // GET: /AnnualCharges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualCharges annualcharges = db.AnnualCharges.Find(id);
            if (annualcharges == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName", annualcharges.StudentId);
            return View(annualcharges);
        }

        // POST: /AnnualCharges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AnnualChargesId,AnnualChargesDescription,ChargesAmount,Date,StudentId")] AnnualCharges annualcharges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annualcharges).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName", annualcharges.StudentId);
            return View(annualcharges);
        }

        // GET: /AnnualCharges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnualCharges annualcharges = db.AnnualCharges.Include(s => s.Student).FirstOrDefault(k => k.AnnualChargesId == id);
            if (annualcharges == null)
            {
                return HttpNotFound();
            }
            return View(annualcharges);
        }

        // POST: /AnnualCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnualCharges annualcharges = db.AnnualCharges.Find(id);
            db.AnnualCharges.Remove(annualcharges);
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
