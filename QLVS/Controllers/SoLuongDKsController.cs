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
    public class SoLuongDKsController : Controller
    {
        private QLVSContext db = new QLVSContext();

        // GET: SoLuongDKs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewBag.TenDaiLySortParm = sortOrder == "tendaily" ? "tendaily_desc" : "madaily";
            ViewBag.NgayDKSortParm = sortOrder == "ngaydk" ? "ngaydk_desc" : "ngaydk";
            ViewBag.SldkSortParm = sortOrder == "sldk" ? "sldk_desc" : "sldk";
            var sldk = from s in db.SoLuongDKs where s.Flag == true select s;
            sldk = sldk.OrderByDescending(s => s.ID);

            if (searchString != null)
                page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                sldk = sldk.Where(s => s.DaiLy.TenDaiLy.ToUpper().Contains(searchString.ToUpper()));
                if (sldk.Count() > 0)
                {
                    TempData["notice"] = "Have result";
                    TempData["dem"] = sldk.Count();
                }
                else
                {
                    TempData["notice"] = "No result";
                }
            }
            switch (sortOrder)
            {
                case "ID":
                    sldk = sldk.OrderBy(s => s.ID);
                    break;
                case "ID_desc":
                    sldk = sldk.OrderByDescending(s => s.ID);
                    break;
                case "tendaily":
                    sldk = sldk.OrderBy(s => s.MaDaiLy);
                    break;
                case "tendaily_desc":
                    sldk = sldk.OrderByDescending(s => s.MaDaiLy);
                    break;
                case "ngaydk":
                    sldk = sldk.OrderBy(s => s.NgayDK);
                    break;
                case "ngaydk_desc":
                    sldk = sldk.OrderByDescending(s => s.NgayDK);
                    break;
                case "sldk":
                    sldk = sldk.OrderBy(s => s.SoLuongDK1);
                    break;
                case "sldk_desc":
                    sldk = sldk.OrderByDescending(s => s.SoLuongDK1);
                    break;

            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(sldk.ToPagedList(pageNumber, pageSize));
        }

        // GET: SoLuongDKs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK soLuongDK = db.SoLuongDKs.Find(id);
            if (soLuongDK == null)
            {
                return HttpNotFound();
            }
            return View(soLuongDK);
        }

        public string getID()
        {
            var countRow = db.SoLuongDKs.Count();
            int getCount = countRow + 1;
            string newMaDK = "DK";
            if (getCount < 10) newMaDK += "00" + getCount.ToString();
            else if (getCount < 100) newMaDK += "0" + getCount.ToString();
            return newMaDK;
        }



        // GET: SoLuongDKs/Create
        public ActionResult Create()
        {

            SoLuongDK sldk = new SoLuongDK();
            sldk.ID = getID();
            sldk.NgayDK = DateTime.Now;
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy");
            return View(sldk);

        }

        // POST: SoLuongDKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaDaiLy,NgayDK,SoLuongDK1,Flag")] SoLuongDK soLuongDK)
        {
            if (ModelState.IsValid)
            {
                soLuongDK.Flag= true;
                db.SoLuongDKs.Add(soLuongDK);
                db.SaveChanges();
                TempData["notice"] = "Successfully create";
                TempData["masl"] = soLuongDK.ID;

                DaiLy givename = db.DaiLies.Where(s => s.MaDaiLy == soLuongDK.MaDaiLy).FirstOrDefault();
                TempData["daily"] = givename.TenDaiLy;
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", soLuongDK.MaDaiLy);
            return View(soLuongDK);
        }

        // GET: SoLuongDKs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK soLuongDK = db.SoLuongDKs.Find(id);
            if (soLuongDK == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", soLuongDK.MaDaiLy);
            return View(soLuongDK);
        }

        // POST: SoLuongDKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaDaiLy,NgayDK,SoLuongDK1,Flag")] SoLuongDK soLuongDK)
        {
            if (ModelState.IsValid)
            {

                soLuongDK.Flag = true;
                db.Entry(soLuongDK).State = EntityState.Modified;
                db.SaveChanges();
                TempData["notice"] = "Successfully edit";
                TempData["masl"] = soLuongDK.MaDaiLy;
                DaiLy givename = db.DaiLies.Where(s => s.MaDaiLy == soLuongDK.MaDaiLy).FirstOrDefault();
                TempData["daily"] = givename.TenDaiLy;
                return RedirectToAction("Index");
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies, "MaDaiLy", "TenDaiLy", soLuongDK.MaDaiLy);
            return View(soLuongDK);
        }

        // GET: SoLuongDKs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK soLuongDK = db.SoLuongDKs.Find(id);
            if (soLuongDK == null)
            {
                return HttpNotFound();
            }
            return View(soLuongDK);
        }

        // POST: SoLuongDKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SoLuongDK soLuongDK = db.SoLuongDKs.Find(id);
            string MaDL = soLuongDK.MaDaiLy;
            int count = db.SoLuongDKs.Where(p => p.ID == id).Count();
            if (count > 0) soLuongDK.Flag = false;
            else db.SoLuongDKs.Remove(soLuongDK);
            db.SaveChanges();
            TempData["notice"] = "Successfully delete";
            TempData["masl"] = soLuongDK.ID;
            DaiLy givename = db.DaiLies.Where(s => s.MaDaiLy == MaDL).FirstOrDefault();
            TempData["daily"] = givename.TenDaiLy;
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
