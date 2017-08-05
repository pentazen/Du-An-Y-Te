using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.EntityModel;
namespace Du_An_Y_Te.Areas.Admin.Models
{
    public class BacSiViewModel
    {
        public class BacSiCreateViewModel
        {

            public List<BenhVien> DsBenhVien { get; set; }
            public List<LoaiBacSi> DsLoaiBacSi { get; set; }
        }
        public class BacSiEditViewModel
        {
            public BacSi BacSi { get; set; }
            public List<BenhVien> DsBenhVien { get; set; }
            public List<LoaiBacSi> DsLoaiBacSi { get; set; }
        }
    }
}