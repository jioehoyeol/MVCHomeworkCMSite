using CMSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSite.Models.SearchModel;
using System.Data;

namespace CMSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected CustomerEntities db = new CustomerEntities();

        protected void ModelSort(string defaultColumn, SortModel model)
        {
            if (!string.IsNullOrEmpty(model.SortColumn))
            {
                if (model.IsSort)
                {
                    if (model.CurrentSortColumn.Equals(model.SortColumn))
                    {
                        model.SortDirection = model.SortDirection.Equals("asc") ? "desc" : "asc";
                    }
                    else
                    {
                        model.CurrentSortColumn = model.SortColumn;
                        model.SortDirection = "asc";
                    }
                    model.IsSort = false;
                }
            }
            else
            {
                model.CurrentSortColumn = defaultColumn;
                model.SortColumn = defaultColumn;
                model.SortDirection = "asc";
            }

        }

        protected FileResult ExportExcel(DataTable dt, string fileName)
        {
            var workbook = new ClosedXML.Excel.XLWorkbook();
            workbook.Worksheets.Add(dt, fileName);

            var exportStream = new System.IO.MemoryStream();
            workbook.SaveAs(exportStream);
            workbook.Dispose();

            exportStream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(exportStream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName + ".xlsx");
        }

        protected void KeepCustomerDataSearchModel()
        {
            if (TempData["CustomerDataSearchModel"] == null)
            {
                var model = new CustomerDataSearchModel();
                if (TryUpdateModel(model))
                {
                    TempData["CustomerDataSearchModel"] = model;
                }
            }
        }

        //這個沒用到
        private void ExportExcel(string data, string fileName)
        {
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
        }
    }
}