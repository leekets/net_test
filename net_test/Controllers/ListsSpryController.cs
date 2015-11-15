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
        public ActionResult Index(int id)
        {
            return RedirectToAction("ListSpryList", new { id = id });
        }

        public ActionResult ListSpryTable(int id)
        {
            return View(db.List.Where(x => x.categoryID == id).ToList());
        }

        public ActionResult ListSpryJson()
        {
            //return Json(db.List.ToList(), JsonRequestBehavior.AllowGet);
            return View(db.List.ToList());
        }

        public ActionResult ListSpryPrintCateNum(int id, int repeated)
        {
            ViewBag.viewRepeated = repeated;
            return View(db.List.Where(x => x.categoryID == id).ToList());
        }

        public ActionResult ListSpryList(int id)
        {
            Category category = db.Category.Find(id);
            ViewBag.viewTitle = category.title;
            ViewBag.viewPID = category.parentID;
            ViewBag.viewID = id;
            return View(db.List.Where(x => x.categoryID == id).ToList());
        }

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

        //todo: sort
        public ActionResult Create(int id)
        {
            ViewBag.viewID = id;
            List list = new List();
            list.categoryID = id;
            list.sort = db.List.Where(x => x.categoryID == id).Count();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,categoryID,subTitle,subject,sen,image,hint")] List list)
        {
            if (ModelState.IsValid)
            {
                db.List.Add(list);
                db.SaveChanges();
                return RedirectToAction("ListSpryList", new { id = list.categoryID });
            }

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListBatchAction(string[] arr, int moveCID, int cid, string act)
        {

            if (act == "moveNews")
            {
                foreach (var selection in arr)
                {
                    int getID= Int32.Parse(selection);
                    db.List.Single(x => x.ID == getID).categoryID = moveCID;
                    db.SaveChanges();
                }

                //string[] stringSeparators = new string[] { "," };
                //string[] lines = arr[].Split(stringSeparators, StringSplitOptions.None);
                //int[] intLine = Array.ConvertAll(lines, s => int.Parse(s));
                //for (int count = 0; count <= intLine.Length - 1; count++)
                //{
                //    var tmp = intLine[count];

                //}


                return RedirectToAction("ListSpryList", new { id = moveCID });
            }
            return RedirectToAction("ListSpryList", new { id = cid , info="error"});

        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,categoryID,subTitle,subject,sen,image,hint")] List list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListSpryList", new { id = list.categoryID });
            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListSpryBatchInsert(int id, string textArea)
        {
            if (textArea != null)
            {
                string[] stringSeparators = new string[] { "\r\n" };
                string[] stringSeparatorsForSingle = new string[] { "\t" };
                string[] lines = textArea.Split(stringSeparators, StringSplitOptions.None);
                string[] single;
                for (int count = 0; count <= lines.Length - 1; count++)
                {
                    single = lines[count].Split(stringSeparatorsForSingle, StringSplitOptions.None);
                    if (single.Length == 3)
                    {
                        db.List.Add(new List() { subject = single[0].Trim(), subTitle = single[1].Trim(), hint = single[2].Trim(), categoryID = id, sort = count });
                    }
                    if (single.Length == 4)
                    {
                        int getCID = Int32.Parse(single[3].Trim());
                        db.List.Add(new List() { subject = single[0].Trim(), subTitle = single[1].Trim(), hint = single[2].Trim(), categoryID = getCID, sort = count });
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("ListSpryList", new { id = id });
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List list = db.List.Find(id);
            db.List.Remove(list);
            db.SaveChanges();
            return RedirectToAction("ListSpryList", new { id = list.categoryID });
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
