using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Service;
using Service.EntityModel;
using Du_An_Y_Te.Models;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Du_An_Y_Te.Controllers
{
    public class AccountController : Controller
    {
        Du_An_Y_Te.Controllers.API.AccountController AccoutAPIControl = new API.AccountController();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        public ActionResult Login()
        {
            if (Session["ThanhVien"] == null)
                return View();
            return RedirectToAction("Index", "Home");
        }
        // Post: Login
        [HttpPost]
        public bool Login(AccountViewModels.LoginViewModel Acount)
        {
            string m = Request.Url.Host;

            ThanhVien thanhvien = AccoutAPIControl.Login(Acount);
            //SendMailToUserAccessAccount(thanhvien);
            if (thanhvien != null)
            {
                Session["ThanhVien"] = thanhvien;
                return true;
            }
            return false;
        }
        // GET : Access ACcount
        public ActionResult Access(int id, string code)
        {
            AccoutAPIControl.Access(id, code);
            return RedirectToAction("Index", "Home");

        }
        // GET : Logout
        public ActionResult Logout()
        {
            Session["ThanhVien"] = null;
            return RedirectToAction("index", "Home");
        }

        // GET : MyProfile
        public ActionResult MyProfile()
        {
            if (Session["ThanhVien"] != null)
            {
                //return View("MyProfile");
                //Service.BenhAn_Service benhanser = new BenhAn_Service();

                ThanhVien thanhvien = AccoutAPIControl.GetById((Session["ThanhVien"] as ThanhVien).id) as ThanhVien;
                //thanhvien.BenhAns= benhanser.LayDanhSachBenhAnTheoIdThangVien(thanhvien.id);
                return View("MyProfile", thanhvien);
            }
            return RedirectToAction("index", "Home");
            //AccoutAPIControl.GetById(1);
            
        }
        // GET : MyProfile
        [HttpPost]
        public bool MyProfile(FormCollection collec)
        {
            if (collec["ModelThanhVien"] != null)
            {
                ThanhVien thanhvien = JsonConvert.DeserializeObject<ThanhVien>(collec["ModelThanhVien"]);
                thanhvien.id = (Session["ThanhVien"] as ThanhVien).id;
                //thanhvien.id_LoaiThanhVien = 4;
                if (Request.Files.Count > 0)
                {
                    Du_An_Y_Te.Controllers.API.FilesController FilesService = new API.FilesController();
                    List<int> dsTapTin = (List<int>)FilesService.SaveFile(Request.Files);
                    TapTin tapntin = FilesService.GetById(dsTapTin.FirstOrDefault());
                    thanhvien.Picture = tapntin.DuongDan;
                }

                return AccoutAPIControl.CapNhatThongTinCaNhan(thanhvien);
            }
            //AccoutAPIControl.Login(new AccountViewModels.LoginViewModel() { TaiKhoan = thanhvien.TaiKhoan, MatKhau = thanhvien.MatKhau });
            //if (AccoutAPIControl.Login(new AccountViewModels.LoginViewModel() { TaiKhoan = thanhvien.TaiKhoan, MatKhau = thanhvien.MatKhau })!=null)
            //{

            //}

            //if (Session["ThanhVien"]!=null)
            //{
            //    return View("MyProfile");
            //}
            //return RedirectToAction("index", "Home");
            //return View("MyProfile");
            return false;
        }
        public ActionResult GetFileByURL(string URL)
        {
            //var stream = XTC.Helpers.ImageResizer.Resize(URL, 150, 200);
            //var result = new FileStreamResult(stream, "image/jpeg");

            //return result;

            return base.File(URL, "image/*");
            //return new System.Web.Mvc.FileStreamResult(new FileStream(URL, FileMode.Open), "image/*");
         
        }

    }
}