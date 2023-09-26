using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class ChuDeController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities();
        // GET: ChuDe
        public ActionResult ChuDePartial()
        {
            return PartialView(db.CHUDEs.Take(5).ToList());
        }
        //Sách theo chủ đề
        public ViewResult SachTheoChuDe(int MaChuDe)
        {
            CHUDE cd = db.CHUDEs.SingleOrDefault(n => n.MaChuDe == MaChuDe);
                if(cd==null)
                {
                Response.StatusCode = 404;
                return null;
                }
            List<SACH> lstSach = db.SACHes.Where(n => n.MaChuDe == MaChuDe).OrderBy(n=>n.GiaBan).ToList();
                if(lstSach.Count==0)
            {
                ViewBag.Sach = "Không có sách nào thuộc chủ đề này";
            }
            return View(lstSach);
        }
        //Hiển thị các chủ đề
        public ViewResult DanhMucChuDe()
        {
            return View(db.CHUDEs.ToList());
        }

    }
}