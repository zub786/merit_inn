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
    public class MonthlyFeeController : Controller
    {
        private ContextClasss db = new ContextClasss();

       

        public ActionResult PaidUnPaid()
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }

            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");
            return View();


        }

        public ActionResult Print()
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }

            var csid = GetEditCSId(Convert.ToInt32(Session["mc"]), Convert.ToInt32(Session["ms"]));

            int cc = Convert.ToInt32(Session["mc"]);
            int ss = Convert.ToInt32(Session["ms"]);
            var ccc = db.Class.Where(i => i.ClassId == cc).ToList();

            ViewBag.classs = ccc[0].ClassName.ToString();

            var sss = db.Section.Where(i => i.SectionId == ss).ToList();

            ViewBag.section = sss[0].SectionName.ToString();



            var Students = db.Student.Where(i => i.ClassSectionId == csid).ToList();
            int yyyy = Convert.ToInt32(Session["my"]);

            int mm = Convert.ToInt32(Session["mm"]);
            var MonthlyPaidStudents = db.MonthlyFee.Where(i => i.Date.Year == yyyy && i.Date.Month == mm && i.Student.Closed == false).ToList();

            ICollection<MonthlyFee> Paid = new List<MonthlyFee>();
            int pc = 0, upc = 0;


            ICollection<Student> UnPaid = new List<Student>();

            foreach (var Student in Students)
            {

                var isPaid = db.MonthlyFee.Where(i => i.StudentId == Student.StudentId && i.Student.Closed == false && i.Date.Year == yyyy && i.Date.Month == mm).ToList();
                if (isPaid.Count() == 0)
                {
                    UnPaid.Add(Student);
                    upc++;
                }


            }


            foreach (var pStudent in MonthlyPaidStudents)
            {

                Paid.Add(pStudent);
                pc++;

            }


            ViewBag.Paids = Paid;
            ViewBag.UnPaids = UnPaid;


            ViewBag.PaidCount = pc;
            ViewBag.UnPaidCount = upc;

            pc = 0;
            upc = 0;


            return View();


        }
        public ActionResult ShowPaidUnpaid(int c, int s, int y, int m)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }

            Session["mc"] = c;
            Session["ms"] = s;
            Session["my"] = y;
            Session["mm"] = m;

            var csid = GetEditCSId(c, s);



            var Students = db.Student.Where(i => i.ClassSectionId == csid && i.Closed == false).ToList();

            var MonthlyPaidStudents = db.MonthlyFee.Include("Student").Where(i => i.ClassSectionId == csid && i.Date.Year == y && i.Date.Month == m && i.Student.Closed == false).ToList();

            ICollection<MonthlyFee> Paid = new List<MonthlyFee>();
            int pc = 0, upc = 0;


            ICollection<Student> UnPaid = new List<Student>();

            foreach (var Student in Students)
            {

                var isPaid = db.MonthlyFee.Where(i => i.StudentId == Student.StudentId && i.Student.ClassSectionId == Student.ClassSectionId && i.Date.Year == y && i.Date.Month == m && i.Student.Closed == false).ToList();
                if (isPaid.Count() == 0)
                {
                    UnPaid.Add(Student);
                    upc++;
                }


            }


            foreach (var pStudent in MonthlyPaidStudents)
            {

                Paid.Add(pStudent);
                pc++;

            }


            ViewBag.Paids = Paid;
            ViewBag.UnPaids = UnPaid;


            ViewBag.PaidCount = pc;
            ViewBag.UnPaidCount = upc;

            pc = 0;
            upc = 0;


            return PartialView();



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

        public int GetEditCSId(int c, int s)
        {
           
            var CSID = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
            return CSID.ClassSectionId;

        }

        public ActionResult GetStudents(int c,int s)
        {
            if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
            {
                return View("Login");
            }
            try
            {

                if (Session["Email"] == null )
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

        public ActionResult AllFeeRecord()
        {
            if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)                
                return View();            
            return View("Login");
        }

        public ActionResult GetListMonthlyFee(int year, int month)
        {
            if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
            {
                var monthlyfee = db.MonthlyFee.Include(m => m.Student)
                    .Include(m => m.ClassSection)
                    .Include(m => m.ClassSection.Class)
                    .Include(m => m.ClassSection.Section)
                    .Where(m => m.Student.Closed == false && m.Date.Year == year && m.Date.Month == month);
                return PartialView("_MonthlyFee", monthlyfee.ToList());

            }
            return View("Login");
        }






        // GET: /MonthlyFee/
        public ActionResult Index()
        {
            if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
            {
                //var monthlyfee = db.MonthlyFee.Include(m => m.Student).Include(m => m.ClassSection).Include(m => m.ClassSection.Class).Include(m => m.ClassSection.Section).Where(m => m.Student.Closed == false);
                return View();
               
            }
            return View("Login");
        }

        // GET: /MonthlyFee/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyfee = db.MonthlyFee.Include(i => i.Student).Include(i => i.ClassSection).Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).Single(i => i.MonthlyFeeId == id);
            if (monthlyfee == null)
            {
                return HttpNotFound();
            }
            return View(monthlyfee);
        }

        public ActionResult MonthlyFeeSlipPrint(int? id)
        
        {


            
            MonthlyFee monthlyfee = db.MonthlyFee.Include(i => i.Student).Include(i => i.ClassSection).Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).Single(i => i.MonthlyFeeId == id);
            return View(monthlyfee);
        }


        // GET: /MonthlyFee/Create
        public ActionResult Create()
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");
         
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId");
            ViewBag.StudentId = new SelectList(db.Student.Where(i => i.StudentId == 0), "StudentId", "StudentName");
            return View();
        }

        // POST: /MonthlyFee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MonthlyFeeId,ClassSectionId,StudentId,FeeAmount,Date")] MonthlyFee monthlyfee)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }

            var j = db.MonthlyFee.Where(i => i.StudentId == monthlyfee.StudentId && i.Date.Month == monthlyfee.Date.Month && i.Date.Year == monthlyfee.Date.Year).ToList();

            if (j.Count > 0)
            {
                ModelState.AddModelError("Date", "Sorry! Fee of this student is already submited for this month");
            }


            var s = db.Student.FirstOrDefault(i => i.StudentId == monthlyfee.StudentId);

            



            if (ModelState.IsValid)
            {
                var StudentForFee = db.Student.Find(monthlyfee.StudentId);
                monthlyfee.Student = StudentForFee;
                db.MonthlyFee.Add(monthlyfee);
                string myDir = "C:/MeritInn/Fee Slips/";
                System.IO.Directory.CreateDirectory(myDir);
                Document doc = new Document(PageSize.A4);

                Student st = new Student();
                st = db.Student.Single(k => k.StudentId == monthlyfee.StudentId);

                PdfWriter.GetInstance(doc, new FileStream("C:/MeritInn/Fee Slips/" + st.BayFormNo + " " + st.StudentName + " Year " + monthlyfee.Date.Year + " Month " + monthlyfee.Date.Month + ".pdf", FileMode.Create));
                doc.Open();

                // System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\zubair Hathora\Desktop\BitsolMeritInSchoolSystem\BitsolMeritInSchoolSystem\Images\MeritInnLogo.png", true);
                //    img.ScaleAbsolute(159f, 159f);    

                //img.Height = 1;
                //    img.Width = 100;
                //    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);


                //doc.Add(pic);
                doc.AddHeader("", "THE MERIT SCHOOL");

                PdfPTable table = new PdfPTable(4);
                PdfPCell Spanecell = new PdfPCell(new Phrase("Monthly Fee Slip"));
                Spanecell.Colspan = 4;
                Spanecell.BackgroundColor = GrayColor.GRAY;
                Spanecell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(Spanecell);
                table.AddCell("Name ");
                table.AddCell(st.StudentName);
                table.AddCell("CNIC # ");
                table.AddCell(st.BayFormNo);

                table.AddCell("Paid Amount ");
                table.AddCell(monthlyfee.FeeAmount.ToString());
                table.AddCell("Date # ");
                table.AddCell(monthlyfee.Date.ToString());

                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.DefaultCell.Border = Rectangle.NO_BORDER;


                doc.Add(table);

                doc.Close();

                db.SaveChanges();
                ViewBag.SuccessMessage = "Monthly Fee Added Ssuccessfully";
                return View("CurrentMonthlyFee", db.MonthlyFee.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).ToList().AsQueryable().Last());

                //---------------------------------------------------
                //db.MonthlyFee.Add(monthlyfee);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");

            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId");
            ViewBag.StudentId = new SelectList(db.Student.Where(i => i.StudentId == 0), "StudentId", "StudentName");
            return View(monthlyfee);
        }

        // GET: /MonthlyFee/Edit/5

        
        public ActionResult Edit(int? id)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyfee = db.MonthlyFee.Include(i => i.Student).Include(i => i.ClassSection).Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).FirstOrDefault(i => i.MonthlyFeeId == id);
            if (monthlyfee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", monthlyfee.ClassSection.ClassId);
            ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", monthlyfee.ClassSection.SectionId);
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", monthlyfee.ClassSectionId);
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName", monthlyfee.StudentId );
            
            return View(monthlyfee);
        }

        // POST: /MonthlyFee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MonthlyFeeId,ClassSectionId,StudentId,FeeAmount,Date")] MonthlyFee monthlyfee)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(monthlyfee).State = EntityState.Modified;
                db.SaveChanges();
                

                
                ViewBag.SuccessMessage = "Monthly Fee Updated Successfully";




                return View("CurrentMonthlyFee", db.MonthlyFee.Include(i => i.Student).Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).FirstOrDefault(i => i.MonthlyFeeId == monthlyfee.MonthlyFeeId));


            }

            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", monthlyfee.ClassSection.ClassId);
            ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", monthlyfee.ClassSection.SectionId);
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", monthlyfee.ClassSectionId);
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "StudentName", monthlyfee.StudentId);
            return View(monthlyfee);
           
        }

        // GET: /MonthlyFee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyfee = db.MonthlyFee.Include(i => i.Student).Include(i => i.ClassSection).Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).Single(i => i.MonthlyFeeId == id);
            if (monthlyfee == null)
            {
                return HttpNotFound();
            }
            return View(monthlyfee);
        }

        // POST: /MonthlyFee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyFee m = db.MonthlyFee.Include(i => i.Student).Include(i => i.ClassSection).Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).FirstOrDefault(i => i.MonthlyFeeId == id);
            ViewBag.SuccessMessage = "Monthly Fee of " + m.Student.StudentName + " Deleted Successfully";
            //var classid = m.ClassSection.Class.ClassName;
            //var sectionid = m.ClassSection.Section.SectionName;
            //var sname = m.Student.StudentName;;
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            MonthlyFee monthlyfee = db.MonthlyFee.Find(id);
            db.MonthlyFee.Remove(monthlyfee);
            db.SaveChanges();
            
            //m.ClassSection.Class.ClassName = classid;
            //m.ClassSection.Section.SectionName = sectionid;
            //m.Student.StudentName = sname;

            return View("CurrentMonthlyFee", m);
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
