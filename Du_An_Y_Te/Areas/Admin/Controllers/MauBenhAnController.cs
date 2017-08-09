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

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public object Create(MauBenhAn mauBenhAn)
        {
            string strPattern = @"[\s]+";
            Regex rgx = new Regex(strPattern);
            string Ouput = rgx.Replace(convertToUnSign3(mauBenhAn.TenMauBenhAn), " ");
            Ouput = Ouput.Replace(" ", "");
            return Ouput;
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
    }
}