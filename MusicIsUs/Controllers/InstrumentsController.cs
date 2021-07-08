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
    public class InstrumentsController : Controller
    {
        private MusicIsUsContext db = new MusicIsUsContext();

        // GET: Instruments
        public ActionResult Index()
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null)
            {
                ViewBag.name = db.Instruments.Select(instrument => instrument.Name).Distinct();
                ViewBag.type = db.Instruments.Select(instrument => instrument.Type).Distinct();
                ViewBag.madeInCountry = db.Instruments.Select(instrument => instrument.MadeInCountry).Distinct();
                ViewBag.brand = db.Instruments.Select(instrument => instrument.Brand).Distinct();
                return View(db.Instruments.ToList());
            }
            return RedirectToAction("NotFound", "Home");
        }

        // GET: Instruments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instruments instruments = db.Instruments.Find(id);
            if (instruments == null)
            {
                return HttpNotFound();
            }
            return View(instruments);
        }

        public ActionResult Search(string name = null, string type = null, string madeInCountry = null, string brand = null)
        {
            ViewBag.name = db.Instruments.Select(instruments => instruments.Name).Distinct();
            ViewBag.type = db.Instruments.Select(instruments => instruments.Type).Distinct();
            ViewBag.madeInCountry = db.Instruments.Select(instruments => instruments.MadeInCountry).Distinct();
            ViewBag.brand = db.Instruments.Select(instruments => instruments.Brand).Distinct();

            var returnDataQurey = db.Instruments.Select(instruments => instruments);

            if (!string.IsNullOrEmpty(name))
            {
                returnDataQurey = returnDataQurey.Where(instruments => instruments.Name.Equals(name));
            }
            if (!string.IsNullOrEmpty(type))
            {
                returnDataQurey = returnDataQurey.Where(instruments => instruments.Type.Equals(type));
            }

            if (!string.IsNullOrEmpty(madeInCountry))
            {
                returnDataQurey = returnDataQurey.Where(instruments => instruments.MadeInCountry.Equals(madeInCountry));
            }

            if (!string.IsNullOrEmpty(brand))
            {
                returnDataQurey = returnDataQurey.Where(instruments => instruments.Brand.Equals(brand));
            }

            return View("Index", returnDataQurey.ToList());
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,MadeInCountry,Brand")] Instruments instruments)
        {
            if (ModelState.IsValid)
            {
                db.Instruments.Add(instruments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instruments);
        }

        // GET: Instruments/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Instruments instruments = db.Instruments.Find(id);
                if (instruments == null)
                {
                    return HttpNotFound();
                }
                return View(instruments);
            }
            return HttpNotFound();
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,MadeInCountry,Brand")] Instruments instruments)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(instruments).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(instruments);
            }
            return HttpNotFound();
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Instruments instruments = db.Instruments.Find(id);
                if (instruments == null)
                {
                    return HttpNotFound();
                }
                return View(instruments);
            }
            return HttpNotFound();
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            if (user != null && user.IsAdmin)
            {
                Instruments instruments = db.Instruments.Find(id);
                db.Instruments.Remove(instruments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Like")]
        public JsonResult Like(int id, bool like)
        {
            var user = ((Users)System.Web.HttpContext.Current.Session["user"]);
            var userPopulated = db.Users.Include("LikedInstruments").SingleOrDefault(i => i.UserName == user.UserName);
            if (user != null)
            {
                Instruments instrument = db.Instruments.Find(id);
                if (!like)
                {
                    userPopulated.LikedInstruments.Add(instrument);
                    db.SaveChanges();
                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    userPopulated.LikedInstruments.Remove(instrument);
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
