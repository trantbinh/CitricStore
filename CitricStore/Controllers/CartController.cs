using CitricStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitricStore.Controllers
{
    public class CartController : Controller
    {

        CitricStoreEntities db = new CitricStoreEntities();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //Hàm để lấy giỏ hàng hiện tại
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;

            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }


        //Thêm một sản phẩm vào giỏ
        public ActionResult AddToCart(int id)
        {
            //Lấy giỏ hàng hiện tại

            List<CartItem> myCart = GetCart();


            CartItem currentProduct = myCart.FirstOrDefault(p => p.MaUngDung == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.SoLuong++; //Sản phẩm đã có trong giỏ thì tăng số lượng lên 1
            }

            if (currentProduct.LoaiUngDung == "Game")
                return RedirectToAction("DetailsGame", "CitricStore", new { id = id });
            else 
                return RedirectToAction("DetailsApp", "CitricStore", new { id = id });

            //return RedirectToAction("Details_Overall", "CitricStore", new { id = id });
        }


        //Tính tổng số lượng mặt hàng được mua

        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.SoLuong);
            return totalNumber;
        }


        //tính tổng tiền của các sản phẩm được mua
        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }


        //Hiển thị thông tin bên trong giỏ hàng
        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("Index", "CitricStore");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về View hiển thị thông tin giỏ hàng
        }

        //Xoá sản phẩm khỏi giỏ hàng
        //public ActionResult RemoveItem()
        //{
        //    var cart = Get();
        //    var cartitem = cart.Find(p => p.product.ProductId == productid);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cart.Remove(cartitem);
        //    }

        //    SaveCartSession(cart);
        //    return RedirectToAction(nameof(Cart));
        //}

        //Icon giỏ hàng trên layout
        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }



        //Trang thanh toán và thông tin nhận hàng
        [HttpGet]
        public ActionResult Page_Payment()
        {
            ViewBag.MaNganHang = new SelectList(db.BANKs, "BankID", "BankName");

            return View();
        }

        [HttpPost]
        public ActionResult Page_Payment(ORDER_INFO or)
        {
            ViewBag.MaNganHang = new SelectList(db.BANKs, "BankID", "BankName");

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(or.TenOrder))
                    ModelState.AddModelError(string.Empty, "Tên nhận hàng không được để trống");
                if (string.IsNullOrEmpty(or.SDTOrder))
                    ModelState.AddModelError(string.Empty, "Số điện thoại nhận hàng không được để trống");
                if (ModelState.IsValid)
                {
                    or.NgayOrder = DateTime.Now;
                    or.MaKH = int.Parse(Session["MaKH"].ToString());

                    db.ORDER_INFO.Add(or);
                    db.SaveChanges();

                    var order = db.ORDER_INFO.FirstOrDefault(s => s.MaOrder == or.MaOrder).MaOrder;
                    Session["MaOrder"] = order;

                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Page_CheckOrder", "Cart");
        }


        //Hiển thị thông tin thanh toán và nhận hàng
        public ActionResult Page_ShowPayment(int idor)
        {
            var or = db.ORDER_INFO.FirstOrDefault(s => s.MaOrder == idor);
             
            return PartialView(or);
        }

        //Trang kiểm tra đơn hàng -> xác nhận đã thanh toán
      
        [HttpGet]
        public ActionResult Page_CheckOrder()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();

            List<CartItem> myCart = GetCart();
            return View(myCart);
        }

        [HttpPost, ActionName("Page_CheckOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderConfirmed()
        {
            List<CartItem> myCart = GetCart();

            foreach (var item in myCart)
            {
                var details = new ORDER_PRODUCT();
                details.MaUngDung = item.MaUngDung;
                details.MaOrder = int.Parse(Session["MaOrder"].ToString());
                details.SoLuong = item.SoLuong;
                details.DonGia = item.DonGia;
                db.ORDER_PRODUCT.Add(details);
                db.SaveChanges();


            }


            return RedirectToAction("Page_PaymentSuccess");
        }
        public ActionResult Page_PaymentSuccess()
        {
            Session.Remove("GioHang");

            return View();
        }


    }
}