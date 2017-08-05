using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Service.EntityModel;
namespace Du_An_Y_Te.Models
{
    public class HealthyViewModels
    {
        public class ThongTinSucKhoe
        {
            public List<float> Giatri { get; set; }
            public List<DateTime> DateRange { get; set; }
            public double CanTren { get; set; }
            public double CanDuoi { get; set; }
            public string LoiKhuyenVuotQuaCanTren { get; set; }
            public string ChuanDoanBenhKhiVuotQuaCanTren { get; set; }
            public string LoiKhuyenVuotQuaCanDuoi { get; set; }
            public string ChuanDoanBenhKhiVuotQuaCanDuoi { get; set; }
        }
        public class HuyetHocViewModel
        {
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
        }
    }
}