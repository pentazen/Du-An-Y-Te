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
    public class PhongChatController : Controller
    {
        PhongThaoLuan_Service PhongThaoLuanService = new PhongThaoLuan_Service();
        DuAnYTeEntities Service = new DuAnYTeEntities();
        // GET: Admin/PhongChat
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(PhongThaoLuanService.GetAll());
        }
        public ActionResult Create()
        {
            return View(Service.LoaiPhongThaoLuans.ToList());
        }
        public ActionResult Edit(int id)
        {
            PhongChatViewModel.PhongChatEditViewModel models = new PhongChatViewModel.PhongChatEditViewModel() { Phong = Service.PhongThaoLuans.FirstOrDefault(ph => ph.id == id), LoaiPhong = Service.LoaiPhongThaoLuans.ToList() };
           
            return View(models);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object DeleteById(int id)
        {
            return PhongThaoLuanService.DeleteById(id);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PhongThaoLuan phong)
        {
            //phong.TenPhong
            phong.NgayTao = DateTime.Now;
            phong.TrangThai = true;
            Service.PhongThaoLuans.Add(phong);
            Service.SaveChanges();
            return RedirectToAction("Index", "PhongChat");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PhongThaoLuan phong)
        {

            //Service.Entry(phong).State = System.Data.Entity.EntityState.Modified;
            var phong_old = Service.PhongThaoLuans.FirstOrDefault(ph => ph.id == phong.id);
            phong_old.TenPhong = phong.TenPhong;
            phong_old.id_LoaiPhongThaoLuan = phong.id_LoaiPhongThaoLuan;


            Service.SaveChanges();
            return RedirectToAction("Index", "PhongChat");
        }
    }
}