using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitricStore.Models;

namespace CitricStore.Areas.Admin.Controllers
{
    public class InputDataAPPController : Controller
    {

        private CitricStoreEntities2 db = new CitricStoreEntities2();


        // GET: Admin/InputDataAPP
        public ActionResult Index()
        {
            var aPPs = db.APPs.Include(a => a.HEDIEUHANH).Include(a => a.NGONNGU).Include(a => a.NHAPHATHANH).Include(a => a.THELOAIAPP);
            return View(aPPs.ToList());
        }

        // GET: Admin/InputDataAPP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APP aPP = db.APPs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            return View(aPP);
        }

        // GET: Admin/InputDataAPP/Create
        public ActionResult Create()
        {
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH");
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu");
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH");
            ViewBag.MaTheLoaiApp = new SelectList(db.THELOAIAPPs, "MaTheLoaiApp", "TenTheLoai");
            return View();
        }

        // POST: Admin/InputDataAPP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaApp,TenApp,GioiThieu,DungLuong,LinkTai,MaTheLoaiApp,MaNPH,MaHDH,MaNgonNgu,NgayCapNhat,DanhGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,DonGia")] APP aPP, 
            HttpPostedFileBase HinhNen, HttpPostedFileBase HinhCT1, HttpPostedFileBase HinhCT2, HttpPostedFileBase HinhCT3, HttpPostedFileBase HinhCT4)
        {
            if (ModelState.IsValid)
            {
                if(HinhNen != null)
                {
                    var fileName = Path.GetFileName(HinhNen.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.HinhNen = fileName;
                    HinhNen.SaveAs(path);
                }
                if (HinhCT1 != null)
                {
                    var fileName = Path.GetFileName(HinhCT1.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.HinhCT1 = fileName;
                    HinhCT1.SaveAs(path);
                }
                if (HinhCT2 != null)
                {
                    var fileName = Path.GetFileName(HinhCT2.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.HinhCT2 = fileName;
                    HinhCT2.SaveAs(path);
                }
                if (HinhCT3 != null)
                {
                    var fileName = Path.GetFileName(HinhCT3.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.HinhCT3 = fileName;
                    HinhCT3.SaveAs(path);
                }
                if (HinhCT4 != null)
                {
                    var fileName = Path.GetFileName(HinhCT4.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.HinhCT4 = fileName;
                    HinhCT4.SaveAs(path);
                }




                db.APPs.Add(aPP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", aPP.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", aPP.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", aPP.MaNPH);
            ViewBag.MaTheLoaiApp = new SelectList(db.THELOAIAPPs, "MaTheLoaiApp", "TenTheLoai", aPP.MaTheLoaiApp);
            return View(aPP);
        }

        // GET: Admin/InputDataAPP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APP aPP = db.APPs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", aPP.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", aPP.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", aPP.MaNPH);
            ViewBag.MaTheLoaiApp = new SelectList(db.THELOAIAPPs, "MaTheLoaiApp", "TenTheLoai", aPP.MaTheLoaiApp);
            return View(aPP);
        }

        // POST: Admin/InputDataAPP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaApp,TenApp,GioiThieu,DungLuong,LinkTai,MaTheLoaiApp,MaNPH,MaHDH,MaNgonNgu,NgayCapNhat,DanhGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,DonGia")] APP aPP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", aPP.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", aPP.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", aPP.MaNPH);
            ViewBag.MaTheLoaiApp = new SelectList(db.THELOAIAPPs, "MaTheLoaiApp", "TenTheLoai", aPP.MaTheLoaiApp);
            return View(aPP);
        }

        // GET: Admin/InputDataAPP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APP aPP = db.APPs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            return View(aPP);
        }

        // POST: Admin/InputDataAPP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APP aPP = db.APPs.Find(id);
            db.APPs.Remove(aPP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
