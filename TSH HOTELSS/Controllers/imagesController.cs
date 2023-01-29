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
    public class imagesController : Controller
    {
        private hmsEntities db = new hmsEntities();

        // GET: images
        public ActionResult Index()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            var images = db.images.Include(i => i.hotels);
            return View(images.ToList());
        }

        // GET: images/Details/5
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
            images images = db.images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: images/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel");
            return View();
        }

        // POST: images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idimg,imagelink,idhotel")] images images)
        {
            if (ModelState.IsValid)
            {
                db.images.Add(images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", images.idhotel);
            return View(images);
        }

        // GET: images/Edit/5
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
            images images = db.images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", images.idhotel);
            return View(images);
        }

        // POST: images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idimg,imagelink,idhotel")] images images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idhotel = new SelectList(db.hotels, "idhotel", "nomhotel", images.idhotel);
            return View(images);
        }

        // GET: images/Delete/5
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
            images images = db.images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            images images = db.images.Find(id);
            db.images.Remove(images);
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
