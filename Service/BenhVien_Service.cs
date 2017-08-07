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
   public class BenhVien_Service : IService<BenhAn>
    {
        public IList<BenhVien> GetAll()
        {
            return DbContext.BenhViens.Where(tv => tv.TrangThai != false).ToList();
        }
    }
}
