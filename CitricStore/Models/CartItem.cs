using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitricStore.Models
{
    public class CartItem
    {

        CitricStoreEntities db = new CitricStoreEntities();

        public int IDApp { get; set; }
        public string NameApp { get; set; }
        public string PicBG { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string SoftOrGame { get; set; }

        public decimal FinalPrice()
        {
            return Quantity * Price;
        }
        public CartItem(int MaUD)
        {
            this.IDApp = MaUD;

            var ud = db.OVERALLs.Single(s => s.IDOverall == MaUD);
            this.NameApp = ud.NameOverall;

            this.PicBG = ud.PicBG;

            this.Price = (decimal)ud.Price;

            this.Quantity = 1;

            this.SoftOrGame = ud.SoftOrGame ;
        }

    }
}
