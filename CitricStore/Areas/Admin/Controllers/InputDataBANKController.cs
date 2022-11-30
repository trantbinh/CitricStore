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
    public class InputDataBANKController : Controller
    {
        private CitricStoreEntities db = new CitricStoreEntities();

        // GET: Admin/InputDataBANK
        public ActionResult Index()
        {
            return View(db.BANKs.ToList());
        }

        // GET: Admin/InputDataBANK/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK bANK = db.BANKs.Find(id);
            if (bANK == null)
            {
                return HttpNotFound();
            }
            return View(bANK);
        }

        // GET: Admin/InputDataBANK/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/InputDataBANK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBank,NameBank")] BANK bANK)
        {
            if (ModelState.IsValid)
            {
                db.BANKs.Add(bANK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bANK);
        }

        // GET: Admin/InputDataBANK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK bANK = db.BANKs.Find(id);
            if (bANK == null)
            {
                return HttpNotFound();
            }
            return View(bANK);
        }

        // POST: Admin/InputDataBANK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBank,NameBank")] BANK bANK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANK);
        }

        // GET: Admin/InputDataBANK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK bANK = db.BANKs.Find(id);
            if (bANK == null)
            {
                return HttpNotFound();
            }
            return View(bANK);
        }

        // POST: Admin/InputDataBANK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BANK bANK = db.BANKs.Find(id);
            db.BANKs.Remove(bANK);
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
