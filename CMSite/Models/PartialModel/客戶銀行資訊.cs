using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSite.Models
{
    [MetadataType(typeof(客戶銀行資訊Metadata))]
    public partial class 客戶銀行資訊
    {
    }

    public class 客戶銀行資訊Metadata
    {
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "銀行名稱不可超過 50 個字元！")]
        public string 銀行名稱 { get; set; }

        [Required]
        public int 銀行代碼 { get; set; }

        public Nullable<int> 分行代碼 { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "帳戶名稱不可超過 50 個字元！")]
        public string 帳戶名稱 { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "帳戶號碼不可超過 20 個字元！")]
        public string 帳戶號碼 { get; set; }

        public bool IsDelete { get; set; }
    }
}