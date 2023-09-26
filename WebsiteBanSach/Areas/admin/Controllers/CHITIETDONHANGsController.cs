using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.admin.Controllers
{
    public class CHITIETDONHANGsController : Controller
    {
        private QLBANSACHEntities db = new QLBANSACHEntities();

        // GET: admin/CHITIETDONHANGs
        public ActionResult Index()
        {
            var cHITIETDONHANGs = db.CHITIETDONHANGs.Include(c => c.DONHANG).Include(c => c.SACH);
            return View(cHITIETDONHANGs.ToList());
        }

        // GET: admin/CHITIETDONHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // GET: admin/CHITIETDONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DONHANGs, "MaDonHang", "MaDonHang");
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach");
            return View();
        }

        // POST: admin/CHITIETDONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaSach,SoLuong,DonGia")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETDONHANGs.Add(cHITIETDONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DONHANGs, "MaDonHang", "MaDonHang", cHITIETDONHANG.MaDonHang);
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", cHITIETDONHANG.MaSach);
            return View(cHITIETDONHANG);
        }

        // GET: admin/CHITIETDONHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonHang = new SelectList(db.DONHANGs, "MaDonHang", "MaDonHang", cHITIETDONHANG.MaDonHang);
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", cHITIETDONHANG.MaSach);
            return View(cHITIETDONHANG);
        }

        // POST: admin/CHITIETDONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaSach,SoLuong,DonGia")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETDONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonHang = new SelectList(db.DONHANGs, "MaDonHang", "MaDonHang", cHITIETDONHANG.MaDonHang);
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", cHITIETDONHANG.MaSach);
            return View(cHITIETDONHANG);
        }

        // GET: admin/CHITIETDONHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // POST: admin/CHITIETDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            db.CHITIETDONHANGs.Remove(cHITIETDONHANG);
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
