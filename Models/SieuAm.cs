//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SieuAm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SieuAm()
        {
            this.SieuAm_TapTin = new HashSet<SieuAm_TapTin>();
        }
    
        public int id { get; set; }
        public int id_ThanhVien { get; set; }
        public string VungSieuAm { get; set; }
        public string Gan { get; set; }
        public string TuiMat { get; set; }
        public string DuongMat { get; set; }
        public string Tuy { get; set; }
        public string Lach { get; set; }
        public string ThanPhai { get; set; }
        public string ThanTrai { get; set; }
        public string BangQuang { get; set; }
        public string TienLietTuyen { get; set; }
        public string XoangBung { get; set; }
        public string DichOBung { get; set; }
        public string DongMachChuBung { get; set; }
        public string HinhDang { get; set; }
        public string CauTrucSieuAm { get; set; }
        public string TonThuong { get; set; }
        public string HachDuoiHam { get; set; }
        public string TuyenBot { get; set; }
        public string CacBoPhanKhac { get; set; }
        public string KetQua { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> KetQuaNgay { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SieuAm_TapTin> SieuAm_TapTin { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}
