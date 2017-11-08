using CMSite.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;

namespace CMSite.Controllers
{
    public class DataController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="keyword"></param>
        /// <param name="sort"></param>
        /// <param name="orderby"></param>
        /// <param name="descYn"></param>
        /// <returns></returns>
        public ActionResult Index(string searchId, string keyword, bool? sort, string orderby, string descYn)
        {
            //組合客戶資料
            var result = from d in db.客戶資料
                         join c in db.Control
                            on new { p1 = "Category", p2 = d.客戶分類 }
                            equals new { p1 = c.TypeNo, p2 = c.KeyNo } into cd
                         from c in cd.DefaultIfEmpty()
                         where d.IsDelete == false
                         select new CustomerDataViewModel
                         {
                             Id = d.Id,
                             CustomerName = d.客戶名稱,
                             TaxNumber = d.統一編號,
                             Telephone = d.電話,
                             Fax = d.傳真,
                             Address = d.地址,
                             Email = d.Email,
                             Category = c.Value
                         };

            //取得客戶分類可用的篩選選項
            ViewBag.CategoryList = result.Select(r => r.Category).Distinct();

            #region 篩選資料
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (searchId)
                {
                    case "0":
                        result = result.Where(e => e.CustomerName.Contains(keyword));
                        break;
                    case "1":
                        result = result.Where(e => e.TaxNumber.Contains(keyword));
                        break;
                    case "2":
                        result = result.Where(e => e.Telephone.Contains(keyword));
                        break;
                    case "3":
                        result = result.Where(e => e.Fax.Contains(keyword));
                        break;
                    case "4":
                        result = result.Where(e => e.Address.Contains(keyword));
                        break;
                    case "5":
                        result = result.Where(e => e.Email.Contains(keyword));
                        break;
                    case "6":
                        result = result.Where(e => e.Category.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 排序資料
            if (sort != null && sort.Value)
            {
                descYn = descYn == "Y" ? "N" : "Y";
            }
            if (!string.IsNullOrEmpty(descYn))
            {
                if (descYn.Equals("Y"))
                {
                    result = result.OrderBy(orderby + " DESC");
                }
                else
                {
                    result = result.OrderBy(orderby);
                }
            }

            #endregion

            ViewBag.DescYn = descYn;
            ViewBag.OrderBy = orderby;
            ViewBag.Keyword = keyword;
            ViewBag.SearchId = searchId;

            return View(result);
        }

        // GET: Data/Statistics
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Statistics()
        {
            var result = from p in db.客戶資料
                         select new CustomerStatisticViewModel
                         {
                             Id = p.Id,
                             CustomerName = p.客戶名稱,
                             ContactCount = p.客戶聯絡人.Where(d => !d.IsDelete).Count(),
                             BankCount = p.客戶銀行資訊.Where(d => !d.IsDelete).Count()
                         };

            return View(result);
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
            QueryCustomerCategoryItem();
            return View();
        }

        // POST: Data/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
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
            QueryCustomerCategoryItem();
            return View(客戶資料);
        }

        // POST: Data/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
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

            if (客戶資料.客戶聯絡人.Any())
            {
                foreach (var contact in 客戶資料.客戶聯絡人)
                {
                    contact.IsDelete = true;
                }
            }

            if (客戶資料.客戶銀行資訊.Any())
            {
                foreach (var bank in 客戶資料.客戶銀行資訊)
                {
                    bank.IsDelete = true;
                }
            }

            客戶資料.IsDelete = true;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void QueryCustomerCategoryItem()
        {
            ViewBag.CategoryList = from c in db.Control
                                   where c.TypeNo == "Category"
                                   orderby c.KeyNo
                                   select new SelectListItem
                                   {
                                       Text = c.Value,
                                       Value = c.KeyNo
                                   };
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
