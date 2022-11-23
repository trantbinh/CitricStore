using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitricStore.Models
{
    public class CartItem
    {
        CitricStoreEntities5 db = new CitricStoreEntities5();
        public int MaUngDung { get; set; }
        public string TenUngDung { get; set; }
        public string HinhNen { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string LoaiUngDung { get; set; }

        //Tính thành tiền = DongGia * SoLuong
        public decimal FinalPrice()
        {
            return SoLuong * DonGia;
        }
        public CartItem(int MaUD)
        {
            this.MaUngDung = MaUD;

            var ud = db.OVERALLs.Single(s => s.Ma == MaUD);
            this.TenUngDung = ud.Ten;

            this.HinhNen = ud.HinhNen;

            this.DonGia = (decimal)ud.DonGia;

            this.SoLuong = 1;

            this.LoaiUngDung = ud.AppOrGame ;
        }

    }
}
