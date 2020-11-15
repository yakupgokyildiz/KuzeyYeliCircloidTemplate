using KuzeyYeliCircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        Model1 ctx = new Model1();
        // GET: Kategori
        public ActionResult Index()
        {
            List<Kategoriler> ktg = ctx.Kategorilers.ToList();
            return View(ktg);
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler ktg)
        {
            ctx.Kategorilers.Add(ktg);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void Sil(int id)
        {
            Kategoriler ktg = ctx.Kategorilers.FirstOrDefault(x => x.KategoriID == id);
            ctx.Kategorilers.Remove(ktg);
            ctx.SaveChanges();
        }
    }
}