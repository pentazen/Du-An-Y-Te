using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class PhanHoi_Service : IService<Models.BenhAn>
    {
        public Models.PhanHoi Create(string TenNguoiPhanHoi, string Email,string SoDienThoai,string DiaChi,string NoiDungPhanHoi)
        {
            Models.PhanHoi phanhoi = new Models.PhanHoi() { TenNguoiPhanHoi = TenNguoiPhanHoi,SoDienThoai=SoDienThoai, Email = Email, DiaChi = DiaChi, NoiDungPhanHoi = NoiDungPhanHoi,NgayTao=DateTime.Now,TrangThai=true };
          
            DbContext.PhanHois.Add(phanhoi);
            DbContext.SaveChanges();
            return phanhoi;
        }

        public List<Models.PhanHoi>  GetAll()
        {
            return DbContext.PhanHois.Where(ph => ph.TrangThai == true).ToList();
        }
        public Models.PhanHoi GetById(int id)
        {
            return DbContext.PhanHois.FirstOrDefault(ph => ph.id == id && ph.TrangThai == true);
        }
    }
}
