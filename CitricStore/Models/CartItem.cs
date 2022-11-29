using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitricStore.Models
{
    public class CartItem
    {

        CitricStoreEntities db = new CitricStoreEntities();

        public int MaUngDung { get; set; }
        public string TenUngDung { get; set; }
        public string PicBG { get; set; }
        public decimal Price { get; set; }
        public int SoLuong { get; set; }
        public string LoaiUngDung { get; set; }

        //Tính thành tiền = DongGia * SoLuong
        public decimal FinalPrice()
        {
            return SoLuong * Price;
        }
        public CartItem(int MaUD)
        {
            this.MaUngDung = MaUD;

            var ud = db.OVERALLs.Single(s => s.IDOverall == MaUD);
            this.TenUngDung = ud.NameOverall;

            this.PicBG = ud.PicBG;

            this.Price = (decimal)ud.Price;

            this.SoLuong = 1;

            this.LoaiUngDung = ud.SoftOrGame ;
        }

    }
}
