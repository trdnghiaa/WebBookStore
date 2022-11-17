using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities(); 
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if(ModelState.IsValid)
            {
            //chèn dữ liệu vào bảng khách hàng
            db.KHACHHANGs.Add(kh);
            //luu vào csdl
            db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap(KHACHHANG kh)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if(kh!=null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công !";
                Session["TaiKhoan"] = kh;
                return View();
            }
            ViewBag.ThongBao = "Tài khoản hoặc khẩu không đúng !";
            return View();
        }
    }
}