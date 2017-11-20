using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    public class CustomerDataViewModel
    {
        public int Id { get; set; }

        [Display(Name="客戶名稱")]
        public string CustomerName { get; set; }

        [Display(Name="統一編號")]
        public string TaxNumber { get; set; }

        [Display(Name = "電話")]
        public string Telephone { get; set; }

        [Display(Name = "傳真")]
        public string Fax { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Display(Name = "客戶分類")]
        public string Category { get; set; }

    }
}