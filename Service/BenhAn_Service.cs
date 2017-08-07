using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Models = Service.EntityModel;
namespace Service
{
    public class BenhAn_Service : IService<Models.BenhAn>
    {
        public Models.BenhAn CapNhatBenhAnChoBenhNhan(Models.BenhAn benhAn)
        {
            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    string km = ex.Message;
            //    return null;
            //}
            DbContext.BenhAns.Add(benhAn);
            DbContext.SaveChanges();
            return benhAn;
        }
        public List<Models.BenhAn> LayDanhSachBenhAnTheoIdThangVien(int idThanhVien)
        {
            List<Models.BenhAn> DanhSachBenhAn = (from item in DbContext.BenhAns.AsEnumerable()
                                                  where item.id_ThanhVien_SoHuu == idThanhVien
                                                  select item).ToList();
            //foreach (Models.BenhAn item in DanhSachBenhAn)
            //{
            //    item.BenhAn_LoaiXetNghiem = (from BenhAnLoaiXetNghiemItem in DbContext.BenhAn_LoaiXetNghiem.AsEnumerable()
            //                                where BenhAnLoaiXetNghiemItem.id_BenhAn == item.id
            //                                select BenhAnLoaiXetNghiemItem).ToList();
            //    DuLieuTraVe.Add(item);
            //}
            return DanhSachBenhAn;
        }
        public Models.BenhAn LayBenhAnTheoIdThanhVienVaIdBenhAn(int idThanhVien, int idBenhAn)
        {
            Models.BenhAn DanhSachBenhAn = (from item in DbContext.BenhAns.AsEnumerable()
                                                  where item.id_ThanhVien_SoHuu == idThanhVien && item.TrangThai!=false
                                                  select item).ToList().FirstOrDefault(ba=>ba.id== idBenhAn);
            //foreach (Models.BenhAn item in DanhSachBenhAn)
            //{
            //    item.BenhAn_LoaiXetNghiem = (from BenhAnLoaiXetNghiemItem in DbContext.BenhAn_LoaiXetNghiem.AsEnumerable()
            //                                where BenhAnLoaiXetNghiemItem.id_BenhAn == item.id
            //                                select BenhAnLoaiXetNghiemItem).ToList();
            //    DuLieuTraVe.Add(item);
            //}
            return DanhSachBenhAn;
        }

        public bool UpdateBenhAn_LoaiXetNghiem(int idThanhVien,Models.BenhAn benhAnMoi, List<Models.BenhAn_LoaiXetNghiem> dsLoaiXetNghiem)
        {
            Models.BenhAn benhan = LayBenhAnTheoIdThanhVienVaIdBenhAn(idThanhVien, benhAnMoi.id);
            benhan.KetQua = benhAnMoi.KetQua;
            benhan.GhiChu= benhAnMoi.GhiChu;
            benhan.KetQuaNgay = benhAnMoi.KetQuaNgay;
            for (int i = 0; i < dsLoaiXetNghiem.Count; i++)
            {
                benhan.BenhAn_LoaiXetNghiem.FirstOrDefault(lxn => lxn.id_LoaiXetNghiem == dsLoaiXetNghiem[i].id_LoaiXetNghiem).GiaTri = dsLoaiXetNghiem[i].GiaTri;
            }
            DbContext.BenhAns.Attach(benhan);
            DbContext.Entry(benhan).State = System.Data.Entity.EntityState.Modified;

            //DbContext.ThanhViens.Change
            DbContext.SaveChanges();
            return true;
        }
    }
}
