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
    public class InputDataOVERALLController : Controller
    {

        private CitricStoreEntities db = new CitricStoreEntities();


        // GET: Admin/InputOVERALL
        public ActionResult Index()
        {
            var oVERALLs = db.OVERALLs.Include(o => o.PLATFORM).Include(o => o.LANGUAGE).Include(o => o.PUBLISHER).Include(o => o.CATEGORY);
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
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform");
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage");
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs , "IDPublisher", "NamePublisher");
            ViewBag.IDCat = new SelectList(db.CATEGORies, "IDCat", "NameCat");
            return View();
        }

        // POST: Admin/InputOVERALL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDOverall,NameOverall,Description,Capacity,IDPublisher,IDPlatform,IDLanguage,IDCat,UpdateDate,Rating,Price,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,SoftOrGame")] OVERALL oVERALL,
            HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)
        {
            if (ModelState.IsValid)
            {
                if (PicBG != null)
                {
                    var fileName = Path.GetFileName(PicBG.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                    oVERALL.PicBG = fileName;
                    PicBG.SaveAs(path);
                }
                if (PicDetail1 != null)
                {
                    var fileName = Path.GetFileName(PicDetail1.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                    oVERALL.PicDetail1 = fileName;
                    PicDetail1.SaveAs(path);
                }
                if (PicDetail2 != null)
                {
                    var fileName = Path.GetFileName(PicDetail2.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                    oVERALL.PicDetail2 = fileName;
                    PicDetail2.SaveAs(path);
                }
                if (PicDetail3 != null)
                {
                    var fileName = Path.GetFileName(PicDetail3.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                    oVERALL.PicDetail3 = fileName;
                    PicDetail3.SaveAs(path);
                }
                if (PicDetail4 != null)
                {
                    var fileName = Path.GetFileName(PicDetail4.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                    oVERALL.PicDetail4 = fileName;
                    PicDetail4.SaveAs(path);
                }
                db.OVERALLs.Add(oVERALL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", oVERALL.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", oVERALL.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", oVERALL.IDPublisher);
            ViewBag.IDCat = new SelectList(db.CATEGORies, "IDCat", "NameCat", oVERALL.IDCat);
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
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", oVERALL.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", oVERALL.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", oVERALL.IDPublisher);
            ViewBag.IDCat = new SelectList(db.CATEGORies, "IDCat", "NameCat", oVERALL.IDCat);
            return View(oVERALL);
        }

        // POST: Admin/InputOVERALL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDOverall,NameOverall,Description,Capacity,LinkTai,IDPublisher,IDPlatform,IDLanguage,IDCat,UpdateDate,Rating,Price,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,SoftOrGame")] OVERALL oVERALL,
            HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)
        {
            if (ModelState.IsValid)
            {
                var appDB = db.OVERALLs.FirstOrDefault(s => s.IDOverall == oVERALL.IDOverall);
                if(appDB != null)
                {
                    appDB.NameOverall = oVERALL.NameOverall;
                    appDB.Description = oVERALL.Description;
                    appDB.Capacity = oVERALL.Capacity;
                    appDB.IDCat = oVERALL.IDCat;
                    appDB.IDPublisher = oVERALL.IDPublisher;
                    appDB.IDPlatform = oVERALL.IDPlatform;
                    appDB.IDLanguage = oVERALL.IDLanguage;
                    appDB.UpdateDate = oVERALL.UpdateDate;
                    appDB.Rating = oVERALL.Rating;
                    appDB.Price = oVERALL.Price;
                    if (PicBG != null)
                    {
                        var fileName = Path.GetFileName(PicBG.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                        appDB.PicBG = fileName;
                        PicBG.SaveAs(path);
                    }
                    if (PicDetail1 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail1.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                        appDB.PicDetail1 = fileName;
                        PicDetail1.SaveAs(path);
                    }
                    if (PicDetail2 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail2.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                        appDB.PicDetail2 = fileName;
                        PicDetail2.SaveAs(path);
                    }
                    if (PicDetail3 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail3.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                        appDB.PicDetail3 = fileName;
                        PicDetail3.SaveAs(path);
                    }
                    if (PicDetail4 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail4.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/OVERALL"), fileName);

                        appDB.PicDetail4 = fileName;
                        PicDetail4.SaveAs(path);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", oVERALL.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", oVERALL.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", oVERALL.IDPublisher);
            ViewBag.IDCat = new SelectList(db.CATEGORies, "IDCat", "NameCat", oVERALL.IDCat);
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
