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
    
    public partial class HuyetHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HuyetHoc()
        {
            this.HuyetHoc_TapTin = new HashSet<HuyetHoc_TapTin>();
        }
    
        public int id { get; set; }
        public int id_ThanhVien { get; set; }
        public Nullable<double> BachCau { get; set; }
        public Nullable<double> HongCau { get; set; }
        public Nullable<double> HuyetSacTo { get; set; }
        public Nullable<double> DungTichHongCau { get; set; }
        public Nullable<double> TheTichTrungBinhCuaMotHongCau { get; set; }
        public Nullable<double> SoLuongHuyetSacToTrungBinhTrongMotHongCau { get; set; }
        public Nullable<double> NongDoTrungBinhHuyetSacToTrongMotHongCau { get; set; }
        public Nullable<double> RDW_SD { get; set; }
        public Nullable<double> RDW_CV { get; set; }
        public Nullable<double> DaNhanTrungTin_PhanTram { get; set; }
        public Nullable<double> DaNhanTrungTin_MM3 { get; set; }
        public Nullable<double> DaNhanAiToan_PhanTram { get; set; }
        public Nullable<double> DaNhanAiToan_MM3 { get; set; }
        public Nullable<double> DaNhanAiKiem_PhanTram { get; set; }
        public Nullable<double> DaNhanAiKiem_MM3 { get; set; }
        public Nullable<double> BachCauLympho_PhanTram { get; set; }
        public Nullable<double> BachCauLympho_MM3 { get; set; }
        public Nullable<double> DonNhan_PhanTram { get; set; }
        public Nullable<double> DonNhan_MM3 { get; set; }
        public Nullable<double> TieuCau { get; set; }
        public Nullable<double> ALPHA_FOETOPROTEINE { get; set; }
        public Nullable<double> TheTichTrungBinhTieuCauTrongMotTheTichMau { get; set; }
        public Nullable<double> DaiPhanBoKichThuocTieuCau { get; set; }
        public Nullable<double> TyLeProthrombin { get; set; }
        public Nullable<double> ThoiGianThromboplastinTungPhan { get; set; }
        public Nullable<double> Fibrinogen { get; set; }
        public string KetQua { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> KetQuaNgay { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HuyetHoc_TapTin> HuyetHoc_TapTin { get; set; }
        public virtual ThanhVien ThanhVien { get; set; }
    }
}