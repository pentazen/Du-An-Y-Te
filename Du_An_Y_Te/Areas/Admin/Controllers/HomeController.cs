using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.EntityModel;

namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
               
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            if ((Session["ThanhVien"] as ThanhVien).id_LoaiThanhVien == 4)
            {
                return RedirectToAction("Index", "../Home");
            }
            return View();
        }
    }
}