using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commercial;

namespace Commercial.Controllers
{
    public class SuppliersController : Controller
    {
        private dbCommercialEntities db = new dbCommercialEntities();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.tblSuppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSupplier tblSupplier = db.tblSuppliers.Find(id);
            if (tblSupplier == null)
            {
                return HttpNotFound();
            }
            return View(tblSupplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,ContactNo")] tblSupplier tblSupplier)
        {
            if (ModelState.IsValid)
            {
                db.tblSuppliers.Add(tblSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSupplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSupplier tblSupplier = db.tblSuppliers.Find(id);
            if (tblSupplier == null)
            {
                return HttpNotFound();
            }
            return View(tblSupplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,ContactNo")] tblSupplier tblSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSupplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSupplier tblSupplier = db.tblSuppliers.Find(id);
            if (tblSupplier == null)
            {
                return HttpNotFound();
            }
            db.tblSuppliers.Remove(tblSupplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSupplier tblSupplier = db.tblSuppliers.Find(id);
            db.tblSuppliers.Remove(tblSupplier);
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
