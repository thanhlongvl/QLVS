using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVS.Models;
using QLVS.Models;
using PagedList;

namespace QLVS.Controllers
{
    public class LoaiVesoController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: LoaiVeso
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaLoaiVeSoSortParm = sortOrder == "maLVS" ? "maLVS_desc" : "maLVS";
            ViewBag.TinhSortParm = sortOrder == "tinh" ? "tinh_desc" : "tinh";
            var LVS = from s in db.LoaiVesoes where s.Flag == true select s;
            LVS = LVS.OrderByDescending(s => s.MaLoaiVeSo);

            if (searchString != null)
                page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                LVS = LVS.Where(s => s.Tinh.ToUpper().Contains(searchString.ToUpper()) || s.MaLoaiVeSo.ToUpper().Contains(searchString.ToUpper()));
                if (LVS.Count() > 0)
                {
                    TempData["notice"] = "Have result";
                    TempData["dem"] = LVS.Count();
                }
                else
                {
                    TempData["notice"] = "No result";
                }
            }
            switch (sortOrder)
            {
                case "maLVS":
                    LVS = LVS.OrderBy(s => s.MaLoaiVeSo);
                    break;
                case "maLVS_desc":
                    LVS = LVS.OrderByDescending(s => s.MaLoaiVeSo);
                    break;
                case "tinh":
                    LVS = LVS.OrderBy(s => s.Tinh);
                    break;
                case "tendaily_desc":
                    LVS = LVS.OrderByDescending(s => s.Tinh);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(LVS.ToPagedList(pageNumber, pageSize));
        }


        // GET: LoaiVeso/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVesoes.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // GET: LoaiVeso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiVeso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiVeSo,Tinh,Flag")] LoaiVeso loaiVeso)
        {
            if (ModelState.IsValid)
            {

                LoaiVeso LVS = db.LoaiVesoes.Find(loaiVeso.MaLoaiVeSo);

                if (LVS == null)
                {
                    loaiVeso.Flag = true;
                    db.LoaiVesoes.Add(loaiVeso);
                    db.SaveChanges();
                    TempData["notice"] = "Successfully create";
                    TempData["maloaiveso"] = loaiVeso.MaLoaiVeSo;
                    TempData["tinh"] = loaiVeso.Tinh;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["notice"] = "Failed create";
                    TempData["maloaiveso"] = loaiVeso.MaLoaiVeSo;

                }

            }

            return View(loaiVeso);
        }

        // GET: LoaiVeso/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVesoes.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // POST: LoaiVeso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiVeSo,Tinh,Flag")] LoaiVeso loaiVeso)
        {
            if (ModelState.IsValid)
            {

                loaiVeso.Flag = true;
                db.Entry(loaiVeso).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Successfully edit";
                TempData["maloaiveso"] = loaiVeso.MaLoaiVeSo;
                return RedirectToAction("Index");

            }
            return View(loaiVeso);
        }

        // GET: LoaiVeso/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVesoes.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // POST: LoaiVeso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            LoaiVeso loaiVeso = db.LoaiVesoes.Find(id);
            int count = db.LoaiVesoes.Where(p => p.MaLoaiVeSo == id).Count();
            if (count > 0) loaiVeso.Flag = false;
            else db.LoaiVesoes.Remove(loaiVeso);
            db.SaveChanges();
            TempData["notice"] = "Successfully delete";
            TempData["maloaiveso"] = loaiVeso.MaLoaiVeSo;
            TempData["tinh"] = loaiVeso.Tinh;
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
