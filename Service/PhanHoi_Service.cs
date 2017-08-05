using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class PhanHoi_Service : IService
    {
        public Models.PhanHoi Create(string TenNguoiPhanHoi, string Email,string SoDienThoai,string DiaChi,string NoiDungPhanHoi)
        {
            Models.PhanHoi phanhoi = new Models.PhanHoi() { TenNguoiPhanHoi = TenNguoiPhanHoi,SoDienThoai=SoDienThoai, Email = Email, DiaChi = DiaChi, NoiDungPhanHoi = NoiDungPhanHoi,NgayTao=DateTime.Now,TrangThai=true };
          
            DuAnYTeEntitiesFramework.PhanHois.Add(phanhoi);
            DuAnYTeEntitiesFramework.SaveChanges();
            return phanhoi;
        }

        public List<Models.PhanHoi>  GetAll()
        {
            return DuAnYTeEntitiesFramework.PhanHois.Where(ph => ph.TrangThai == true).ToList();
        }
        public Models.PhanHoi GetById(int id)
        {
            return DuAnYTeEntitiesFramework.PhanHois.FirstOrDefault(ph => ph.id == id && ph.TrangThai == true);
        }
    }
}
