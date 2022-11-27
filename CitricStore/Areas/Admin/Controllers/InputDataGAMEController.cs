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
    public class InputDataGAMEController : Controller
    {
        private CitricStoreEntities db = new CitricStoreEntities();

        // GET: Admin/InputDataGAME
        public ActionResult Index()
        {
            var gAMEs = db.GAMEs.Include(g => g.PUBLISHER).Include(g => g.LANGUAGE).Include(g => g.PLATFORM).Include(g => g.CATEGORY_GAME);
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
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform");
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage");
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher");
            ViewBag.IDCatGame = new SelectList(db.CATEGORY_GAME, "IDCatGame", "NameCatGame");
            return View();
        }

        // POST: Admin/InputDataGAME/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGame,NameGame,Description,Capacity,LinkTai,IDCatGame,IDPublisher,IDPlatform,IDLanguage,UpdateDate,Rating,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,Price")] GAME gAME,
         HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)
        {
            if (ModelState.IsValid)
            {
                if (PicBG != null)
                {
                    var fileName = Path.GetFileName(PicBG.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    gAME.PicBG = fileName;
                    PicBG.SaveAs(path);
                }
                if (PicDetail1 != null)
                {
                    var fileName = Path.GetFileName(PicDetail1.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    gAME.PicDetail1 = fileName;
                    PicDetail1.SaveAs(path);
                }
                if (PicDetail2 != null)
                {
                    var fileName = Path.GetFileName(PicDetail2.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    gAME.PicDetail2 = fileName;
                    PicDetail2.SaveAs(path);
                }
                if (PicDetail3 != null)
                {
                    var fileName = Path.GetFileName(PicDetail3.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    gAME.PicDetail3 = fileName;
                    PicDetail3.SaveAs(path);
                }
                if (PicDetail4 != null)
                {
                    var fileName = Path.GetFileName(PicDetail4.FileName);

                    var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                    gAME.PicDetail4 = fileName;
                    PicDetail4.SaveAs(path);
                }


                db.GAMEs.Add(gAME);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", gAME.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", gAME.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", gAME.IDPublisher);
            ViewBag.IDCatGame = new SelectList(db.CATEGORY_GAME, "IDCatGame", "NameCatGame", gAME.IDCatGame);
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
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", gAME.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", gAME.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", gAME.IDPublisher);
            ViewBag.IDCatGame = new SelectList(db.CATEGORY_GAME, "IDCatGame", "NameCatGame", gAME.IDCatGame);
            return View(gAME);
        }

        // POST: Admin/InputDataGAME/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGame,NameGame,Description,Capacity,LinkTai,IDCatGame,IDPublisher,IDPlatform,IDLanguage,UpdateDate,Rating,PicBG,PicDetail1,PicDetail2,PicDetail3,PicDetail4,Price")] GAME gAME,
        HttpPostedFileBase PicBG, HttpPostedFileBase PicDetail1, HttpPostedFileBase PicDetail2, HttpPostedFileBase PicDetail3, HttpPostedFileBase PicDetail4)

        {
            if (ModelState.IsValid)
            {
                var gameDB = db.GAMEs.FirstOrDefault(s => s.IDGame == gAME.IDGame);
                if (gameDB != null)
                {
                    gameDB.NameGame = gAME.NameGame;
                    gameDB.Description = gAME.Description;
                    gameDB.Capacity = gAME.Capacity;
                    gameDB.IDCatGame = gAME.IDCatGame;
                    gameDB.IDPublisher = gAME.IDPublisher;
                    gameDB.IDPlatform = gAME.IDPlatform;
                    gameDB.IDLanguage = gAME.IDLanguage;
                    gameDB.UpdateDate = gAME.UpdateDate;
                    gameDB.Rating = gAME.Rating;
                    gameDB.Price = gAME.Price;
                    if (PicBG != null)
                    {
                        var fileName = Path.GetFileName(PicBG.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        gameDB.PicBG = fileName;
                        PicBG.SaveAs(path);
                    }
                    if (PicDetail1 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail1.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        gameDB.PicDetail1 = fileName;
                        PicDetail1.SaveAs(path);
                    }
                    if (PicDetail2 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail2.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        gameDB.PicDetail2 = fileName;
                        PicDetail2.SaveAs(path);
                    }
                    if (PicDetail3 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail3.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        gameDB.PicDetail3 = fileName;
                        PicDetail3.SaveAs(path);
                    }
                    if (PicDetail4 != null)
                    {
                        var fileName = Path.GetFileName(PicDetail4.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/APP"), fileName);

                        gameDB.PicDetail4 = fileName;
                        PicDetail4.SaveAs(path);
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPlatform = new SelectList(db.PLATFORMs, "IDPlatform", "NamePlatform", gAME.IDPlatform);
            ViewBag.IDLanguage = new SelectList(db.LANGUAGEs, "IDLanguage", "NameLanguage", gAME.IDLanguage);
            ViewBag.IDPublisher = new SelectList(db.PUBLISHERs, "IDPublisher", "NamePublisher", gAME.IDPublisher);
            ViewBag.IDCatGame = new SelectList(db.CATEGORY_GAME, "IDCatGame", "NameCatGame", gAME.IDCatGame);
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
