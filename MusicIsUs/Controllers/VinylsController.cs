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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vinyls
        public ActionResult Index()
        {
            ViewBag.originCountry = db.Vinyls.Select(vinyl => vinyl.OriginCountry).Distinct();
            ViewBag.artistName = db.Vinyls.Select(vinyl => vinyl.ArtistName).Distinct();
            ViewBag.genere = db.Vinyls.Select(vinyl => vinyl.Genere).Distinct();
            return View(db.Vinyls.ToList());
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
            // session from HEVER
            /*vavinylcurrentUsevinyl= (Users)HttpContext.Session["user"];
            if (currentUsevinyl== null)
            {
                return RedirectToAction("Index", "Error", new { message = "You are not logged in" });
            }*/

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

        // POST: Vinyls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vinyls vinyls = db.Vinyls.Find(id);
            db.Vinyls.Remove(vinyls);
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
