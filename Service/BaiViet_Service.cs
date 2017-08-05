using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Thư viện
using Service.EntityModel;
using System.IO;
using System.Reflection;
#endregion
namespace Service
{
    public class BaiViet_Service : IService
    {
        int Limit = 2;
        public List<BaiViet> GetAll()
        {
            return DuAnYTeEntitiesFramework.BaiViets.Where(bv => bv.TrangThai == true).ToList();
        }
        public int GetNumberPage()
        {
            return DuAnYTeEntitiesFramework.BaiViets.Count() / Limit;
        }
        public List<BaiViet> GetByPage(int page)
        {
            return DuAnYTeEntitiesFramework.BaiViets.OrderByDescending(bv=>bv.NgayTao).Skip(page * Limit).Take(Limit).ToList();
        }
        public BaiViet GetById(int id)
        {
            var aa= DuAnYTeEntitiesFramework.BaiViets.Where(bv => bv.TrangThai == true && bv.id == id).FirstOrDefault();
            return DuAnYTeEntitiesFramework.BaiViets.Where(bv => bv.TrangThai == true && bv.id == id).FirstOrDefault();
        }
        public BaiViet CreateByModel(BaiViet baiViet)
        {
            baiViet.NgayTao = DateTime.Now;
            baiViet.TrangThai = true;
            DuAnYTeEntitiesFramework.BaiViets.Add(baiViet);
            DuAnYTeEntitiesFramework.SaveChanges();
            return baiViet;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.BaiViets.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
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
                BaiViet old = DuAnYTeEntitiesFramework.BaiViets.FirstOrDefault(tt => tt.id == baiViet.id);
                old.id_ChuDe = baiViet.id_ChuDe;
                old.TenBaiViet = baiViet.TenBaiViet;
                old.MoTa = baiViet.MoTa;
                old.Nguon = baiViet.Nguon;
                old.id_AnhNen = (baiViet.id_AnhNen!=null)?baiViet.id_AnhNen:old.id_AnhNen;
                old.NoiDungBaiViet = baiViet.NoiDungBaiViet;
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
