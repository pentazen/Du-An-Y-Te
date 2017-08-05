using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Du_An_Y_Te.Areas.Admin.Models;
using Du_An_Y_Te.Controllers.API;
namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class BaiVietController : Controller
    {
        BaiViet_Service BaiVietService = new BaiViet_Service();
        ChuDe_Service ChuDeService = new ChuDe_Service();
        FilesController FilesService = new FilesController();
        // GET: Admin/BaiViet
        public ActionResult Index()
        {
            if (Session["ThanhVien"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(BaiVietService.GetAll());
        }

        public ActionResult View(int id)
        {
            return View("View",BaiVietService.GetById(id));
        }
        public ActionResult Create()
        {
            return View(ChuDeService.GetAll());
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BaiViet collec, FormCollection collecFile)
        {
            collec.NoiDungBaiViet= HttpUtility.HtmlEncode(collec.NoiDungBaiViet);
            //// Decode model.Data as it is Encoded after post
            //string decodedString = HttpUtility.HtmlDecode(collec.NoiDungBaiViet);
            //// Clean HTML
            ////string sanitizedHtmlText = HtmlUtility.SanitizeHtml(decodedString);

            //string encoded = HttpUtility.HtmlEncode(collec.NoiDungBaiViet);
            collec.id_ThanhVien_DangBaiViet = (Session["ThanhVien"] as ThanhVien).id;
            if (Request.Files.Count > 0)
            {
                //return HealthFilesService.CapNhatBenhAnTapTin(benhAn, dsTapTin);
                collec.id_AnhNen = FilesService.SaveFile(Request.Files)[0];
            }
            BaiVietService.CreateByModel(collec);
            return RedirectToAction("Index","BaiViet");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public object DeleteById(int id)
        {
            return BaiVietService.DeleteById(id);
        }
        public ActionResult Edit(int id)
        {
            return View(new BaiVietEditViewModel() { BaiViet=BaiVietService.GetById(id),DsChuDe=ChuDeService.GetAll()});
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BaiViet collec, FormCollection collecFile)
        {
            //collec.NoiDungBaiViet = HttpUtility.HtmlEncode(collec.NoiDungBaiViet);
            ////// Decode model.Data as it is Encoded after post
            ////string decodedString = HttpUtility.HtmlDecode(collec.NoiDungBaiViet);
            ////// Clean HTML
            //////string sanitizedHtmlText = HtmlUtility.SanitizeHtml(decodedString);

            ////string encoded = HttpUtility.HtmlEncode(collec.NoiDungBaiViet);
            //BaiVietService.CreateByModel(collec);
            if (Request.Files.Count > 0)
            {
                if(!string.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    //return HealthFilesService.CapNhatBenhAnTapTin(benhAn, dsTapTin);
                    collec.id_AnhNen = FilesService.SaveFile(Request.Files)[0];
                }
            
            }
            BaiVietService.UpdateByModel(collec);
            return RedirectToAction("View", "BaiViet",new {id= collec .id});
        }
    }
}