using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Service.EntityModel;
using Service;
//using Du_An_Y_Te.Models;
namespace Du_An_Y_Te.Controllers.API
{
    public class HealthFilesController : ApiController
    {
        Service.BenhAn_TapTin_Service BenhAnTapTinService = new BenhAn_TapTin_Service();
        //Service.SinhHoaMau_TapTin_Service SinhHoaMauTapTinService = new SinhHoaMau_TapTin_Service();
        //Service.NuocTieu_TapTin_Servive NuocTieuTapTinServive = new NuocTieu_TapTin_Servive();
        //Service.SieuAm_TapTin_Service SieuAmTapTinService = new SieuAm_TapTin_Service();
        public bool CapNhatBenhAnTapTin(BenhAn benhAn, List<int> DSFile)
        {
            return BenhAnTapTinService.CapNhatBenhAnTapTin(benhAn, DSFile);
        }
        //public bool CapNhatTapTinSinhHoaMau(int idSinhHoaMau, List<int> DSFile)
        //{
        //    return SinhHoaMauTapTinService.CapNhatSinhHoaMau_TapTin(idSinhHoaMau, DSFile);
        //}

        //public bool CapNhatTapTinNuocTieu(int idNuocTieu, List<int> DSFile)
        //{
        //    return NuocTieuTapTinServive.CapNhatNuocTieu_TapTin(idNuocTieu, DSFile);
        //}

        //public bool CapNhatTapTinSieuAm(int idSieuAm, List<int> DSFile)
        //{
        //    return SieuAmTapTinService.CapNhatSieuAm_TapTin(idSieuAm, DSFile);
        //}
    }
}
