using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        QLBANSACHEntities db = new QLBANSACHEntities();
        private double dTongTien;

        //Lay gio hang`
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                // Neu gio hang` chua ton` tai thi tien hanh khoi tao lstGioHang (Session GioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }



        //Them gio hang`
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //Kiem tra sach da ton` tai trong Session[giohang] chua
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }


        //cap nhat gio hang`
        public ActionResult CapNhatGioHang(int iMaSP,FormCollection f)
        {
            //kiem tra masp
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSP);
            //neu get sai masp thi` return 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lay giohang` tu session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiem tra sp co ton` tai trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //Neu ton` tai thi cho sua so' luong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse( f["txtSoLuong"].ToString());
            }
            return View("GioHang");
        }


        public ActionResult XoaGioHang(int iMaSP)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            return RedirectToAction("GioHang");
        }


        //Xay dung trang gio hang`
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }


        //Xay dung tinh tong so luong & tong tien`
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double TongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }


    }
}