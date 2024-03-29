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
    
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            this.BacSis = new HashSet<BacSi>();
            this.BaiViets = new HashSet<BaiViet>();
            this.BenhAns = new HashSet<BenhAn>();
            this.ChatPrivateMessageDetails = new HashSet<ChatPrivateMessageDetail>();
            this.ChatPrivateMessageDetails1 = new HashSet<ChatPrivateMessageDetail>();
            this.ChatUserDetails = new HashSet<ChatUserDetail>();
            this.ChatUserDetailsRooms = new HashSet<ChatUserDetailsRoom>();
            this.NoiDungTinNhanPhongThaoLuans = new HashSet<NoiDungTinNhanPhongThaoLuan>();
        }
    
        public int id { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string MatKhauMaHoa { get; set; }
        public string MaKichHoat { get; set; }
        public decimal TienAo { get; set; }
        public decimal TienThat { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string TenDayDu { get; set; }
        public Nullable<System.DateTime> NTNS { get; set; }
        public Nullable<int> CMND { get; set; }
        public Nullable<int> SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Picture { get; set; }
        public Nullable<double> ChieuCao { get; set; }
        public Nullable<double> CanNang { get; set; }
        public Nullable<int> MaNhomMau { get; set; }
        public int id_LoaiThanhVien { get; set; }
        public bool TrangThai { get; set; }
        public Nullable<System.DateTime> LanCuoiDangNhap { get; set; }
        public Nullable<System.DateTime> LanCuoiDangXuat { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<System.DateTime> NgaySua { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BacSi> BacSis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BenhAn> BenhAns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatPrivateMessageDetail> ChatPrivateMessageDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatPrivateMessageDetail> ChatPrivateMessageDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatUserDetail> ChatUserDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatUserDetailsRoom> ChatUserDetailsRooms { get; set; }
        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
        public virtual NhomMau NhomMau { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoiDungTinNhanPhongThaoLuan> NoiDungTinNhanPhongThaoLuans { get; set; }
    }
}
