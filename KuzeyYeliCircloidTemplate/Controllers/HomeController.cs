using KuzeyYeliCircloidTemplate.App_Classes;
using KuzeyYeliCircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.AktifKullanici = HttpContext.Application["AktifKullanici"];
            ViewBag.ToplamZiyaretci= HttpContext.Application["ToplamZiyaretci"];
            return View();
        }

        public ActionResult Sepetim()
        {
            List<Urunler> urunler = new List<Urunler>();
            if (Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)Session["AktifSepet"];
                urunler = s.Urunler;
            }
            return View(urunler);
            
        }
    }
}