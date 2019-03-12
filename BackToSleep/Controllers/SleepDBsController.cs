using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackToSleep.Models;

namespace BackToSleep.Controllers
{
    public class SleepDBsController : Controller
    {
        private SleeperDbContext db = new SleeperDbContext();

        // GET: SleepDBs
        public ActionResult Index()
        {
            return View(db.SleepDBs.ToList());
        }

        // GET: SleepDBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepDB sleepDB = db.SleepDBs.Find(id);
            if (sleepDB == null)
            {
                return HttpNotFound();
            }
            return View(sleepDB);
        }

        // GET: SleepDBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SleepDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,YelpKey,Description,SleepHours")] SleepDB sleepDB)
        {
            if (ModelState.IsValid)
            {
                db.SleepDBs.Add(sleepDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sleepDB);
        }

        // GET: SleepDBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepDB sleepDB = db.SleepDBs.Find(id);
            if (sleepDB == null)
            {
                return HttpNotFound();
            }
            return View(sleepDB);
        }

        // POST: SleepDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,YelpKey,Description,SleepHours")] SleepDB sleepDB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sleepDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sleepDB);
        }

        // GET: SleepDBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SleepDB sleepDB = db.SleepDBs.Find(id);
            if (sleepDB == null)
            {
                return HttpNotFound();
            }
            return View(sleepDB);
        }

        // POST: SleepDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SleepDB sleepDB = db.SleepDBs.Find(id);
            db.SleepDBs.Remove(sleepDB);
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
