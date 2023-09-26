using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanSach.Areas.admin.Controllers
{
    public class homeAdminController : Controller
    {
        // GET: admin/homeAdmin
        QLBANSACHEntities db = new QLBANSACHEntities(); 
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.SACHes.ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize));
        }
    }
}