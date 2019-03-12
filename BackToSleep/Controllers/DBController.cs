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
    public class DBController : Controller
    {
        private BackToSleepDbEntities db = new BackToSleepDbEntities();

        // GET: DB
        public ActionResult Index()
        {
            return View(db.Researches.ToList());
        }

        // GET: DB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.Researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // GET: DB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Yelp_Keyword,Description")] Research research)
        {
            if (ModelState.IsValid)
            {
                db.Researches.Add(research);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(research);
        }

        // GET: DB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.Researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // POST: DB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Yelp_Keyword,Description")] Research research)
        {
            if (ModelState.IsValid)
            {
                db.Entry(research).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(research);
        }

        // GET: DB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Research research = db.Researches.Find(id);
            if (research == null)
            {
                return HttpNotFound();
            }
            return View(research);
        }

        // POST: DB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Research research = db.Researches.Find(id);
            db.Researches.Remove(research);
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
