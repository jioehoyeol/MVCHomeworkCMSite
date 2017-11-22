using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.Models.SearchModel
{
    public class SortModel
    {
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
    }
}