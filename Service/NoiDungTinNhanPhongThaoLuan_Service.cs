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
   public class NoiDungTinNhanPhongThaoLuan_Service : IService
    {
        public NoiDungTinNhanPhongThaoLuan GetById(int id)
        {
            return DuAnYTeEntitiesFramework.NoiDungTinNhanPhongThaoLuans.FirstOrDefault(bs => bs.TrangThai != false && bs.id == id);
        }
        public void Add(NoiDungTinNhanPhongThaoLuan user)
        {
            DuAnYTeEntitiesFramework.NoiDungTinNhanPhongThaoLuans.Add(user);
            DuAnYTeEntitiesFramework.SaveChanges();

        }
        public List<NoiDungTinNhanPhongThaoLuan> GetAll(int id,int limit=20)
        {
            return DuAnYTeEntitiesFramework.NoiDungTinNhanPhongThaoLuans.Where(bs => bs.TrangThai != false&&bs.id_PhongThaoLuan==id)
                .OrderByDescending(noidung=>noidung.NgayTao)
                .Take(limit)
                .OrderBy(noidung=>noidung.NgayTao)
                .ToList();
        }
        public object DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.NoiDungTinNhanPhongThaoLuans.FirstOrDefault(bs => bs.id == id).TrangThai = false;
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
