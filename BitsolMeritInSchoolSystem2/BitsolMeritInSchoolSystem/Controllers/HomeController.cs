using BitsolMeritInSchoolSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class HomeController : Controller
    {
        ContextClasss db = new ContextClasss();

        public ActionResult HomePage()
        {
            try
            {

                ViewBag.TotalStudents = db.Student.Where(i => i.Closed == false).Count();
                ViewBag.TotalEmployees = db.StaffMember.Count();
                ViewBag.FacultyMembers = db.StaffMember.Where(i => i.Designation == "Teacher").Count();
                ViewBag.TotalClasses = db.ClassSections.Count();

                ViewBag.TotalDailyExpenditures =  (from te in db.DailyExpenditure
                 where te.Date.Year == DateTime.Now.Year && te.Date.Month == DateTime.Now.Month
                 select (int?)te.ExpenditureAmount).Sum();

                ViewBag.TotalSalaryExpenditures = (from te in db.MonthlySalarySlip
                                                  where te.Date.Year == DateTime.Now.Year && te.Date.Month == DateTime.Now.Month
                                                  select (int?)te.Salary).Sum();



                ViewBag.TotalRevenue = (from tr in db.MonthlyFee
                                             where tr.Date.Year == DateTime.Now.Year && tr.Date.Month == DateTime.Now.Month
                                             select (int?)tr.FeeAmount).Sum();

                



                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
            
           



        }
        public ActionResult Login()        
        {
            try
            {

                if (Session["Email"] != null)
                {

                    return RedirectToAction("../Home/HomePage");
                }
                else
                {
                    ViewBag.UserName = "";
                    return View();

                }

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }

          
                
            
           
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            try
            {

                var isFound = db.User.Where(i => i.UserName == username).ToList();



                if (isFound.Count() != 0)
                {
                    if (isFound[0].UserName == username && isFound[0].Password == password)
                    {
                        if (isFound[0].UserRole == 1)
                        {
                            Session["Email"] = username;
                            Session["UserRole"] = 1;
                            return RedirectToAction("../Home/HomePage");
                        }
                        else
                        {
                            Session["Email"] = username;
                            Session["UserRole"] = 2;
                            return RedirectToAction("../Attendence/Index");
                        }
                    }
                    else
                    {
                        ViewBag.IncorrectPW = "Incorrect Password";
                        ViewBag.UserName = username.ToString();
                        return View("Login");
                    }
                }
                else
                {

                    ViewBag.UserNF = "User Not Found";
                    return View("Login");

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "Sequence contains no elements")
                {
                    Exception e2 = (Exception)Activator.CreateInstance(ex.GetType(), "Sorry ! this user NOT FOUND", ex);
                    throw e2;
                   
                }

                ViewBag.UserName = username.ToString();
                ViewBag.IncorrectPW = ex.ToString();
                return View("Login");

            }

            
           
           
        }


        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                return View("Login");


            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


           

        }

    }
}