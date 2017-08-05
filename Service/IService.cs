using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Service.EntityModel;
namespace Service
{
    
    public class IService
    {
        protected DuAnYTeEntities DuAnYTeEntitiesFramework = new DuAnYTeEntities();
        public IService()
        {
            //DuAnYTeEntitiesFramework.Database.Connection.Open();

        }
    }
}
