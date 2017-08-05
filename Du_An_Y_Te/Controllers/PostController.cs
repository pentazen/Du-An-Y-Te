using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.EntityModel;
using Service;

namespace Du_An_Y_Te.Controllers
{
    public class PostController : Controller
    {
        BaiViet_Service BaiVietService = new BaiViet_Service();
        // GET: Post
        public ActionResult Index(int page=0)
        {
            ViewBag.NumberPage = BaiVietService.GetNumberPage();
            ViewBag.CurrentPage = page;
            return View(BaiVietService.GetByPage(page));
        }

        public ActionResult Detail(int id)
        {
            return View("Detail",BaiVietService.GetById(id));
        }
    }
}