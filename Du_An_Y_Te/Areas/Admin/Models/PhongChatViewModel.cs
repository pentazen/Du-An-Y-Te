using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.EntityModel;
namespace Du_An_Y_Te.Areas.Admin.Models
{
    public class PhongChatViewModel
    {
        public class PhongChatEditViewModel
        {
            public PhongThaoLuan Phong { get; set; }
            public List<LoaiPhongThaoLuan> LoaiPhong { get; set; }
        }
    }
}