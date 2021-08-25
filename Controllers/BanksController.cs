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
    public class BanksController : Controller
    {
        private dbCommercialEntities db = new dbCommercialEntities();

        // GET: Banks
        public ActionResult Index()
        {
            var bankList = db.tblBanks.ToList();
            return View(bankList);
        }

        // GET: Banks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBank tblBank = db.tblBanks.Find(id);
            if (tblBank == null)
            {
                return HttpNotFound();
            }
            return View(tblBank);
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankName")] tblBank tblBank)
        {
            if (ModelState.IsValid)
            {
                db.tblBanks.Add(tblBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblBank);
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBank tblBank = db.tblBanks.Find(id);
            if (tblBank == null)
            {
                return HttpNotFound();
            }
            return View(tblBank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankName")] tblBank tblBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBank);
        }

        // GET: Banks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBank tblBank = db.tblBanks.Find(id);
            db.tblBanks.Remove(tblBank);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBank tblBank = db.tblBanks.Find(id);
            db.tblBanks.Remove(tblBank);
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
