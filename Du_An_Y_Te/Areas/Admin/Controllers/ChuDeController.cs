using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.EntityModel;
using Service;
namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        ChuDe_Service ChuDeService = new ChuDe_Service();
        // GET: Admin/ChuDe
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(ChuDeService.GetAll());
        }
        public object DeleteById(int id)
        {
            return ChuDeService.DeleteById(id);
        }
        public ActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ChuDe chude)
        {
            ChuDeService.CreateByModel(chude);
            return RedirectToAction("Index", "ChuDe");
        }
    }
}