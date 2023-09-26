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
    public class THAMGIAsController : Controller
    {
        private QLBANSACHEntities db = new QLBANSACHEntities();

        // GET: admin/THAMGIAs
        public ActionResult Index()
        {
            var tHAMGIAs = db.THAMGIAs.Include(t => t.SACH).Include(t => t.TACGIA);
            return View(tHAMGIAs.ToList());
        }

        // GET: admin/THAMGIAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIA tHAMGIA = db.THAMGIAs.Find(id);
            if (tHAMGIA == null)
            {
                return HttpNotFound();
            }
            return View(tHAMGIA);
        }

        // GET: admin/THAMGIAs/Create
        public ActionResult Create()
        {
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach");
            ViewBag.MaTacGia = new SelectList(db.TACGIAs, "MaTacGia", "TenTacGia");
            return View();
        }

        // POST: admin/THAMGIAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,MaTacGia,VaiTro")] THAMGIA tHAMGIA)
        {
            if (ModelState.IsValid)
            {
                db.THAMGIAs.Add(tHAMGIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", tHAMGIA.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TACGIAs, "MaTacGia", "TenTacGia", tHAMGIA.MaTacGia);
            return View(tHAMGIA);
        }

        // GET: admin/THAMGIAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIA tHAMGIA = db.THAMGIAs.Find(id);
            if (tHAMGIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", tHAMGIA.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TACGIAs, "MaTacGia", "TenTacGia", tHAMGIA.MaTacGia);
            return View(tHAMGIA);
        }

        // POST: admin/THAMGIAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,MaTacGia,VaiTro")] THAMGIA tHAMGIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHAMGIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSach = new SelectList(db.SACHes, "MaSach", "TenSach", tHAMGIA.MaSach);
            ViewBag.MaTacGia = new SelectList(db.TACGIAs, "MaTacGia", "TenTacGia", tHAMGIA.MaTacGia);
            return View(tHAMGIA);
        }

        // GET: admin/THAMGIAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMGIA tHAMGIA = db.THAMGIAs.Find(id);
            if (tHAMGIA == null)
            {
                return HttpNotFound();
            }
            return View(tHAMGIA);
        }

        // POST: admin/THAMGIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THAMGIA tHAMGIA = db.THAMGIAs.Find(id);
            db.THAMGIAs.Remove(tHAMGIA);
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
