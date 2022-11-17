using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QLBANSACHEntities db = new QLBANSACHEntities();
        public ActionResult Index()
        {
            return View(db.SACHes.Where(n=>n.Moi==1).ToList());
        }

    }
}