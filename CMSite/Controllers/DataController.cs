using ClosedXML.Excel;
using CMSite.Data;
using CMSite.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using CMSite.Extension;
using CMSite.ActionFilter;
using System;

namespace CMSite.Controllers
{
    public class DataController : BaseController
    {
        // GET: Data/Index
        public ActionResult Index(CustomerDataSearchModel searchData)
        {
            #region 排序資料升冪/降冪判斷
            if (!string.IsNullOrEmpty(searchData.SortColumn))
            {
                if (searchData.IsSort)
                {
                    if (searchData.CurrentSortColumn.Equals(searchData.SortColumn))
                    {
                        searchData.SortDirection = searchData.SortDirection.Equals("asc") ? "desc" : "asc";
                    }
                    else
                    {
                        searchData.CurrentSortColumn = searchData.SortColumn;
                        searchData.SortDirection = "asc";
                    }
                    searchData.IsSort = false;
                }
            }
            else
            {
                searchData.CurrentSortColumn = "CustomerName";
                searchData.SortColumn = "CustomerName";
                searchData.SortDirection = "asc";
            }
            #endregion

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

            //轉成Excel
            var workbook = new XLWorkbook();
            workbook.Worksheets.Add(result, "CustomerData");

            #region 寫法二(已註解)
            //var sheet = workbook.Worksheets.Add("CustomerData");
            //int columnIndex = 1;
            //int rowIndex = 1;
            ////標題列
            //string[] titleList = new string[] { "Id", "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email", "客戶分類" };
            //foreach (var title in titleList)
            //{
            //    sheet.Cell(rowIndex, columnIndex).Value = title;
            //    columnIndex++;
            //}
            ////內容列
            //rowIndex = 2;
            //foreach (var data in result)
            //{
            //    sheet.Cell(rowIndex, 1).Value = data.Id;
            //    sheet.Cell(rowIndex, 2).Value = data.CustomerName;
            //    sheet.Cell(rowIndex, 3).Value = data.TaxNumber;
            //    sheet.Cell(rowIndex, 4).Value = data.Telephone;
            //    sheet.Cell(rowIndex, 5).Value = data.Fax;
            //    sheet.Cell(rowIndex, 6).Value = data.Address;
            //    sheet.Cell(rowIndex, 7).Value = data.Email;
            //    sheet.Cell(rowIndex, 8).Value = data.Category;
            //    rowIndex++;
            //}

            //foreach (IXLColumn col in sheet.Columns())
            //{
            //    col.AdjustToContents();
            //}
            #endregion

            MemoryStream exportStream = new MemoryStream();
            workbook.SaveAs(exportStream);
            workbook.Dispose();

            exportStream.Seek(0, SeekOrigin.Begin);
            return File(exportStream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "CustomerData.xlsx");
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
        [CustomerCategoryDropDownList]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Data/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerCategoryDropDownList]
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
            catch(Exception ex)
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
