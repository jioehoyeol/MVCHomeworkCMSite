using CMSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.Data
{
    public class BaseDAO
    {
        protected CustomerEntities db = new CustomerEntities();
    }
}