using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellYourStuff.Models;
using Microsoft.AspNet.Identity;
using SellYourStuff.ModelViews;

namespace SellYourStuff.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public async Task<ActionResult> Index(int? SelectedCategory, string searchString)
        {

            var categories = db.Catogeries.OrderBy(q => q.Title).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "Id", "Title", SelectedCategory);
            int categoryId = SelectedCategory.GetValueOrDefault();
            var products = 
                db.Products
                .Where((c => !SelectedCategory.HasValue || c.CategoryId == categoryId))
                .OrderBy(d => d.CategoryId)
                .Include(d => d.Category);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(o => o.Title.ToLower().StartsWith(searchString.ToLower()));
            }
            string cName = "regionCookie";
            HttpCookie cookie = Request.Cookies[cName];
            if (cookie != null)
                {
                    string message = Server.UrlDecode(
                        Request.Cookies[cName].Value);
                if (message != "Sweden")
                {
                    products = products.Where(o => o.ApplicationUser.Region.RegionName == message);
                }
            }

            return View(await products.ToListAsync());
        }

        [HttpPost]
        public ActionResult RegionCookie(string regionName)
        {
            string cName = "regionCookie";
            HttpCookie cookie = Request.Cookies[cName];
            if (!String.IsNullOrEmpty(regionName))
            {
                Response.Cookies[cName].Value =
                        Server.UrlEncode(regionName);
                Response.Cookies[cName].Expires =
                    DateTime.Now.AddDays(10);

            }
            return Json(0);
        }

        [Authorize]
        public async Task<ActionResult> MyProducts()
        {
            var user = User.Identity.GetUserId();
            var products = db.Products.Where(o => o.ApplicationUserId == user).ToListAsync();
            return View(await products);
        }
        [ChildActionOnly]
        public ActionResult SearchPartial(int? SelectedCategory)
        {
            var categories = db.Catogeries.OrderBy(q => q.Title).ToList();
            SearchView searchView = new SearchView();
            searchView.dropDown = new SelectList(categories, "Id", "Title", SelectedCategory);
            return PartialView("_SearchPartial",searchView);
        }
        // GET: Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize]
        // GET: Product/Create
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Price,Image,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ApplicationUserId = User.Identity.GetUserId();
				product.PublishedDate = DateTime.Now;

				db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateCategoryDropDownList(product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateCategoryDropDownList(product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Price,Image,PublishedDate,ApplicationUserId,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
				
                db.Entry(product).State = EntityState.Modified;
				await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categorysQuery = from d in db.Catogeries
                                 orderby d.Title
                                 select d;
            ViewBag.CategoryId = new SelectList(categorysQuery, "Id", "Title", selectedCategory);
        }


    }
}
