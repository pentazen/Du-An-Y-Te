using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class NhomMau_Service : IService<Models.NhomMau>
    {
        public List<Models.NhomMau> GetAll()
        {
            return DbContext.NhomMaus.ToList();
        }
    }
}
