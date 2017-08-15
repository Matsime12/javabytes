using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using javabytes_prototype.Models;

namespace javabytes_prototype.Controllers
{
    public class UsersController : Controller
    {
        private javabytesDBEntities db = new javabytesDBEntities();

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            User user = new User();
            if (db.Users.Where(x => x.username == username).Count() == 1)
            {
                user = db.Users.Where(x => x.username == username).First();
            }
            else
            {
                return Json(false);
            }

            if (user.password == password)
            {
                Session["User"] = user.user_id;
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public ActionResult LoginErrorView()
        {
            return View();
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.User_Type);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,name,surname,user_type_id,username,password,preferred_delivery_location")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "name", user.user_type_id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "name", user.user_type_id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,name,surname,user_type_id,username,password,preferred_delivery_location")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "name", user.user_type_id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
