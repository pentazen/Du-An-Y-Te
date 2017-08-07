using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class ChuDe_Service : IService<Models.ChuDe>
    {
        public List<Models.ChuDe> GetAll()
        {
            return DbContext.ChuDes.Where(cd=>cd.TrangThai==true).ToList();
        }

        public bool DeleteById(int id)
        {
            try
            {
                DbContext.ChuDes.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CreateByModel(Models.ChuDe chude)
        {
            try
            {
                chude.NgayTao = DateTime.Now;
                chude.TrangThai = true;

                DbContext.ChuDes.Add(chude);
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
