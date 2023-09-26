using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QLBANSACHEntities db = new QLBANSACHEntities();
        public ActionResult Index(int? page)
        {
            // Tạo số sản phẩm trên trang
            int pageSize = 9;
            //Tạo số trang
            int pageNumber = (page ?? 1);
            return View(db.SACHes.ToList().OrderBy(n=>n.GiaBan).ToPagedList(pageNumber, pageSize ));
        }

    }
}