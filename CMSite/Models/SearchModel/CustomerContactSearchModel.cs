using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.Models.SearchModel
{
    public class CustomerContactSearchModel : SortModel
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 職稱選項
        /// </summary>
        public string JobTitleFilter { get; set; }

        public IEnumerable<客戶聯絡人> DataModel { get; set; }
    }
}