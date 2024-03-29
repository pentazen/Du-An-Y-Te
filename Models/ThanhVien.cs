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
    
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            this.HuyetHocs = new HashSet<HuyetHoc>();
            this.NuocTieux = new HashSet<NuocTieu>();
            this.SieuAms = new HashSet<SieuAm>();
            this.SinhHoaMaus = new HashSet<SinhHoaMau>();
        }
    
        public int id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string TenDayDu { get; set; }
        public Nullable<System.DateTime> NTNS { get; set; }
        public Nullable<int> CMND { get; set; }
        public Nullable<int> SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Picture { get; set; }
        public Nullable<bool> TrangThai { get; set; }
        public Nullable<System.DateTime> LanCuoiDangNhap { get; set; }
        public Nullable<System.DateTime> LanCuoiDangXuat { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgaySua { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HuyetHoc> HuyetHocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NuocTieu> NuocTieux { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SieuAm> SieuAms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SinhHoaMau> SinhHoaMaus { get; set; }
    }
}
