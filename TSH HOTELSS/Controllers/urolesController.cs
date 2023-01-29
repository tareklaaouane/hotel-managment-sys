using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSH_HOTELSS.Models;

namespace TSH_HOTELSS.Controllers
{
    public class urolesController : Controller
    {
        private hmsEntities db = new hmsEntities();

        // GET: uroles
        public ActionResult Index()
        {
            return View(db.uroles.ToList());
        }

        // GET: uroles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uroles uroles = db.uroles.Find(id);
            if (uroles == null)
            {
                return HttpNotFound();
            }
            return View(uroles);
        }

        // GET: uroles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: uroles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleid,rolename")] uroles uroles)
        {
            if (ModelState.IsValid)
            {
                db.uroles.Add(uroles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uroles);
        }

        // GET: uroles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uroles uroles = db.uroles.Find(id);
            if (uroles == null)
            {
                return HttpNotFound();
            }
            return View(uroles);
        }

        // POST: uroles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleid,rolename")] uroles uroles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uroles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uroles);
        }

        // GET: uroles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uroles uroles = db.uroles.Find(id);
            if (uroles == null)
            {
                return HttpNotFound();
            }
            return View(uroles);
        }

        // POST: uroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uroles uroles = db.uroles.Find(id);
            db.uroles.Remove(uroles);
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
