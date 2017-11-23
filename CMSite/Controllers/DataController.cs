using CMSite.ActionFilter;
using CMSite.DataAccess;
using CMSite.Extension;
using CMSite.Models;
using CMSite.Models.SearchModel;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;

namespace CMSite.Controllers
{
    public class DataController : BaseController
    {
        // GET: Data/Index
        public ActionResult Index(CustomerDataSearchModel searchData)
        {
            if (TempData["CustomerDataSearchModel"] != null)
            {
                searchData = TempData["CustomerDataSearchModel"] as CustomerDataSearchModel;
            }

            //排序資料升冪/降冪判斷
            ModelSort("CustomerName", searchData);

            //取得關鍵字查詢下拉選單
            ViewBag.KeywordKindList = SelectListItemDataDAO.QueryKeywordKindList();

            //組合客戶資料
            CustomerDataDAO dao = new CustomerDataDAO();
            searchData.DataModel = dao.QueryData(searchData);

            return View(searchData);
        }

        // GET: Data/Export
        public ActionResult Export(CustomerDataSearchModel searchData)
        {
            //取資料(轉為DataTable)
            CustomerDataDAO dao = new CustomerDataDAO();
            var result = dao.QueryData(searchData).ToDataTable();

            //匯出Excel
            return ExportExcel(result, "CustomerData");
        }

        // GET: Data/GEtCustomerType
        /// <summary>
        /// 取得可篩選選項列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCustomerType()
        {
            SelectListItemDataDAO dao = new SelectListItemDataDAO();
            var result = dao.QueryCustomerCategoryList().Select(s => s.Text);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Data/Statistics
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Statistics()
        {
            CustomerDataDAO dao = new CustomerDataDAO();
            var result = dao.QueryStatisticData();

            return View(result);
        }

        public ActionResult StatisticsExport()
        {
            CustomerDataDAO dao = new CustomerDataDAO();
            var result = dao.QueryStatisticData().ToDataTable();

            return ExportExcel(result, "StatisticData");
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

            KeepCustomerDataSearchModel();
            return View(客戶資料);
        }

        // GET: Data/Create
        [CustomerCategoryDropDownList]
        public ActionResult Create()
        {
            KeepCustomerDataSearchModel();
            return View();
        }

        // POST: Data/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerCategoryDropDownList]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error.Entity")]
        public ActionResult Create(FormCollection form)
        {
            客戶資料 data = new 客戶資料();
            if (TryUpdateModel(data, new string[] { "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email", "客戶分類" }))
            {
                data.電話 = data.電話.Replace("8", "A");

                db.客戶資料.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(data);
        }

        // GET: Data/Edit/5
        [CustomerCategoryDropDownList]
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

            KeepCustomerDataSearchModel();
            return View(客戶資料);
        }

        // POST: Data/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerCategoryDropDownList]
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
            #region 取得要刪除的客戶資料
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            #endregion
            #region 判斷是否有聯絡人，若有則刪除
            if (客戶資料.客戶聯絡人.Any())
            {
                foreach (var contact in 客戶資料.客戶聯絡人)
                {
                    contact.IsDelete = true;
                }
            }
            #endregion
            #region 判斷是否有銀行資訊，若有則刪除
            if (客戶資料.客戶銀行資訊.Any())
            {
                foreach (var bank in 客戶資料.客戶銀行資訊)
                {
                    bank.IsDelete = true;
                }
            }
            #endregion

            客戶資料.IsDelete = true;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
