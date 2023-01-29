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
    public class hotelsController : Controller
    {
        private hmsEntities db = new hmsEntities();

        // GET: hotels
        public ActionResult Index()
        {
            return View(db.hotels.ToList());
        }

        // GET: hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotels hotels = db.hotels.Find(id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            return View(hotels);
        }

        // GET: hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idhotel,nomhotel,ville,nbetoile,rating,prix,descr")] hotels hotels)
        {
            if (ModelState.IsValid)
            {
                db.hotels.Add(hotels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotels);
        }

        // GET: hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotels hotels = db.hotels.Find(id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            return View(hotels);
        }

        // POST: hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idhotel,nomhotel,ville,nbetoile,rating,prix,descr")] hotels hotels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotels);
        }

        // GET: hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hotels hotels = db.hotels.Find(id);
            if (hotels == null)
            {
                return HttpNotFound();
            }
            return View(hotels);
        }

        // POST: hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hotels hotels = db.hotels.Find(id);
            db.hotels.Remove(hotels);
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

        public ActionResult List()
        {
            return View(db.hotels.ToList());
        }

        public ActionResult Search(string q)
        {

            var pquery1 = db.hotels.Where(elt => elt.ville.Contains(q)).ToList();
            if (pquery1.Count != 0)
            {

                return View(pquery1);
            }

            else
            {
                var pquery2 = db.hotels.Where(elt => elt.nomhotel.Contains(q)).ToList();
                return View(pquery2);
            }

        }



    }
}
