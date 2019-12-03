using BitsolMeritInSchoolSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class RevenueController : Controller
    {
        private ContextClasss db = new ContextClasss();
        public ActionResult ShowDailyRevenue(DateTime Date)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }




                ViewBag.TotalDailyExpense = db.MonthlyFee.Where(i => i.Date.Month == Date.Month && i.Date.Year == Date.Year && i.Date.Day == Date.Day).Sum(i => i.FeeAmount);
                ViewBag.TotalDailyExpenseDetails = db.MonthlyFee.Include("Student").Include("ClassSection").Where(i => i.Date.Month == Date.Month && i.Date.Year == Date.Year && i.Date.Day == Date.Day).ToList();
                return PartialView();



            }
            catch (Exception e)
            {
                if (e.Message == "The cast to value type 'System.Int32' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.")
                {
                    ViewBag.ERROR = "Sorry! There is no record for this year";
                    return View("ErrorPage");

                }
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }

        public ActionResult DailyRevenue(){

            return View();
        }




        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
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
        public ActionResult ShowMonthlyRevenue(int m, int y)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
              
             


                ViewBag.TotalMonthlyExpense = db.MonthlyFee.Where(i => i.Date.Month == m && i.Date.Year == y).Sum(i => i.FeeAmount);

                ViewBag.TotalMonthlyExpenseDetails = db.MonthlyFee.Include("Student").Include("ClassSection").Where(i => i.Date.Month == m && i.Date.Year == y).ToList();
                return PartialView();

               

            }
            catch (Exception e)
            {
                if (e.Message == "The cast to value type 'System.Int32' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.")
                {
                    ViewBag.ERROR = "Sorry! There is no record for this year";
                    return View("ErrorPage");

                }
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }

        public ActionResult ShowAnnualRevenue(int y)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
              



                int TME = db.MonthlyFee.Where(i => i.Date.Year == y).Sum(i => i.FeeAmount);

                if (TME == 0)
                {
                    ViewBag.TotalAnnualExpense = 0;
                    return PartialView();
                }

                ViewBag.TotalAnnualExpense = TME;

                ViewBag.TotalAnnualExpenseDetails = db.MonthlyFee.Include("Student").Include("ClassSection").Where(i => i.Date.Year == y).ToList();
                return PartialView();

            }
            catch (Exception e)
            {
                if (e.Message == "The cast to value type 'System.Int32' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.")
                {
                    ViewBag.ERROR = "Sorry! There is no record for this combination year and month";
                    return View("ErrorPage");

                }
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }


        }



        public ActionResult AnnualRevenue()
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
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

        public ActionResult MonthlyRevenue()
        {
            try
            {

                ViewBag.Expenses = db.MonthlyFee.ToList();
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }



        }


    

     
  

    


	}
}