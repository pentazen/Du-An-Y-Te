//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TapTin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TapTin()
        {
            this.BaiViets = new HashSet<BaiViet>();
            this.BenhAn_TapTin = new HashSet<BenhAn_TapTin>();
        }
    
        public int id { get; set; }
        public string TenTapTin { get; set; }
        public string LoaiTapTin { get; set; }
        public string DuongDan { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<double> KichThuoc { get; set; }
        public bool TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BenhAn_TapTin> BenhAn_TapTin { get; set; }
    }
}