using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitricStore.Models
{
    public class OrderItem
    {
        CitricStoreEntities db = new CitricStoreEntities();

        public int MaOr { get; set; }
        public DateTime NgayOr { get; set; }
        public int? MaKhachHang { get; set; }
        public string TenOr { get; set; }
        public string SDTOr { get; set; }
        public string EmailOr { get; set; }
        public int? IDBank { get; set; }
        public string MaTK { get; set; }
        public string TenTK { get; set; }
        public decimal? TotalPrice { get; set; }
        public OrderItem(int MaOrd)
        {
            this.MaOr = MaOrd;

            var or = db.ORDER_INFO.Single(s => s.IDOrder == MaOr);

            this.NgayOr = (DateTime)or.DateOrder;
            this.MaKhachHang = or.IDCus;
            this.TenOr = or.NameOrder;
            this.SDTOr = or.PhoneOrder;
            this.EmailOr = or.EmailOrder;
            this.IDBank = or.IDBank;
            this.MaTK = or.IDPayAccount;
            this.TenTK = or.NamePayAccount;
            this.TotalPrice = or.TotalPrice;


        }

    }
}