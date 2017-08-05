using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class NhomMau_Service : IService
    {
        public List<Models.NhomMau> GetAll()
        {
            return DuAnYTeEntitiesFramework.NhomMaus.ToList();
        }
    }
}
