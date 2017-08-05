using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.EntityModel;
using Service;
using Du_An_Y_Te.Models;
namespace Du_An_Y_Te.Controllers
{
    public class PhongThaoLuanController : Controller
    {
        PhongThaoLuan_Service PhongThaoLuanService = new PhongThaoLuan_Service();
        ThanhVien_Service ThanhVienService = new ThanhVien_Service();
        // GET: PhongThaoLuan
        public ActionResult Index()
        {
            return View(PhongThaoLuanService.GetAll());
        }
        public ActionResult Phong(int id)
        {
            return View(PhongThaoLuanService.GetById(id));
        }
        [HttpGet]
        public object LayViewThanhVienRoom(int id_ThanhVien)
        {
            return PartialView("~/Views/Shared/Partial/_ThanhVienTrongRoomChat.cshtml", ThanhVienService.GetById(id_ThanhVien));
        }
    }
}