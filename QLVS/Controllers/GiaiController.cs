using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVS.Models;

namespace QLVS.Controllers
{
    public class GiaiController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: Giai
        public ActionResult Index()
        {
            return View(db.Giais.ToList());
        }

        // GET: Giai/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giais.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // GET: Giai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Giai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiai,TenGiai,SoTienNhan,Flag")] Giai giai)
        {
            if (ModelState.IsValid)
            {
                db.Giais.Add(giai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giai);
        }

        // GET: Giai/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giais.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // POST: Giai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiai,TenGiai,SoTienNhan,Flag")] Giai giai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giai);
        }

        // GET: Giai/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giais.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // POST: Giai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Giai giai = db.Giais.Find(id);
            db.Giais.Remove(giai);
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
