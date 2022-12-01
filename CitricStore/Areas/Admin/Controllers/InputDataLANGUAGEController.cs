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
    public class InputDataLANGUAGEController : Controller
    {
        private CitricStoreEntities db = new CitricStoreEntities();


        // GET: Admin/InputDataNGONNGU
        public ActionResult Index()
        {
            return View(db.LANGUAGEs.ToList());
        }

        // GET: Admin/InputDataNGONNGU/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGE nGONNGU = db.LANGUAGEs.Find(id);
            if (nGONNGU == null)
            {
                return HttpNotFound();
            }
            return View(nGONNGU);
        }

        // GET: Admin/InputDataNGONNGU/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/InputDataNGONNGU/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLanguage,NameLanguage")] LANGUAGE nGONNGU)
        {
            if (ModelState.IsValid)
            {
                db.LANGUAGEs.Add(nGONNGU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nGONNGU);
        }

        // GET: Admin/InputDataNGONNGU/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGE nGONNGU = db.LANGUAGEs.Find(id);
            if (nGONNGU == null)
            {
                return HttpNotFound();
            }
            return View(nGONNGU);
        }

        // POST: Admin/InputDataNGONNGU/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLanguage,NameLanguage")] LANGUAGE nGONNGU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGONNGU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nGONNGU);
        }

        // GET: Admin/InputDataNGONNGU/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGE nGONNGU = db.LANGUAGEs.Find(id);
            if (nGONNGU == null)
            {
                return HttpNotFound();
            }
            return View(nGONNGU);
        }

        // POST: Admin/InputDataNGONNGU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LANGUAGE nGONNGU = db.LANGUAGEs.Find(id);
            db.LANGUAGEs.Remove(nGONNGU);
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
