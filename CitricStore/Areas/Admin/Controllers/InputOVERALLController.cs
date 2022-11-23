using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitricStore.Models;

namespace CitricStore.Areas.Admin.Controllers
{
    public class InputOVERALLController : Controller
    {
        private CitricStoreEntities5 db = new CitricStoreEntities5();

        // GET: Admin/InputOVERALL
        public ActionResult Index()
        {
            var oVERALLs = db.OVERALLs.Include(o => o.HEDIEUHANH).Include(o => o.NGONNGU).Include(o => o.NHAPHATHANH).Include(o => o.THELOAI);
            return View(oVERALLs.ToList());
        }

        // GET: Admin/InputOVERALL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OVERALL oVERALL = db.OVERALLs.Find(id);
            if (oVERALL == null)
            {
                return HttpNotFound();
            }
            return View(oVERALL);
        }

        // GET: Admin/InputOVERALL/Create
        public ActionResult Create()
        {
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH");
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu");
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH");
            ViewBag.MaTheLoai = new SelectList(db.THELOAIs, "MaTheLoai", "TenTheLoai");
            return View();
        }

        // POST: Admin/InputOVERALL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma,Ten,GioiThieu,DungLuong,LinkTai,MaNPH,MaHDH,MaNgonNgu,MaTheLoai,NgayCapNhat,DanhGia,DonGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,AppOrGame")] OVERALL oVERALL)
        {
            if (ModelState.IsValid)
            {
                db.OVERALLs.Add(oVERALL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", oVERALL.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", oVERALL.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", oVERALL.MaNPH);
            ViewBag.MaTheLoai = new SelectList(db.THELOAIs, "MaTheLoai", "TenTheLoai", oVERALL.MaTheLoai);
            return View(oVERALL);
        }

        // GET: Admin/InputOVERALL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OVERALL oVERALL = db.OVERALLs.Find(id);
            if (oVERALL == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", oVERALL.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", oVERALL.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", oVERALL.MaNPH);
            ViewBag.MaTheLoai = new SelectList(db.THELOAIs, "MaTheLoai", "TenTheLoai", oVERALL.MaTheLoai);
            return View(oVERALL);
        }

        // POST: Admin/InputOVERALL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma,Ten,GioiThieu,DungLuong,LinkTai,MaNPH,MaHDH,MaNgonNgu,MaTheLoai,NgayCapNhat,DanhGia,DonGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,AppOrGame")] OVERALL oVERALL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oVERALL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", oVERALL.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", oVERALL.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", oVERALL.MaNPH);
            ViewBag.MaTheLoai = new SelectList(db.THELOAIs, "MaTheLoai", "TenTheLoai", oVERALL.MaTheLoai);
            return View(oVERALL);
        }

        // GET: Admin/InputOVERALL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OVERALL oVERALL = db.OVERALLs.Find(id);
            if (oVERALL == null)
            {
                return HttpNotFound();
            }
            return View(oVERALL);
        }

        // POST: Admin/InputOVERALL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OVERALL oVERALL = db.OVERALLs.Find(id);
            db.OVERALLs.Remove(oVERALL);
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
