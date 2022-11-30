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
    public class InputDataPLATFORMController : Controller
    {

        private CitricStoreEntities db = new CitricStoreEntities();


        // GET: Admin/InputDataHEDIEUHANH
        public ActionResult Index()
        {
            return View(db.PLATFORMs.ToList());
        }

        // GET: Admin/InputDataHEDIEUHANH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            if (pLATFORM == null)
            {
                return HttpNotFound();
            }
            return View(pLATFORM);
        }

        // GET: Admin/InputDataHEDIEUHANH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/InputDataHEDIEUHANH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPlatform,NamePlatform")] PLATFORM pLATFORM)
        {
            if (ModelState.IsValid)
            {
                db.PLATFORMs.Add(pLATFORM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pLATFORM);
        }

        // GET: Admin/InputDataHEDIEUHANH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            if (pLATFORM == null)
            {
                return HttpNotFound();
            }
            return View(pLATFORM);
        }

        // POST: Admin/InputDataHEDIEUHANH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPlatform,NamePlatform")] PLATFORM pLATFORM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLATFORM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pLATFORM);
        }

        // GET: Admin/InputDataHEDIEUHANH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            if (pLATFORM == null)
            {
                return HttpNotFound();
            }
            return View(pLATFORM);
        }

        // POST: Admin/InputDataHEDIEUHANH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            db.PLATFORMs.Remove(pLATFORM);
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
