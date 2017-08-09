using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Service.EntityModel;
using Du_An_Y_Te.Models;
using Newtonsoft.Json;

namespace Du_An_Y_Te.Controllers
{
    public class HealthController : Controller
    {
        Du_An_Y_Te.Controllers.API.HealthController HealthyService = new API.HealthController();
        Du_An_Y_Te.Controllers.API.FilesController FilesService = new API.FilesController();
        Du_An_Y_Te.Controllers.API.HealthFilesController HealthFilesService = new API.HealthFilesController();
        // GET: Health
        public ActionResult Index()
        {
            if (Session["ThanhVien"] != null)
            {
                DuAnYTeEntities entity = new DuAnYTeEntities();

                return View(entity.MauBenhAns.Where(mba=>mba.TrangThai==true).ToList());

            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public object CapNhatBenhAn(FormCollection collec)
        {
            try
            {
                if (Session["ThanhVien"] != null)
                {
                    if (collec["ModelbenhAn"] != null)
                    {
                        DuAnYTeEntities entity = new DuAnYTeEntities();
                        BenhAn benhAn = JsonConvert.DeserializeObject<BenhAn>(collec["ModelbenhAn"]);
                        benhAn.id_ThanhVien_SoHuu = (Session["ThanhVien"] as ThanhVien).id;
                        BenhAn benhAnTim = entity.BenhAns.FirstOrDefault(ba => ba.id == benhAn.id);
                        if (benhAnTim == null) return false;
                        if (benhAnTim.id_ThanhVien_SoHuu != (Session["ThanhVien"] as ThanhVien).id) return false;
                        if (collec["dsloai"] != null)
                        {
                            List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);

                            DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAnTim, DanhSachBenhAnLoaiXetNghiem);


                        }
                        if (Request.Files.Count > 0)
                        {
                            List<int> dsTapTin = (List<int>)FilesService.SaveFile(Request.Files);
                            if (HealthFilesService.CapNhatBenhAnTapTin(benhAnTim, dsTapTin))
                                return true;
                            else
                            {
                                return new { KetQua = true, NoiDung = "Không Lưu Được Tập Tin!" };
                            }
                        }
                        return true;
                    }
                    return false;
                }
                return false;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        // GET: HealthViewChart
        public ActionResult MyHealth()
        {
            if (Session["ThanhVien"] != null)
            {
                ViewBag.idThanhVien = (Session["ThanhVien"] as ThanhVien).id;
                List<ThangDoiChieu> DSThangDoiChieu = HealthyService.GetAllThangDoiChieu();
                return View(DSThangDoiChieu);
            }
            return RedirectToAction("Index", "Home");


        }

        public ActionResult MyHealViewToUpdate(int id)
        {
            if (Session["ThanhVien"] != null)
            {
                //ViewBag.idThanhVien = (Session["ThanhVien"] as ThanhVien).id;
                //HealthyService.LayBenhAnTheoIdThanhVienVaIdBenhAn(1, id);
                BenhAn benhan = HealthyService.LayBenhAnTheoIdThanhVienVaIdBenhAn((Session["ThanhVien"] as ThanhVien).id, id);
                var temp = from iten in benhan.BenhAn_TapTin.ToList()
                           where iten.TrangThai != false
                           select iten;
                benhan.BenhAn_TapTin = temp.ToList();
                return View(benhan);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public object MyHealViewToUpdate(FormCollection collec)
        {
            try
            {
                if (Session["ThanhVien"] != null)
                {
                    if (collec["ModelbenhAn"] != null)
                    {
                        BenhAn benhAn = JsonConvert.DeserializeObject<BenhAn>(collec["ModelbenhAn"]);
                        benhAn.id_ThanhVien_SoHuu = (Session["ThanhVien"] as ThanhVien).id;
                        //benhAn.id_ThanhVien_SoHuu = 1;
                        //benhAn = HealthyService.CapNhatBenhAn(benhAn);
                        if (collec["dsloai"] != null)
                        {
                            List<BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem = JsonConvert.DeserializeObject<List<BenhAn_LoaiXetNghiem>>(collec["dsloai"]);
                            benhAn.id = int.Parse(collec["idBenhAn"]);
                            //int idBenhAn =int.Parse( collec["idBenhAn"]);
                            HealthyService.UpdateBenhAn_LoaiXetNghiem(benhAn.id_ThanhVien_SoHuu, benhAn, DanhSachBenhAnLoaiXetNghiem);
                            //DanhSachBenhAnLoaiXetNghiem = HealthyService.CapNhatBenhAn_LoaiXetNghiem(benhAn, DanhSachBenhAnLoaiXetNghiem);


                        }
                        if (Request.Files.Count > 0)
                        {
                            List<int> dsTapTin = (List<int>)FilesService.SaveFile(Request.Files);
                            return HealthFilesService.CapNhatBenhAnTapTin(benhAn, dsTapTin);
                        }
                        return true;
                    }
                    return false;
                }
                return false;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }



            //ModelbenhAn.id_ThanhVien_SoHuu = 1;
            //Service.BenhAn_Service BenhAnService = new Service.BenhAn_Service();
            //for (int i = 0; i < 2; i++)
            //{
            //    BenhAn_LoaiXetNghiem m = new BenhAn_LoaiXetNghiem();
            //    m.id_LoaiXetNghiem = i+1;
            //    m.GiaTri = (i + 10).ToString();
            //    m.DonVi = "mmol/l";
            //    dsloai.Add(m);
            //}
            //return (BenhAnService.CapNhatBenhAnChoBenhNhan(benhAn)!=null);

            //if(Session["ThanhVien"]!=null)
            //{
            //    List<int> ds = new List<int>();
            //    if (Request.Files.Count > 0)
            //    {

            //         ds=(List < int >) FilesService.SaveFile(Request.Files);
            //    }
            //    int idHuyetHoc= HealthyService.CapNhatHuyetHoc(HuyetHocModel, ((ThanhVien)Session["ThanhVien"]).id);
            //    if(ds.Count>0)
            //    {
            //        return HealthFilesService.CapNhatTapTinHuyetHoc(idHuyetHoc, ds);
            //    }
            //    return true;
            //}
        }
        public ActionResult GetFileById(int id)
        {

            //var stream = XTC.Helpers.ImageResizer.Resize(URL, 150, 200);
            //var result = new FileStreamResult(stream, "image/jpeg");

            //return result;
            TapTin tapntin = FilesService.GetById(id);
            return base.File(tapntin.DuongDan, tapntin.LoaiTapTin, tapntin.TenTapTin);
            //return new System.Web.Mvc.FileStreamResult(new FileStream(URL, FileMode.Open), "image/*");

        }


        public bool XoaFile(int idBenhAn, int idTapTin)
        {
            return FilesService.XoaTapTinBenhAn(idBenhAn, idTapTin);
        }
        //#region MyRegion
        //[HttpPost]
        //public object CapNhatSinhHoaMau(SinhHoaMau SinhHoaMauModel)
        //{
        //    if (Session["ThanhVien"] != null)
        //    {
        //        List<int> ds = new List<int>();
        //        if (Request.Files.Count > 0)
        //        {

        //            ds = (List<int>)FilesService.SaveFile(Request.Files);
        //        }
        //        int idSinhHoaMau = HealthyService.CapNhatSinhHoaMau(SinhHoaMauModel, ((ThanhVien)Session["ThanhVien"]).id);
        //        if (ds.Count > 0)
        //        {
        //            return HealthFilesService.CapNhatTapTinSinhHoaMau(idSinhHoaMau, ds);
        //        }
        //        return true;

        //    }
        //    return false;
        //}
        //[HttpPost]
        //public object CapNhatNuocTieu(NuocTieu NuocTieuModel)
        //{
        //    if (Session["ThanhVien"] != null)
        //    {
        //        List<int> ds = new List<int>();
        //        if (Request.Files.Count > 0)
        //        {

        //            ds = (List<int>)FilesService.SaveFile(Request.Files);
        //        }
        //        int idNuocTieu = HealthyService.CapNhatNuocTieu(NuocTieuModel, ((ThanhVien)Session["ThanhVien"]).id);
        //        if (ds.Count > 0)
        //        {
        //            return HealthFilesService.CapNhatTapTinNuocTieu(idNuocTieu, ds);
        //        }
        //        return true;

        //    }
        //    return false;
        //}
        //[HttpPost]
        //public object CapNhatSieuAm(SieuAm SieuAmModel)
        //{
        //    if (Session["ThanhVien"] != null)
        //    {
        //        List<int> ds = new List<int>();
        //        if (Request.Files.Count > 0)
        //        {

        //            ds = (List<int>)FilesService.SaveFile(Request.Files);
        //        }
        //        int idSieuAm = HealthyService.CapNhatSieuAm(SieuAmModel, ((ThanhVien)Session["ThanhVien"]).id);
        //        if (ds.Count > 0)
        //        {
        //            return HealthFilesService.CapNhatTapTinSieuAm(idSieuAm, ds);
        //        }
        //        return true;

        //    }
        //    return false;
        //}
        //#endregion

    }
}