namespace CMSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ControlMetaData))]
    public partial class Control
    {
    }
    
    public partial class ControlMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(30, ErrorMessage="欄位長度不得大於 30 個字元")]
        [Required]
        public string TypeNo { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string KeyNo { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        [Required]
        public string Value { get; set; }
    }
}
