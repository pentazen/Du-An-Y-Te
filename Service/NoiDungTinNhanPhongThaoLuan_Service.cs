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
   public class NoiDungTinNhanPhongThaoLuan_Service : IService<NoiDungTinNhanPhongThaoLuan>
    {
        public NoiDungTinNhanPhongThaoLuan GetById(int id)
        {
            return DbContext.NoiDungTinNhanPhongThaoLuans.FirstOrDefault(bs => bs.TrangThai != false && bs.id == id);
        }
        public void Add(NoiDungTinNhanPhongThaoLuan user)
        {
            DbContext.NoiDungTinNhanPhongThaoLuans.Add(user);
            DbContext.SaveChanges();

        }
        public List<NoiDungTinNhanPhongThaoLuan> GetAll(int id,int limit=20)
        {
            return DbContext.NoiDungTinNhanPhongThaoLuans.Where(bs => bs.TrangThai != false&&bs.id_PhongThaoLuan==id)
                .OrderByDescending(noidung=>noidung.NgayTao)
                .Take(limit)
                .OrderBy(noidung=>noidung.NgayTao)
                .ToList();
        }
        public object DeleteById(int id)
        {
            try
            {
                DbContext.NoiDungTinNhanPhongThaoLuans.FirstOrDefault(bs => bs.id == id).TrangThai = false;
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
