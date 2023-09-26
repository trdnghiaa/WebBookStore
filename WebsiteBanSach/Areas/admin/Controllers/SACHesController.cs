using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Areas.admin.Controllers
{
    public class SACHesController : Controller
    {
        private QLBANSACHEntities db = new QLBANSACHEntities();

        // GET: admin/SACHes
        public ActionResult Index(int? page)
        {
            // Tạo số sản phẩm trên trang
            int pageSize = 9;
            //Tạo số trang
            int pageNumber = (page ?? 1);
            return View(db.SACHes.OrderBy(n=>n.MaSach).ToPagedList(pageNumber, pageSize));
        }

        // GET: admin/SACHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // GET: admin/SACHes/Create
        public ActionResult Create()
        {
            ViewBag.MaChuDe = new SelectList(db.CHUDEs, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB");
            return View();
        }

        // POST: admin/SACHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,AnhBia,NgayCapNhat,SoLuongTon,MaNXB,MaChuDe,Moi")] SACH sACH , HttpPostedFileBase AnhBia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (AnhBia.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(AnhBia.FileName);
                        string _path = Path.Combine(Server.MapPath("~/ImagesSP"), _FileName);
                        AnhBia.SaveAs(_path);
                        sACH.AnhBia = _FileName;
                    }
                    db.SACHes.Add(sACH);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "Không thành công";
                }
            }

            ViewBag.MaChuDe = new SelectList(db.CHUDEs, "MaChuDe", "TenChuDe", sACH.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: admin/SACHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuDe = new SelectList(db.CHUDEs, "MaChuDe", "TenChuDe", sACH.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // POST: admin/SACHes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,AnhBia,NgayCapNhat,SoLuongTon,MaNXB,MaChuDe,Moi")] SACH sACH, HttpPostedFileBase AnhBia, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (AnhBia != null)
                    {
                        string _FileName = Path.GetFileName(AnhBia.FileName);
                        string _path = Path.Combine(Server.MapPath("~/ImagesSP"), _FileName);
                        AnhBia.SaveAs(_path);
                        sACH.AnhBia = _FileName;
                        // get Path of old image for deleting it
                        _path = Path.Combine(Server.MapPath("~/ImagesSP"), form["oldimage"]);
                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        sACH.AnhBia = form["oldimage"];
                    db.Entry(sACH).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }
            ViewBag.MaChuDe = new SelectList(db.CHUDEs, "MaChuDe", "TenChuDe", sACH.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: admin/SACHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // POST: admin/SACHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH sACH = db.SACHes.Find(id);
            db.SACHes.Remove(sACH);
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
