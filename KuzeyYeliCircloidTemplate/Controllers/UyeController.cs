using KuzeyYeliCircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KuzeyYeliCircloidTemplate.Controllers
{
    [Authorize]
    public class UyeController : Controller
    {
        // GET: Uye
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)
        {
           bool durum= Membership.ValidateUser(k.KullaniciAdi, k.Parola);
            if (durum)
            {
                if (Hatirla=="on")
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);

                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı ya da Parola Hatalı!";
                return View();
            }
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }
    }
}