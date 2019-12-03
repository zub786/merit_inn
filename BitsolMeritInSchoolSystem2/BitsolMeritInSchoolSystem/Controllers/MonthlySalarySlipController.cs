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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class MonthlySalarySlipController : Controller
    {
        private ContextClasss db = new ContextClasss();

        public ActionResult MonthlySalarySlipPrint(int? id)
        {
            try
            {
                if (Session["Email"] == null)
                {
                    return View("LoginPage");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MonthlySalarySlip monthlysalaryslip = db.MonthlySalarySlip.Find(id);
                if (monthlysalaryslip == null)
                {
                    return HttpNotFound();
                }
                return View(monthlysalaryslip);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }

        // GET: /MonthlySalarySlip/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                return View(db.MonthlySalarySlip.ToList());

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /MonthlySalarySlip/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (Session["Email"] == null)
                {
                    return View("LoginPage");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MonthlySalarySlip monthlysalaryslip = db.MonthlySalarySlip.Find(id);
                if (monthlysalaryslip == null)
                {
                    return HttpNotFound();
                }
                return View(monthlysalaryslip);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
            
        }

        // GET: /MonthlySalarySlip/Create
        public ActionResult Create()
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                ViewBag.CNIC = new SelectList(db.StaffMember, "StaffMemberCNIC", "StaffMemberCNIC");
                //var cnices = db.StaffMember.Select(i => i.StaffMemberCNIC);
                //string cnicesString = null;
                //foreach (var temp in cnices)
                //{
                //    cnicesString += ",""+temp.ToString()+""" ;
                //}
                //cnicesString = cnicesString.Replace(",", "," + System.Environment.NewLine);
                //Session["CNIC"] = cnicesString;
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        public JsonResult GetStaffMember(string cnic)
        {
            var Member = db.StaffMember.Single(i => i.StaffMemberCNIC == cnic);
            var data = new List<object>();
            data.Add(new { name = Member.StaffMemberName, salary = Member.Salary });
            return Json(data, JsonRequestBehavior.AllowGet);
           

           
        
           
        }

        // POST: /MonthlySalarySlip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MonthlySalarySlipId,Name,CNIC,Salary,Date")] MonthlySalarySlip monthlysalaryslip)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.MonthlySalarySlip.Add(monthlysalaryslip);
                    db.SaveChanges();
                    string myDir = "C:/MeritInn/Salary Slips/";
                    System.IO.Directory.CreateDirectory(myDir);
                    string d = monthlysalaryslip.Date.Month.ToString();
                    Document doc = new Document(PageSize.A4);
                    Header h = new Header("The Merit Inn School Samanabad, Lahore", "content");
                    PdfWriter.GetInstance(doc, new FileStream("C:/MeritInn/Salary Slips/" + monthlysalaryslip.CNIC + " " + monthlysalaryslip.Name + " Month " + monthlysalaryslip.Date.Month + " Year " + monthlysalaryslip.Date.Year + ".pdf", FileMode.Create));
                    doc.Open();
                    PdfPTable table = new PdfPTable(4);
                    PdfPCell Spanecell = new PdfPCell(new Phrase("Monthly Employee Salary Slip"));
                    Spanecell.Colspan = 4;
                    Spanecell.BackgroundColor = GrayColor.GRAY;
                    Spanecell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(Spanecell);
                    table.AddCell("Name ");
                    table.AddCell(monthlysalaryslip.Name);
                    table.AddCell("CNIC # ");
                    table.AddCell(monthlysalaryslip.CNIC);

                    table.AddCell("Paid Amount ");
                    table.AddCell(monthlysalaryslip.Salary.ToString());
                    table.AddCell("Date # ");
                    table.AddCell(monthlysalaryslip.Date.ToString());

                    table.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.DefaultCell.Border = Rectangle.NO_BORDER;


                    doc.Add(table);

                    doc.Close();



                    return RedirectToAction("Index");
                }

                return View(monthlysalaryslip);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /MonthlySalarySlip/Edit/5
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
                MonthlySalarySlip monthlysalaryslip = db.MonthlySalarySlip.Find(id);
                if (monthlysalaryslip == null)
                {
                    return HttpNotFound();
                }
                return View(monthlysalaryslip);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // POST: /MonthlySalarySlip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MonthlySalarySlipId,Name,CNIC,Salary,Date")] MonthlySalarySlip monthlysalaryslip)
        {
            try
            {

                if (Session["Email"] == null)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(monthlysalaryslip).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(monthlysalaryslip);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /MonthlySalarySlip/Delete/5
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
                MonthlySalarySlip monthlysalaryslip = db.MonthlySalarySlip.Find(id);
                if (monthlysalaryslip == null)
                {
                    return HttpNotFound();
                }
                return View(monthlysalaryslip);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /MonthlySalarySlip/Delete/5
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
                MonthlySalarySlip monthlysalaryslip = db.MonthlySalarySlip.Find(id);
                db.MonthlySalarySlip.Remove(monthlysalaryslip);
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
