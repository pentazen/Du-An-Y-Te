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
    public class PhongThaoLuan_Service : IService<PhongThaoLuan>
    {
        public IList<PhongThaoLuan> GetAll()
        {
            return DbContext.PhongThaoLuans.Where(bs => bs.TrangThai != false).ToList();
        }
        public PhongThaoLuan GetById(int id)
        {
            return DbContext.PhongThaoLuans.FirstOrDefault(bs => bs.TrangThai != false&&bs.id==id);
        }
        public bool DeleteById(int id)
        {
            try
            {
                DbContext.PhongThaoLuans.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
