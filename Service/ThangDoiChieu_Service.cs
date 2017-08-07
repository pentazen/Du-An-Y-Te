using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class ThangDoiChieu_Service : IService<Models.ThangDoiChieu>
    {
        public List<Models.ThangDoiChieu> GetAll()
        {
            return DbContext.ThangDoiChieux.AsEnumerable().Where(tdc => tdc.TrangThai == true).ToList() ;
        }
        public Models.ThangDoiChieu GetById(int id)
        {
            return DbContext.ThangDoiChieux.FirstOrDefault(tdc => tdc.TrangThai == true&&tdc.id==id);
        }
        public Models.ThangDoiChieu LayTheoIdLoaiXetNghiem(int id)
        {
            return DbContext.ThangDoiChieux.ToList().FirstOrDefault(x=>x.id_LoaiXetNghiem==id);
        }
        public Models.ThangDoiChieu CreateByModel(Models.ThangDoiChieu ThangDoiChieu)
        {
            ThangDoiChieu.TrangThai = true;
            ThangDoiChieu.NgayTao = DateTime.Now;
            DbContext.ThangDoiChieux.Add(ThangDoiChieu);
            DbContext.SaveChanges();
            return ThangDoiChieu;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DbContext.ThangDoiChieux.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public Models.ThangDoiChieu UpdateByModel(Models.ThangDoiChieu thangdoichieu,Models.LoaiXetNghiem loaixetnghiem)
        {
            Models.ThangDoiChieu thangdoichieu_old = DbContext.ThangDoiChieux.FirstOrDefault(tdc => tdc.id == thangdoichieu.id);
            thangdoichieu_old.CanDuoi = thangdoichieu.CanDuoi;
            thangdoichieu_old.CanTren = thangdoichieu.CanTren;
            thangdoichieu_old.DonVi = thangdoichieu.DonVi;

            thangdoichieu_old.ChuanDoanBenhKhiVuotQuaCanDuoi = thangdoichieu.ChuanDoanBenhKhiVuotQuaCanDuoi;
            thangdoichieu_old.ChuanDoanBenhKhiVuotQuaCanTren = thangdoichieu.ChuanDoanBenhKhiVuotQuaCanTren;
            thangdoichieu_old.LoiKhuyenVuotQuaCanDuoi = thangdoichieu.LoiKhuyenVuotQuaCanDuoi;
            thangdoichieu_old.LoiKhuyenVuotQuaCanTren = thangdoichieu.LoiKhuyenVuotQuaCanTren;
            DbContext.SaveChanges();

            Models.LoaiXetNghiem loaixetnghiem_old = DbContext.LoaiXetNghiems.FirstOrDefault(tdc => tdc.id == thangdoichieu_old.id_LoaiXetNghiem);
            loaixetnghiem_old.TenLoaiXetNghiem = loaixetnghiem.TenLoaiXetNghiem;
            loaixetnghiem_old.TenHienThi = loaixetnghiem.TenHienThi;
            loaixetnghiem_old.GhiChu = loaixetnghiem.GhiChu;
            DbContext.SaveChanges();
            return thangdoichieu_old;
        }
    }
}
