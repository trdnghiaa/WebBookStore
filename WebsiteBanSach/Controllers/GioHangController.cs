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

        #region giỏ hàng
        //Lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì tiến hành khởi tạo list giỏ hafg (session gio hang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();

            //Kiểm tra sách này đã tồn tại trong session["giohang"] chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);

            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                // ADD SẢN PHẨM MỚI THÊM VÀO LIST
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }   
        }

        // CẬP NHẬT GIỎ HÀNG
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //kiểm tra masp
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSP);

            //nếu get sai mã sp thì trả về trang 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            
            //kiểm tra sp có tồn tịa trong session["giohang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            
            //NẾU TỒN TỊA THÌ CHO SỬA SỐ LƯỢNG
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        // XÓA GIỎ HÀNG
        public ActionResult XoaGioHang(int iMaSP)
        {
            //kiểm tra masp
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSP);

            //nếu get sai mã sp thì trả về trang 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();

            //kiểm tra sp có tồn tịa trong session["giohang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);

            //NẾU TỒN TỊA THÌ CHO SỬA SỐ LƯỢNG
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }

        //XÂY DỰNG TRANG GIỎ HÀNG
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        // TÍNH TỔNG SỐ LƯỢNG & TỔNG TIỀN
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
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }

        //Tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        // XÂY DỰNG VIEW CHO NG DÙNG EDIT GIỎ HÀNG
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion

        #region đặt hàng
        //XÂY DỰNG CHỨC NĂNG ĐẶT HÀNG
        [HttpPost]
        public ActionResult DatHang()
        {
            // Kiểm tra đăng nhập
            if(Session["TaiKhoan"] ==null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap","NguoiDung");
            }

            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            // Thêm đơn hàng
            DONHANG ddh = new DONHANG();
            KHACHHANG kh = (KHACHHANG) Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DONHANGs.Add(ddh);
            db.SaveChanges();

            // Thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                CHITIETDONHANG ctDH = new CHITIETDONHANG();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSach = item.iMaSach;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.DonGia = (int)item.dDonGia;
                db.CHITIETDONHANGs.Add(ctDH);
            }
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }
        #endregion
    }
}