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
    
    public partial class SinhHoaMau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhHoaMau()
        {
            this.SinhHoaMau_TapTin = new HashSet<SinhHoaMau_TapTin>();
        }
    
        public int id { get; set; }
        public int id_ThanhVien { get; set; }
        public Nullable<double> DuongHuyetLucDoi { get; set; }
        public Nullable<double> DuongHuyetSauKhiAn { get; set; }
        public Nullable<double> Cholesterol { get; set; }
        public Nullable<double> Cholesterol_HDL { get; set; }
        public Nullable<double> Triglycerides { get; set; }
        public Nullable<double> TinhTrangHuyetThanh { get; set; }
        public Nullable<double> Cholesterol_LDL { get; set; }
        public Nullable<double> Urea { get; set; }
        public Nullable<double> Creatinine { get; set; }
        public Nullable<double> ACIT_URIC { get; set; }
        public Nullable<double> ALAT { get; set; }
        public Nullable<double> ASAT { get; set; }
        public string KetQua { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> KetQuaNgay { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SinhHoaMau_TapTin> SinhHoaMau_TapTin { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}