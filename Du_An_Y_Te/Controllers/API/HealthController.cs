using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Service.EntityModel;
using Service;
using Du_An_Y_Te.Models;
using Newtonsoft.Json;

namespace Du_An_Y_Te.Controllers.API
{
    public class HealthController : ApiController
    {
        Service.BenhAn_Service BenhAnService = new BenhAn_Service();
        Service.BenhAn_LoaiXetNghiem_Service BenhAnLoaiXetNghiemService = new BenhAn_LoaiXetNghiem_Service();
        Service.ThangDoiChieu_Service ThangDoiChieuService = new ThangDoiChieu_Service();
        [HttpPost]
        public BenhAn CapNhatBenhAn(BenhAn benhAn)
        {
            return BenhAnService.CapNhatBenhAnChoBenhNhan(benhAn);
        }
        public List<BenhAn_LoaiXetNghiem> CapNhatBenhAn_LoaiXetNghiem(BenhAn benhAn, List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem)
        {
            return BenhAnLoaiXetNghiemService.CapNhatLoaiXetNghiemChoBenhAn(benhAn, DanhSachBenhAnLoaiXetNghiem);
        }
        public List<ThangDoiChieu> GetAllThangDoiChieu()
        {
            return ThangDoiChieuService.GetAll();
        }
        [Route("api/Health/LayGiaTriThangDoiChieuVaGiaTriLoaiXetNghiem")]
        [HttpGet]
        public object LayGiaTriThangDoiChieuVaGiaTriLoaiXetNghiem(int idThanhVien, int idThongSo)
        {
            List<BenhAn> DSBenhAn = BenhAnService.LayDanhSachBenhAnTheoIdThangVien(idThanhVien);
            if (DSBenhAn != null)
            {
                DSBenhAn = (from item in DSBenhAn
                            where (
                            (from BenhAnLoaiXetNghiemItem in item.BenhAn_LoaiXetNghiem
                             where BenhAnLoaiXetNghiemItem.id_LoaiXetNghiem == idThongSo && item.TrangThai == true
                             orderby item.KetQuaNgay
                             select BenhAnLoaiXetNghiemItem).ToList().Count > 0
                            )
                            select item).ToList();
                DSBenhAn = DSBenhAn.OrderBy(x => x.KetQuaNgay).ToList();
                List<float> Giatri = new List<float>();
                List<DateTime> DateRange = new List<DateTime>();
                foreach (var item in DSBenhAn)
                {

                    if (item.KetQuaNgay != null)
                    {
                        var kk = (from item3 in item.BenhAn_LoaiXetNghiem.ToList()
                                  where item3.id_LoaiXetNghiem == idThongSo
                                  select item3).FirstOrDefault();

                        if (kk != null)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(kk.GiaTri) && !string.IsNullOrWhiteSpace(kk.GiaTri))
                                {
                                    DateRange.Add(item.KetQuaNgay);
                                    Giatri.Add(float.Parse(kk.GiaTri));
                                }
                            }
                            catch (Exception ex)
                            {
                                return false;
                            }

                        }
                    }

                }
                HealthyViewModels.ThongTinSucKhoe KetQuaTraVe = new HealthyViewModels.ThongTinSucKhoe();
                KetQuaTraVe.Giatri = Giatri;
                KetQuaTraVe.DateRange = DateRange;

                ThangDoiChieu ThangDoiChieuThongSo = ThangDoiChieuService.LayTheoIdLoaiXetNghiem(idThongSo);
                if (ThangDoiChieuThongSo.CanTren.HasValue)
                    KetQuaTraVe.CanTren = ThangDoiChieuThongSo.CanTren.Value;
                if (ThangDoiChieuThongSo.CanDuoi.HasValue)
                    KetQuaTraVe.CanDuoi = ThangDoiChieuThongSo.CanDuoi.Value;
                KetQuaTraVe.LoiKhuyenVuotQuaCanTren = ThangDoiChieuThongSo.LoiKhuyenVuotQuaCanTren;
                KetQuaTraVe.ChuanDoanBenhKhiVuotQuaCanTren = ThangDoiChieuThongSo.ChuanDoanBenhKhiVuotQuaCanTren;
                KetQuaTraVe.LoiKhuyenVuotQuaCanDuoi = ThangDoiChieuThongSo.LoiKhuyenVuotQuaCanDuoi;
                KetQuaTraVe.ChuanDoanBenhKhiVuotQuaCanDuoi = ThangDoiChieuThongSo.ChuanDoanBenhKhiVuotQuaCanDuoi;
                return Json(KetQuaTraVe);



                ;
            }
            return false;
        }
        public BenhAn LayBenhAnTheoIdThanhVienVaIdBenhAn(int id, int idBenhAn)
        {
            return BenhAnService.LayBenhAnTheoIdThanhVienVaIdBenhAn(id, idBenhAn);
        }
        public bool UpdateBenhAn_LoaiXetNghiem(int idThanhVien, BenhAn benhAn, List<BenhAn_LoaiXetNghiem> dsLoaiXetNghiem)
        {
            return BenhAnService.UpdateBenhAn_LoaiXetNghiem(idThanhVien, benhAn, dsLoaiXetNghiem);
        }
    }
}
