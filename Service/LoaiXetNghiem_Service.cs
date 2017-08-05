using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class LoaiXetNghiem_Service : IService
    {
        public List<Models.LoaiXetNghiem> GetAll()
        {
            return DuAnYTeEntitiesFramework.LoaiXetNghiems.Where(tdc => tdc.TrangThai == true).ToList();
        }
        public Models.LoaiXetNghiem CreateByModel(Models.LoaiXetNghiem loaixetnghiem)
        {
            loaixetnghiem.TrangThai = true;
            DuAnYTeEntitiesFramework.LoaiXetNghiems.Add(loaixetnghiem);
            DuAnYTeEntitiesFramework.SaveChanges();
            return loaixetnghiem;
        }
       
    }
}
