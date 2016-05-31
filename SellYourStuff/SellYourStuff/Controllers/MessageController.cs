﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SellYourStuff.Models;
using Microsoft.AspNet.Identity;

namespace SellYourStuff.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Message
        public ActionResult Index()
        {
            //var messages = db.Messages.Include(m => m.ApplicationUser);
            var user = User.Identity.GetUserId();
            var messages = db.Messages.Include(m=>m.ApplicationUser).Where(o=>o.ApplicationUserId== user);
            return View(messages.ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string messageString,string userId, string subject )
        {
            var message = new Message();
            message.ApplicationUserId = userId;
            message.DateRecieved = DateTime.Now;
            message.IsSeen = false;
            message.Subject = subject;
            message.SenderId= User.Identity.GetUserId();
            message.MessageRecieved = messageString;
            db.Messages.Add(message);
            db.SaveChanges();

            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", message.ApplicationUserId);
            return Json("Message send");
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
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
    }
}
