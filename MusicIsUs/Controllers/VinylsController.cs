using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicIsUs.Models;

namespace MusicIsUs.Controllers
{
    public class VinylsController : Controller
    {
        private MusicIsUsContext db = new MusicIsUsContext();

        // GET: Vinyls
        public ActionResult Index()
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null)
            {
                db.Configuration.LazyLoadingEnabled = false;
                ViewBag.originCountry = db.Vinyls.Select(vinyl => vinyl.OriginCountry).Distinct();
                ViewBag.artistName = db.Vinyls.Select(vinyl => vinyl.ArtistName).Distinct();
                ViewBag.genere = db.Vinyls.Select(vinyl => vinyl.Genere).Distinct();
                var userPopulated = db.Users.Include("LikedVinyls").SingleOrDefault(i => i.UserName == user.UserName);
                ViewBag.user = userPopulated;
                ViewBag.Liked = userPopulated.LikedVinyls.ToList();
                return View(db.Vinyls.ToList());
            }
            return RedirectToAction("NotFound", "Home");
        }

        // GET: Vinyls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vinyls vinyls = db.Vinyls.Find(id);
            if (vinyls == null)
            {
                return HttpNotFound();
            }
            return View(vinyls);
        }

        public ActionResult Search(string artistName = null, string genere = null, string originCountry = null)
        {
            ViewBag.originCountry = db.Vinyls.Select(vinyl => vinyl.OriginCountry).Distinct();
            ViewBag.artistName = db.Vinyls.Select(vinyl => vinyl.ArtistName).Distinct();
            ViewBag.genere = db.Vinyls.Select(vinyl => vinyl.Genere).Distinct();

            var returnDataQurey = db.Vinyls.Select(vinyl => vinyl);

            if (!string.IsNullOrEmpty(artistName))
            {
                returnDataQurey = returnDataQurey.Where(vinyl => vinyl.ArtistName.Equals(artistName));
            }
            if (!string.IsNullOrEmpty(genere))
            {
                returnDataQurey = returnDataQurey.Where(vinyl => vinyl.Genere.Equals(genere));
            }

            if (!string.IsNullOrEmpty(originCountry))
            {
                returnDataQurey = returnDataQurey.Where(vinyl => vinyl.OriginCountry.Equals(originCountry));
            }

            return View("Index", returnDataQurey.ToList());
        }

        // GET: Vinyls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vinyls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ArtistName,PublishDate,Genere,OriginCountry")] Vinyls vinyls)
        {
            if (ModelState.IsValid)
            {
                db.Vinyls.Add(vinyls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vinyls);
        }

        // GET: Vinyls/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vinyls vinyls = db.Vinyls.Find(id);
                if (vinyls == null)
                {
                    return HttpNotFound();
                }
                return View(vinyls);
            }
            return HttpNotFound();
        }

        // POST: Vinyls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ArtistName,PublishDate,Genere,OriginCountry")] Vinyls vinyls)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vinyls).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vinyls);
        }

        // GET: Vinyls/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vinyls vinyls = db.Vinyls.Find(id);
                if (vinyls == null)
                {
                    return HttpNotFound();
                }
                return View(vinyls);
            }
            return HttpNotFound();
        }

        // POST: Vinyls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                Vinyls vinyls = db.Vinyls.Find(id);
                db.Vinyls.Remove(vinyls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Like")]
        public JsonResult Like(int id, bool like)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            var userPopulated = db.Users.Include("LikedVinyls").SingleOrDefault(i => i.UserName == user.UserName);
            if (user != null)
            {
                Vinyls vinyls = db.Vinyls.Find(id);
                if (!like)
                {
                    userPopulated.LikedVinyls.Add(vinyls);
                    db.SaveChanges();
                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    userPopulated.LikedVinyls.Remove(vinyls);
                    db.SaveChanges();
                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { message = "login please" }, JsonRequestBehavior.AllowGet);
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
