using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    [MetadataType(typeof(客戶資料Metadata))]
    public partial class 客戶資料
    {

    }

    public class 客戶資料Metadata
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0}不可超過 50 個字元！")]
        public string 客戶名稱 { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "{0}不可超過 8 碼！")]
        public string 統一編號 { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0}不可超過 50 個字元！")]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "{0}不可超過 50 個字元！")]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "{0}不可超過 100 個字元！")]
        public string 地址 { get; set; }

        [StringLength(250, ErrorMessage = "{0}不可超過 250 個字元！")]
        [EmailAddress(ErrorMessage = "{0}格式不正確")]
        public string Email { get; set; }

        [Required]
        //[StringLength(50, ErrorMessage = "{0}不可超過 100 個字元！")]
        public string 客戶分類 { get; set; }

        public bool IsDelete { get; set; }
    }
}