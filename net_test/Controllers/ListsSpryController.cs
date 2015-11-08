using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using net_test.Models;

namespace net_test.Controllers
{
    public class ListsSpryController : Controller
    {
        private TestDbContext db = new TestDbContext();

        // GET: ListsSpry
        public ActionResult Index()
        {
            return View(db.List.ToList());
        }

        // GET: ListsSpry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // GET: ListsSpry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListsSpry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,categoryID,subTitle,subject,sen,image,hint,sort")] List list)
        {
            if (ModelState.IsValid)
            {
                db.List.Add(list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(list);
        }

        // GET: ListsSpry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: ListsSpry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,categoryID,subTitle,subject,sen,image,hint,sort")] List list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }

        // GET: ListsSpry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: ListsSpry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List list = db.List.Find(id);
            db.List.Remove(list);
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
