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
    public class DaiLyController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: DaiLy
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaDaiLySortParm = sortOrder == "madaily" ? "madaily_desc" : "madaily";
            ViewBag.TenDaiLySortParm = sortOrder == "tendaily" ? "tendaily_desc" : "tendaily";
            ViewBag.DiaChiSortParm = sortOrder == "diachi" ? "diachi_desc" : "diachi";
            var daily = from s in db.DaiLies where s.Flag == true select s;
            daily = daily.OrderByDescending(s => s.MaDaiLy);

            if (searchString != null)
                page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                daily = daily.Where(s => s.TenDaiLy.ToUpper().Contains(searchString.ToUpper()));
                if (daily.Count() > 0)
                {
                    TempData["notice"] = "Have result";
                    TempData["dem"] = daily.Count();
                }
                else
                {
                    TempData["notice"] = "No result";
                }
            }
            switch (sortOrder)
            {
                case "madaily":
                    daily = daily.OrderBy(s => s.MaDaiLy);
                    break;
                case "madaily_desc":
                    daily = daily.OrderByDescending(s => s.MaDaiLy);
                    break;
                case "tendaily":
                    daily = daily.OrderBy(s => s.TenDaiLy);
                    break;
                case "tendaily_desc":
                    daily = daily.OrderByDescending(s => s.TenDaiLy);
                    break;
                case "diachi":
                    daily = daily.OrderBy(s => s.DiaChi);
                    break;
                case "diachi_desc":
                    daily = daily.OrderByDescending(s => s.DiaChi);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(daily.ToPagedList(pageNumber, pageSize));
        }


        // GET: DaiLy/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // GET: DaiLy/Create

        public string getMaDaiLy()
        {
            var countRow = db.DaiLies.Count();
            int getCount = countRow + 1;
            string newMaDL = "DL";
            if (getCount < 10) newMaDL += "00" + getCount.ToString();
            else if (getCount < 100) newMaDL += "0" + getCount.ToString();
            return newMaDL;
        }

        public ActionResult Create()
        {
            DaiLy dl = new DaiLy();
            dl.MaDaiLy = getMaDaiLy();
            return View(dl);
        }

        // POST: DaiLy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDaiLy,TenDaiLy,DiaChi,SDT,Flag")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                daiLy.Flag = true;
                db.DaiLies.Add(daiLy);
                db.SaveChanges();
                TempData["notice"] = "Successfully create";
                TempData["tendaily"] = daiLy.TenDaiLy;
                return RedirectToAction("Index");
            }

            return View(daiLy);
        }

        // GET: DaiLy/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // POST: DaiLy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDaiLy,TenDaiLy,DiaChi,SDT,Flag")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                daiLy.Flag = true;
                db.Entry(daiLy).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Successfully edit";
                TempData["tendaily"] = daiLy.TenDaiLy;
                TempData["madaily"] = daiLy.MaDaiLy;
                return RedirectToAction("Index");
            }
            return View(daiLy);
        }

        // GET: DaiLy/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daiLy = db.DaiLies.Find(id);
            if (daiLy == null)
            {
                return HttpNotFound();
            }
            return View(daiLy);
        }

        // POST: DaiLy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            DaiLy daiLy = db.DaiLies.Find(id);
            int count = db.DaiLies.Where(p => p.MaDaiLy == id).Count();
            if (count > 0) daiLy.Flag = false;
            else db.DaiLies.Remove(daiLy);
            db.SaveChanges();
            TempData["notice"] = "Successfully delete";
            TempData["tendaily"] = daiLy.TenDaiLy;
            TempData["madaily"] = daiLy.MaDaiLy;
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
