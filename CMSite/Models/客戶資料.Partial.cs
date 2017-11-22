using CMSite.Models.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMSite.Models
{
    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (CustomerEntities db = new CustomerEntities())
            {
                if (db.客戶資料.Any(o => o.Id != this.Id && o.Email == this.Email && o.IsDelete == false))
                {
                    yield return new ValidationResult(
                                    "輸入的 Email 已存在，請重新輸入！",
                                    new string[] { "Email" });
                }
            }
        }
    }

    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "欄位長度不得大於 8 個字元")]
        [Range(0, int.MaxValue, ErrorMessage = "請輸入數字")]
        [VATNumber]
        public string 統一編號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        [Phone]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Phone]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 客戶分類 { get; set; }

        public bool IsDelete { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
