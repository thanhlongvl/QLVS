using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLVS.Models;
using PagedList;

namespace QLVS.Controllers
{
    public class DotPhatHanhController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: DotPhatHanh
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TenDaiLySortParm = sortOrder == "tendaily" ? "tendaily_desc" : "tendaily";
            ViewBag.MaLVSSortParm = sortOrder == "MaLVS" ? "MaLVS_desc" : "MaLVS";
            ViewBag.TenTinhSortParm = sortOrder == "TenTinh" ? "TenTinh_desc" : "TenTinh";
            ViewBag.NgayNhanSortParm = sortOrder == "NgayNhan" ? "NgayNhan_desc" : "NgayNhan";
            ViewBag.SLPHSortParm = sortOrder == "SLPH" ? "SLPH_desc" : "SLPH";
            ViewBag.SLBDSortParm = sortOrder == "SLBD" ? "SLBD_desc" : "SLBD";
            ViewBag.TienThanhToanSortParm = sortOrder == "TienThanhToan" ? "TienThanhToan_desc" : "TienThanhToan";
            var phathanh = from s in db.DotPhatHanhs where s.Flag == true select s;
            phathanh = phathanh.OrderByDescending(s => s.MaDaiLy);

            if (searchString != null)
                page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                phathanh = phathanh.Where(s => s.DaiLy.TenDaiLy.ToUpper().Contains(searchString.ToUpper()) || s.MaLoaiVeSo.ToUpper().Contains(searchString.ToUpper()));
                if (phathanh.Count() > 0)
                {
                    TempData["notice"] = "Have result";
                    TempData["dem"] = phathanh.Count();
                }
                else
                {
                    TempData["notice"] = "No result";
                }
            }
            switch (sortOrder)
            {
                case "tendaily":
                    phathanh = phathanh.OrderBy(s => s.DaiLy.TenDaiLy);
                    break;
                case "tendaily_desc":
                    phathanh = phathanh.OrderByDescending(s => s.DaiLy.TenDaiLy);
                    break;

                case "MaLVS":
                    phathanh = phathanh.OrderBy(s => s.LoaiVeso.MaLoaiVeSo);
                    break;
                case "MaLVS_desc":
                    phathanh = phathanh.OrderByDescending(s => s.LoaiVeso.MaLoaiVeSo);
                    break;

                case "TenTinh":
                    phathanh = phathanh.OrderBy(s => s.LoaiVeso.Tinh);
                    break;
                case "TenTinh_desc":
                    phathanh = phathanh.OrderByDescending(s => s.LoaiVeso.Tinh);
                    break;

                case "NgayNhan":
                    phathanh = phathanh.OrderBy(s => s.NgayNhan);
                    break;
                case "NgayNhan_desc":
                    phathanh = phathanh.OrderByDescending(s => s.NgayNhan);
                    break;

                case "SLPH":
                    phathanh = phathanh.OrderBy(s => s.SoLuong);
                    break;
                case "SLPH_desc":
                    phathanh = phathanh.OrderByDescending(s => s.SoLuong);
                    break;

                case "SLBD":
                    phathanh = phathanh.OrderBy(s => s.SLBanDuoc);
                    break;
                case "SLBD_desc":
                    phathanh = phathanh.OrderByDescending(s => s.SLBanDuoc);
                    break;

                case "TienThanhToan":
                    phathanh = phathanh.OrderBy(s => s.TienThanhToan);
                    break;
                case "TienThanhToan_desc":
                    phathanh = phathanh.OrderByDescending(s => s.TienThanhToan);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(phathanh.ToPagedList(pageNumber, pageSize));
        }

        // GET: DotPhatHanh/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotPhatHanh dotPhatHanh = db.DotPhatHanhs.Find(id);
            if (dotPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(dotPhatHanh);
        }

        // GET: DotPhatHanh/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh");
            return View();
        }

        // POST: DotPhatHanh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDaiLy,MaLoaiVeSo,NgayNhan,SoLuong,SLBanDuoc,TienThanhToan,Flag")] DotPhatHanh dotPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.DotPhatHanhs.Add(dotPhatHanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", dotPhatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", dotPhatHanh.MaLoaiVeSo);
            return View(dotPhatHanh);
        }

        // GET: DotPhatHanh/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotPhatHanh dotPhatHanh = db.DotPhatHanhs.Find(id);
            if (dotPhatHanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", dotPhatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", dotPhatHanh.MaLoaiVeSo);
            return View(dotPhatHanh);
        }

        // POST: DotPhatHanh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDaiLy,MaLoaiVeSo,NgayNhan,SoLuong,SLBanDuoc,TienThanhToan,Flag")] DotPhatHanh dotPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dotPhatHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", dotPhatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes, "MaLoaiVeSo", "Tinh", dotPhatHanh.MaLoaiVeSo);
            return View(dotPhatHanh);
        }

        // GET: DotPhatHanh/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DotPhatHanh dotPhatHanh = db.DotPhatHanhs.Find(id);
            if (dotPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(dotPhatHanh);
        }

        // POST: DotPhatHanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DotPhatHanh dotPhatHanh = db.DotPhatHanhs.Find(id);
            db.DotPhatHanhs.Remove(dotPhatHanh);
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
