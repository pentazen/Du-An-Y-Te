using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class ThangDoiChieu_Service : IService
    {
        public List<Models.ThangDoiChieu> GetAll()
        {
            return DuAnYTeEntitiesFramework.ThangDoiChieux.AsEnumerable().Where(tdc => tdc.TrangThai == true).ToList() ;
        }
        public Models.ThangDoiChieu GetById(int id)
        {
            return DuAnYTeEntitiesFramework.ThangDoiChieux.FirstOrDefault(tdc => tdc.TrangThai == true&&tdc.id==id);
        }
        public Models.ThangDoiChieu LayTheoIdLoaiXetNghiem(int id)
        {
            return DuAnYTeEntitiesFramework.ThangDoiChieux.ToList().FirstOrDefault(x=>x.id_LoaiXetNghiem==id);
        }
        public Models.ThangDoiChieu CreateByModel(Models.ThangDoiChieu ThangDoiChieu)
        {
            ThangDoiChieu.TrangThai = true;
            ThangDoiChieu.NgayTao = DateTime.Now;
            DuAnYTeEntitiesFramework.ThangDoiChieux.Add(ThangDoiChieu);
            DuAnYTeEntitiesFramework.SaveChanges();
            return ThangDoiChieu;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.ThangDoiChieux.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public Models.ThangDoiChieu UpdateByModel(Models.ThangDoiChieu thangdoichieu,Models.LoaiXetNghiem loaixetnghiem)
        {
            Models.ThangDoiChieu thangdoichieu_old = DuAnYTeEntitiesFramework.ThangDoiChieux.FirstOrDefault(tdc => tdc.id == thangdoichieu.id);
            thangdoichieu_old.CanDuoi = thangdoichieu.CanDuoi;
            thangdoichieu_old.CanTren = thangdoichieu.CanTren;
            thangdoichieu_old.DonVi = thangdoichieu.DonVi;

            thangdoichieu_old.ChuanDoanBenhKhiVuotQuaCanDuoi = thangdoichieu.ChuanDoanBenhKhiVuotQuaCanDuoi;
            thangdoichieu_old.ChuanDoanBenhKhiVuotQuaCanTren = thangdoichieu.ChuanDoanBenhKhiVuotQuaCanTren;
            thangdoichieu_old.LoiKhuyenVuotQuaCanDuoi = thangdoichieu.LoiKhuyenVuotQuaCanDuoi;
            thangdoichieu_old.LoiKhuyenVuotQuaCanTren = thangdoichieu.LoiKhuyenVuotQuaCanTren;
            DuAnYTeEntitiesFramework.SaveChanges();

            Models.LoaiXetNghiem loaixetnghiem_old = DuAnYTeEntitiesFramework.LoaiXetNghiems.FirstOrDefault(tdc => tdc.id == thangdoichieu_old.id_LoaiXetNghiem);
            loaixetnghiem_old.TenLoaiXetNghiem = loaixetnghiem.TenLoaiXetNghiem;
            loaixetnghiem_old.TenHienThi = loaixetnghiem.TenHienThi;
            loaixetnghiem_old.GhiChu = loaixetnghiem.GhiChu;
            DuAnYTeEntitiesFramework.SaveChanges();
            return thangdoichieu_old;
        }
    }
}
