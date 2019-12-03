using BitsolMeritInSchoolSystem.DAL;
using BitsolMeritInSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class PromotClassController : Controller
    {

        ContextClasss db = new ContextClasss();
        //
        // GET: /PromotClass/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null)
                {

                    return RedirectToAction("../Home/Login");
                }
                else
                {
                    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                    ViewBag.SectionId = new SelectList(db.Section.Where(s => s.SectionId == -1), "SectionId", "SectionName");
                    ViewBag.PromotToClassId = new SelectList(db.Class, "ClassId", "ClassName");
                    ViewBag.PromotToSectionId = new SelectList(db.Section.Where(s => s.SectionId == 0), "SectionId", "SectionName");
                    return View();

                }






            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

        }


        public string Promot(string PromotList, int PromotTo, int PromotToSec)
        {

            int counter = 0, total = 0;
            string[] values = PromotList.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                int id = int.Parse(values[i].ToString());

                var st = db.Student.Single(kk => kk.StudentId == id);

                var PTO = db.ClassSections.Include(c => c.Class).Include(s => s.Section).Where(pto => pto.Class.ClassId == PromotTo && pto.Section.SectionId == PromotToSec).ToList();




                ClassSection cs = new ClassSection();
                cs = PTO[0];

                if (st.Update == false && st.Closed == false)
                {

                    st.ClassSection = cs;
                    st.Update = true;
                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();


                    counter++;
                }
                total++;

            }

            int NotPromoted = total - counter;


            if (counter > 0)
            {
                return counter + " Students of " + total + " could promoted successfully\n" + NotPromoted + "Students are already promoted ";
            }

            else
            {
                return "No one student could promoted\nif you want to do so, then first done prmotions then promot them again";
            }

        }

        public string Promoted()
        {
            db.Student.ToList().ForEach(a => a.Update = false);
            db.SaveChanges();






            return "Students promotion has been done successfully";


        }

        public ActionResult ShowStudents(int c, int s)
        {
            try
            {


                var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
                var Students = db.Student.Include(cs => cs.ClassSection).Include(cs1 => cs1.ClassSection.Class).Include(cs2 => cs2.ClassSection.Section).Where(std => std.ClassSectionId == csid.ClassSectionId && std.Closed == false).ToList();

                ViewBag.Students = Students;
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

                //if (Session["Email"] == null)
                //{
                //    return View("Login");
                //}
                Session["Class"] = id;
                var Sections = db.ClassSections.Include("Class").Include("Section").Where(i => i.ClassId == id).ToList();
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


        public ActionResult PromotToGetSections(int id)
        {
            try
            {
                //if (Session["Email"] == null)
                //{
                //    return View("Login");
                //}
                Session["Class"] = id;
                var Sections = db.ClassSections.Include("Class").Include("Section").Where(i => i.ClassId == id).ToList();
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



    }
}