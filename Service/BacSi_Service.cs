using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Thư viện
using Service.EntityModel;
using System.Security.Cryptography;
using Blog_Guitar.Service;
#endregion
namespace Service
{
    public class BacSi_Service : IService<BacSi>
    {
        public IList<BacSi> GetAll()
        {
            return DbContext.BacSis.Where(bs => bs.TrangThai != false).ToList();
        }
        public BacSi GetById(int id)
        {
            return DbContext.BacSis.FirstOrDefault(bs => bs.TrangThai != false&&bs.id==id);
        }
        public object DeleteById(int id)
        {
            try
            {
                DbContext.BacSis.FirstOrDefault(bs => bs.id == id).TrangThai = false;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public BacSi CreateByModel(BacSi bacsi)
        {
            try
            {
                bacsi.NgayTao = DateTime.Now;
                bacsi.TrangThai = true;
                DbContext.BacSis.Add(bacsi);
                DbContext.SaveChanges();
                return bacsi;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public BacSi UpdateByModel(BacSi bacsi)
        {
            try
            {
                BacSi bacsi_old = GetById(bacsi.id);
                bacsi_old.DiaChiPhongKhamTu = bacsi.DiaChiPhongKhamTu;
                bacsi_old.id_ThanhVien = bacsi.id_ThanhVien;
                bacsi_old.id_LoaiBacSi = bacsi.id_LoaiBacSi;
                bacsi_old.id_BenhVienLamViec = bacsi.id_BenhVienLamViec;
                DbContext.BacSis.Attach(bacsi_old);
                DbContext.Entry(bacsi_old).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
                return bacsi_old;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
