using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.EntityModel;
using Du_An_Y_Te.Areas.Admin.Models;
using Du_An_Y_Te.Controllers.API;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Du_An_Y_Te.Areas.Admin.Controllers
{
    public class MauBenhAnController : Controller
    {
        MauBenhAn_Service MauBenhAnService = new MauBenhAn_Service();
        // GET: Admin/MauBenhAn
        public ActionResult Index()
        {
            return View(MauBenhAnService.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public object Create(MauBenhAn mauBenhAn)
        {
            try
            {
                string strPattern = @"[\s]+";
                Regex rgx = new Regex(strPattern);
                string Ouput = rgx.Replace(convertToUnSign3(mauBenhAn.TenMauBenhAn), " ");
                Ouput = Ouput.Replace(" ", "");
                if (MauBenhAnService.Count(mba => mba.TenMauBenhAn.ToLower() == mauBenhAn.TenMauBenhAn.ToLower()) > 0)
                {
                    return false;

                }
                if (MauBenhAnService.Count(mba => mba.TenKhongDau == Ouput) > 0)
                {
                    int i = 1;
                    string fixOutPut = Ouput + i.ToString();
                    while (MauBenhAnService.Count(mba => mba.TenKhongDau == fixOutPut) > 0)
                    {
                        i++;
                        fixOutPut = Ouput + i.ToString();
                    }
                    mauBenhAn.TenKhongDau = fixOutPut;

                }
                else
                {
                    mauBenhAn.TenKhongDau = Ouput;
                }
                MauBenhAnService.dbSet.Add(mauBenhAn);
                MauBenhAnService.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public object CapNhatTrangThai(MauBenhAn mauBenhAn)
        {
            try
            {
                MauBenhAn mauBenhAnCapNhat = MauBenhAnService.GetSingleById(mauBenhAn.id);
                mauBenhAnCapNhat.TrangThai = mauBenhAn.TrangThai;
                MauBenhAnService.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public ActionResult Edit(int id)
        {
            return View(MauBenhAnService.GetSingleById(id));
        }
        [HttpPost]
        public object Edit(FormCollection collec)
        {
            try
            {
                if (collec["mauBenhAn_LXN"] != null)
                {
                    MauBenhAn_LoaiXetNghiem mauBenhAn_LXN = JsonConvert.DeserializeObject<MauBenhAn_LoaiXetNghiem>(collec["mauBenhAn_LXN"]);
                    MauBenhAn mauBenhAn = MauBenhAnService.GetSingleById(mauBenhAn_LXN.id_MauBenhAn);
                    MauBenhAn_LoaiXetNghiem timDuoc = mauBenhAn.MauBenhAn_LoaiXetNghiem.Where(lxn => lxn.id_LoaiXetNghiem == mauBenhAn_LXN.id_LoaiXetNghiem).FirstOrDefault();
                    timDuoc.ThuTu = mauBenhAn_LXN.ThuTu;
                    MauBenhAnService.Commit();
                    return true;
                }

                return false;

            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public object RemoveLoaiXetNghiem(FormCollection collec)
        {
            try
            {
                if (collec["mauBenhAn_LXN"] != null)
                {
                    MauBenhAn_LoaiXetNghiem mauBenhAn_LXN = JsonConvert.DeserializeObject<MauBenhAn_LoaiXetNghiem>(collec["mauBenhAn_LXN"]);
                    MauBenhAn mauBenhAn = MauBenhAnService.GetSingleById(mauBenhAn_LXN.id_MauBenhAn);
                    MauBenhAn_LoaiXetNghiem timDuoc = MauBenhAnService.DbContext.MauBenhAn_LoaiXetNghiem.Where(mauBA => mauBA.id_MauBenhAn == mauBenhAn_LXN.id_MauBenhAn && mauBA.id_LoaiXetNghiem == mauBenhAn_LXN.id_LoaiXetNghiem).FirstOrDefault();
                    MauBenhAnService.DbContext.MauBenhAn_LoaiXetNghiem.Remove(timDuoc);
                    MauBenhAnService.Commit();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public object LoaiXetNghiem()
        {
            MauBenhAnService.DbContext.Configuration.LazyLoadingEnabled = false;
            return new JavaScriptSerializer().Serialize(new { data = MauBenhAnService.DbContext.LoaiXetNghiems.ToList() });
        }
        [HttpPost]
        public object ThemLoaiXetNghiemVaoMauBenhAn(FormCollection collec)
        {
            try
            {
                if (collec["mauBenhAn_LXN"] != null)
                {
                    MauBenhAn_LoaiXetNghiem mauBenhAn_LXN = JsonConvert.DeserializeObject<MauBenhAn_LoaiXetNghiem>(collec["mauBenhAn_LXN"]);
                    MauBenhAn mauBenhAn = MauBenhAnService.GetSingleById(mauBenhAn_LXN.id_MauBenhAn);
                    MauBenhAn_LoaiXetNghiem timDuoc = MauBenhAnService.DbContext.MauBenhAn_LoaiXetNghiem.Where(mauBA => mauBA.id_MauBenhAn == mauBenhAn_LXN.id_MauBenhAn && mauBA.id_LoaiXetNghiem == mauBenhAn_LXN.id_LoaiXetNghiem).FirstOrDefault();
                    if (timDuoc != null)
                    {
                        return false;
                    }
                    mauBenhAn_LXN.ThuTu = mauBenhAn.MauBenhAn_LoaiXetNghiem.Count + 1;
                    MauBenhAnService.DbContext.MauBenhAn_LoaiXetNghiem.Add(mauBenhAn_LXN);
                    MauBenhAnService.Commit();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}