using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellYourStuff.Models;
using Microsoft.AspNet.Identity;
using SellYourStuff.ModelViews;

namespace SellYourStuff.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index(string id)
        {
            //string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(o => o.Id == id);
            var p = new ProfileViewModel();
            p.Email = currentUser.Email;
            p.Number = currentUser.PhoneNumber;
            p.Products = db.Products.Where(o => o.ApplicationUserId == id).ToList();
            p.Region = currentUser.Region;
            return View(p);
        }

        public ActionResult FavoriteList()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(o => o.Id == currentUserId);
            var fav = db.FavLists.Where(o => o.ApplicationUserID == currentUserId).FirstOrDefault();

            return View(fav.Products.ToList());
        }
       
    }
}
