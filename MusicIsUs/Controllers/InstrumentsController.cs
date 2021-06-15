using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicIsUs.Models;
using MusicIsUs.Models;

namespace MusicIsUs.Controllers
{
    public class InstrumentsController : Controller
    {
        private MusicIsUsContext db = new MusicIsUsContext();

        // GET: Instruments
        public ActionResult Index()
        {
            return View(db.Instruments.ToList());
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

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,MadeInCountry,Brand")] Instruments instruments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instruments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instruments);
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instruments instruments = db.Instruments.Find(id);
            db.Instruments.Remove(instruments);
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
