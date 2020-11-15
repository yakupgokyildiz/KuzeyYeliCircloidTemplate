using KuzeyYeliCircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class TedarikciController : Controller
    {
        Model1 ctx = new Model1();
        // GET: Tedarikci
        public ActionResult Index()
        {
            List<Tedarikciler> td = ctx.Tedarikcilers.ToList();
            return View(td);
        }

        [HttpPost]
        public string Sil(int id)
        {
            Tedarikciler ted = ctx.Tedarikcilers.FirstOrDefault(x => x.TedarikciID == id);
            ctx.Tedarikcilers.Remove(ted);
            
            try
            {
                ctx.SaveChanges();
                return "başarılı";
            }
            catch (Exception)
            {
                return "hata";
                
            }
        }
    }
}