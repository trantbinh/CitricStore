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
    public class InputDataNHAPHATHANHController : Controller
    {

        private CitricStoreEntities db = new CitricStoreEntities();


        // GET: Admin/InputDataNHAPHATHANH
        public ActionResult Index()
        {
            return View(db.PUBLISHERs.ToList());
        }

        // GET: Admin/InputDataNHAPHATHANH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // GET: Admin/InputDataNHAPHATHANH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/InputDataPLATFORM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPublisher,NamePublisher")] PUBLISHER pUBLISHER)
        {
            if (ModelState.IsValid)
            {
                db.PUBLISHERs.Add(pUBLISHER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pUBLISHER);
        }

        // GET: Admin/InputDataNHAPHATHANH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // POST: Admin/InputDataNHAPHATHANH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPublisher,NamePublisher")] PUBLISHER pUBLISHER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUBLISHER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pUBLISHER);
        }

        // GET: Admin/InputDataNHAPHATHANH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // POST: Admin/InputDataNHAPHATHANH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            db.PUBLISHERs.Remove(pUBLISHER);
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
