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
    public class InputDataGAMEController : Controller
    {
        private CitricStoreEntities4 db = new CitricStoreEntities4();

        // GET: Admin/InputDataGAME
        public ActionResult Index()
        {
            var gAMEs = db.GAMEs.Include(g => g.HEDIEUHANH1).Include(g => g.NGONNGU1).Include(g => g.NHAPHATHANH).Include(g => g.THELOAIGAME);
            return View(gAMEs.ToList());
        }

        // GET: Admin/InputDataGAME/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // GET: Admin/InputDataGAME/Create
        public ActionResult Create()
        {
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH");
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu");
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH");
            ViewBag.MaTheLoaiGame = new SelectList(db.THELOAIGAMEs, "MaTheLoaiGame", "TenTheLoai");
            return View();
        }

        // POST: Admin/InputDataGAME/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGame,TenGame,GioiThieu,DungLuong,LinkTai,MaTheLoaiGame,MaNPH,MaHDH,MaNgonNgu,NgayCapNhat,DanhGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,DonGia")] GAME gAME)
        {
            if (ModelState.IsValid)
            {
                db.GAMEs.Add(gAME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", gAME.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", gAME.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", gAME.MaNPH);
            ViewBag.MaTheLoaiGame = new SelectList(db.THELOAIGAMEs, "MaTheLoaiGame", "TenTheLoai", gAME.MaTheLoaiGame);
            return View(gAME);
        }

        // GET: Admin/InputDataGAME/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", gAME.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", gAME.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", gAME.MaNPH);
            ViewBag.MaTheLoaiGame = new SelectList(db.THELOAIGAMEs, "MaTheLoaiGame", "TenTheLoai", gAME.MaTheLoaiGame);
            return View(gAME);
        }

        // POST: Admin/InputDataGAME/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGame,TenGame,GioiThieu,DungLuong,LinkTai,MaTheLoaiGame,MaNPH,MaHDH,MaNgonNgu,NgayCapNhat,DanhGia,HinhNen,HinhCT1,HinhCT2,HinhCT3,HinhCT4,DonGia")] GAME gAME)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gAME).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHDH = new SelectList(db.HEDIEUHANHs, "MaHDH", "TenHDH", gAME.MaHDH);
            ViewBag.MaNgonNgu = new SelectList(db.NGONNGUs, "MaNgonNgu", "TenNgonNgu", gAME.MaNgonNgu);
            ViewBag.MaNPH = new SelectList(db.NHAPHATHANHs, "MaNPH", "TenNPH", gAME.MaNPH);
            ViewBag.MaTheLoaiGame = new SelectList(db.THELOAIGAMEs, "MaTheLoaiGame", "TenTheLoai", gAME.MaTheLoaiGame);
            return View(gAME);
        }

        // GET: Admin/InputDataGAME/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME gAME = db.GAMEs.Find(id);
            if (gAME == null)
            {
                return HttpNotFound();
            }
            return View(gAME);
        }

        // POST: Admin/InputDataGAME/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GAME gAME = db.GAMEs.Find(id);
            db.GAMEs.Remove(gAME);
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
