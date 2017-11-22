using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMSite.Models
{
    [MetadataType(typeof(客戶銀行資訊MetaData))]
    public partial class 客戶銀行資訊 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (CustomerEntities db = new CustomerEntities())
            {
                if (db.客戶銀行資訊.Any(o => o.Id != this.Id && o.客戶Id == this.客戶Id
                                        && o.銀行代碼 == this.銀行代碼 && o.帳戶號碼 == this.帳戶號碼 
                                        && o.IsDelete == false))
                {
                    yield return new ValidationResult(
                                    "輸入的[帳戶號碼]已存在，請重新輸入！",
                                    new string[] { "帳戶號碼" });
                }
            }
        }
    }

    public partial class 客戶銀行資訊MetaData
    {
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }

        [Required]
        public int 銀行代碼 { get; set; }

        public Nullable<int> 分行代碼 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
