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
    public class InputDataSOFTWAREController : Controller
    {

        private CitricStoreEntities db = new CitricStoreEntities();


        // GET: Admin/InputDataSOFTWARE
        public ActionResult Index()
        {
            var aPPs = db.SOFTWAREs.Include(a => a.PLATFORM).Include(a => a.LANGUAGE).Include(a => a.PUBLISHER).Include(a => a.CATEGORY_SOFTWARE);
            return View(aPPs.ToList());
        }

        // GET: Admin/InputDataSOFTWARE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOFTWARE aPP = db.SOFTWAREs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            return View(aPP);
        }

        // GET: Admin/InputDataSOFTWARE/Create
        public ActionResult Create()
        {
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform");
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage");
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher");
            ViewBag.IDCatSoft = new SelectList(db.CATEGORY_SOFTWARE, "IDCatSoft", "NameCatSoft");
            return View();
        }

        // POST: Admin/InputDataSOFTWARE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSoft,NameSoft,Description,Capacity,LinkTai,IDCatSoft,IDPublisher,IDPlatform,IDLanguage,UpdateDate,Rating,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,Price")] SOFTWARE aPP, 
            HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)
        {
            if (ModelState.IsValid)
            {
                if(PicBG != null)
                {
                    var fileName = Path.GetFileName(PicBG.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.PicBG = fileName;
                    PicBG.SaveAs(path);
                }
                if (PicDetail1 != null)
                {
                    var fileName = Path.GetFileName(PicDetail1.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.PicDetail1 = fileName;
                    PicDetail1.SaveAs(path);
                }
                if (PicDetail2 != null)
                {
                    var fileName = Path.GetFileName(PicDetail2.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.PicDetail2 = fileName;
                    PicDetail2.SaveAs(path);
                }
                if (PicDetail3 != null)
                {
                    var fileName = Path.GetFileName(PicDetail3.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.PicDetail3 = fileName;
                    PicDetail3.SaveAs(path);
                }
                if (PicDetail4 != null)
                {
                    var fileName = Path.GetFileName(PicDetail4.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    aPP.PicDetail4 = fileName;
                    PicDetail4.SaveAs(path);
                }
                db.SOFTWAREs.Add(aPP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", aPP.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", aPP.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", aPP.IDPublisher);
            ViewBag.IDCatSoft = new SelectList(db.CATEGORY_SOFTWARE, "IDCatSoft", "NameCatSoft", aPP.IDCatSoft);
            return View(aPP);
        }

        // GET: Admin/InputDataSOFTWARE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOFTWARE aPP = db.SOFTWAREs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", aPP.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", aPP.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", aPP.IDPublisher);
            ViewBag.IDCatSoft = new SelectList(db.CATEGORY_SOFTWARE, "IDCatSoft", "NameCatSoft", aPP.IDCatSoft);
            return View(aPP);
        }

        // POST: Admin/InputDataSOFTWARE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSoft,NameSoft,Description,Capacity,LinkTai,IDCatSoft,IDPublisher,IDPlatform,IDLanguage,UpdateDate,Rating,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,Price")] SOFTWARE sOFTWARE,
            HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)
        {
            if (ModelState.IsValid)
            {
                var softDB = db.SOFTWAREs.FirstOrDefault(s => s.IDSoft == sOFTWARE.IDSoft);
                if(softDB != null)
                {
                    softDB.NameSoft = sOFTWARE.NameSoft;
                    softDB.Description = sOFTWARE.Description;
                    softDB.Capacity = sOFTWARE.Capacity;
                    softDB.IDCatSoft = sOFTWARE.IDCatSoft;
                    softDB.IDPublisher = sOFTWARE.IDPublisher;
                    softDB.IDPlatform = sOFTWARE.IDPlatform;
                    softDB.IDLanguage = sOFTWARE.IDLanguage;
                    softDB.UpdateDate = sOFTWARE.UpdateDate;
                    softDB.Rating = sOFTWARE.Rating;
                    softDB.Price = sOFTWARE.Price;
                    if (PicBG != null)
                    {
                        var fileName = Path.GetFileName(PicBG.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        softDB.PicBG = fileName;
                        PicBG.SaveAs(path);
                    }
                    if (PicDetail1 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail1.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        softDB.PicDetail1 = fileName;
                        PicDetail1.SaveAs(path);
                    }
                    if (PicDetail2 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail2.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        softDB.PicDetail2 = fileName;
                        PicDetail2.SaveAs(path);
                    }
                    if (PicDetail3 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail3.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        softDB.PicDetail3 = fileName;
                        PicDetail3.SaveAs(path);
                    }
                    if (PicDetail4 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail4.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        softDB.PicDetail4 = fileName;
                        PicDetail4.SaveAs(path);
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", sOFTWARE.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", sOFTWARE.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", sOFTWARE.IDPublisher);
            ViewBag.IDCatSoft = new SelectList(db.CATEGORY_SOFTWARE, "IDCatSoft", "NameCatSoft", sOFTWARE.IDCatSoft);
            return View(sOFTWARE);
        }

        // GET: Admin/InputDataSOFTWARE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOFTWARE aPP = db.SOFTWAREs.Find(id);
            if (aPP == null)
            {
                return HttpNotFound();
            }
            return View(aPP);
        }

        // POST: Admin/InputDataSOFTWARE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SOFTWARE aPP = db.SOFTWAREs.Find(id);
            db.SOFTWAREs.Remove(aPP);
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
