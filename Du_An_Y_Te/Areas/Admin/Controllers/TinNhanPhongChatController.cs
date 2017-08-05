using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Du_An_Y_Te.Areas.Admin.Models;
namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class TinNhanPhongChatController : Controller
    {
        DuAnYTeEntities Service = new DuAnYTeEntities();
        
        // GET: Admin/TinNhanPhongChat
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(Service.NoiDungTinNhanPhongThaoLuans.OrderByDescending(ph=>ph.NgayTao).ToList());
        }
        public object DeleteById(int id)
        {
             Service.NoiDungTinNhanPhongThaoLuans.Remove(Service.NoiDungTinNhanPhongThaoLuans.FirstOrDefault(ph=>ph.id==id));
            return (Service.SaveChanges()>0)?true:false;
        }
    }
}