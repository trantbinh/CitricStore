using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitricStore.Models
{
    public class OrderItem
    {
        CitricStoreEntities2 db = new CitricStoreEntities2();

        public int MaOr { get; set; }
        public DateTime NgayOr { get; set; }
        public int? MaKhachHang { get; set; }
        public string TenOr { get; set; }
        public string SDTOr { get; set; }
        public string EmailOr { get; set; }
        public int? BankID { get; set; }
        public string MaTK { get; set; }
        public string TenTK { get; set; }
        public decimal? TongTien { get; set; }
        public OrderItem(int MaOrd)
        {
            this.MaOr = MaOrd;

            var or = db.ORDER_INFO.Single(s => s.MaOrder == MaOr);

            this.NgayOr = (DateTime)or.NgayOrder;
            this.MaKhachHang = or.MaKH;
            this.TenOr = or.TenOrder;
            this.SDTOr = or.SDTOrder;
            this.EmailOr = or.EmailOrder;
            this.BankID = or.MaNganHang;
            this.MaTK = or.MaTaiKhoan;
            this.TenTK = or.TenTaiKhoan;
            this.TongTien = or.TongTien;


        }

    }
}