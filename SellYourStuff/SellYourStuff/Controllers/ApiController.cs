using SellYourStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellYourStuff.Controllers
{
    public class ApiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Api
        public ActionResult Index()
        {
            var matches = db.Products.Select(o => o.Id);
            if (matches.Count() > 0)
                return Json(matches, JsonRequestBehavior.AllowGet);
            else
                return Json("No matches found", JsonRequestBehavior.AllowGet);
        }

        // GET: Api/Details/5
        public ActionResult Details(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var matches = db.Products.Where(o=>o.Id==id);
            if (matches.Count() > 0)
                return Json(matches, JsonRequestBehavior.AllowGet);
            else
                return Json("No matches found", JsonRequestBehavior.AllowGet);
        }
    }
}
