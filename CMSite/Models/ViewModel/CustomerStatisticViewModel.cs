using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    public class CustomerStatisticViewModel
    {
        public int Id { get; set; }

        [Display(Name = "客戶名稱")]
        public string CustomerName { get; set; }

        [Display(Name = "聯絡人數量")]
        public int ContactCount { get; set; }

        [Display(Name = "銀行帳戶數量")]
        public int BankCount { get; set; }
    }
}