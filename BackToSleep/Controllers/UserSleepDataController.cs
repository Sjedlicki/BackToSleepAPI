using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackToSleep.Models;
using Microsoft.AspNet.Identity;

namespace BackToSleep.Controllers
{
    [Authorize]
    public class UserSleepDataController : Controller
    {
        private SleeperDbContext db = new SleeperDbContext();

        // GET: UserSleepData
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            var UserSleeps = db.SleepDatas.Where(s => s.UserID == userID).ToList();
            return View(UserSleeps);
        }

        // GET: UserSleepData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepData sleepData = db.SleepDatas.Find(id);
            if (sleepData == null)
            {
                return HttpNotFound();
            }
            return View(sleepData);
        }

        // GET: UserSleepData/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: UserSleepData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SleepHours,SleepQuality,Day,Date")] SleepData sleepData)
        {
            sleepData.UserID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.SleepDatas.Add(sleepData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", sleepData.UserID);
            return View(sleepData);
        }

        // GET: UserSleepData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepData sleepData = db.SleepDatas.Find(id);
            if (sleepData == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", sleepData.UserID);
            return View(sleepData);
        }

        // POST: UserSleepData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SleepHours,SleepQuality,Day,Date,UserID")] SleepData sleepData)
        {
            sleepData.UserID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(sleepData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", sleepData.UserID);
            return View(sleepData);
        }

        // GET: UserSleepData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepData sleepData = db.SleepDatas.Find(id);
            if (sleepData == null)
            {
                return HttpNotFound();
            }
            return View(sleepData);
        }

        // POST: UserSleepData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SleepData sleepData = db.SleepDatas.Find(id);
            db.SleepDatas.Remove(sleepData);
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
