using HaiStore.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace HaiStore.Controllers
{
    public class HomeController : Controller
    {
      Qlbanhang db = new Qlbanhang();
        public ActionResult Index(string SearchString = "")
        {
            if(SearchString != null)
            {
                var sanpham = db.Sanphams.Include(x=>x.Masp).Where(x=>x.Tensp.ToUpper().Contains(SearchString.ToUpper()));
                return View(sanpham);
            }
           
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();

        }

       
    }
}