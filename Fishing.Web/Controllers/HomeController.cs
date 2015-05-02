using Fishing.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fishing.Web.Controllers
{
    public class HomeController : Controller
    {
        private MattMillerTimeEntities db = new MattMillerTimeEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var vm = new { Categories = db.Categories }.ToExpando();
            return View(vm);
        }
    }
}
