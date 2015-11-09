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
    public class CategoriesSpryController : Controller
    {
        private TestDbContext db = new TestDbContext();

        // GET: CategoriesSpry
        public ActionResult Index(int? id)
        {
            return RedirectToAction("CategorySpryList", new { id = id.GetValueOrDefault(0) });
        }

        public ActionResult CategorySpryJson()
        {
            return View(db.Category.ToList());
           // return View(Json(new { test=db.Category.ToList() }, JsonRequestBehavior.AllowGet));
        }

        public ActionResult CategorySpryTable(int? id)
        {

            int getID = id.GetValueOrDefault(0);

            return View(db.Category.Where(x => x.parentID == getID).ToList());
        }

        public ActionResult CategorySpryList(int? id)
        {
            int getID = id.GetValueOrDefault(0);
            if (getID != 0)
            {
                Category category = db.Category.Find(id);
                ViewBag.viewTitle = category.title;
                ViewBag.viewParentID = category.parentID;
                ViewBag.viewSortNum = category.sortNum;

            }
            ViewBag.viewID = getID;
            return View(db.Category.Where(x => x.parentID == getID).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create(int id)
        {
            ViewBag.viewID = id;
            Category category = new Category();
            category.parentID = id;
            category.sortNum = db.Category.Where(x => x.parentID == id).Count() + 1;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("CategorySpryList", new { id = category.parentID });
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,title,parentID,sortNum")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("CategorySpryList", new { id = category.parentID });
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
