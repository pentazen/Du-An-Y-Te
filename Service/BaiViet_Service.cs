using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Thư viện
using Service.EntityModel;
using System.IO;
using System.Reflection;
using Blog_Guitar.Service;
#endregion
namespace Service
{
    public class BaiViet_Service : IService<BaiViet>
    {
        int Limit = 2;
        public List<BaiViet> GetAll()
        {
            return DbContext.BaiViets.Where(bv => bv.TrangThai == true).ToList();
        }
        public int GetNumberPage()
        {
            return DbContext.BaiViets.Count() / Limit;
        }
        public List<BaiViet> GetByPage(int page)
        {
            return DbContext.BaiViets.OrderByDescending(bv=>bv.NgayTao).Skip(page * Limit).Take(Limit).ToList();
        }
        public BaiViet GetById(int id)
        {
            var aa= DbContext.BaiViets.Where(bv => bv.TrangThai == true && bv.id == id).FirstOrDefault();
            return DbContext.BaiViets.Where(bv => bv.TrangThai == true && bv.id == id).FirstOrDefault();
        }
        public BaiViet CreateByModel(BaiViet baiViet)
        {
            baiViet.NgayTao = DateTime.Now;
            baiViet.TrangThai = true;
            DbContext.BaiViets.Add(baiViet);
            DbContext.SaveChanges();
            return baiViet;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DbContext.BaiViets.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateByModel(BaiViet baiViet)
        {
            try
            {
                BaiViet old = DbContext.BaiViets.FirstOrDefault(tt => tt.id == baiViet.id);
                old.id_ChuDe = baiViet.id_ChuDe;
                old.TenBaiViet = baiViet.TenBaiViet;
                old.MoTa = baiViet.MoTa;
                old.Nguon = baiViet.Nguon;
                old.id_AnhNen = (baiViet.id_AnhNen!=null)?baiViet.id_AnhNen:old.id_AnhNen;
                old.NoiDungBaiViet = baiViet.NoiDungBaiViet;
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
