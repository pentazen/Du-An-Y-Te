using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class LoaiXetNghiem_Service : IService<Models.LoaiXetNghiem>
    {
        public List<Models.LoaiXetNghiem> GetAll()
        {
            return DbContext.LoaiXetNghiems.Where(tdc => tdc.TrangThai == true).ToList();
        }
        public Models.LoaiXetNghiem CreateByModel(Models.LoaiXetNghiem loaixetnghiem)
        {
            loaixetnghiem.TrangThai = true;
            DbContext.LoaiXetNghiems.Add(loaixetnghiem);
            DbContext.SaveChanges();
            return loaixetnghiem;
        }
       
    }
}
