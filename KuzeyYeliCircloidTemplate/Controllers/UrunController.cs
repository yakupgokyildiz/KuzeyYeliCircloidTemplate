using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuzeyYeliCircloidTemplate.App_Classes;
using KuzeyYeliCircloidTemplate.Models;

namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        Model1 ctx = new Model1();
        
        // GET: Urun
        public ActionResult Index()
        {
            List<Urunler> urun = ctx.Urunlers.ToList();
            return View(urun);
        }

        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = ctx.Kategorilers.ToList();
            ViewBag.Tedarikciler = ctx.Tedarikcilers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urunler u)
        {
            ctx.Urunlers.Add(u);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }

        [HttpPost]
        public ActionResult UrunSil(Urunler u)
        {
            Urunler urun = ctx.Urunlers.FirstOrDefault(x => x.UrunID == u.UrunID);
            ctx.Urunlers.Remove(urun);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunDetay()
        {
            int id = Convert.ToInt32( Request.QueryString["id"].ToString());
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }


        [HttpPost]
        public void SepeteAt(int id)
        {
            Sepet s;
            if (Session["AktifSepet"]==null)
            {
                s = new Sepet();
            }
            else
            {
                s = (Sepet)Session["AktifSepet"];
            }
            Urunler u = ctx.Urunlers.FirstOrDefault(x => x.UrunID == id);
            s.Urunler.Add(u);
            Session["AktifSepet"] = s;

        }
    }
}