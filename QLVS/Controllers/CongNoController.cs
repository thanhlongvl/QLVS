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
    public class CongNoController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: CongNo
        public ActionResult Index()
        {
            var congNoes = db.CongNoes.Include(c => c.DaiLy);
            return View(congNoes.ToList());
        }

        // GET: CongNo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congNo = db.CongNoes.Find(id);
            if (congNo == null)
            {
                return HttpNotFound();
            }
            return View(congNo);
        }

        // GET: CongNo/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy");
            return View();
        }

        // POST: CongNo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaDaiLy,Ngay,SoTienNo,Flag")] CongNo congNo)
        {
            if (ModelState.IsValid)
            {
                db.CongNoes.Add(congNo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", congNo.MaDaiLy);
            return View(congNo);
        }

        // GET: CongNo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congNo = db.CongNoes.Find(id);
            if (congNo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", congNo.MaDaiLy);
            return View(congNo);
        }

        // POST: CongNo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaDaiLy,Ngay,SoTienNo,Flag")] CongNo congNo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congNo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", congNo.MaDaiLy);
            return View(congNo);
        }

        // GET: CongNo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congNo = db.CongNoes.Find(id);
            if (congNo == null)
            {
                return HttpNotFound();
            }
            return View(congNo);
        }

        // POST: CongNo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CongNo congNo = db.CongNoes.Find(id);
            db.CongNoes.Remove(congNo);
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
