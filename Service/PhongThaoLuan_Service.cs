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
    public class PhongThaoLuan_Service : IService
    {
        public IList<PhongThaoLuan> GetAll()
        {
            return DuAnYTeEntitiesFramework.PhongThaoLuans.Where(bs => bs.TrangThai != false).ToList();
        }
        public PhongThaoLuan GetById(int id)
        {
            return DuAnYTeEntitiesFramework.PhongThaoLuans.FirstOrDefault(bs => bs.TrangThai != false&&bs.id==id);
        }
        public bool DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.PhongThaoLuans.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
