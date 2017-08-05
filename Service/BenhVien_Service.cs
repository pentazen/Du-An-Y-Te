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
   public class BenhVien_Service : IService
    {
        public IList<BenhVien> GetAll()
        {
            return DuAnYTeEntitiesFramework.BenhViens.Where(tv => tv.TrangThai != false).ToList();
        }
    }
}
