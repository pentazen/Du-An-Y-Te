using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.EntityModel;
namespace Du_An_Y_Te.Areas.Admin.Models
{
    public class TaiKhoanCreateViewModel
    {
        public List<LoaiThanhVien> DanhSachLoaiThanhVien { get; set; }
        public List<NhomMau> DanhSachNhomMau { get; set; }
    }
    public class TaiKhoanEditViewModel
    {
        public List<LoaiThanhVien> DanhSachLoaiThanhVien { get; set; }
        public List<NhomMau> DanhSachNhomMau { get; set; }
        public ThanhVien ThanhVien { get; set; }
    }
}