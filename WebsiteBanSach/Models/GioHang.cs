using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebsiteBanSach.Models
{
    public class GioHang
    {
        private int iMaSP;
        QLBANSACHEntities db = new QLBANSACHEntities();
        /*public int IMaSP { get => iMaSP; set => iMaSP = value; }*/
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            SACH sach = db.SACHes.SingleOrDefault(n => n.MaSach == iMaSach);
            sTenSach = sach.TenSach;
            sAnhBia = sach.AnhBia;
            dDonGia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }


    }
}