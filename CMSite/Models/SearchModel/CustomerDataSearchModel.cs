using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.Models.SearchModel
{
    public class CustomerDataSearchModel : SortModel
    {
        /// <summary>
        /// 關鍵字查詢分類
        /// </summary>
        public string KeywordKind { get; set; }
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 客戶分類選項
        /// </summary>
        public string CustomerTypeFilter { get; set; }

        public IEnumerable<CustomerDataViewModel> DataModel { get; set; }
    }
}