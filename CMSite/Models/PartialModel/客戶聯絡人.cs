using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    [MetadataType(typeof(客戶聯絡人Metadata))]
    public partial class 客戶聯絡人
    {
    }

    public class 客戶聯絡人Metadata
    {
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "職稱不可超過 50 個字元！")]
        public string 職稱 { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "姓名不可超過 50 個字元！")]
        public string 姓名 { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Email不可超過 250 個字元！")]
        [EmailAddress(ErrorMessage = "{0}格式不正確！")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "手機不可超過 50 個字元！")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "電話不可超過 50 個字元！")]
        public string 電話 { get; set; }

        public bool IsDelete { get; set; }
    }
}