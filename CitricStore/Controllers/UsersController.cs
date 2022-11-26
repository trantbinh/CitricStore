using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitricStore.Models;
using System.Resources;
using PagedList;

namespace CitricStore.Controllers
{
    public class UsersController : Controller
    {

        private CitricStoreEntities database = new CitricStoreEntities();

        //GET: User
        //ĐĂNG KÝ
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.HoTenKH))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.Matkhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được đểtrống");
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.DienthoaiKH))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                var khachhang = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
                if (ModelState.IsValid)
                {
                    database.KHACHHANGs.Add(kh);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        //ĐĂNG NHẬP
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.Matkhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    
                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN && k.Matkhau == kh.Matkhau);

                    var makh = database.KHACHHANGs.Where(g => g.TenDN == kh.TenDN).Select(g => g.MaKH);

                    if (khach != null)
                    {

                        //Lưu vào session
                        Session["TaiKhoan"] = khach;

                        Session["TenDN"] = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN).TenDN;

                        Session["MaKH"] = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN).MaKH;

                        if(kh.TenDN == "admin")
                        {
                            return RedirectToAction("Index", "Admin/Admin");
                        }    
                        return RedirectToAction("Index", "CitricStore");

                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }

                }
              

            }
            return View();

        }




        //XEM THÔNG TIN KHÁCH HÀNG
        public ActionResult ViewInfo(int idkh)
        {
            var kh = database.KHACHHANGs.FirstOrDefault(g => g.MaKH == idkh);
            return View(kh);
        }

        //SỬA THÔNG TIN KHÁCH HÀNG
        public ActionResult EditInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kh = database.KHACHHANGs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);

        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "MaKH,HoTenKH,DienthoaiKH,TenDN,Matkhau,Ngáyinh,Email,Daduyet,GioiTinh")] KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                database.Entry(kh).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("ViewInfo","Users", new { idkh = Session["MaKH"] });
            }
            return View(kh);
        }


        public ActionResult LogOut()
        {
            Session.Remove("TaiKhoan");
            Session.Remove("TenDN");
            Session.Remove("MaKH");

            return RedirectToAction("Index","CitricStore");
        }


        //Quên mật khẩu
        public ActionResult FogotPass()
        {
            return View();
        }

        [HttpPost]

        public ActionResult FogotPass( KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.Email))
                    ModelState.AddModelError(String.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập không được để trống");
                if (ModelState.IsValid)
                {

                    var khach = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN && k.Email == kh.Email);

                    var makh = database.KHACHHANGs.Where(g => g.TenDN == kh.TenDN).Select(g => g.MaKH);

                    if (khach != null)
                    {
                        //Session["TaiKhoan"] = khach;

                        //Session["TenDN"] = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN).TenDN;

                        Session["MaKH"] = database.KHACHHANGs.FirstOrDefault(k => k.TenDN == kh.TenDN).MaKH;

                        return RedirectToAction("ResetPass", "Users", new {id = Session["MaKH"] });


                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc email không đúng";
                    }

                }


            }
            return View();

        }

        //Reset Password
        public ActionResult ResetPass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kh = database.KHACHHANGs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass([Bind(Include = "MaKH,HoTenKH,DienthoaiKH,TenDN,Matkhau,Ngáyinh,Email,Daduyet,GioiTinh")] KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                database.Entry(kh).State = EntityState.Modified;
                database.SaveChanges();

                ViewBag.ThongBao = "Đổi mật khẩu thành công!";
            }
            return View(kh);
        }



        //Đơn đã mua

       

        private List<ORDER_INFO> DonHang(int idkh)
        {
            return database.ORDER_INFO.Where(s => s.MaKH == idkh).OrderBy(x => x.MaOrder).ToList();
        }
        public ActionResult Users_DaMua(int?page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            //var ORDER_INFO = (from l in database.ORDER_INFO select l).OrderBy(x => x.MaOrder);
            //var o = database.ORDER_INFO.OrderBy(x => x.MaOrder);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            int idkh = (int)Session["MaKH"];

            var info = database.ORDER_INFO.Where(s => s.MaKH == idkh).OrderBy(s => s.MaOrder);
            
            //var dsDonHang = SoDonHang(5, idkh);
            return View(info.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DaMua_Product(int idor)
        {
            var or = database.ORDER_PRODUCT.Where(s => s.MaOrder == idor);
            return PartialView(or);
        }

        public ActionResult DaMua_UngDung(int idud)
        {
            var ud = database.OVERALLs.Where(s => s.Ma == idud) ;
            return PartialView(ud);
        }

    }

}