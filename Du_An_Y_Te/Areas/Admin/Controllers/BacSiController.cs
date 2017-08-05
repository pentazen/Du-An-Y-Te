using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Du_An_Y_Te.Areas.Admin.Models;
using Du_An_Y_Te.Controllers.API;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class BacSiController : Controller
    {
        FilesController FilesService = new FilesController();
        BacSi_Service BacSiService = new BacSi_Service();
        BenhVien_Service BenhVienService = new BenhVien_Service();
        LoaiBacSi_Service LoaiBacSiService = new LoaiBacSi_Service();
        ThanhVien_Service ThanhVienService = new ThanhVien_Service();
        // GET: Admin/BacSi
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(BacSiService.GetAll());
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object DeleteById(int id)
        {
            return BacSiService.DeleteById(id);
        }
        public ActionResult Create()
        {
            BacSiViewModel.BacSiCreateViewModel bacsiview = new BacSiViewModel.BacSiCreateViewModel();
            bacsiview.DsBenhVien = BenhVienService.GetAll().ToList();
            bacsiview.DsLoaiBacSi = LoaiBacSiService.GetAll().ToList();

            return View(bacsiview);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object Create(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["BacSi"] != null)
                    {

                        BacSi bacsi = JsonConvert.DeserializeObject<BacSi>(collec["BacSi"]);
                        var taikhoan = collec["TaiKhoan"];
                        if(!string.IsNullOrEmpty(taikhoan))
                        {
                            ThanhVien thanhvien = ThanhVienService.GetByTaiKhoan(taikhoan);
                            if(thanhvien!=null)
                            {
                                bacsi.id_ThanhVien = thanhvien.id;
                            }
                            else
                            {
                                return new JavaScriptSerializer().Serialize(new { KetQua = false, Message = "Tài khoản không tồn tại" });
                            }
                        }
                        bacsi = BacSiService.CreateByModel(bacsi) as BacSi;
                        //if (collec["dsloai"] != null)
                        //{
                        //    List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);

                        //    DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAn, DanhSachBenhAnLoaiXetNghiem);


                        //}

                        if (bacsi != null)
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

        public ActionResult Edit(int id)
        {
            BacSiViewModel.BacSiEditViewModel bacsiview = new BacSiViewModel.BacSiEditViewModel();
            bacsiview.DsBenhVien = BenhVienService.GetAll().ToList().ToList();
            bacsiview.DsLoaiBacSi = LoaiBacSiService.GetAll().ToList();
            bacsiview.BacSi = BacSiService.GetById(id);

            return View(bacsiview);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public object Edit(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["BacSi"] != null)
                    {

                        BacSi bacsi = JsonConvert.DeserializeObject<BacSi>(collec["BacSi"]);
                        var taikhoan = collec["TaiKhoan"];
                        if (!string.IsNullOrEmpty(taikhoan))
                        {
                            ThanhVien thanhvien = ThanhVienService.GetByTaiKhoan(taikhoan);
                            if (thanhvien != null)
                            {
                                bacsi.id_ThanhVien = thanhvien.id;
                            }
                            else
                            {
                                return new JavaScriptSerializer().Serialize(new { KetQua = false, Message = "Tài khoản không tồn tại" });
                            }
                        }
                        else
                        {
                            bacsi.id_ThanhVien = null;
                        }
                        bacsi = BacSiService.UpdateByModel(bacsi) as BacSi;
                        //if (collec["dsloai"] != null)
                        //{
                        //    List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);

                        //    DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAn, DanhSachBenhAnLoaiXetNghiem);


                        //}

                        if (bacsi != null)
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
    }
}