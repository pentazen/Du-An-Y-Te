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
    
    public partial class BenhAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhAn()
        {
            this.BenhAn_LoaiXetNghiem = new HashSet<BenhAn_LoaiXetNghiem>();
            this.BenhAn_TapTin = new HashSet<BenhAn_TapTin>();
        }
    
        public int id { get; set; }
        public int id_MauBenhAn { get; set; }
        public int id_ThanhVien_SoHuu { get; set; }
        public string NoiKham { get; set; }
        public string BacSiKham { get; set; }
        public string KetQua { get; set; }
        public string GhiChu { get; set; }
        public System.DateTime KetQuaNgay { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public bool TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BenhAn_LoaiXetNghiem> BenhAn_LoaiXetNghiem { get; set; }
        public virtual MauBenhAn MauBenhAn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BenhAn_TapTin> BenhAn_TapTin { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}
