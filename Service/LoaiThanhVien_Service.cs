using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class LoaiThanhVien_Service : IService<Models.LoaiThanhVien>
    {
        public List<Models.LoaiThanhVien> GetAll()
        {
            return DbContext.LoaiThanhViens.ToList();
        }
    }
}
