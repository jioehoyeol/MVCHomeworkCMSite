﻿using CMSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.DataAccess
{
    public class BaseDAO
    {
        protected CustomerEntities db = new CustomerEntities();
    }
}