using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using CMSite.Models;
using CMSite.Models.SearchModel;

namespace CMSite.DataAccess
{
    public class CustomerContactDAO : BaseDAO
    {
        public IEnumerable<客戶聯絡人> QueryData(CustomerContactSearchModel search)
        {
            IEnumerable<客戶聯絡人> rtnVal = null;

            var result = db.客戶聯絡人.Where(d => d.IsDelete == false);
            #region 搜尋姓名欄位
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                result = result.Where(o => o.姓名.Contains(search.Keyword));
            }

            #endregion
            #region 篩選職稱
            if (!string.IsNullOrEmpty(search.JobTitleFilter))
            {
                result = result.Where(o => o.職稱 == search.JobTitleFilter);
            }

            #endregion
            #region 排序資料
            if (search.SortColumn.Equals("客戶名稱"))
            {
                if (search.SortDirection.Equals("asc"))
                {
                    result = result.OrderBy(o => o.客戶資料.客戶名稱);
                }
                else
                {
                    result = result.OrderByDescending(o => o.客戶資料.客戶名稱);
                }
            }
            else
            {
                if (search.SortDirection.Equals("asc"))
                {
                    result = result.OrderBy(search.SortColumn);
                }
                else
                {
                    result = result.OrderBy(search.SortColumn + " DESC");
                }
            }

            #endregion
            #region 判斷回傳值
            if (result == null)
            {
                rtnVal = new List<客戶聯絡人>();
            }
            else
            {
                rtnVal = result;
            }
            #endregion

            return rtnVal;
        }

        public IEnumerable<string> QueryJobTitleList()
        {
            return db.客戶聯絡人.Where(w => w.IsDelete == false).Select(r => r.職稱).Distinct();
        }

    }
}