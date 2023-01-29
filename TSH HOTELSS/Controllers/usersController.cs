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
    public class usersController : Controller
    {
        private hmsEntities db = new hmsEntities();

        // GET: users
        public ActionResult Index()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            var users = db.users.Include(u => u.uroles);
            return View(users.ToList());
        }

        // GET: users/Details/5
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
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iduser,username,email,pass,roleid")] users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename", users.roleid);
            return View(users);
        }

        // GET: users/Edit/5
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
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename", users.roleid);
            return View(users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iduser,username,email,pass,roleid")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename", users.roleid);
            return View(users);
        }

        // GET: users/Delete/5
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
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
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
        //login 
        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");

            }


            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(users user)
        {


            if (ModelState.IsValid)
            {
                var LoggedUser = db.users.FirstOrDefault(u => u.username == user.username && u.pass == user.pass);

                if (LoggedUser != null)
                {
                    //Store user information in session
                    Session["Username"] = user.username;
                    Session["IsAuthenticated"] = true;
                    return RedirectToAction("Index", "Home");
                }
            }

            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(user);
        }

        //Registration

        public ActionResult Register()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "iduser,username,email,pass,roleid")] users users)
        {    if (Request.IsAuthenticated)
            {
                return View("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roleid = new SelectList(db.uroles, "roleid", "rolename", users.roleid);
            return View(users);
        }
        
        
         

        
        }
    }

