using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Du_An_Y_Te.Areas.Admin.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Du_An_Y_Te.Controllers.API;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        ThanhVien_Service ThanhVienService = new ThanhVien_Service();
        NhomMau_Service NhomMauService = new NhomMau_Service();
        LoaiThanhVien_Service LoaiThanhVienService = new LoaiThanhVien_Service();
        FilesController FilesService = new FilesController();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(ThanhVienService.GetAll());
        }
        public ActionResult Create()
        {
           
            return View("Create", new TaiKhoanCreateViewModel() { DanhSachLoaiThanhVien = LoaiThanhVienService.GetAll(), DanhSachNhomMau = NhomMauService.GetAll() });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object Create(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["ThanhVien"] != null)
                    {

                        ThanhVien thanhVien = JsonConvert.DeserializeObject<ThanhVien>(collec["ThanhVien"]);
                        if (Request.Files.Count > 0)
                        {
                            //return HealthFilesService.CapNhatBenhAnTapTin(benhAn, dsTapTin);
                            thanhVien.Picture = FilesService.SavePictureFile(Request.Files);
                        }
                        thanhVien = ThanhVienService.TaoTaiKhoan(thanhVien) as ThanhVien;
                        //if (collec["dsloai"] != null)
                        //{
                        //    List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);

                        //    DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAn, DanhSachBenhAnLoaiXetNghiem);


                        //}
                        
                        if (thanhVien!=null)
                        return new JavaScriptSerializer().Serialize(new  { KetQua = true });
                    }
                    return new JavaScriptSerializer().Serialize(new { KetQua = false });
                }
                return new JavaScriptSerializer().Serialize(new { KetQua = false });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { KetQua = false, Message = ex.Message });
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public bool XoaThanhVien(int id)
        {
            return ThanhVienService.DeleteById(id);
        }
        public ActionResult Edit(int id)
        {
            return View("Edit", new TaiKhoanEditViewModel() { DanhSachLoaiThanhVien = LoaiThanhVienService.GetAll(), DanhSachNhomMau = NhomMauService.GetAll(),ThanhVien= ThanhVienService.GetById(id) });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object Edit(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["ThanhVien"] != null)
                    {

                        ThanhVien thanhVien = JsonConvert.DeserializeObject<ThanhVien>(collec["ThanhVien"]);
                        if (Request.Files.Count > 0)
                        {
                            //return HealthFilesService.CapNhatBenhAnTapTin(benhAn, dsTapTin);
                            thanhVien.Picture = FilesService.SavePictureFile(Request.Files);
                        }
                        
                        //if (collec["dsloai"] != null)
                        //{
                        //    List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);

                        //    DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAn, DanhSachBenhAnLoaiXetNghiem);


                        //}

                        if (ThanhVienService.CapNhatThongTinCaNhan(thanhVien))
                            return new JavaScriptSerializer().Serialize(new { KetQua = true });
                    }
                    return new JavaScriptSerializer().Serialize(new { KetQua = false });
                }
                return new JavaScriptSerializer().Serialize(new { KetQua = false });

            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { KetQua = false, Message = ex.Message });
            }
        }


        public object GetFileByUrl(string url)
        {

            return base.File(url, "image/*");

        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object DangNhap(string TaiKhoan,string MatKhau)
        {
            ThanhVien thanhvien = ThanhVienService.KiemTraDangNhapVaoTrangAdmin(TaiKhoan, MatKhau);
            if (thanhvien != null)
            {
                Session["ThanhVien"] = thanhvien;
                return true;
            }
            return false;
        }
        public ActionResult DangXuat()
        {
            Session["ThanhVien"] = null;
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}