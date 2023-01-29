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
    public class reviewsController : Controller
    {
        private hmsEntities db = new hmsEntities();

        // GET: reviews
        public ActionResult Index()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            var reviews = db.reviews.Include(r => r.hotels).Include(r => r.users);
            return View(reviews.ToList());
        }

        // GET: reviews/Details/5
        public ActionResult Details(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reviews reviews = db.reviews.Find(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        // GET: reviews/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel");
            ViewBag.iduser = new SelectList(db.users, "iduser", "username");
            return View();
        }

        // POST: reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idrev,titlerev,descr,idhotel,iduser,score")] reviews reviews)
        {
            if (ModelState.IsValid)
            {
                db.reviews.Add(reviews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", reviews.idhotel);
            ViewBag.iduser = new SelectList(db.users, "iduser", "username", reviews.iduser);
            return View(reviews);
        }

        // GET: reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reviews reviews = db.reviews.Find(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", reviews.idhotel);
            ViewBag.iduser = new SelectList(db.users, "iduser", "username", reviews.iduser);
            return View(reviews);
        }

        // POST: reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idrev,titlerev,descr,idhotel,iduser,score")] reviews reviews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", reviews.idhotel);
            ViewBag.iduser = new SelectList(db.users, "iduser", "username", reviews.iduser);
            return View(reviews);
        }

        // GET: reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reviews reviews = db.reviews.Find(id);
            if (reviews == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        // POST: reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reviews reviews = db.reviews.Find(id);
            db.reviews.Remove(reviews);
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
