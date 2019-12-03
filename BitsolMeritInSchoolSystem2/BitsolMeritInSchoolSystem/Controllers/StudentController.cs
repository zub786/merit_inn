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
    public class StudentController : Controller
    {
        private ContextClasss db = new ContextClasss();

        public ActionResult AllStudentsDetailsPartialView(int c, int s)
        {
            try
            {


                var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
                ViewBag.Students = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs2 => cs2.ClassSection.Section).Where(std => std.ClassSectionId == csid.ClassSectionId && std.Closed == false).ToList();


                return PartialView(ViewBag.Students);
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }




        }


        public ActionResult AllStudentsDetails()
        {
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");
            return View();



        }




        public ActionResult OpenStudent(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = db.Student.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                var std = db.Student.Single(sid => sid.StudentId == id);
                std.Closed = false;

                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ClosedIndex");
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }






        }
        public ActionResult ClosedIndex()
        {
            try
            {
                var student = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).Where(i => i.Closed == true).ToList();

                return View(student);
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }


        public ActionResult Close(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = db.Student.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                var std = db.Student.Single(sid => sid.StudentId == id);
                std.Closed = true;

                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

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
                //var Sections = db.Section.Where(i => i.ClassId == id).ToList();
                var Sections = db.ClassSections.Include("Section").Where(i => i.ClassId == id).ToList();
                var data = new List<object>();
                foreach (var temp in Sections)
                {
                    data.Add(new { value = temp.SectionId, display = temp.Section.SectionName });
                }
                return Json(data, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }


        public ActionResult GetEditSections(int id)
        {

            try
            {

                Session["Class"] = id;
                //var Sections = db.Section.Where(i => i.ClassId == id).ToList();
                var Sections = db.ClassSections.Include("Section").Where(i => i.ClassId == id).ToList();
                var data = new List<object>();
                foreach (var temp in Sections)
                {
                    data.Add(new { value = temp.SectionId, display = temp.Section.SectionName });
                }
                return Json(data, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }
        //// GET: /Student/
        //public ActionResult Index()
        //{
        //    if (Session["Email"] == null)
        //    {

        //        return RedirectToAction("../Home/Login");
        //    }
        //    else
        //    {
        //        var students = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).Where(i => i.Closed == false).ToList();





        //        List<StudentModelView> StudentModelViewIC = new List<StudentModelView>();





        //        foreach (var st in students)
        //        {
                   
        //                StudentModelView SVM = new StudentModelView();
        //                if (st.StudentImage != null)
        //                {
        //                    string image = Convert.ToBase64String(st.StudentImage);

        //                    SVM.StudentImage = "data:image/png;base64," + image;
        //                }
        //                else
        //                {
        //                    SVM.StudentImage = null;
        //                }
        //                SVM.StudentId = st.StudentId;
        //                SVM.StudentName = st.StudentName;
        //                SVM.FatherName = st.FatherName;
        //                SVM.ClassSection = st.ClassSection;
        //                SVM.ClassSectionId = st.ClassSectionId;
        //                SVM.Closed = st.Closed;
        //                SVM.Update = st.Update;
        //                SVM.BayFormNo = st.BayFormNo;
        //                SVM.ContactNo = st.ContactNo;
        //                SVM.Date = st.Date;
        //                SVM.DateOfBirth = st.DateOfBirth;
        //                SVM.Email = st.Email;


        //                StudentModelViewIC.Add(SVM);
                   
                   
                    


        //        }


        //        return View(StudentModelViewIC);
        //    }





        //}








        public ActionResult AllStudentIndex()
        {
            if (Session["Email"] == null)
            {

                return RedirectToAction("../Home/Login");
            }
            else
            {
                var student = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).Where(i => i.Closed == false).ToList();
                return View(student);
            }





        }






        // GET: /Student/
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {

                return RedirectToAction("../Home/Login");
            }
            else
            {
                //var student = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).Where(i => i.Closed == false).ToList();
                return View();
            }





        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student st = db.Student.Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).Single(i => i.StudentId == id);
            if (st == null)
            {
                return HttpNotFound();
            }


              StudentModelView SVM = new StudentModelView();
                        if (st.StudentImage != null)
                        {
                            string image = Convert.ToBase64String(st.StudentImage);

                            SVM.StudentImage = "data:image/png;base64," + image;
                        }
                        else
                        {
                            SVM.StudentImage = null;
                        }
                       
                        SVM.StudentName = st.StudentName;
                        SVM.FatherName = st.FatherName;
                        SVM.ClassSection = st.ClassSection;
                        SVM.ClassSectionId = st.ClassSectionId;
                        SVM.Closed = st.Closed;
                        SVM.Update = st.Update;
                        SVM.BayFormNo = st.BayFormNo;
                        SVM.ContactNo = st.ContactNo;
                        SVM.Date = st.Date;
                        SVM.DateOfBirth = st.DateOfBirth;
                        SVM.Email = st.Email;

                      return View(SVM);
           
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            //  ViewBag.CSID = null;
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId");
            ViewBag.CSERROR = "";
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,StudentName,FatherName,ContactNo,BayFormNo,Date,Email,DateOfBirth,StudentImage,ClassSectionId,Closed,Update", Exclude = "StudentImage")] Student student, HttpPostedFileBase StudentImage)
        {
            try
            {

                if (StudentImage != null)
                {
                    student.StudentImage = new byte[StudentImage.ContentLength];
                    StudentImage.InputStream.Read(student.StudentImage, 0, StudentImage.ContentLength);
                }
              




                Regex Cno = new Regex(@"^[0-9]");


                if (student.ClassSectionId == 0)
                {
                    ModelState.AddModelError("ClassSectionId", " Please Choose Class and Section");

                    //   ViewBag.CSERROR = " Please Choose Class and Section";

                }



                if (student.ContactNo != null)
                {
                    bool ismatch = Cno.IsMatch(student.ContactNo.ToString());
                    if (ismatch == false)
                    {
                        ModelState.AddModelError("ContactNo", "Contact No is not in correct format");
                    }

                }

                if (student.BayFormNo != null)
                {
                    var isFoundStd = db.Student.Where(i => i.BayFormNo == student.BayFormNo).ToList();
                    if (isFoundStd.Count > 0)
                    {
                        ModelState.AddModelError("BayFormNo", "Sorry this student already exist");
                    }
                }


                if (ModelState.IsValid)
                {
                    db.Student.Add(student);
                    db.SaveChanges();

                    ViewBag.Model = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).ToList().AsQueryable().Last();
                    ViewBag.SucccessMessage = "Student added successfully";
                    return View("CurrentIndex");
                }
                //ClassSection csid = db.ClassSections.Find(student.ClassSectionId);

                //ViewBag.CSID = csid;
                //ViewBag.Cities = new SelectList(db.Cities.Where(i => i.isDelete == false), "CityID", "cityName", student.ClassSection.ClassId);
                if (student.ClassSectionId != 0)
                {
                    var cs = db.ClassSections.Where(i => i.ClassSectionId == student.ClassSectionId).ToList();
                    int cid = cs[0].ClassId;
                    int sid = cs[0].SectionId;

                    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", cid);
                    ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName", sid);
                    ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", student.ClassSectionId);
                    return View(student);
                }
                ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", student.ClassSection.ClassId);
                ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", student.ClassSection.SectionId);
                ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", student.ClassSectionId);
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Include(i => i.ClassSection).FirstOrDefault(i => i.StudentId == id);

            if (student.BayFormNo != null)
            {
                Session["ForEditCNIC"] = student.BayFormNo.ToString();
            }
            else
            {
                Session["ForEditCNIC"] = null;
            }


            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName", student.ClassSection.ClassId);
            ViewBag.SectionId = new SelectList(db.Section, "SectionId", "SectionName", student.ClassSection.SectionId);
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", student.ClassSectionId);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StudentName,FatherName,ContactNo,BayFormNo,Date,Email,DateOfBirth,StudentImage,ClassSectionId,Closed,Update", Exclude = "StudentImage")] Student student, HttpPostedFileBase StudentImage)
        {
            if (StudentImage != null)
            {
                student.StudentImage = new byte[StudentImage.ContentLength];
                StudentImage.InputStream.Read(student.StudentImage, 0, StudentImage.ContentLength);
            }


            Regex Cno = new Regex(@"^[0-9]");


            if (student.ClassSectionId == 0)
            {
                ModelState.AddModelError("ClassId", " Please choose class");

                ModelState.AddModelError("SectionId", " Please choose section");
            }



            if (Cno.IsMatch(student.ContactNo))
            {

            }
            else
            {
                ModelState.AddModelError("ContactNo", "Contact No is not in correct format");
            }

           


            if (student.BayFormNo != null)
            {

                if (student.BayFormNo != (string)Session["ForEditCNIC"])
                {
                    var isFoundStd = db.Student.Where(i => i.BayFormNo == student.BayFormNo).ToList();
                    if (isFoundStd.Count > 0)
                    {
                        ModelState.AddModelError("BayFormNo", "Sorry this student already exist");
                    }
                }
               

                    var isFoundStd2 = db.Student.Where(i => i.BayFormNo == student.BayFormNo).ToList();
                    if (isFoundStd2.Count > 0)
                    {
                        ModelState.AddModelError("BayFormNo", "Sorry this student already exist");
                    }
                



            }




            if (ModelState.IsValid)
            {

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");


                ViewBag.Model = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs1 => cs1.ClassSection.Section).FirstOrDefault(i => i.StudentId == student.StudentId);
                ViewBag.Model = student;
                ViewBag.SucccessMessage = "Student information updated successfully";
                return View("CurrentIndex");



            }
            ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
            ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0).ToList(), "SectionId", "SectionName");
            ViewBag.ClassSectionId = new SelectList(db.ClassSections, "ClassSectionId", "ClassSectionId", student.ClassSectionId);
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Include(i => i.ClassSection.Class).Include(i => i.ClassSection.Section).Single(i => i.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            try
            {
                Student student = db.Student.Find(id);
                db.Student.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("An error occurred while updating the entries"))
                {
                    ViewBag.ERROR = "Sorry first delete the reference of this student from Attendence & Monthly Fees Otherwise you should Close this student.";
                }


                return View("ErrorPage");
            }
        }
        public int GetCSId(int c, int s)
        {
            var CSID = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
            return CSID.ClassSectionId;

        }


        public int GetEditCSId(int c, int s)
        {
            var CSID = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
            return CSID.ClassSectionId;

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
