using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    public class CustomerDataSearchModel
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
        /// <summary>
        /// 是否點擊排序動作
        /// </summary>
        public bool IsSort { get; set; }
        /// <summary>
        /// 目前排序欄位
        /// </summary>
        public string CurrentSortColumn { get; set; }
        /// <summary>
        /// 排序欄位
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序方向(asc=正序,desc=倒序)
        /// </summary>
        public string SortDirection { get; set; }

        public IEnumerable<CustomerDataViewModel> DataModel { get; set; }
    }
}