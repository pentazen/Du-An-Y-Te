using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Thư viện
using Service.EntityModel;
using System.Security.Cryptography;
#endregion
namespace Service
{
    public class BacSi_Service : IService
    {
        public IList<BacSi> GetAll()
        {
            return DuAnYTeEntitiesFramework.BacSis.Where(bs => bs.TrangThai != false).ToList();
        }
        public BacSi GetById(int id)
        {
            return DuAnYTeEntitiesFramework.BacSis.FirstOrDefault(bs => bs.TrangThai != false&&bs.id==id);
        }
        public object DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.BacSis.FirstOrDefault(bs => bs.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
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
                DuAnYTeEntitiesFramework.BacSis.Add(bacsi);
                DuAnYTeEntitiesFramework.SaveChanges();
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
                DuAnYTeEntitiesFramework.BacSis.Attach(bacsi_old);
                DuAnYTeEntitiesFramework.Entry(bacsi_old).State = System.Data.Entity.EntityState.Modified;
                DuAnYTeEntitiesFramework.SaveChanges();
                return bacsi_old;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
