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
    public class KetQuaSoXoController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: KetQuaSoXo
        public ActionResult Index()
        {
            var ketQuaSoXoes = db.KetQuaSoXoes.Include(k => k.Giai).Include(k => k.LoaiVeso);
            return View(ketQuaSoXoes.ToList());
        }

        // GET: KetQuaSoXo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXoes.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXo/Create
        public ActionResult Create()
        {
            ViewBag.MaGiai = new SelectList(db.Giais, "MaGiai", "TenGiai");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh");
            return View();
        }

        // POST: KetQuaSoXo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaLoaiVeSo,MaGiai,NgaySo,SoTrung,Flag")] KetQuaSoXo ketQuaSoXo)
        {
            if (ModelState.IsValid)
            {
                db.KetQuaSoXoes.Add(ketQuaSoXo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGiai = new SelectList(db.Giais, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXoes.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGiai = new SelectList(db.Giais, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // POST: KetQuaSoXo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaLoaiVeSo,MaGiai,NgaySo,SoTrung,Flag")] KetQuaSoXo ketQuaSoXo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ketQuaSoXo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGiai = new SelectList(db.Giais, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXoes.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaSoXo);
        }

        // POST: KetQuaSoXo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXoes.Find(id);
            db.KetQuaSoXoes.Remove(ketQuaSoXo);
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
