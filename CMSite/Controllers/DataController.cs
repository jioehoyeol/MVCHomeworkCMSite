using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSite.Models;

namespace CMSite.Controllers
{
    public class DataController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: Data
        public ActionResult Index(string SearchId, string keyword, string sort)
        {
            IQueryable<Models.客戶資料> result = null;

            //選資料
            if (!string.IsNullOrEmpty(keyword))
            {
                ViewBag.Keyword = keyword;
                //result = db.客戶資料.Where(c => c.客戶名稱.Contains(keyword));

                if (SearchId == "0")
                    result = db.客戶資料.Where(e => e.客戶名稱.Contains(keyword));
                else if (SearchId == "1")
                    result = db.客戶資料.Where(e => e.統一編號.Contains(keyword));
                else if (SearchId == "2")
                    result = db.客戶資料.Where(e => e.電話.Contains(keyword));
                else if (SearchId == "3")
                    result = db.客戶資料.Where(e => e.傳真.Contains(keyword));
                else if (SearchId == "4")
                    result = db.客戶資料.Where(e => e.地址.Contains(keyword));
                else if (SearchId == "5")
                    result = db.客戶資料.Where(e => e.Email.Contains(keyword));
            }
            else
            {
                result = db.客戶資料.AsQueryable();
            }           

            //排序資料
            if (!string.IsNullOrEmpty(sort))
            {
                ViewBag.NameOrder = string.Empty;
                result = result.OrderByDescending(o => o.客戶名稱);
            }
            else
            {
                ViewBag.NameOrder = "name_desc";
                result = result.OrderBy(o => o.客戶名稱);
            }

            return View(result.ToList());
        }

        // GET: Data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Data/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Data/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Data/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            db.客戶資料.Remove(客戶資料);
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
