﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DuAnYTeEntities : DbContext
    {
        public DuAnYTeEntities()
            : base("name=DuAnYTeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BacSi> BacSis { get; set; }
        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<BenhAn> BenhAns { get; set; }
        public virtual DbSet<BenhAn_LoaiXetNghiem> BenhAn_LoaiXetNghiem { get; set; }
        public virtual DbSet<BenhAn_TapTin> BenhAn_TapTin { get; set; }
        public virtual DbSet<BenhVien> BenhViens { get; set; }
        public virtual DbSet<ChatPrivateMessageDetail> ChatPrivateMessageDetails { get; set; }
        public virtual DbSet<ChatUserDetail> ChatUserDetails { get; set; }
        public virtual DbSet<ChatUserDetailsRoom> ChatUserDetailsRooms { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<LoaiBacSi> LoaiBacSis { get; set; }
        public virtual DbSet<LoaiPhongThaoLuan> LoaiPhongThaoLuans { get; set; }
        public virtual DbSet<LoaiThanhVien> LoaiThanhViens { get; set; }
        public virtual DbSet<LoaiXetNghiem> LoaiXetNghiems { get; set; }
        public virtual DbSet<MauBenhAn> MauBenhAns { get; set; }
        public virtual DbSet<MauBenhAn_LoaiXetNghiem> MauBenhAn_LoaiXetNghiem { get; set; }
        public virtual DbSet<NhomMau> NhomMaus { get; set; }
        public virtual DbSet<NoiDungTinNhanPhongThaoLuan> NoiDungTinNhanPhongThaoLuans { get; set; }
        public virtual DbSet<PhanHoi> PhanHois { get; set; }
        public virtual DbSet<PhongThaoLuan> PhongThaoLuans { get; set; }
        public virtual DbSet<TapTin> TapTins { get; set; }
        public virtual DbSet<ThangDoiChieu> ThangDoiChieux { get; set; }
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
    }
}
