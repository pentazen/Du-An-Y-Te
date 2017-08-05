using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using System.Web;
using Service.EntityModel;
using Service;
//using Du_An_Y_Te.Models;
using System.Configuration;
using System.IO;

namespace Du_An_Y_Te.Controllers.API
{
    public class FilesController : ApiController
    {

        private TapTin_Service TapTinService = new TapTin_Service();
        private BenhAn_TapTin_Service BenhAnTapTinService = new BenhAn_TapTin_Service();
        public List<int> SaveFile(HttpFileCollectionBase files)
        {
            List<int> dsTapTinSave = new List<int>();
            TapTin_Service.DuongDanLuuTapTin = ConfigurationManager.AppSettings["DuongDanLuuTapTin"];


            for (int i = 0; i < files.Count; i++)
            {
                dsTapTinSave.Add((int)TapTinService.LuuTapTin(files[i].InputStream, files[i].FileName, files[i].ContentType, files[i].ContentLength));
            }
            return dsTapTinSave;
        }
        public string SavePictureFile(HttpFileCollectionBase files)
        {
            TapTin_Service.DuongDanLuuTapTin = ConfigurationManager.AppSettings["DuongDanLuuTapTin"];
            return TapTinService.LuuTapTinTraVeDuongDan(files[0].InputStream, files[0].FileName, files[0].ContentType, files[0].ContentLength).ToString();
        }
        public TapTin GetById(int id)
        {
            return TapTinService.GetById(id);
        }
        public bool XoaTapTinBenhAn(int idBenhAn,int idFile)
        {
            return BenhAnTapTinService.XoaTapTinBenhAn(idBenhAn, idFile);
        }
    }
}
