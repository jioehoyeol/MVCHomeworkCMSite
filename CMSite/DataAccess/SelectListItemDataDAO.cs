using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSite.DataAccess
{
    public class SelectListItemDataDAO : BaseDAO
    {
        public static List<SelectListItem> QueryKeywordKindList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem() { Text= "客戶名稱", Value="0" },
                new SelectListItem() { Text= "統一編號", Value="1" },
                new SelectListItem() { Text= "電話", Value="2" },
                new SelectListItem() { Text= "傳真", Value="3" },
                new SelectListItem() { Text= "地址", Value="4" },
                new SelectListItem() { Text= "Email", Value="5" },
                new SelectListItem() { Text= "客戶分類", Value="6" }
            };
        }

        public IEnumerable<SelectListItem> QueryCustomerCategoryList()
        {
            return from c in db.Control
                   where c.TypeNo == "Category"
                   orderby c.KeyNo
                   select new SelectListItem
                   {
                       Text = c.Value,
                       Value = c.KeyNo
                   };
        }
    }
}