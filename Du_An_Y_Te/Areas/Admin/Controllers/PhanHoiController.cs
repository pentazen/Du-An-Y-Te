using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class PhanHoiController : Controller
    {
        PhanHoi_Service PhanHoiService = new PhanHoi_Service();
        // GET: Admin/PhanHoi
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(PhanHoiService.GetAll());
        }
        public ActionResult View(int id)
        {
            return View(PhanHoiService.GetById(id));
        }
    }
}