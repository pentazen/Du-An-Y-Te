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
    public class LoaiBacSi_Service : IService
    {
        public IList<LoaiBacSi> GetAll()
        {
            return DuAnYTeEntitiesFramework.LoaiBacSis.Where(tv => tv.TrangThai != false).ToList();
        }
    }
}
