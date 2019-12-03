using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitsolMeritInSchoolSystem.Controllers
{
    public class ShowErrorController : Controller
    {
        //
        // GET: /ShowError/
        public ActionResult Show()
        {
            ViewBag.ERROR = "Here is an ERROR";
            return View("ErrorPage");
        }
	}
}