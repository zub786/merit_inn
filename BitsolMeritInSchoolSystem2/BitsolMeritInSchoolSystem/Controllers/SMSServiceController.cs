using BitsolMeritInSchoolSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using BitsolMeritInSchoolSystem.Models;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class SMSServiceController : Controller
    {



        ContextClasss db = new ContextClasss();

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult EmailPassword()
        {

            try
            {
                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    if ((Session["CredentialEmail"] == null || Session["CredentialEmail"].ToString() == "") && (Session["CredentialPassword"] == null || Session["CredentialPassword"] == ""))
                    {
                        return View();
                    }
                    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                    ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");

                    return View("Index");

                }
                else
                {
                    return View("Login");

                }
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }




        }
        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {

            Session["CredentialEmail"] = Email.ToString();
            Session["CredentialPassword"] = Password.ToString();


            try
            {

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

        public ActionResult Index()
        {
            try
            {
                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    if ((Session["CredentialEmail"] == null || Session["CredentialEmail"] == "") && (Session["CredentialPassword"] == null || Session["CredentialPassword"] == ""))
                    {
                        return View("EmailPassword");
                    }
                    ViewBag.ClassId = new SelectList(db.Class, "ClassId", "ClassName");
                    ViewBag.SectionId = new SelectList(db.Section.Where(i => i.SectionId == 0), "SectionId", "SectionName");

                    return View("Index");

                }
                else
                {
                    return View("Login");

                }


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

                if (Session["Email"] == null)
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



        public string SendMessage(int c, int s, string m, string sub)
        {


            var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
            var emails = db.Student.Include("ClassSection").Where(cs => cs.ClassSectionId == csid.ClassSectionId && cs.Closed == false).Select(i => i.Email).ToList();

            string Too = string.Join(",", emails);
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.hotmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), Session["CredentialPassword"].ToString());
                MailMessage msg = new MailMessage();
                msg.To.Add(Too);
                msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                msg.Subject = sub;
                msg.Body = m.ToString();
                //Attachment data = new Attachment(browsingpathtextbox.Text);
                //msg.Attachments.Add(data);
                client.Send(msg);
                return "Message Send Successfully";


            }
            catch (Exception e)
            {
                return e.ToString();
            }




        }






        public string SendAllStaffMessage(string staffm, string staffsub)
        {

            var semails = db.StaffMember.Select(i => i.StaffMemberEmail).ToList();
            string Too = string.Join(",", semails);
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), "kilopbscsM");
                MailMessage msg = new MailMessage();
                msg.To.Add(Too);
                msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                msg.Subject = staffsub;
                msg.Body = staffm.ToString();

                client.Send(msg);
                return "Message Send Successfully";


            }
            catch (Exception e)
            {
                return e.ToString();
            }



        }


        public string SendFacultyMessage(string facultym, string staffsub)
        {


            var semails = db.StaffMember.Where(i => i.Designation == "Teacher").Select(i => i.StaffMemberEmail).ToList();
            string Too = string.Join(",", semails);
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), "kilopbscsM");
                MailMessage msg = new MailMessage();
                msg.To.Add(Too);
                msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                msg.Subject = staffsub;
                msg.Body = facultym.ToString();

                client.Send(msg);
                return "Message Send Successfully";


            }
            catch (Exception e)
            {
                return e.ToString();
            }



        }





        public string SendAllMessage(string m)
        {


            var emails = db.Student.Where(cs => cs.Closed == false).Select(i => i.Email).ToList();
            string Too = string.Join(",", emails);
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), "kilopbscsM");
                MailMessage msg = new MailMessage();
                msg.To.Add(Too);
                msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                msg.Subject = "MERIT SCHOOL NOTICFICATION";
                msg.Body = m.ToString();
                //Attachment data = new Attachment(browsingpathtextbox.Text);
                //msg.Attachments.Add(data);
                client.Send(msg);
                return "Message Send Successfully";


            }
            catch (Exception e)
            {
                return e.ToString();
            }



        }






        public ActionResult ShowSelectiveStudents(int c, int s)
        {
            try
            {

                var csid = db.ClassSections.Single(i => i.ClassId == c && i.SectionId == s);
                var Students = db.Student.Include("ClassSection").Where(cs => cs.ClassSectionId == csid.ClassSectionId && cs.Closed == false).ToList();

                ViewBag.AllStudents = Students;

                return PartialView();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }

        public ActionResult ShowStaff()
        {
            try
            {

                var mem = db.StaffMember.ToList();

                ViewBag.Members = mem;

                return PartialView();


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }


        public string SendSelectiveStudentMsg(string selected, string m, string stdsub)
        {


            int stdID;
            string[] values = selected.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                stdID = Int32.Parse(values[i]);
                Student std = new Student();
                std = db.Student.Where(cs => cs.Closed == false).Single(s => s.StudentId == stdID);



                try
                {
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Timeout = 100000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), Session["CredentialPassword"].ToString());
                    MailMessage msg = new MailMessage();
                    msg.To.Add(std.Email.ToString());
                    msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                    msg.Subject = stdsub;
                    msg.Body = m.ToString();

                    client.Send(msg);



                }
                catch (Exception e)
                {
                    return e.ToString();

                }

            }

            return "Message Send Successfully To Selective Students";


        }

        public string SendSelectiveStaffMsg(string selected, string m, string staffsub)
        {


            int stfID;
            string[] values = selected.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                stfID = Int32.Parse(values[i]);
                StaffMember stf = new StaffMember();
                stf = db.StaffMember.Single(s => s.StaffMemberId == stfID);



                try
                {
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Timeout = 100000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Session["CredentialEmail"].ToString(), Session["CredentialPassword"].ToString());
                    MailMessage msg = new MailMessage();
                    msg.To.Add(stf.StaffMemberEmail.ToString());
                    msg.From = new MailAddress(Session["CredentialEmail"].ToString());
                    msg.Subject = staffsub;
                    msg.Body = m.ToString();

                    client.Send(msg);



                }
                catch (Exception e)
                {
                    return e.ToString();

                }

            }

            return "Message Send Successfully To Selective Staff Members";



        }





    }
}