using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CMSite.Models;

namespace CMSite.Data
{
    public class CustomerDataDAO : BaseDAO
    {
        /// <summary>
        /// 依照搜尋條件，查詢資料
        /// </summary>
        /// <param name="searchData"></param>
        /// <returns></returns>
        public IEnumerable<CustomerDataViewModel> QueryData(CustomerDataSearchModel searchData)
        {
            IEnumerable<CustomerDataViewModel> rtnVal = null;

            #region 取得完整資料
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
            #endregion
            #region 關鍵字查詢
            if (!string.IsNullOrEmpty(searchData.KeywordKind)
                && !string.IsNullOrEmpty(searchData.Keyword))
            {
                switch (searchData.KeywordKind)
                {
                    case "0":
                        result = result.Where(e => e.CustomerName.Contains(searchData.Keyword));
                        break;
                    case "1":
                        result = result.Where(e => e.TaxNumber.Contains(searchData.Keyword));
                        break;
                    case "2":
                        result = result.Where(e => e.Telephone.Contains(searchData.Keyword));
                        break;
                    case "3":
                        result = result.Where(e => e.Fax.Contains(searchData.Keyword));
                        break;
                    case "4":
                        result = result.Where(e => e.Address.Contains(searchData.Keyword));
                        break;
                    case "5":
                        result = result.Where(e => e.Email.Contains(searchData.Keyword));
                        break;
                    case "6":
                        result = result.Where(e => e.Category.Contains(searchData.Keyword));
                        break;
                    default:
                        break;
                }
            }
            #endregion
            #region 客戶分類篩選
            if (!string.IsNullOrEmpty(searchData.CustomerTypeFilter))
            {
                result = result.Where(e => e.Category.Equals(searchData.CustomerTypeFilter));
            }
            #endregion
            #region 排序資料
            if (!string.IsNullOrEmpty(searchData.SortDirection))
            {
                if (searchData.SortDirection.Equals("asc"))
                {
                    result = result.OrderBy(searchData.SortColumn);
                }
                else
                {
                    result = result.OrderBy(searchData.SortColumn + " DESC");
                }
            }
            #endregion
            #region 判斷回傳值
            if (result == null)
            {
                rtnVal = new List<CustomerDataViewModel>();
            }
            else
            {
                rtnVal = result;
            }
            #endregion

            return rtnVal;
        }

        /// <summary>
        /// 取得客戶的聯絡人與銀行數量統計資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerStatisticViewModel> QueryStatisticData()
        {
            return from p in db.客戶資料
                   where !p.IsDelete
                   select new CustomerStatisticViewModel
                   {
                       Id = p.Id,
                       CustomerName = p.客戶名稱,
                       ContactCount = p.客戶聯絡人.Where(d => !d.IsDelete).Count(),
                       BankCount = p.客戶銀行資訊.Where(d => !d.IsDelete).Count()
                   };
        }

    }
}