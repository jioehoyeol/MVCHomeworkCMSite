using CMSite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSite.ActionFilter
{
    public class CustomerCategoryDropDownListAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SelectListItemDataDAO dao = new SelectListItemDataDAO();
            filterContext.Controller.ViewBag.CategoryList = dao.QueryCustomerCategoryList();
            base.OnActionExecuting(filterContext);
        }
    }
}