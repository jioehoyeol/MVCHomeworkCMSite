using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSite.Models;

namespace CMSite.Controllers
{
    public class ContactController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: Contact
        public ActionResult Index(string keyword, string jobtitle, bool? sort, string orderby, string descYn)
        {
            IQueryable<Models.客戶聯絡人> result = db.客戶聯絡人.Where(d => d.IsDelete == false);

            ViewBag.JobTitleList = result.Select(r => r.職稱).Distinct();

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(o => o.姓名.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(jobtitle))
            {
                result = result.Where(o => o.職稱 == jobtitle);
            }

            if (sort != null && sort.Value)
            {
                descYn = descYn == "Y" ? "N" : "Y";
            }


            if (!string.IsNullOrEmpty(descYn) && !string.IsNullOrEmpty(orderby))
            {
                if (descYn.Equals("Y"))
                {
                    if (orderby.Equals("客戶名稱"))
                    {
                        result = result.OrderByDescending(o => o.客戶資料.客戶名稱);
                    }
                    else
                    {
                        result = result.OrderBy(orderby + " DESC");
                    }
                }
                else
                {
                    if (orderby.Equals("客戶名稱"))
                    {
                        result = result.OrderBy(o => o.客戶資料.客戶名稱);
                    }
                    else
                    {
                        result = result.OrderBy(orderby);
                    }
                }
            }
            ViewBag.Keyword = keyword;
            ViewBag.JobTitle = jobtitle;
            ViewBag.OrderBy = orderby;
            ViewBag.DescYn = descYn;

            return View(result);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                if (!CheckEmailDuplicate(客戶聯絡人.Id, 客戶聯絡人.客戶Id, 客戶聯絡人.Email))
                {
                    db.客戶聯絡人.Add(客戶聯絡人);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Email", "輸入的 Email 已存在，請重新輸入！");
                }
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                if (!CheckEmailDuplicate(客戶聯絡人.Id, 客戶聯絡人.客戶Id, 客戶聯絡人.Email))
                {
                    db.Entry(客戶聯絡人).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Email", "輸入的 Email 已存在，請重新輸入！");
                }
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private bool CheckEmailDuplicate(int Id, int 客戶Id, string Email)
        {
            if (Id > 0)
            {
                return db.客戶聯絡人.Any(o => o.Id != Id && o.客戶Id == 客戶Id && o.Email == Email);
            }
            else
            {
                return db.客戶聯絡人.Any(o => o.客戶Id == 客戶Id && o.Email == Email);
            }
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
