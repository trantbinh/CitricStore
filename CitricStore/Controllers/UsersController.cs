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
        public ActionResult Register(CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.NameCus))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.LogName))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.LogPass))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được đểtrống");
                if (string.IsNullOrEmpty(kh.EmailCus))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                var logname = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName);
                if (logname != null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập đã được dùng");

                var logmail = database.CUSTOMERs.FirstOrDefault(m => m.EmailCus == kh.EmailCus);
                if (logmail != null)
                    ModelState.AddModelError(string.Empty, "Email đã được dùng");

                if (ModelState.IsValid)
                {
                    database.CUSTOMERs.Add(kh);
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
        public ActionResult Login(CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.LogName))
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.LogPass))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    
                    var khach = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName && k.LogPass == kh.LogPass);

                    var makh = database.CUSTOMERs.Where(g => g.LogName == kh.LogName).Select(g => g.IDCus);

                    if (khach != null)
                    {

                        //Lưu vào session
                        Session["TaiKhoan"] = khach;

                        Session["LogName"] = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName).LogName;

                        Session["IDCus"] = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName).IDCus;

                        if(kh.LogName == "admin")
                        {
                            return RedirectToAction("Index", "Admin/Admin");
                        }    
                        return RedirectToAction("Index", "CitricStore");

                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    }

                }
              

            }
            return View();

        }




        //XEM THÔNG TIN KHÁCH HÀNG
        public ActionResult ViewInfo(int idkh)
        {
            var kh = database.CUSTOMERs.FirstOrDefault(g => g.IDCus == idkh);
            return View(kh);
        }

        //SỬA THÔNG TIN KHÁCH HÀNG
        public ActionResult EditInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER kh = database.CUSTOMERs.Find(id);
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
        public ActionResult EditInfo([Bind(Include = "IDCus,NameCus,PhoneCus,LogName,LogPass,Birthday,EmailCus,Sex")] CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                var cusDB = database.CUSTOMERs.FirstOrDefault(c => c.IDCus == kh.IDCus);
                if (cusDB != null)
                {
                    cusDB.NameCus = kh.NameCus;
                    cusDB.PhoneCus = kh.PhoneCus;
                    cusDB.LogPass = kh.LogPass;
                    cusDB.Birthday = kh.Birthday;
                    cusDB.EmailCus = kh.EmailCus;
                    cusDB.Sex = kh.Sex;
                }
                    //var logmail = database.CUSTOMERs.FirstOrDefault(m => m.EmailCus == kh.EmailCus);
                    //if (logmail != null)
                    //    ModelState.AddModelError(string.Empty, "Đã tồn tại email này!");


                    database.SaveChanges();
                    return RedirectToAction("ViewInfo", "Users", new { idkh = Session["IDCus"] });

            }
            return View(kh);
        }


        public ActionResult LogOut()
        {
            Session.Remove("TaiKhoan");
            Session.Remove("LogName");
            Session.Remove("IDCus");

            return RedirectToAction("Index","CitricStore");
        }


        //Quên mật khẩu
        public ActionResult FogotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FogotPass(CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.EmailCus))
                    ModelState.AddModelError(String.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.LogName))
                    ModelState.AddModelError(String.Empty, "Tên đăng nhập không được để trống");
                if (ModelState.IsValid)
                {

                    var khach = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName && k.EmailCus == kh.EmailCus);

                    var makh = database.CUSTOMERs.Where(g => g.LogName == kh.LogName).Select(g => g.IDCus);

                    if (khach != null)
                    {
                        //Session["TaiKhoan"] = khach;

                        //Session["LogName"] = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName).LogName;

                        Session["IDCus"] = database.CUSTOMERs.FirstOrDefault(k => k.LogName == kh.LogName).IDCus;

                        return RedirectToAction("ResetPass", "Users", new {id = Session["IDCus"] });


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
            CUSTOMER kh = database.CUSTOMERs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass([Bind(Include = "IDCus,NameCus,PhoneCus,LogName,LogPass,Birthday,EmailCus,Daduyet,Sex")] CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                var pas = database.CUSTOMERs.FirstOrDefault(p => p.IDCus == kh.IDCus);
                if(kh.LogPass != pas.LogPass)
                {
                    if (ModelState.IsValid)
                    {
                        pas.LogPass = kh.LogPass;
                        database.SaveChanges();
                        ViewBag.ThongBao = "Đổi mật khẩu thành công!";
                    }

                }
                else
                {
                    ViewBag.ThongBao = "Vui lòng nhập mật khẩu mới!";
                }
            }
            return View(kh);
        }



        //Đổi mật khẩu
        public ActionResult ChangePass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER kh = database.CUSTOMERs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass([Bind(Include = "IDCus,NameCus,PhoneCus,LogName,LogPass,Birthday,EmailCus,Daduyet,Sex")] CUSTOMER kh)
        {
            if (ModelState.IsValid)
            {
                var pas = database.CUSTOMERs.FirstOrDefault(p => p.IDCus == kh.IDCus);
                if (kh.LogPass != pas.LogPass)
                {
                    if (ModelState.IsValid)
                    {
                        pas.LogPass = kh.LogPass;
                        database.SaveChanges();
                        ViewBag.ThongBao = "Đổi mật khẩu thành công!";
                    }

                }
                else
                {
                    ViewBag.ThongBao = "Vui lòng nhập mật khẩu mới!";
                }
            }
            return View(kh);
        }




        private List<ORDER_INFO> DonHang(int idkh)
        {
            return database.ORDER_INFO.Where(s => s.IDCus == idkh).OrderBy(x => x.IDOrder).ToList();
        }
        public ActionResult Users_DaMua(int?page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            //var ORDER_INFO = (from l in database.ORDER_INFO select l).OrderBy(x => x.IDOrder);
            //var o = database.ORDER_INFO.OrderBy(x => x.IDOrder);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            int idkh = (int)Session["IDCus"];

            var info = database.ORDER_INFO.Where(s => s.IDCus == idkh).OrderBy(s => s.IDOrder);
            
            //var dsDonHang = SoDonHang(5, idkh);
            return View(info.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DaMua_Product(int idor)
        {
            var or = database.ORDER_PRODUCT.Where(s => s.IDOrder == idor);
            return PartialView(or);
        }

        public ActionResult DaMua_UngDung(int idud)
        {
            var ud = database.OVERALLs.Where(s => s.IDOverall == idud) ;
            return PartialView(ud);
        }

    }

}