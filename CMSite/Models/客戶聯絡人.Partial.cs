namespace CMSite.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (CustomerEntities db = new CustomerEntities())
            {
                if (db.客戶聯絡人.Any(o => o.Id != this.Id && o.客戶Id == this.客戶Id && o.Email == this.Email))
                {
                    yield return new ValidationResult(
                                    "輸入的 Email 已存在，請重新輸入！",
                                    new string[] { "Email" });
                }
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Phone]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
