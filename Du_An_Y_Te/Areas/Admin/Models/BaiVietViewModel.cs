using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.EntityModel;
namespace Du_An_Y_Te.Areas.Admin.Models
{
    public class BaiVietViewModel
    {
    }
    public class BaiVietEditViewModel
    {
        public BaiViet BaiViet { get; set; }
        public List<ChuDe> DsChuDe { get; set; }
    }
}