using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBanPartial
        QLBANSACHEntities db = new QLBANSACHEntities();
        public PartialViewResult NhaXuatBanPartial()
        {

            return PartialView(db.NXBs.Take(5).OrderBy(n => n.TenNXB).ToList());
        }

        //Hiển thị sách theo nhà xuất bản
        public ViewResult SachTheoNXB(int MaNXB=0)
        {
            NXB nxb = db.NXBs.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Truy xuất các quyển sách theo NXB
            List<SACH> lstSach = db.SACHes.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có sách nào thuộc Nhà Xuất Bản này";
            }
            return View(lstSach);
        }
        //Hiển thị theo NXB
        public ViewResult DanhMucNXB()
        {
            return View(db.NXBs.ToList());
        }
    }
}