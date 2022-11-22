using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: /SachMoiPartial/

        QLBANSACHEntities db = new QLBANSACHEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult UploadSachMoi()
        {
            var lstSachMoi = db.SACHes.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        //Xem chi tiet
        public ViewResult XemChiTiet(int MaSach =0)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach==null)
            {
                //Tra ve Loi~
                Response.StatusCode = 404;
                return null;
            }
            /*CHUDE cd = db.CHUDEs.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.TenCD = cd.TenChuDe;*/
            ViewBag.TenChuDe = db.CHUDEs.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.NhaXuatBan = db.NXBs.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            return View(sach);
        }

    }
}