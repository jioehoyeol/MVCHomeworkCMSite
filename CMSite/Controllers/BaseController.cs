using CMSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected CustomerEntities db = new CustomerEntities();
    }
}