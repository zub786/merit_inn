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
    public class DailyExpenditureController : Controller
    {
        private ContextClasss db = new ContextClasss();

        public ActionResult ShowMonthlyExpense(int m,int y) {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                ViewBag.Expenses = db.DailyExpenditure.Where(i => i.Date.Month == m && i.Date.Year == y).ToList();

                ViewBag.TotalMonthlyExpense = db.DailyExpenditure.Where(i => i.Date.Month == m && i.Date.Year == y).Sum(i => i.ExpenditureAmount);
                return PartialView();

            }
            catch (Exception e)
            {
                if (e.Message == "The cast to value type 'System.Int32' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type.")
                {
                    ViewBag.ERROR = "Sorry! There is no record for this combinaation of year and month";
                    return View("ErrorPage");

                }
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        
        }

        public ActionResult ShowAnnualExpense(int y)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                ViewBag.Expenses = db.DailyExpenditure.Where(i => i.Date.Year == y).ToList();

                ViewBag.TotalAnnualExpense = db.DailyExpenditure.Where(i => i.Date.Year == y).Sum(i => i.ExpenditureAmount);


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
        public ActionResult SalaryExpense()
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


        public ActionResult ShowMonthlySalaryExpense(int m, int y)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                ViewBag.Expenses = db.MonthlySalarySlip.Where(i => i.Date.Month == m && i.Date.Year == y).ToList();

                ViewBag.TotalMonthlySalaryExpense = db.MonthlySalarySlip.Where(i => i.Date.Month == m && i.Date.Year == y).Sum(i => i.Salary);
                return PartialView();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
        }
        public ActionResult AnnualExpense()
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

        public ActionResult MonthlyExpense() {
            try
            {

                ViewBag.Expenses = db.DailyExpenditure.ToList();
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        
        
        }


        // GET: /DailyExpenditure/
        public ActionResult Index()
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                return View(db.DailyExpenditure.ToList());

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // GET: /DailyExpenditure/Details/5
        public ActionResult Details(int? id)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DailyExpenditure dailyexpenditure = db.DailyExpenditure.Find(id);
                if (dailyexpenditure == null)
                {
                    return HttpNotFound();
                }
                return View(dailyexpenditure);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /DailyExpenditure/Create
        public ActionResult Create()
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

        // POST: /DailyExpenditure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DailyExpenditureId,ExpenditureDescription,ExpenditureAmount,Date")] DailyExpenditure dailyexpenditure)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.DailyExpenditure.Add(dailyexpenditure);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(dailyexpenditure);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /DailyExpenditure/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DailyExpenditure dailyexpenditure = db.DailyExpenditure.Find(id);
                if (dailyexpenditure == null)
                {
                    return HttpNotFound();
                }
                return View(dailyexpenditure);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
         
        }

        // POST: /DailyExpenditure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DailyExpenditureId,ExpenditureDescription,ExpenditureAmount,Date")] DailyExpenditure dailyexpenditure)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(dailyexpenditure).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dailyexpenditure);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
           
        }

        // GET: /DailyExpenditure/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DailyExpenditure dailyexpenditure = db.DailyExpenditure.Find(id);
                if (dailyexpenditure == null)
                {
                    return HttpNotFound();
                }
                return View(dailyexpenditure);

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
          
        }

        // POST: /DailyExpenditure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                if (Session["Email"] == null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return View("Login");
                }
                DailyExpenditure dailyexpenditure = db.DailyExpenditure.Find(id);
                db.DailyExpenditure.Remove(dailyexpenditure);
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
