using BitsolMeritInSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitsolMeritInSchoolSystem.DAL;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class AdministratorController : Controller
    {
        ContextClasss db = new ContextClasss();
        //
        // GET: /Administrator/
        public ActionResult Admin()
        {
            try
            {

                if (Session["Email"] != null && Convert.ToInt32(Session["UserRole"]) == 1)
                {
                    return RedirectToAction("../Student/Index");

                }
                return View("Login"); 

            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message.ToString();
                return View("ErrorPage");
            }
            
           
        }

        public string Create(User User)
        {
            db.User.Add(User);
            db.SaveChanges();
            return "Add Successfully";
            
        }
	}
}