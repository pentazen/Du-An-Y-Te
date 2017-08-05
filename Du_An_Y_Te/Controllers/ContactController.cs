using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
namespace Du_An_Y_Te.Controllers
{
    public class ContactController : Controller
    {
        PhanHoi_Service PhanHoiService = new PhanHoi_Service();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public object Create(string TenNguoiPhanHoi,string Email,string SoDienThoai,string DiaChi,string NoiDungPhanHoi)
        {
            if(PhanHoiService.Create(TenNguoiPhanHoi, Email, SoDienThoai, DiaChi, NoiDungPhanHoi)!=null)
            {
                return true;
            }
            return false;
        }
    }
}