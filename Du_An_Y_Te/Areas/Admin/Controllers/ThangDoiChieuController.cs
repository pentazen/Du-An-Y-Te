using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class ThangDoiChieuController : Controller
    {
        ThangDoiChieu_Service ThangDoiChieuService = new ThangDoiChieu_Service();
        LoaiXetNghiem_Service LoaiXetNghiemService = new LoaiXetNghiem_Service();
        // GET: Admin/ThangDoiChieu
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            return View(ThangDoiChieuService.GetAll());
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public object Create(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["ThangDoiChieu"] != null&& collec["LoaiXetNghiem"] != null)
                    {
                        ThangDoiChieu ThangDoiChieu = JsonConvert.DeserializeObject<ThangDoiChieu>(collec["ThangDoiChieu"]);
                        LoaiXetNghiem LoaiXetNghiem = JsonConvert.DeserializeObject<LoaiXetNghiem>(collec["LoaiXetNghiem"]);
                       
                       
                        ThangDoiChieu.id_LoaiXetNghiem = LoaiXetNghiemService.CreateByModel(LoaiXetNghiem).id;
                        ThangDoiChieu=ThangDoiChieuService.CreateByModel(ThangDoiChieu);
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public bool XoaThangDoiChieu(int id)
        {
            return ThangDoiChieuService.DeleteById(id);
        }
        public ActionResult Edit(int id)
        {
            return View(ThangDoiChieuService.LayTheoIdLoaiXetNghiem(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object Edit(FormCollection collec)
        {
            try
            {
                //if (Session["ThanhVien"] != null)
                {
                    if (collec["ThangDoiChieu"] != null && collec["LoaiXetNghiem"] != null)
                    {
                        ThangDoiChieu ThangDoiChieu = JsonConvert.DeserializeObject<ThangDoiChieu>(collec["ThangDoiChieu"]);
                        LoaiXetNghiem LoaiXetNghiem = JsonConvert.DeserializeObject<LoaiXetNghiem>(collec["LoaiXetNghiem"]);

                        ThangDoiChieuService.UpdateByModel(ThangDoiChieu, LoaiXetNghiem);
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