using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class ChuDe_Service : IService
    {
        public List<Models.ChuDe> GetAll()
        {
            return DuAnYTeEntitiesFramework.ChuDes.Where(cd=>cd.TrangThai==true).ToList();
        }

        public bool DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.ChuDes.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
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

                DuAnYTeEntitiesFramework.ChuDes.Add(chude);
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
