using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicIsUs.Models;
using System.Collections;

namespace MusicIsUs.Controllers
{
    public class UsersController : Controller
    {
        private MusicIsUsContext db = new MusicIsUsContext();

        // GET: Users
        public ActionResult Index()
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            bool isAdmin = user != null ? user.IsAdmin : false;
            if (isAdmin)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,IsAdmin")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,IsAdmin")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            var userInDataBase = db.Users.Where(u => u.UserName.Equals(login, System.StringComparison.Ordinal) &&
                                                 u.Password.Equals(password, System.StringComparison.Ordinal)).SingleOrDefault();

            if (userInDataBase != null)
            {
                System.Web.HttpContext.Current.Session["user"] = userInDataBase;
                return RedirectToAction("Index", "Home", new { id = userInDataBase.Id });
            }

            ViewBag.ErrMsg = "User name or password are incorrect.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string login, string password, string passwordRep)
        {
            if (password == passwordRep)
            {
                var userInDataBase = db.Users.Where(u => u.UserName.Equals(login, System.StringComparison.Ordinal) &&
                                                     u.Password.Equals(password, System.StringComparison.Ordinal)).SingleOrDefault();
                if (userInDataBase == null)
                {
                    try
                    {
                        var user = db.Users.Add(new Users()
                        {
                            IsAdmin = false,
                            Password = password,
                            UserName = login
                        });
                        System.Web.HttpContext.Current.Session["user"] = user;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home", new { id = user.Id });
                    }
                    catch (Exception)
                    {
                        ViewBag.ErrMsg = "Internal Error. something went wrong.";
                        return View();
                    }
                }

                ViewBag.ErrMsg = "User with the user name " + login + " already exists";
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Session["user"] = null;
            return RedirectToAction("Index", "Home");
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
